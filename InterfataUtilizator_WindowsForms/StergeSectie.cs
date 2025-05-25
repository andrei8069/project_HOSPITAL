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
using MetroFramework;
using System.Configuration;
using System.IO;
using LibrarieModele;
using NivelStocareDate;

namespace InterfataUtilizator_WindowsForms
{
    public partial class StergeSectie : MetroForm
    {
        private DataGridView rezultateSectie;
        private Sectii_FISIERTEXT adminSectii;

        public StergeSectie()
        {
            InitializeComponent();
            this.Text = "Sterge Sectie";
            this.Theme = MetroThemeStyle.Light;
            this.Style = MetroColorStyle.Red;
            this.BackColor = Color.FromArgb(240, 248, 255);
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void StergeSectie_Load(object sender, EventArgs e)
        {
            string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string numeFisierSectii = ConfigurationManager.AppSettings["NumeFisierSectii"];
            string caleCompletaFisierSectii = locatieFisierSolutie + "\\" + numeFisierSectii;
            adminSectii = new Sectii_FISIERTEXT(caleCompletaFisierSectii);

            rezultateSectie = new DataGridView();
            rezultateSectie.Name = "rezultateSectie";
            rezultateSectie.Location = new Point(30, 100);
            rezultateSectie.Size = new Size(920, 450);
            rezultateSectie.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            rezultateSectie.ReadOnly = true;
            rezultateSectie.AllowUserToAddRows = false;
            rezultateSectie.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            rezultateSectie.Columns.Add("CodSectie", "Cod Sectie");
            rezultateSectie.Columns.Add("NumeSectie", "Nume Sectie");
            rezultateSectie.Columns.Add("Etaj", "Etaj");
            rezultateSectie.Columns.Add("CapacitateMaxima", "Capacitate Maxima");
            rezultateSectie.Columns.Add("NrPacientiInternati", "Nr. Pacienti Internati");
            rezultateSectie.Columns.Add("TemperaturaMediu", "Temperatura Mediu");
            rezultateSectie.Columns.Add("SuprafataSectie", "Suprafata Sectie");
            rezultateSectie.Columns.Add("BugetSectie", "Buget Sectie");
            rezultateSectie.Columns.Add("Status", "Status Functionare");
            rezultateSectie.Columns.Add("Dotari", "Dotari");

            this.Controls.Add(rezultateSectie);

            Button btnSterge = new Button();
            btnSterge.Text = "Șterge Secție";
            btnSterge.Location = new Point(30, 570);
            btnSterge.Size = new Size(150, 40);
            btnSterge.BackColor = Color.Red;
            btnSterge.ForeColor = Color.White;
            btnSterge.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnSterge.FlatStyle = FlatStyle.Flat;
            btnSterge.FlatAppearance.BorderSize = 0;
            btnSterge.Cursor = Cursors.Hand;
            btnSterge.Click += BtnSterge_Click;
            this.Controls.Add(btnSterge);

            Button btnInapoi = new Button();
            btnInapoi.Text = "Înapoi";
            btnInapoi.Location = new Point(200, 570);
            btnInapoi.Size = new Size(100, 40);
            btnInapoi.BackColor = Color.Gray;
            btnInapoi.ForeColor = Color.White;
            btnInapoi.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnInapoi.FlatStyle = FlatStyle.Flat;
            btnInapoi.FlatAppearance.BorderSize = 0;
            btnInapoi.Cursor = Cursors.Hand;
            btnInapoi.Click += BtnInapoi_Click;
            this.Controls.Add(btnInapoi);

            IncarcaSectii();
        }

        private void IncarcaSectii()
        {
            rezultateSectie.Rows.Clear();
            List<SectieSpital> sectii = adminSectii.GetSectii();

            foreach (SectieSpital sectie in sectii)
            {
                rezultateSectie.Rows.Add(
                    sectie.CodSectie,
                    sectie.NumeSectie,
                    sectie.Etaj,
                    sectie.CapacitateMaxima,
                    sectie.NrPacientiInternati,
                    sectie.TemperaturaMediu,
                    sectie.SuprafataSectie,
                    sectie.BugetSectie,
                    sectie.Status,
                    sectie.DotariSec
                );
            }
        }

        private void BtnSterge_Click(object sender, EventArgs e)
        {
            if (rezultateSectie.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selectează o secție din tabel!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow rand = rezultateSectie.SelectedRows[0];
            int codSectie = Convert.ToInt32(rand.Cells["CodSectie"].Value);

            adminSectii.StergeSectie(codSectie);
            MessageBox.Show("Secție ștearsă cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            IncarcaSectii();
        }

        private void BtnInapoi_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
} 