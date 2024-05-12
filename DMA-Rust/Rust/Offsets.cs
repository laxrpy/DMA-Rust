using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMA_Rust.Rust
{
    public class Offsets
    {
        #region Signatures
        public static uint LocalPlayer_c = 0x3B36E80; // "Signature": "LocalPlayer_c*"
        public static uint TodSky = 0x3B0C808; // "Signature": "TOD_Sky_c*"
        public static uint ConVar_Graphics_c = 0x3B04FF0; // "Signature": "ConVar_Graphics_c*"


        #endregion


        #region Chains
        public static uint[] BasePlayerChain = new uint[] { LocalPlayer_c, 0xB8, 0x0 };
        public static uint[] TodChain = new uint[] { TodSky, 0xb8, 0x0, 0x10, 0x20 };



        #endregion
    }
}
