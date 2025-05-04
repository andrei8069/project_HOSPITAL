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

            if (pacientGasit != null)
            {
                MessageBox.Show(
                    $"Pacient gasit:\n\n" +
                    $"Cod Pacient: {pacientGasit.CodPacient}\n"+
                    $"Nume: {pacientGasit.Nume}\n" +
                    $"Prenume: {pacientGasit.Prenume}\n" +
                    $"CNP: {pacientGasit.Cnp}\n" +
                    $"Varsta: {pacientGasit.Varsta}\n" +
                    $"Greutate: {pacientGasit.Greutate}\n" +
                    $"Inaltime: {pacientGasit.Inaltime}\n" +
                    $"Temperatura corp: {pacientGasit.TemperaturaCorp}\n" +
                    $"Grupa de sange: {pacientGasit.Grupa}\n" +
                    $"Afectiuni: {pacientGasit.AfectiuniMed}",
                    "Pacient gasit",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
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
    }
}
