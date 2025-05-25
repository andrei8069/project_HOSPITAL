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
    public partial class Cautare_Dupa_CNP : MetroForm
    {
        private DataGridView rezultatePacient;
        private const int CNP_LUNGIME = 13;
        public Cautare_Dupa_CNP()
        {
            InitializeComponent();
            this.Text = "Cautare dupa CNP";

            this.Theme = MetroThemeStyle.Light;
            this.Style = MetroColorStyle.Teal;

            this.BackColor = Color.FromArgb(240, 248, 255);
            this.Size = new Size(1000, 700);

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Cautare_Dupa_CNP_Load(object sender, EventArgs e)
        {
            rezultatePacient = new DataGridView();
            rezultatePacient.Name = "rezultatePacient";
            rezultatePacient.Location = new Point(30, 150);
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
        }

        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string eroare = "";
            metroTextBox1.Style = MetroColorStyle.Blue;
            metroTextBox1.UseStyleColors = true;
            // CNP
            string cnp = metroTextBox1.Text.Trim();
            if (cnp.Length != CNP_LUNGIME)
            {
                eroare += "CNP-ul trebuie sa aiba 13 caractere!\n";

                metroTextBox1.Style = MetroColorStyle.Red;
                metroTextBox1.UseStyleColors = true;
            }
            if (eroare != "")
            {   //afiseaza textul , titul ferestrei , un buton ok si un icon de avertizare
                MessageBox.Show(eroare, "Date invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            //linia de sus ia locatia folder-ului principal al solutiei


            string numeFisierPacienti = ConfigurationManager.AppSettings["NumeFisierPacienti"];
            string caleCompletaFisierPacienti = locatieFisierSolutie + "\\" + numeFisierPacienti;


            Pacienti_FISIERTEXT adminPacienti = new Pacienti_FISIERTEXT(caleCompletaFisierPacienti);
            Pacient pacientGasit = adminPacienti.FindCNP(cnp);

            rezultatePacient.Rows.Clear();

            if (pacientGasit != null)
            {
                rezultatePacient.Rows.Add(
                    pacientGasit.CodPacient,
                    pacientGasit.Nume,
                    pacientGasit.Prenume,
                    pacientGasit.Cnp,
                    pacientGasit.Varsta,
                    pacientGasit.Greutate,
                    pacientGasit.Inaltime,
                    pacientGasit.TemperaturaCorp,
                    pacientGasit.Grupa.ToString(),
                    pacientGasit.AfectiuniMed.ToString()
                );
            }
            else
            {
                MessageBox.Show("Pacientul cu CNP-ul introdus nu a fost gasit.", "CNP inexistent", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form formPacienti = Application.OpenForms["Form1"];

            if (formPacienti != null)
            {
                formPacienti.Show();
            }
            else
            {
                //meniu nou doar daca nu exista
                Form1 formNouPacienti = new Form1();
                formNouPacienti.Show();
            }

            this.Close();
        }

        private void Modifica_Click(object sender, EventArgs e)
        {
            if (rezultatePacient.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selectează un pacient din tabel!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow rand = rezultatePacient.SelectedRows[0];

            Pacient pacient = new Pacient
            {
                CodPacient = Convert.ToInt32(rand.Cells["CodPacient"].Value),
                Nume = rand.Cells["Nume"].Value.ToString(),
                Prenume = rand.Cells["Prenume"].Value.ToString(),
                Cnp = rand.Cells["Cnp"].Value.ToString(),
                Varsta = Convert.ToInt32(rand.Cells["Varsta"].Value),
                Greutate = Convert.ToDouble(rand.Cells["Greutate"].Value),
                Inaltime = Convert.ToDouble(rand.Cells["Inaltime"].Value),
                TemperaturaCorp = Convert.ToDouble(rand.Cells["Temperatura"].Value),
                Grupa = (GrupaSangePacient)Enum.Parse(typeof(GrupaSangePacient), rand.Cells["Grupa"].Value.ToString()),
                AfectiuniMed = (AfectiuniMedicale)Enum.Parse(typeof(AfectiuniMedicale), rand.Cells["Afectiuni"].Value.ToString())
            };

            ModificaPacient modifica = new ModificaPacient();
            modifica.SeteazaPacient(pacient);
            modifica.Show();
        }
    }
}
