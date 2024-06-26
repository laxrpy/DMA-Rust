﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DMA_Rust.mem.memory;

namespace DMA_Rust.Rust.Classes
{
    public class Playermodel
    {
        public static ulong PM;

        public static bool BuildPlayerModel()
        {
            PM = ReadMemory<ulong>(BasePlayer.BP + 0x598);
            if (PM == 0)
            {
                return false;
            }

            Console.ForegroundColor = ConsoleColor.Green; // Set color to Green
            Console.WriteLine("[Classes] Found PlayerModel at: 0x" + PM.ToString("X"));
            Console.ResetColor();
            return true;

        }


    }
}
