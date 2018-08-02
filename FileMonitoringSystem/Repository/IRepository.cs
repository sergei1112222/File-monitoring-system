using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMonitoringSystem.Repository
{
    public class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public bool IsSend { get; set; }
        public Entity() { }

        public Entity(int id, string name, string type, string path)
        {
            Id = id;
            Name = name;
            Type = type;
            Path = path;
        }
    }

    interface IRepository
    {
       
        void Insert(Entity entity);
        void Delete(int id);
        Entity GetEntityForSend();
    }
}
