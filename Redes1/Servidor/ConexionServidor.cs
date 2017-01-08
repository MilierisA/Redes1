using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servidor
{
    public class ConexionServidor
    {

        static void AceptarConexiones()
        {
            Console.WriteLine("Esperando a nuevos clientes...");
            Socket socketServer = new Socket(AddressFamily.InterNetwork,
                                        SocketType.Stream,
                                        ProtocolType.Tcp);
            IPEndPoint localEp = new IPEndPoint(IPAddress.Parse("172.20.1.106"), 6000);
            socketServer.Bind(localEp);
            socketServer.Listen(100);
            while (true)
            {
                Socket client = socketServer.Accept();
                Thread clientHandler = new Thread(() => HandleClient(client));
                clientHandler.Start();
            }
        }

        static void HandleClient(Socket client)
        {
            bool connected = true;
            while (connected)
            {
                try
                {
                    int i;
                    byte[] data = new byte[256];
                    i = client.Receive(data);
                    Console.WriteLine(Encoding.UTF8.GetString(data).TrimEnd());
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("El cliente cerró la conexión");
                    connected = false;
                }
            }
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
    }
}
