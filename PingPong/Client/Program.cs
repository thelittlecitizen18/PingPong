using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

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


             Person MakeNewPerson()
            {
                Console.WriteLine("Please Enter A Name:");
                string name = Console.ReadLine();
                Console.WriteLine("Please Enter An age:");
                int age = Convert.ToInt32(Console.ReadLine());
                Person person = new Person(name, age);
                return person;
            }
           

            while (true)
            { 
                byte[] bytes1 = new byte[1024];
                Console.WriteLine("Write a message:");
                string recivedMsg = MakeNewPerson().ToString();
                byte[] msg = Encoding.ASCII.GetBytes(recivedMsg);
                sender.Send(msg);
                var task = Task.Factory.StartNew(obj =>
                {

                    int backmsg = sender.Receive(bytes1);
                    string msg2 = Encoding.ASCII.GetString(bytes1, 0, backmsg);
                    Console.WriteLine(msg2);
                }, sender
                );
                if (recivedMsg == "Break")
                {
                    break;
                }

            }

            sender.Shutdown(SocketShutdown.Send);
            sender.Close();


            //int bytesSent = sender.Send(msg);
            //int bytesRec = sender.Receive(bytes);
            //Console.WriteLine("Echoed test = {0}",
            //    Encoding.ASCII.GetString(bytes, 0, bytesRec));
            //sender.Shutdown(SocketShutdown.Both);
            //sender.Close();




        }
    }
}
