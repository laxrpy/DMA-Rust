namespace DMA_Rust
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.materialSwitch_BrightNight = new MaterialSkin.Controls.MaterialSwitch();
            this.materialSlider_FOVChanger = new MaterialSkin.Controls.MaterialSlider();
            this.materialSwitch_FovChanger = new MaterialSkin.Controls.MaterialSwitch();
            this.SuspendLayout();
            // 
            // materialSwitch_BrightNight
            // 
            this.materialSwitch_BrightNight.AutoSize = true;
            this.materialSwitch_BrightNight.Depth = 0;
            this.materialSwitch_BrightNight.Location = new System.Drawing.Point(3, 77);
            this.materialSwitch_BrightNight.Margin = new System.Windows.Forms.Padding(0);
            this.materialSwitch_BrightNight.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialSwitch_BrightNight.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialSwitch_BrightNight.Name = "materialSwitch_BrightNight";
            this.materialSwitch_BrightNight.Ripple = true;
            this.materialSwitch_BrightNight.Size = new System.Drawing.Size(142, 37);
            this.materialSwitch_BrightNight.TabIndex = 1;
            this.materialSwitch_BrightNight.Text = "Bright Night";
            this.materialSwitch_BrightNight.UseVisualStyleBackColor = true;
            this.materialSwitch_BrightNight.CheckedChanged += new System.EventHandler(this.materialSwitch_BrightNight_CheckedChanged);
            // 
            // materialSlider_FOVChanger
            // 
            this.materialSlider_FOVChanger.Depth = 0;
            this.materialSlider_FOVChanger.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialSlider_FOVChanger.Location = new System.Drawing.Point(6, 499);
            this.materialSlider_FOVChanger.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialSlider_FOVChanger.Name = "materialSlider_FOVChanger";
            this.materialSlider_FOVChanger.RangeMax = 210;
            this.materialSlider_FOVChanger.RangeMin = 50;
            this.materialSlider_FOVChanger.Size = new System.Drawing.Size(151, 40);
            this.materialSlider_FOVChanger.TabIndex = 2;
            this.materialSlider_FOVChanger.Text = "";
            this.materialSlider_FOVChanger.Value = 90;
            this.materialSlider_FOVChanger.Click += new System.EventHandler(this.materialSlider_FOVChangerAmm_Click);
            // 
            // materialSwitch_FovChanger
            // 
            this.materialSwitch_FovChanger.AutoSize = true;
            this.materialSwitch_FovChanger.Depth = 0;
            this.materialSwitch_FovChanger.Location = new System.Drawing.Point(6, 468);
            this.materialSwitch_FovChanger.Margin = new System.Windows.Forms.Padding(0);
            this.materialSwitch_FovChanger.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialSwitch_FovChanger.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialSwitch_FovChanger.Name = "materialSwitch_FovChanger";
            this.materialSwitch_FovChanger.Ripple = true;
            this.materialSwitch_FovChanger.Size = new System.Drawing.Size(151, 37);
            this.materialSwitch_FovChanger.TabIndex = 3;
            this.materialSwitch_FovChanger.Text = "FOV Changer";
            this.materialSwitch_FovChanger.UseVisualStyleBackColor = true;
            this.materialSwitch_FovChanger.CheckedChanged += new System.EventHandler(this.materialSwitch_FovChanger_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 545);
            this.Controls.Add(this.materialSwitch_FovChanger);
            this.Controls.Add(this.materialSlider_FOVChanger);
            this.Controls.Add(this.materialSwitch_BrightNight);
            this.Name = "Form1";
            this.Text = "Rusty Toolbox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MaterialSkin.Controls.MaterialSwitch materialSwitch_BrightNight;
        private MaterialSkin.Controls.MaterialSlider materialSlider_FOVChanger;
        private MaterialSkin.Controls.MaterialSwitch materialSwitch_FovChanger;
    }
}

