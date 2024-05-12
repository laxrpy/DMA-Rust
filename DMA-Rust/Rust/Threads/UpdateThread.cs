using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DMA_Rust.mem.memory;
using DMA_Rust;
using DMA_Rust.Rust.Classes;
using System.Threading;
using System.CodeDom;
using System.Windows.Forms;
using MaterialSkin.Controls;


namespace DMA_Rust.Rust
{
    public class UpdateThread
    {
        public static void UTBegin()
        {


            //Form1.MaterialForm.materialListBox_ActivePlayers;




            while (true)
            {
                ulong night = ReadMemory<ulong>(Tod_sky.TS + 0x60);
                ulong ambiend = ReadMemory<ulong>(Tod_sky.TS + 0x98);

                if (bools.BrightNight != false)
                {
                    WriteMemory<float>(night + 0x50, 4.0f);
                    WriteMemory<float>(night + 0x54, 1.0f);
                    WriteMemory<float>(ambiend + 0x18, 10.0f);
                    WriteMemory<float>(ambiend + 0x14, 0.0f);
                } //takes a while to go into effect idk why..


                if (bools.FOVChanger != false)
                {
                    WriteMemory<float>(ConvarGraphics.CG + 0x18, (float)bools.FOV_Value);
                }

                
                
                while (bools.Chams != false)
                {

                    ulong go = GameObject();
                    ulong objectClasses = ReadMemory<ulong>(go + 0x30);
                    ulong Entity = ReadMemory<ulong>(objectClasses + 0x18);
                    ulong baseEntity = ReadMemory<ulong>(Entity + 0x28);

                    if (baseEntity != 0 && Tod_sky.TS != 0)
                    {

                        ulong components = ReadMemory<ulong>(Tod_sky.TS + 0xB0); //change to 0x0
                        //ulong scattering = ReadMemory<ulong>(components + 0x1A8); //	private TOD_Scattering <Scattering>k__BackingField; // 0x1A8
                                                                                  //ulong material = ReadMemory<ulong>(scattering + 0x80);
                        ulong sunmat = ReadMemory<ulong>(components + 0x140);    //doesnt really work, make try changing sun brightness in tod?                 

                        ulong playerModel = ReadMemory<ulong>(baseEntity + 0x598); //playerModel in Baseplayer
                        ulong skinSet = ReadMemory<ulong>(playerModel + 0x158); //female skin set offset inside playermodel
                        ulong skinSetMale = ReadMemory<ulong>(playerModel + 0x150); //male skin set offsets


                        setMaterial(skinSetMale, 0); //0 is null chams (pink) or use reflective_material()
                        setMaterial(skinSet, 0);     //0 is null chams (pink) or use reflective_material()
                        WriteMemory<bool>(baseEntity + 0x608, true);




                    }     

                }




            }


        }

        public static ulong GameObject()
        {
            ulong baseEntity = ReadMemory<ulong>(GameAssembly.vaBase + Offsets.BaseNetworkable_c);

            ulong bufferList = ReadChain<ulong>(baseEntity, Offsets.ToBuffList);
            ulong objectList = ReadMemory<ulong>(bufferList + 0x18);
            uint objectListSize = ReadMemory<uint>(bufferList + 0x10);
            for (ulong i = 0; i < objectListSize; i++)
            {

                ulong curObj = ReadMemory<ulong>(objectList + (0x20 + (i * 8)));
                return ReadChain<ulong>(curObj, Offsets.GameObjectChain);

            }
            Console.Write("Failed To Obtain GameObject in Write Features...");
            return 0;
        }


        public static void setMaterial(ulong skinset, ulong material)
        {
            if (skinset != 0)
            {
                ulong skins = ReadMemory<ulong>(skinset + 0x18);
                int size = ReadMemory<int>(skins + 0x18);

                if (size < 20)
                {
                    for (int e = 0; e < size; e++)
                    {
                        ulong currentSkinSet = ReadMemory<ulong>(skins + 0x20 + (ulong)(e * 0x8));

                        if (currentSkinSet != 0)
                        {                   
                            WriteMemory<ulong>(currentSkinSet + 0x68, material);
                            WriteMemory<ulong>(currentSkinSet + 0x70, material);
                            WriteMemory<ulong>(currentSkinSet + 0x78, material);               
                        }
                    }
                }

            }
        }

        static ulong reflective_material()
        {
            ulong reflective_mateiral = ReadMemory<ulong>(GameAssembly.vaBase + 0x3B5B798); //OutlineManager_c
            ulong reflective_mateiral_padding = ReadMemory<ulong>(reflective_mateiral + 0xB8); //OutlineManager_c
            return ReadMemory<ulong>(reflective_mateiral_padding + 0x0); //OutlineManager_c //reflective_mateiral_nigger
        }

    }
}
