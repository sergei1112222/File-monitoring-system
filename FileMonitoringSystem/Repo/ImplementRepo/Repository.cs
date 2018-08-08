using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileMonitoringSystem.Repo;

namespace FileMonitoringSystem.Repo.ImplementRepo
{
    public class Repository: IRepository
    {
        private List<FileData> _db;
        private Queue<FileData> _qBuffer;
        
        private const string DBpath = "db.dat";

        public Repository()
        {
            _db = new List<FileData>();
        }

        public void OperationWithModFile(string name, int flagOperation)
        {
            Guid id = Guid.NewGuid();
            string[] fileData = name.Split('.');
            Insert(new FileData(id, name, fileData[fileData.Length - 1], DateTime.Now));
            _qBuffer.Enqueue(new FileData(id, name, fileData[fileData.Length - 1], DateTime.Now));
        }


        public void Insert(FileData fileData)
        {
            _db.Add(fileData);

        }

        public void Delete(FileData removeData)
        {
            _db.Remove(removeData);
        }

        public FileData GetFileDataForSend()
        {
            //EMPTY CLASS
            return new FileData();
        }

        public void SaveFileData()
        {
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(DBpath, FileMode.Create)))
                {
                    foreach (var elem in _db)
                    {
                        writer.Write(elem.Id.ToString());
                        writer.Write(elem.Name);
                        writer.Write(elem.Type);
                        
                    }
                }
            }
            catch
            {

            }
        }

        public bool ReadPersonData()
        {
            try
            {
                ReadFileDataFromfile();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static void Compress(string sourceFile, string compressedFile)
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

        private void ReadFileDataFromfile()
        {
            using (BinaryReader reader = new BinaryReader(File.Open(DBpath, FileMode.OpenOrCreate)))
            {
                while (reader.PeekChar() > -1)
                {
                    Guid id = Guid.Parse(reader.ReadString());
                    string fileName = reader.ReadString();
                    string filetype = reader.ReadString();
                    

                   // _db.Add(new FileData(id, fileName, filetype));
                }
            }
        }
        
    }
}
