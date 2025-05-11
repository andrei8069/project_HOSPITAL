using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using MetroFramework.Forms;
using MetroFramework.Controls;
using MetroFramework.Components;



using System.Configuration;
using System.IO;
using LibrarieModele;
using NivelStocareDate;
using static System.Collections.Specialized.BitVector32;
using MetroFramework;

namespace InterfataUtilizator_WindowsForms
{
    public partial class HospitalMenu : MetroForm
    {
        public HospitalMenu()
        {
            InitializeComponent();

            // Setari fereastra
            this.Text = "Hospital";
            this.Theme = MetroThemeStyle.Light;
            this.Style = MetroColorStyle.Blue;
            this.BackColor = Color.FromArgb(135, 206, 235); // SkyBlue (cer senin)
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

           // Titlu central
            MetroLabel lblTitlu = new MetroLabel();
            lblTitlu.Text = "Main Menu";
            lblTitlu.FontSize = MetroLabelSize.Tall;
            lblTitlu.FontWeight = MetroLabelWeight.Bold;
            lblTitlu.AutoSize = true;
            lblTitlu.Location = new Point((this.ClientSize.Width - lblTitlu.PreferredWidth) / 2, 30);
            //latimea disponibila a ferestrei - latimea ideala / 2 , 30 - pozitia pe axa y
            this.Controls.Add(lblTitlu);

            // CARD PACIENTI
            Panel cardPacienti = new Panel();
            cardPacienti.Size = new Size(300, 300);
            cardPacienti.Location = new Point(160, 150);
            cardPacienti.BackColor = Color.FromArgb(230, 240, 255); // albastru deschis
            cardPacienti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(cardPacienti);

            PictureBox imgDoctor = new PictureBox();
            imgDoctor.Size = new Size(128, 128);
            imgDoctor.Location = new Point(86, 20);
            imgDoctor.SizeMode = PictureBoxSizeMode.Zoom;
            try { imgDoctor.Image = Image.FromFile("images/doctor.png"); }
            catch { imgDoctor.BackColor = Color.LightGray; }
            cardPacienti.Controls.Add(imgDoctor);

            Button btnPacienti = new Button();
            btnPacienti.Text = "Gestionare pacienți";
            btnPacienti.Size = new Size(200, 50);
            btnPacienti.Location = new Point(50, 180);
            btnPacienti.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnPacienti.BackColor = Color.DeepSkyBlue;      // fundal modern
            btnPacienti.ForeColor = Color.White;            // text alb
            btnPacienti.FlatStyle = FlatStyle.Flat;
            btnPacienti.FlatAppearance.BorderSize = 0;
            btnPacienti.Cursor = Cursors.Hand;
            btnPacienti.Click += BtnPacienti_Click;
            cardPacienti.Controls.Add(btnPacienti);

            // CARD SECTII 
            Panel cardSectii = new Panel();
            cardSectii.Size = new Size(300, 300);
            cardSectii.Location = new Point(520, 150);
            cardSectii.BackColor = Color.FromArgb(230, 240, 255); // albastru deschis
            cardSectii.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(cardSectii);

            PictureBox imgHospital = new PictureBox();
            imgHospital.Size = new Size(128, 128);
            imgHospital.Location = new Point(86, 20);
            imgHospital.SizeMode = PictureBoxSizeMode.Zoom;
            try { imgHospital.Image = Image.FromFile("images/hospital.png"); }
            catch { imgHospital.BackColor = Color.LightGray; }
            cardSectii.Controls.Add(imgHospital);

            Button btnSectii = new Button();
            btnSectii.Text = "Gestionare secții";
            btnSectii.Size = new Size(200, 50);
            btnSectii.Location = new Point(50, 180);
            btnSectii.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnSectii.BackColor = Color.MediumSlateBlue;    // fundal violet modern
            btnSectii.ForeColor = Color.White;              // text alb
            btnSectii.FlatStyle = FlatStyle.Flat;
            btnSectii.FlatAppearance.BorderSize = 0;
            btnSectii.Cursor = Cursors.Hand;
            btnSectii.Click += BtnSectii_Click;
            cardSectii.Controls.Add(btnSectii);
        }



        private void HospitalMenu_Load(object sender, EventArgs e)
        {
        }

        private void BtnPacienti_Click(object sender, EventArgs e)
        {
            Form1 Pacienti = new Form1();
            Pacienti.Show();
        }

        private void BtnSectii_Click(object sender, EventArgs e)
        {
            Sectii_Spital Sectii = new Sectii_Spital();
            Sectii.Show();
        }
    }
}