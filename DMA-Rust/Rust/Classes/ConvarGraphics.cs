using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DMA_Rust.mem.memory;

namespace DMA_Rust.Rust.Classes
{
    public class ConvarGraphics
    {
        public static ulong CG;

        public static bool getConvarGraphics()
        {
            ulong graphics = ReadMemory<ulong>(GameAssembly.vaBase + Offsets.ConVar_Graphics_c);
            CG = ReadMemory<ulong>(graphics + 0xB8);
            if (CG == 0)
            {
                return false;
            }

            Console.ForegroundColor = ConsoleColor.Green; // Set color to Green
            Console.WriteLine("[Classes] Found Convar_Graphics at: 0x" + CG.ToString("X"));
            Console.ResetColor();
            return true;

        }
    }
}
