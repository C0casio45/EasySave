using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;

namespace EasySave_Client.ViewModels
{
    public class NetworkViewModel
    {
        private Socket _server;
        public string _data;

        public NetworkViewModel()
        {
            new Thread(Connect).Start();
        }

        private void Connect()
        {
            //cf Server
            string ip;
            if (File.Exists("./ServerIp.json"))
            {
                ip = JObject.Parse(File.ReadAllText("./ServerIp.json"))["ServerIp"].ToString();
            }
            else
            {
                ip = JObject.Parse(File.ReadAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "./ServerIp.json"))["ServerIp"].ToString();
            }
            IPAddress myIP = IPAddress.Parse(ip);
            IPEndPoint remoteEP = new IPEndPoint(myIP, 11000);

            // Create a TCP/IP  socket.
            Socket server = new Socket(myIP.AddressFamily,SocketType.Stream, ProtocolType.Tcp);

            try
            {
                server.Connect(remoteEP);
            }
            catch
            {
                Thread.Sleep(2000);
                Connect();
            }
            

            HandleConnection(server);
        }

        private void HandleConnection(Socket server)
        {
            _server = server;
            GetDatas(server);                                               //Call data reception method
        }

        public void SendDatas(string Message)
        {
            // Encode the data string into a byte array.
            byte[] msg = Encoding.UTF8.GetBytes(Message);

            // Send the data through the socket.
            _ = _server.Send(msg);
        }

        private void GetDatas(Socket server)
        {
            try
            {
                // Incoming data from the server.
                string data = null;
                byte[] bytes = null;

                while (true)
                {
                    //Setup mesage table
                    bytes = new byte[4096];
                    //Recieve message from client
                    int bytesRec = server.Receive(bytes);
                    //Decode message from bytes to UTF8
                    data = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    
                    //Display message
                    
                    if (data.Substring(0, 5) == "<INI>")
                    {
                        //Initialization of the client display
                        //Get all worksaves
                        data = data.Substring(5);
                        _data = data;

                        //_ = MessageBox.Show(data);

                        //Load saves as objects
                        //_ = new WorkSaveListViewModel(data);
                    }
                    if (data.Substring(0, 5) == "<PRO>")
                    {
                        //Get progress
                        //Get only working saves (where SState is ACTIVE), cf EasySave project - NetworkViewModel
                        data = data.Substring(5);
                        _data = data;
                        //Update only working item
                    }
                    if (data.Substring(0, 5) == "<EOC>")
                    {
                        //Client disctionnected
                        Disconnect();                     //Close client socket
                        break;                                  //break the while => end the current thread
                    }
                }
            }
            catch { }
        }

        public void Disconnect()
        {
            //End socket
            _server.Close();
        }
    }
}
