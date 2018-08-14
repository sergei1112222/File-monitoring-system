using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using FileMonitoringSystem.Repo;
using FileMonitoringSystem.Common;

using log4net;
using System.IO.Compression;

namespace FileMonitoringSystem.Monitoring
{
    public class ChangeHandler : ThreadWorker
    {
        private const string _pathCopiedFiles = @"Copied\";
        private IRepository _repository;
        private ChangesBuffer _changeBuf;
        private ILog _log = LogManager.GetLogger(typeof(ChangeHandler).Name);

        public bool FlagHandle { get; set; }

        public ChangeHandler(ChangesBuffer changeBuf, IRepository repo)
        {
            _repository = repo;
            _changeBuf = changeBuf;
        }

        protected override void Work()
        {
            while (!CancelFlag.IsCancellationRequested)
            {
                FileState temp = _changeBuf.Dequeue(10);
                
                if (temp != null)

                {
                    _log.Info("Add file state");

                    AddFileState(temp);
                   
                }

                else
                    // поспим 5 секунд, чтоб не жрать процессор этим циклом, пока что у нас нет подходящих FileState
                    Sleep(1000);
            }
        }

        private void AddFileState(FileState fileState)
        {
            FileData fd;
            if (fileState.OldPath != null)
            {
                 fd = _repository.ReturnDataBase().FirstOrDefault(entry => entry.Path == fileState.OldPath);
            }
            else
            {
                 fd = _repository.ReturnDataBase().FirstOrDefault(entry => entry.Path == fileState.Path);
            }
            FileData locFileData = null;
            if ((fd != null) && (!fd.IsRemove))
            {
                if (fileState.IsDeleted)
                {
                    locFileData = new FileData(fd.Id, fd.Path, fd.Name, fd.OldPath, true, fileState.TimeSpan, fd.ContentHash);
                    _log.Info($"Add entry (delete {fd.Path})");
                }
                else
                {
                    if ((fileState.Path != fileState.OldPath) && (fileState.OldPath != null))
                    {
                        locFileData = new FileData(fd.Id, fileState.Path, fileState.Name, fd.OldPath, fd.IsRemove, fileState.TimeSpan, fd.ContentHash);
                        _log.Info($"Add entry (rename {fd.Path})");
                    }
                    else
                    {
                        string hashInputFile = computeContentHash(fileState.Path, fileState.Name);
                        if (hashInputFile != fd.ContentHash)
                        {
                            locFileData = new FileData(fd.Id, fd.Path, fd.Name, fd.OldPath, fd.IsRemove, fileState.TimeSpan, hashInputFile);
                            compressOperation(locFileData);
                            _log.Info($"Add entry (update {fd.Path})");
                        }
                    }
                    
                }
            }
            else
            {
                _log.Info("In addfilestate: = null");
                if (!fileState.IsDeleted)
                {
                    if ((fileState.Path != fileState.OldPath) && (fileState.OldPath != null))
                    {
                        locFileData = new FileData(Guid.NewGuid(), fileState.Path, fileState.Name, fileState.OldPath, fileState.IsDeleted, fileState.TimeSpan, computeContentHash(fileState.Path, fileState.Name));
                        compressOperation(locFileData);
                        _log.Info($"Add entry (rename {fileState.OldPath} --> {fileState.Path})");
                    }
                    else
                    {
                        locFileData = new FileData(Guid.NewGuid(), fileState.Path, fileState.Name, fileState.OldPath, fileState.IsDeleted, fileState.TimeSpan, computeContentHash(fileState.Path, fileState.Name));
                        _log.Info($"Add entry (new {fileState.Path})");
                        compressOperation(locFileData);
                    }

                }
                else
                {
                    locFileData =  new FileData(Guid.NewGuid(), fileState.Path, fileState.Name, fileState.OldPath, true, fileState.TimeSpan, "");
                    _log.Info($"Add entry (delete {fileState.Path})");  
                }
                
            }
            _repository.Insert(locFileData);
        }

        private string computeContentHash(string path, string name)
        {
            File.Copy(path, _pathCopiedFiles + name);
            string hash = "";
            using (FileStream fs = File.OpenRead(_pathCopiedFiles + name))
            {
                _log.Info("success opening copy file");
                hash = Encoding.Default.GetString(MD5.Create().ComputeHash(fs));
            }
            File.Delete(_pathCopiedFiles + name);
            _log.Info("end computing hash");
            return hash;
        }

        private void compress(string sourceFile, string compressedFile)
        {
            // поток для чтения исходного файла
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
            {
                // поток для записи сжатого файла
                using (FileStream targetStream = File.Create(compressedFile))
                {
                    // поток архивации
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой
                    }
                }
            }
        }

        private void compressOperation(FileData fileData)
        {
            _log.Info($"Compress start, {fileData.Path}");
            compress(fileData.Path, $"ZipStorage\\ {fileData.Id}.zip");
            _log.Info($"Compress success1, {fileData.Path}"); 
        }

    }
}
