using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hestia
{
    class Program
    {
        public static int Base = 0x00509B74;
        public static int Health = 0xF8;

        public static int PosX = 0x34;
        public static int PosY = 0x38;
        public static int PosZ = 0x3c;

        public static Random rnd;


        static void Main(string[] args)
        {
            rnd = new Random();

            VAMemory vam = new VAMemory("ac_client");

            int LocalPlayer = vam.ReadInt32((IntPtr)Base);

            Console.WriteLine("Player Address:" + LocalPlayer);

            while(true)
            {
                int healthAdr = LocalPlayer + Health;
                vam.WriteInt32((IntPtr)healthAdr, 9999);
                Console.WriteLine("GodMode!");

                int posX = LocalPlayer + PosX;
                vam.WriteFloat((IntPtr)posX, rnd.Next(-1000, 1000));

                int posY = LocalPlayer + PosY;
                vam.WriteFloat((IntPtr)posY, rnd.Next(-1000, 1000));

                int posZ = LocalPlayer + PosZ;
                vam.WriteFloat((IntPtr)posZ, rnd.Next(-1000, 1000));



            }


        }
    }
}
