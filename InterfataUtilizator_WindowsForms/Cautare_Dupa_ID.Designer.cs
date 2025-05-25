namespace InterfataUtilizator_WindowsForms
{
    partial class Cautare_Dupa_ID
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
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroTextBox1 = new MetroFramework.Controls.MetroTextBox();
            this.metroButton1 = new System.Windows.Forms.Button();
            this.metroButton2 = new System.Windows.Forms.Button();
            this.Modifica = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(23, 94);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(262, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Introduceti numele sectiei pe care o cautati:";
            this.metroLabel1.Click += new System.EventHandler(this.metroLabel1_Click);
            // 
            // metroTextBox1
            // 
            this.metroTextBox1.Location = new System.Drawing.Point(23, 131);
            this.metroTextBox1.Name = "metroTextBox1";
            this.metroTextBox1.Size = new System.Drawing.Size(250, 40);
            this.metroTextBox1.TabIndex = 1;
            this.metroTextBox1.Click += new System.EventHandler(this.metroTextBox1_Click);
            // 
            // metroButton1
            // 
            this.metroButton1.BackColor = System.Drawing.Color.SteelBlue;
            this.metroButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.metroButton1.FlatAppearance.BorderSize = 0;
            this.metroButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.metroButton1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.metroButton1.ForeColor = System.Drawing.Color.White;
            this.metroButton1.Location = new System.Drawing.Point(340, 132);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(138, 39);
            this.metroButton1.TabIndex = 2;
            this.metroButton1.Text = "Verifică";
            this.metroButton1.UseVisualStyleBackColor = false;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // metroButton2
            // 
            this.metroButton2.BackColor = System.Drawing.Color.Crimson;
            this.metroButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.metroButton2.FlatAppearance.BorderSize = 0;
            this.metroButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.metroButton2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.metroButton2.ForeColor = System.Drawing.Color.White;
            this.metroButton2.Location = new System.Drawing.Point(711, 133);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(138, 39);
            this.metroButton2.TabIndex = 3;
            this.metroButton2.Text = "Înapoi";
            this.metroButton2.UseVisualStyleBackColor = false;
            this.metroButton2.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // Modifica
            // 
            this.Modifica.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.Modifica.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Modifica.FlatAppearance.BorderSize = 0;
            this.Modifica.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Modifica.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.Modifica.ForeColor = System.Drawing.Color.White;
            this.Modifica.Location = new System.Drawing.Point(524, 132);
            this.Modifica.Name = "Modifica";
            this.Modifica.Size = new System.Drawing.Size(138, 39);
            this.Modifica.TabIndex = 4;
            this.Modifica.Text = "Modifică";
            this.Modifica.UseVisualStyleBackColor = false;
            this.Modifica.Click += new System.EventHandler(this.Modifica_Click);
            // 
            // Cautare_Dupa_ID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1319, 748);
            this.Controls.Add(this.Modifica);
            this.Controls.Add(this.metroButton2);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.metroTextBox1);
            this.Controls.Add(this.metroLabel1);
            this.Name = "Cautare_Dupa_ID";
            this.Text = "Cautare_Dupa_ID";
            this.Load += new System.EventHandler(this.Cautare_Dupa_ID_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox metroTextBox1;
        private System.Windows.Forms.Button metroButton1;
        private System.Windows.Forms.Button metroButton2;
        private System.Windows.Forms.Button Modifica;
    }
}