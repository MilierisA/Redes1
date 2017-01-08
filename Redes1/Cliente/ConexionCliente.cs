using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Protocolo;

namespace Cliente
{
    public class ConexionCliente
    {
        static void ConectarseAlServidor()
        {
            bool isRunning = true;
            //Lo correcto sería verificar si el puerto está en uso, y si lo está pedir un puerto nuevo o buscar uno.
            IPEndPoint clientEndPoint =
                new IPEndPoint(IPAddress.Parse("172.20.1.106"), 6001);
            IPEndPoint serverEndPoint =
                new IPEndPoint(IPAddress.Parse("172.20.1.106"), 6000);
            Socket socket = new Socket(AddressFamily.InterNetwork,
                                        SocketType.Stream,
                                        ProtocolType.Tcp);
            socket.Bind(clientEndPoint);
            socket.Connect(serverEndPoint);
            Console.WriteLine("Conectado al servidor");
            try
            {
                while (isRunning)
                {
                    String message = "REQ" + Console.ReadLine();
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    Protocolo.Protocolo prot =  Protocolo.Protocolo.Codificar(data);
                    socket.Send(data);
                }
            }
            catch (SocketException ex)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                Console.WriteLine("El servidor cerró la conexión");
                Console.ReadLine();
            }
        }
    }
}
