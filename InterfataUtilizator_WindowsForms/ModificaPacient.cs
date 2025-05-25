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
    public partial class ModificaPacient : MetroForm
    {

        public ModificaPacient()
        {
            InitializeComponent();
            this.Text = "Modifica pacient";

            this.Theme = MetroThemeStyle.Light;
            this.Style = MetroColorStyle.Teal;

            this.BackColor = Color.FromArgb(240, 248, 255);
            this.Size = new Size(1000, 700);

            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void ModificaPacient_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
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

        public void SeteazaPacient(Pacient pacient)
        {
            cod.Text = pacient.CodPacient.ToString();
            nume.Text = pacient.Nume;
            prenume.Text = pacient.Prenume;
            cnp.Text = pacient.Cnp;
            varsta.Text = pacient.Varsta.ToString();
            greutate.Text = pacient.Greutate.ToString();
            inaltime.Text = pacient.Inaltime.ToString();
            temperatura.Text = pacient.TemperaturaCorp.ToString();

            foreach (var ctrl in Controls)
            {
                if (ctrl is RadioButton rb && rb.Text == pacient.Grupa.ToString())
                {
                    rb.Checked = true;
                }
            }

            foreach (var ctrl in Controls)
            {
                if (ctrl is CheckBox cb && Enum.TryParse(cb.Text, out AfectiuniMedicale af))
                {
                    cb.Checked = pacient.AfectiuniMed.HasFlag(af);
                }
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(cod.Text.Trim(), out int codPacient))
            {
                MessageBox.Show("Introduceți un COD valid.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string locatieFisier = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string numeFisierPacienti = ConfigurationManager.AppSettings["NumeFisierPacienti"];
            string caleFisier = Path.Combine(locatieFisier, numeFisierPacienti);

            Pacienti_FISIERTEXT adminPacienti = new Pacienti_FISIERTEXT(caleFisier);
            List<Pacient> pacienti = adminPacienti.GetPacienti();

            Pacient pacientGasit = pacienti.FirstOrDefault(p => p.CodPacient == codPacient);

            if (pacientGasit == null)
            {
                MessageBox.Show("Nu există un pacient cu acest cod.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string grupaSange = Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked)?.Text;
            List<string> afectiuni = Controls.OfType<CheckBox>().Where(cb => cb.Checked).Select(cb => cb.Text).ToList();

            var rezultat = adminPacienti.VerificaCampuriModificate(
                nume.Text.Trim(), prenume.Text.Trim(), cnp.Text.Trim(),
                varsta.Text.Trim(), greutate.Text.Trim(), inaltime.Text.Trim(), temperatura.Text.Trim(),
                grupaSange, afectiuni, pacienti, codPacient);

            if (!rezultat.valid)
            {
                MessageBox.Show(rezultat.mesaj, "Date invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool modificat = false;

            if (!string.IsNullOrWhiteSpace(nume.Text) && nume.Text.Trim() != pacientGasit.Nume)
            {
                pacientGasit.Nume = nume.Text.Trim();
                modificat = true;
            }

            if (!string.IsNullOrWhiteSpace(prenume.Text) && prenume.Text.Trim() != pacientGasit.Prenume)
            {
                pacientGasit.Prenume = prenume.Text.Trim();
                modificat = true;
            }

            if (!string.IsNullOrWhiteSpace(cnp.Text) && cnp.Text.Trim() != pacientGasit.Cnp)
            {
                pacientGasit.Cnp = cnp.Text.Trim();
                modificat = true;
            }

            if (!string.IsNullOrWhiteSpace(varsta.Text) && int.TryParse(varsta.Text.Trim(), out int varstaNoua))
            {
                if (varstaNoua != pacientGasit.Varsta)
                {
                    pacientGasit.Varsta = varstaNoua;
                    modificat = true;
                }
            }

            if (!string.IsNullOrWhiteSpace(greutate.Text) && double.TryParse(greutate.Text.Trim(), out double greutateNoua))
            {
                if (greutateNoua != pacientGasit.Greutate)
                {
                    pacientGasit.Greutate = greutateNoua;
                    modificat = true;
                }
            }

            if (!string.IsNullOrWhiteSpace(inaltime.Text) && double.TryParse(inaltime.Text.Trim(), out double inaltimeNoua))
            {
                if (inaltimeNoua != pacientGasit.Inaltime)
                {
                    pacientGasit.Inaltime = inaltimeNoua;
                    modificat = true;
                }
            }

            if (!string.IsNullOrWhiteSpace(temperatura.Text) && double.TryParse(temperatura.Text.Trim(), out double temperaturaNoua))
            {
                if (temperaturaNoua != pacientGasit.TemperaturaCorp)
                {
                    pacientGasit.TemperaturaCorp = temperaturaNoua;
                    modificat = true;
                }
            }

            if (!string.IsNullOrWhiteSpace(grupaSange) &&
                Enum.TryParse(grupaSange, out GrupaSangePacient grupaNoua) &&
                grupaNoua != pacientGasit.Grupa)
            {
                pacientGasit.Grupa = grupaNoua;
                modificat = true;
            }

            AfectiuniMedicale afectiuniNou = AfectiuniMedicale.Nespecificat;
            foreach (string af in afectiuni)
                if (Enum.TryParse(af, out AfectiuniMedicale afMed)) afectiuniNou |= afMed;

            if (afectiuni.Count > 0 && afectiuniNou != pacientGasit.AfectiuniMed)
            {
                pacientGasit.AfectiuniMed = afectiuniNou;
                modificat = true;
            }

            if (!modificat)
            {
                MessageBox.Show("Nu ați completat niciun câmp sau toate datele sunt identice!", "Fără modificări", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            for (int i = 0; i < pacienti.Count; i++)
            {
                if (pacienti[i].CodPacient == pacientGasit.CodPacient)
                {
                    pacienti[i] = pacientGasit;
                    break;
                }
            }

            File.WriteAllLines(caleFisier, pacienti.Select(p => p.ConversieLaSir_PentruFisier()));

            // Reset
            nume.ResetText();
            prenume.ResetText();
            cnp.ResetText();
            varsta.ResetText();
            greutate.ResetText();
            inaltime.ResetText();
            temperatura.ResetText();
            foreach (var ctrl in Controls)
            {
                if (ctrl is RadioButton rb) rb.Checked = false;
                if (ctrl is CheckBox cb) cb.Checked = false;
            }

            MessageBox.Show("Pacientul a fost modificat cu succes.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }









    }
}
