using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DMA_Rust.Rust;
using DMA_Rust.Rust.Classes;
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
            materialSlider_FOVChanger.Hide();

        }

        private void materialSwitch_BrightNight_CheckedChanged(object sender, EventArgs e)
        {
            bools.BrightNight = materialSwitch_BrightNight.Checked;
        } //BrightNight


        #region FOV-Changer
        private void materialSlider_FOVChangerAmm_Click(object sender, EventArgs e)
        {
            
            bools.FOV_Value = materialSlider_FOVChanger.Value;
        }

        private void materialSwitch_FovChanger_CheckedChanged(object sender, EventArgs e)
        {
            bools.FOVChanger = materialSwitch_FovChanger.Checked;
            
            if (materialSwitch_FovChanger.Checked)
            {
                materialSlider_FOVChanger.Show();
            }
            else
            {
                mem.memory.WriteMemory<float>(ConvarGraphics.CG + 0x18, 90.0f);
                materialSlider_FOVChanger.Hide();
            }
        }



        #endregion

        private void materialSwitch_Chams_CheckedChanged(object sender, EventArgs e)
        {
            bools.Chams = materialSwitch_Chams.Checked;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void materialLabel_DMAStatus_Click(object sender, EventArgs e)
        {

        }

        private void label_DMAStatus_Click(object sender, EventArgs e)
        {

        }


    }
}
