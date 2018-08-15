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
        private const string _pathDirCopiedFiles = @"Copied\";
        private IFileStateRepository _repository;
        private ChangesBuffer _changeBuf;
        private ILog _log = LogManager.GetLogger(typeof(ChangeHandler).Name);

        public bool FlagHandle { get; set; }

        public ChangeHandler(ChangesBuffer changeBuf, IFileStateRepository repo)
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
            FileData locFileData = null;
            if (fileState.IsDeleted)
            {
                locFileData = new FileData(Guid.NewGuid(), fileState.Path, fileState.OldPath, true, fileState.TimeSpan, "");
                _log.Info($"Add entry (delete) {fileState.Path}");
            }
            else
            {
                Guid g = Guid.NewGuid();
                locFileData = new FileData(g, fileState.Path, fileState.OldPath, fileState.IsDeleted, fileState.TimeSpan, ComputeContentHashWithCompress(fileState.Path, g.ToString()));
                _log.Info($"Add entry  {fileState.Path}");
            }
            lock (_repository)
            {
                _repository.Insert(locFileData);
            }
        }

        private string ComputeContentHashWithCompress(string path, string id)
        {
            string pathForCopyFile = _pathDirCopiedFiles + id + Path.GetExtension(path);
            File.Copy(path, pathForCopyFile);
            string hash = "";
            using (FileStream fs = File.OpenRead(pathForCopyFile))
            {
                _log.Info("success opening copy file");
                hash = Encoding.Default.GetString(MD5.Create().ComputeHash(fs));
            }
            CompressOperation(pathForCopyFile);
            File.Delete(pathForCopyFile);
            _log.Info("end computing hash");
            return hash;
        }

        private void Compress(string sourceFile, string compressedFile)
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

        private void CompressOperation(string path)
        {
            _log.Info($"Compress start, {path}");
            Compress(path, $"ZipStorage\\ {Path.GetFileNameWithoutExtension(path)}.zip");
            _log.Info($"Compress success1, {path}"); 
        }

    }
}
