using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] bytes = new byte[1024];
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);
            Socket sender = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            sender.Connect(remoteEP);

            Console.WriteLine("Socket connected to {0}",
                sender.RemoteEndPoint.ToString());

            while (true)
            {
                byte[] bytes1 = new byte[1024];
                Console.WriteLine("Write a message:");
                byte[] msg = Encoding.ASCII.GetBytes(Console.ReadLine());
                sender.Send(msg);
                int backmsg = sender.Receive(bytes1);
                string msg2 = Encoding.ASCII.GetString(bytes1, 0, backmsg);
                Console.WriteLine(msg2);
            }

            //byte[] bytes = new byte[1024];
            //int bytesRec = handler.Receive(bytes);
            //string data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
            //byte[] msg = Encoding.ASCII.GetBytes(data);
            //handler.Send(msg);







            //int bytesSent = sender.Send(msg);
            //int bytesRec = sender.Receive(bytes);
            //Console.WriteLine("Echoed test = {0}",
            //    Encoding.ASCII.GetString(bytes, 0, bytesRec));
            //sender.Shutdown(SocketShutdown.Both);
            //sender.Close();




        }
    }
}
