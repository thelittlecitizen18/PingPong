using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;


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
            
            
            while (true)
            {
                Socket handler = listener.Accept();
                var task = Task.Factory.StartNew(obj =>
                {
                    while (true)
                    {


                        byte[] bytes = new byte[1024];
                        int bytesRec = handler.Receive(bytes);
                        string data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        Console.WriteLine("Return {0}", data);
                        byte[] msg = Encoding.ASCII.GetBytes(data);
                        handler.Send(msg);
                    }
                }, listener
                ); 
            } 
            




        }
    }
}
