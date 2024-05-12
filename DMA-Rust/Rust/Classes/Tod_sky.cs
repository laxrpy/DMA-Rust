using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DMA_Rust.mem.memory;

namespace DMA_Rust.Rust.Classes
{
    public class Tod_sky
    {
        public static ulong TS;
        public static bool BuildTOD()
        {
            TS = ReadChain<ulong>(GameAssembly.vaBase, Offsets.TodChain);
            if (TS == 0)
            {
                return false;
            }

            Console.ForegroundColor = ConsoleColor.Green; // Set color to Green
            Console.WriteLine("[Classes] Found TOD_Sky at: 0x" + TS.ToString("X"));
            Console.ResetColor();

            return true;
        }
    }
}
