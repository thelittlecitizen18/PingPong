using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Client2
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
                var task = Task.Factory.StartNew(obj =>
                {

                    int backmsg = sender.Receive(bytes1);
                    string msg2 = Encoding.ASCII.GetString(bytes1, 0, backmsg);
                    Console.WriteLine(msg2);
                }, sender
                );
                
            }

        }
    }
}
