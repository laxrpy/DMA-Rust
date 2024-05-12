using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DMA_Rust.mem.memory;


namespace DMA_Rust.Rust.Classes
{
    
    public class BasePlayer
    {
        public static ulong BP = 0;


        public static bool BuildBasePlayer()
        {

            BP = ReadChain<ulong>(GameAssembly.vaBase, Offsets.BasePlayerChain);
            if (BP == 0)
            {
                return false;
            }

            Console.ForegroundColor = ConsoleColor.Green; // Set color to Green
            Console.WriteLine("[Classes] Found BasePlayer at: 0x" + BP.ToString("X"));
            Console.ResetColor();
            return true;
        }
        

    }
}
