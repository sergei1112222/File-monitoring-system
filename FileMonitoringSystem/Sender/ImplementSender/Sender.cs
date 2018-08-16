using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using FileMonitoringSystem.Repo;

namespace FileMonitoringSystem.Sender.ImplementSender
{
    public class Sender: ISender
    {
        private int _port = 8080;
        private string _address = "127.0.0.1";

        public void Connect()
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(_address), _port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // подключаемся к удаленному хосту
            socket.Connect(ipPoint);
            if (!socket.Connected)
                throw new ConnectException("Could not connect!");
        }

        public void Send(FileData fileData)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.S
        }

        public void Receive()
        {

        }

       

        public bool ConnectionStatus()
        {
            return false;
        }
    }
}
