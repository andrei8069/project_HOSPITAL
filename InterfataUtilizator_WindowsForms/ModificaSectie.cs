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
    public partial class ModificaSectie : MetroForm
    {
        //constante pt valori bune sectii
        private const int LUNGIME_MIN_NUME_SECTIE = 2;
        private const int LUNGIME_MAX_NUME_SECTIE = 50;
        private const int ETAJ_MIN = 0;
        private const int ETAJ_MAX = 20;
        private const int CAPACITATE_MIN = 1;
        private const int CAPACITATE_MAX = 500;
        private const int NR_PACIENTI_MIN = 0;
        private const double TEMPERATURA_MIN = 10.0;
        private const double TEMPERATURA_MAX = 35.0;
        private const double SUPRAFATA_MIN = 20.0;
        private const double SUPRAFATA_MAX = 1000.0;
        private const double BUGET_MIN = 0;

        public ModificaSectie()
        {
            InitializeComponent();
            this.Text = "Modifica sectie";

            this.Theme = MetroThemeStyle.Light;
            this.Style = MetroColorStyle.Teal;

            this.BackColor = Color.FromArgb(240, 248, 255);
            this.Size = new Size(1000, 700);

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void ModificaSectie_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form formPacienti = Application.OpenForms["Sectii_Spital"];

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

        public void SeteazaSectie(SectieSpital sectie)
        {
            cod.Value = sectie.CodSectie;
            nume.Text = sectie.NumeSectie;
            etaj.Value = sectie.Etaj;
            capacitate.Text = sectie.CapacitateMaxima.ToString();
            nrPacienti.Text = sectie.NrPacientiInternati.ToString();
            temperatura.Text = sectie.TemperaturaMediu.ToString();
            suprafata.Text = sectie.SuprafataSectie.ToString();
            buget.Text = sectie.BugetSectie.ToString();

            foreach(var control in Controls)
            {
                if (control is RadioButton rb && rb.Text == sectie.Status.ToString())
                {
                    rb.Checked = true;
                }
            }

            foreach (var control in Controls)
            {
                if(control is CheckBox cb && Enum.TryParse(cb.Text, out DotariSectie dotare))
                {
                   cb.Checked = sectie.DotariSec.HasFlag(dotare);
                }
            }

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            int codSectie = (int)cod.Value;
            string locatieFisier = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string numeFisierSectii = ConfigurationManager.AppSettings["NumeFisierSectii"];
            string caleFisier = Path.Combine(locatieFisier, numeFisierSectii);

            Sectii_FISIERTEXT adminSectii = new Sectii_FISIERTEXT(caleFisier);
            List<SectieSpital> sectii = adminSectii.GetSectii();
            SectieSpital sectieGasita = sectii.FirstOrDefault(s => s.CodSectie == codSectie);

            if (sectieGasita == null)
            {
                MessageBox.Show("Nu există o secție cu acest cod.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // preluare status selectat si dotari bifate
            string statusSelectat = Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked)?.Text;
            List<string> dotariSelectate = Controls.OfType<CheckBox>().Where(cb => cb.Checked).Select(cb => cb.Text).ToList();

            // validare partiala
            var rezultat = adminSectii.VerificaCampuriModificateSectie(
                nume.Text.Trim(), etaj.Value.ToString(), capacitate.Text.Trim(), nrPacienti.Text.Trim(),
                temperatura.Text.Trim(), suprafata.Text.Trim(), buget.Text.Trim(),
                statusSelectat, dotariSelectate, sectii, codSectie);

            if (!rezultat.valid)
            {
                MessageBox.Show(rezultat.mesaj, "Date invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool modificat = false;

            if (!string.IsNullOrWhiteSpace(nume.Text) && nume.Text.Trim() != sectieGasita.NumeSectie)
            {
                sectieGasita.NumeSectie = nume.Text.Trim();
                modificat = true;
            }

            if ((int)etaj.Value != sectieGasita.Etaj)
            {
                sectieGasita.Etaj = (int)etaj.Value;
                modificat = true;
            }

            if (!string.IsNullOrWhiteSpace(capacitate.Text) &&
                int.TryParse(capacitate.Text.Trim(), out int capacitateNoua) &&
                capacitateNoua != sectieGasita.CapacitateMaxima)
            {
                sectieGasita.CapacitateMaxima = capacitateNoua;
                modificat = true;
            }

            if (!string.IsNullOrWhiteSpace(nrPacienti.Text) &&
                int.TryParse(nrPacienti.Text.Trim(), out int nrPacientiNou) &&
                nrPacientiNou != sectieGasita.NrPacientiInternati)
            {
                sectieGasita.NrPacientiInternati = nrPacientiNou;
                modificat = true;
            }

            if (!string.IsNullOrWhiteSpace(temperatura.Text) &&
                double.TryParse(temperatura.Text.Trim(), out double tempNoua) &&
                tempNoua != sectieGasita.TemperaturaMediu)
            {
                sectieGasita.TemperaturaMediu = tempNoua;
                modificat = true;
            }

            if (!string.IsNullOrWhiteSpace(suprafata.Text) &&
                double.TryParse(suprafata.Text.Trim(), out double suprafNoua) &&
                suprafNoua != sectieGasita.SuprafataSectie)
            {
                sectieGasita.SuprafataSectie = suprafNoua;
                modificat = true;
            }

            if (!string.IsNullOrWhiteSpace(buget.Text) &&
                double.TryParse(buget.Text.Trim(), out double bugetNou) &&
                bugetNou != sectieGasita.BugetSectie)
            {
                sectieGasita.BugetSectie = bugetNou;
                modificat = true;
            }

            if (!string.IsNullOrWhiteSpace(statusSelectat) &&
                Enum.TryParse(statusSelectat, out StatusFunctionareSectie statusNou) &&
                statusNou != sectieGasita.Status)
            {
                sectieGasita.Status = statusNou;
                modificat = true;
            }

            DotariSectie dotariNou = DotariSectie.Nespecificat;
            foreach (string d in dotariSelectate)
                if (Enum.TryParse(d, out DotariSectie dotare))
                    dotariNou |= dotare;

            if (dotariSelectate.Count > 0 && dotariNou != sectieGasita.DotariSec)
            {
                sectieGasita.DotariSec = dotariNou;
                modificat = true;
            }

            if (!modificat)
            {
                MessageBox.Show("Nu ați completat niciun câmp sau toate datele sunt identice!", "Fără modificări", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // salvare
            for (int i = 0; i < sectii.Count; i++)
                if (sectii[i].CodSectie == sectieGasita.CodSectie)
                {
                    sectii[i] = sectieGasita;
                    break;
                }

            File.WriteAllLines(caleFisier, sectii.Select(s => s.ConversieLaSir_PentruFisier()));

            // reset UI
            nume.ResetText();
            etaj.ResetText();
            capacitate.ResetText();
            nrPacienti.ResetText();
            temperatura.ResetText();
            suprafata.ResetText();
            buget.ResetText();
            foreach (var ctrl in Controls)
            {
                if (ctrl is RadioButton rb) rb.Checked = false;
                if (ctrl is CheckBox cb) cb.Checked = false;
            }

            MessageBox.Show("Secția a fost modificată cu succes.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }




    }
}
