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
    public partial class StergePacient : MetroForm
    {
        private DataGridView rezultatePacient;
        private Pacienti_FISIERTEXT adminPacienti;

        public StergePacient()
        {
            InitializeComponent();
            this.Text = "Sterge Pacient";
            this.Theme = MetroThemeStyle.Light;
            this.Style = MetroColorStyle.Red;
            this.BackColor = Color.FromArgb(240, 248, 255);
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void StergePacient_Load(object sender, EventArgs e)
        {
            string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string numeFisierPacienti = ConfigurationManager.AppSettings["NumeFisierPacienti"];
            string caleCompletaFisierPacienti = locatieFisierSolutie + "\\" + numeFisierPacienti;
            adminPacienti = new Pacienti_FISIERTEXT(caleCompletaFisierPacienti);

            rezultatePacient = new DataGridView();
            rezultatePacient.Name = "rezultatePacient";
            rezultatePacient.Location = new Point(30, 100);
            rezultatePacient.Size = new Size(920, 450);
            rezultatePacient.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            rezultatePacient.ReadOnly = true;
            rezultatePacient.AllowUserToAddRows = false;
            rezultatePacient.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            rezultatePacient.Columns.Add("CodPacient", "Cod Pacient");
            rezultatePacient.Columns.Add("Nume", "Nume");
            rezultatePacient.Columns.Add("Prenume", "Prenume");
            rezultatePacient.Columns.Add("Cnp", "CNP");
            rezultatePacient.Columns.Add("Varsta", "Vârstă");
            rezultatePacient.Columns.Add("Greutate", "Greutate");
            rezultatePacient.Columns.Add("Inaltime", "Înălțime");
            rezultatePacient.Columns.Add("Temperatura", "Temperatură");
            rezultatePacient.Columns.Add("Grupa", "Grupa Sânge");
            rezultatePacient.Columns.Add("Afectiuni", "Afectiuni");

            this.Controls.Add(rezultatePacient);

            Button btnSterge = new Button();
            btnSterge.Text = "Șterge Pacient";
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

            IncarcaPacienti();
        }

        private void IncarcaPacienti()
        {
            rezultatePacient.Rows.Clear();
            List<Pacient> pacienti = adminPacienti.GetPacienti();

            foreach (Pacient pacient in pacienti)
            {
                rezultatePacient.Rows.Add(
                    pacient.CodPacient,
                    pacient.Nume,
                    pacient.Prenume,
                    pacient.Cnp,
                    pacient.Varsta,
                    pacient.Greutate,
                    pacient.Inaltime,
                    pacient.TemperaturaCorp,
                    pacient.Grupa.ToString(),
                    pacient.AfectiuniMed.ToString()
                );
            }
        }

        private void BtnSterge_Click(object sender, EventArgs e)
        {
            if (rezultatePacient.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selectează un pacient din tabel!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow rand = rezultatePacient.SelectedRows[0];
            int codPacient = Convert.ToInt32(rand.Cells["CodPacient"].Value);

            adminPacienti.StergePacient(codPacient);
            MessageBox.Show("Pacient șters cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            IncarcaPacienti();
        }

        private void BtnInapoi_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
} 