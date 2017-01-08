using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocolo
{
    public class Protocolo
    {
        private const int LargoHeader = 3;
        private const int LargoCmd = 2;
        private const int LargoMensaje = 4;
        private Header header;
        private string comando;
        private string largo;
        private byte[] datos;

        public Protocolo(Header header, string com, string largo, byte[] datos)
        {
            this.header = header;
            comando = com;
            this.largo = largo;
            this.datos = datos;
        }

        public static void Leer(Protocolo protocolo)
        {

        }

        public static Protocolo Codificar(byte[] mensaje)
        {
            int i = 0;
            Header protocolHeader;
            string header = "";
            while (i < LargoHeader)
            {
                header += mensaje[i];
                i++;
            }
            protocolHeader = header == "RES" ? Header.RES : Header.REQ;
            string com = mensaje[i].ToString();
            int largo = 5 + mensaje.Length;
            string largoFormateado = largo.ToString("0000");
            Protocolo prot = new Protocolo(protocolHeader, com, largoFormateado, mensaje.)

        }

        public override string ToString()
        {
            return this.header.ToString() + "0" + this.comando.ToString() + this.largo.ToString() + datos;
        }
    }

    public enum Header
    {
        RES,
        REQ
    }

    public enum Comando
    {
        ListaArchivos,
        CargaArchivo,
        DescargaArchivo,
        SyncArchivos
    }

}
