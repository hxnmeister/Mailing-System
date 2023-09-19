using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Server;

namespace Server
{
    internal class Program
    {
        static public void SendMethod(ref IPAddress ipAddress, ref int port, ref string sendingMessage)
        {
            byte[] datagram = Encoding.UTF8.GetBytes(sendingMessage);
            IPEndPoint endPoint = new IPEndPoint(ipAddress, port);

            try
            {
                using(Socket sendingSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
                {
                    sendingSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 2);
                    sendingSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(ipAddress));

                    sendingSocket.Connect(endPoint);
                    sendingSocket.Send(datagram);
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine($" {se.Message}\r\n");
            }
            catch (Exception e)
            {
                Console.WriteLine($" {e.Message}\r\n");
            }
        }

        static void Main(string[] args)
        {
            int port;
            string sendingMessage;
            IPAddress ipAddress;

            ValueValidator.CheckIPValue(out ipAddress);

            Console.Clear();

            ValueValidator.CheckPortValue(out port);

            do
            {
                Console.Clear();

                Console.Write("\n Enter your message: ");
                sendingMessage = Console.ReadLine();

                SendMethod(ref ipAddress, ref port, ref sendingMessage);

                Console.Write("\n Do you want to send anothed message? (Y/N)");
            } while (char.ToUpper(Console.ReadKey().KeyChar) != 'N');
            
        }
    }
}
