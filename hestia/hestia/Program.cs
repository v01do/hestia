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
        public static int Ammo = 0x150;

        public static int PosX = 0x34;
        public static int PosY = 0x38;
        public static int PosZ = 0xC;

        public static Random rnd;

        public static Hera.HeraMenu heraMenu = new Hera.HeraMenu("Hestia created by github.com/v01do - AssaultCube Game Hack");

        public static bool isActivated = false;

        static void Main(string[] args)
        {
            
            heraMenu.AddHeraMenu("GodMode");
            heraMenu.AddHeraMenu("Infinite Ammo");
            heraMenu.AddHeraMenu("Random Teleport");
            heraMenu.AddHeraMenu("Fly (spacebar)");
            heraMenu.AddHeraMenu("Activate");

            RefreshConsole();

            rnd = new Random();

            bool foundPlayerLocalAddress = false;
            VAMemory vam = new VAMemory();
            int LocalPlayer = 0;
            
            while (true)
            {
                if(!isActivated)
                {
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        heraMenu.MenuIndexUp();
                    }
                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        heraMenu.MenuIndexDown();
                    }
                    if (key.Key == ConsoleKey.Enter)
                    {
                        heraMenu.MenuIndexLock();
                        if(heraMenu.GetCurrentMenuCursorIndex() == 4)
                        {
                            Console.WriteLine("HACK STARTED!");
                            isActivated = true;
                        }
                    }

                    RefreshConsole();
                }
                else
                {
                    // EXECUTE GAME HACKS
                    if(!foundPlayerLocalAddress)
                    {

                        vam = new VAMemory("ac_client");
                        LocalPlayer = vam.ReadInt32((IntPtr)Base);
                        foundPlayerLocalAddress = true;
                    }
                    else
                    {
                        foreach(var hackDex in heraMenu.GetHackIndex())
                        {
                            switch(hackDex)
                            {
                                case 0:
                                    int healthAdr = LocalPlayer + Health;
                                    vam.WriteInt32((IntPtr)healthAdr, 9999);
                                    break;
                                case 1:
                                    int ammoAdr = LocalPlayer + Ammo;
                                    vam.WriteInt32((IntPtr)ammoAdr, 9999);
                                    break;
                                case 2:
                                    break;
                                case 3:
                                    int posZ = LocalPlayer + PosZ;
                                    float currentPosz = vam.ReadFloat((IntPtr)posZ);
                                    vam.WriteFloat((IntPtr)posZ, posZ+2f);
                                    break;
                            }
                        }
                    }
                    




                }




                //int healthAdr = LocalPlayer + Health;
                //vam.WriteInt32((IntPtr)healthAdr, 9999);

                //int posX = LocalPlayer + PosX;
                //vam.WriteFloat((IntPtr)posX, rnd.Next(-1000, 1000));

                //int posY = LocalPlayer + PosY;
                //vam.WriteFloat((IntPtr)posY, rnd.Next(-1000, 1000));

                //int posZ = LocalPlayer + PosZ;
                //vam.WriteFloat((IntPtr)posZ, rnd.Next(-1000, 1000));



            }


        }

        public static void RefreshConsole()
        {
            Console.Clear();
            Console.Write(heraMenu.GetMenuAsStringWithSeperatedLines());
        }
    }
}
