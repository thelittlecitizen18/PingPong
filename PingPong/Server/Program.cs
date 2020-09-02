using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Server!");

            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
           
            listener.Bind(localEndPoint);
 
            listener.Listen(10);

            Console.WriteLine("Waiting for a connection...");
            Socket handler = listener.Accept();

            while (true)
            {
                byte[] bytes = new byte[1024];
                int bytesRec = handler.Receive(bytes);
                string data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                Console.WriteLine("Return {0}", data);
                byte[] msg = Encoding.ASCII.GetBytes(data);
                handler.Send(msg);

            }







            //string data = null;
            //byte[] bytes = null;

            //while (true)
            //{
            //    bytes = new byte[1024];
            //    int bytesRec = handler.Receive(bytes);
            //    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
            //    if (data.IndexOf("<EOF>") > -1)
            //    {
            //        break;
            //    }
            //}

            //Console.WriteLine("Text received : {0}", data);

            //byte[] msg = Encoding.ASCII.GetBytes(data);
            //handler.Send(msg);
            //handler.Shutdown(SocketShutdown.Both);
            //handler.Close();

        }
    }
}
