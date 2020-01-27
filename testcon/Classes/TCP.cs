using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace testcon.Classes
{
    public static class TCP
    {
        private static readonly TcpListener TL = new TcpListener(IPAddress.Any, 6869);
        public static string PacketResult { get; set; }

        public static void Start()
        {
            Thread thread = new Thread(new ParameterizedThreadStart(async _ => await Listen()));
            thread.Start();
        }

        public static async Task Listen()
        {
            try
            {
                TL.Start();
                while (true)
                {
                    TcpClient Receiver = await TL.AcceptTcpClientAsync();
                    NetworkStream ns2 = Receiver.GetStream();
                    if (ns2.ReadByte() == 2)
                    {
                        PacketResult = Encoding.UTF8.GetString(await ByteStream(ns2));
                        Receiver.Close();
                        ns2.Close();
                        await GetBytes(PacketResult);
                        Console.WriteLine(PacketResult);
                    }
                }

            }
            catch (SocketException err)
            {
                PacketResult = err.Message;
            }
            finally
            {
                TL.Stop();
            }
        }

        private static async Task<byte[]> ByteStream(NetworkStream n)
        {
            int rb;
            string SLength = "";
            int BOffSet = 0;

            while ((rb = n.ReadByte()) != 4)
            {
                SLength += (char)rb;
            }
            int DLength = int.Parse(SLength);

            byte[] buffer = new byte[DLength];
            if (BOffSet < DLength)
            {
                await n.ReadAsync(buffer, BOffSet, DLength - BOffSet);
            }
            return buffer;
        }

        public static async Task GetBytes(string input)
        {
            try
            {
                using MemoryStream ms = new MemoryStream();
                using NetworkStream ns = new TcpClient("127.0.0.1", 6868).GetStream();
                byte[] EncodeTxtL = Encoding.UTF8.GetBytes(Convert.ToString(input.Length));
                byte[] EncodeTxt = Encoding.UTF8.GetBytes(input);

                ms.WriteByte(2);
                await ms.WriteAsync(EncodeTxtL, 0, EncodeTxtL.Length);
                ms.WriteByte(4);
                await ms.WriteAsync(EncodeTxt, 0, EncodeTxt.Length);
                await ns.WriteAsync(ms.ToArray(), 0, ms.ToArray().Length);

                //return ms.ToArray();

            }
            catch (TargetInvocationException)
            {
                //return null;
            }
        }
    }
}
