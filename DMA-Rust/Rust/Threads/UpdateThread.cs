using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DMA_Rust.mem.memory;
using DMA_Rust;
using DMA_Rust.Rust.Classes;


namespace DMA_Rust.Rust
{
    public class UpdateThread
    {
        public static void UTBegin()
        {

            while (true)
            {
                ulong night = ReadMemory<ulong>(Tod_sky.TS + 0x60);
                ulong ambiend = ReadMemory<ulong>(Tod_sky.TS + 0x98);
                Console.WriteLine(bools.FOV_Value);
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


            }


        }


    }
}
