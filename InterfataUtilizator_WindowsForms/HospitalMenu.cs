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

            //setari fereastra
            this.Text = "Meniu Principal";

            this.Theme = MetroThemeStyle.Light;
            this.Style = MetroColorStyle.Teal;

            this.BackColor = Color.FromArgb(240, 248, 255);
            this.Size = new Size(1000, 700);

            this.StartPosition = FormStartPosition.CenterScreen;

            // Buton pentru Pacienti
            MetroButton btnPacienti = new MetroButton();
            btnPacienti.Text = "Gestionare Pacienti";
            btnPacienti.Size = new Size(200, 200);
            btnPacienti.Location = new Point(200, 250);
            //200 distanta pe axa X , 250 distanta pe axa Y
            btnPacienti.Style = MetroColorStyle.Green;
            btnPacienti.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnPacienti.Click += BtnPacienti_Click;
            this.Controls.Add(btnPacienti);

            // Buton pentru Sectii
            MetroButton btnSectii = new MetroButton();
            btnSectii.Text = "Gestionare Sectii";
            btnSectii.Size = new Size(200, 200);

            btnSectii.Location = new Point(600, 250);
            btnSectii.Style = MetroColorStyle.Blue;
            btnSectii.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnSectii.Click += BtnSectii_Click;
            this.Controls.Add(btnSectii);
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
