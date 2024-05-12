using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace DMA_Rust
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
            mem.memory.StartUp();
        }
    }
}
