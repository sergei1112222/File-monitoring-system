using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using FileMonitoringSystem.Repo;
using FileMonitoringSystem.Common;

namespace FileMonitoringSystem.Monitoring
{
    public class ChangeHandler : ThreadWorker
    {
        private const string _pathCopiedFiles = @"Copied\";
        private IRepository _repository;
        private ChangesBuffer _changeBuf;

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
                    AddFileState(temp);
                else
                    // поспим 5 секунд, чтоб не жрать процессор этим циклом, пока что у нас нет подходящих FileState
                    Sleep(5000);
            }
        }

        private void AddFileState(FileState fileState)
        {
            FileData fd = _repository.ReturnDataBase().FirstOrDefault(entry => entry.OldPath == fileState.OldPath);
            if (fd != null)
            {
                if (fileState.IsDeleted)
                {
                    _repository.Insert(new FileData(fd.Id, fd.Path, fd.Name, fd.OldPath, true, fileState.TimeSpan, fd.ContentHash));
                }
                if (fileState.Path != fileState.OldPath)
                {
                    _repository.Insert(new FileData(fd.Id, fileState.Path, fileState.Name, fd.OldPath, fd.IsRemove, fileState.TimeSpan, fd.ContentHash));
                }
                string hashInputFile = ComputeContentHash(fileState.Path, fileState.Name);
                if (hashInputFile != fd.ContentHash)
                {
                    _repository.Insert(new FileData(fd.Id, fd.Path, fd.Name, fd.OldPath, fd.IsRemove, fileState.TimeSpan, hashInputFile));
                }
            }
            else
            {
                _repository.Insert(new FileData(Guid.NewGuid(), fileState.Path, fileState.Name,fileState.OldPath, fileState.IsDeleted, fileState.TimeSpan, ComputeContentHash(fileState.Path, fileState.Name)));
            }
        }
        private string ComputeContentHash(string path, string name)
        {
            File.Copy(path, _pathCopiedFiles + name);
            string hash = "";
            using (FileStream fs = File.OpenRead(_pathCopiedFiles + name))
            {
                hash = Encoding.Default.GetString(MD5.Create().ComputeHash(fs));
            }
            File.Delete(_pathCopiedFiles + name);
            return hash;
        }



    }
}
