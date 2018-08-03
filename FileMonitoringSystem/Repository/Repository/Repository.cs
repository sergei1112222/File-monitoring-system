using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMonitoringSystem.Repository.Repository
{
    public class Repository: IRepository
    {
        private List<FileData> _db;
        private const string DBpath = "db.dat";

        public Repository()
        {
            _db = new List<FileData>();
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
            return new FileData();
        }

        public void SaveNotebook()
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
                        writer.Write(elem.Path);
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
                ReadNoteBookDataFromfile();
            }
            catch
            {
                return false;
            }
            return true;
        }

       

        private void ReadNoteBookDataFromfile()
        {
            using (BinaryReader reader = new BinaryReader(File.Open(DBpath, FileMode.OpenOrCreate)))
            {
                while (reader.PeekChar() > -1)
                {
                    Guid id = Guid.Parse(reader.ReadString());
                    string fileName = reader.ReadString();
                    string filetype = reader.ReadString();
                    string filePath = reader.ReadString();

                    _db.Add(new FileData(id, fileName, filetype, filePath));
                }
            }
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
    }
}
