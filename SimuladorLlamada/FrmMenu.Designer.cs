namespace SimuladorLlamadas
{
    partial class FrmMenu
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
            TELEFONO1 = new Button();
            TELEFONO2 = new Button();
            TELEFONO3 = new Button();
            TELEFONO4 = new Button();
            TELEFONO5 = new Button();
            SuspendLayout();
            // 
            // TELEFONO1
            // 
            TELEFONO1.Location = new Point(31, 12);
            TELEFONO1.Name = "TELEFONO1";
            TELEFONO1.Size = new Size(238, 23);
            TELEFONO1.TabIndex = 0;
            TELEFONO1.Text = "TELEFONO 1";
            TELEFONO1.UseVisualStyleBackColor = true;
            TELEFONO1.Click += TELEFONO1_Click;
            // 
            // TELEFONO2
            // 
            TELEFONO2.Location = new Point(31, 64);
            TELEFONO2.Name = "TELEFONO2";
            TELEFONO2.Size = new Size(238, 23);
            TELEFONO2.TabIndex = 1;
            TELEFONO2.Text = "TELEFONO 2";
            TELEFONO2.UseVisualStyleBackColor = true;
            TELEFONO2.Click += TELEFONO2_Click;
            // 
            // TELEFONO3
            // 
            TELEFONO3.Location = new Point(31, 117);
            TELEFONO3.Name = "TELEFONO3";
            TELEFONO3.Size = new Size(238, 23);
            TELEFONO3.TabIndex = 2;
            TELEFONO3.Text = "TELEFONO 3";
            TELEFONO3.UseVisualStyleBackColor = true;
            TELEFONO3.Click += TELEFONO3_Click;
            // 
            // TELEFONO4
            // 
            TELEFONO4.Location = new Point(31, 171);
            TELEFONO4.Name = "TELEFONO4";
            TELEFONO4.Size = new Size(238, 23);
            TELEFONO4.TabIndex = 3;
            TELEFONO4.Text = "TELEFONO 4";
            TELEFONO4.UseVisualStyleBackColor = true;
            TELEFONO4.Click += TELEFONO4_Click;
            // 
            // TELEFONO5
            // 
            TELEFONO5.Location = new Point(31, 234);
            TELEFONO5.Name = "TELEFONO5";
            TELEFONO5.Size = new Size(238, 23);
            TELEFONO5.TabIndex = 4;
            TELEFONO5.Text = "TELEFONO 5";
            TELEFONO5.UseVisualStyleBackColor = true;
            TELEFONO5.Click += TELEFONO5_Click;
            // 
            // FrmMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(296, 276);
            Controls.Add(TELEFONO5);
            Controls.Add(TELEFONO4);
            Controls.Add(TELEFONO3);
            Controls.Add(TELEFONO2);
            Controls.Add(TELEFONO1);
            Name = "FrmMenu";
            Text = "FrmMenu";
            ResumeLayout(false);
        }

        #endregion

        private Button TELEFONO1;
        private Button TELEFONO2;
        private Button TELEFONO3;
        private Button TELEFONO4;
        private Button TELEFONO5;
    }
}