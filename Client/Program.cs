using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Server;

namespace Client
{
    internal class Program
    {
        static private void ReceiveMethod(ref IPAddress ipAddress, ref int port)
        {
            int acceptedBytes;
            byte[] datagram;
            string receivedMessage = null;

            try
            {
                while (true)
                {
                    using (Socket receiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
                    {
                        receiveSocket.Bind(new IPEndPoint(IPAddress.Any, port));
                        receiveSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(ipAddress, IPAddress.Any));

                        datagram = new byte[1024];

                        acceptedBytes = receiveSocket.Receive(datagram);
                        receivedMessage = Encoding.UTF8.GetString(datagram, 0, acceptedBytes);
                    }

                    Console.WriteLine($"\n {receivedMessage}\r");
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
            IPAddress ipAddress;

            ValueValidator.CheckIPValue(out ipAddress);

            Console.Clear();

            ValueValidator.CheckPortValue(out port);

            Console.Clear();

            ReceiveMethod(ref ipAddress, ref port);

            Console.ReadKey();
        }
    }
}
