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

        private void metroButton2_Click(object sender, EventArgs e)
        {
            int codSectie = (int)cod.Value;
            string locatieFisier = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string numeFisierSectii = ConfigurationManager.AppSettings["NumeFisierSectii"];
            string caleFisier = Path.Combine(locatieFisier, numeFisierSectii);

            Sectii_FISIERTEXT adminSectii = new Sectii_FISIERTEXT(caleFisier);
            SectieSpital[] sectii = adminSectii.GetSectie(out int nrSectii);

            bool gasit = false;
            for (int i = 0; i < nrSectii; i++)
            {
                if (sectii[i].CodSectie == codSectie)
                {
                    gasit = true;
                    SectieSpital modificat = sectii[i];
                    bool modificatMacarUnCamp = false;
                    string eroare = "";

                    if (!string.IsNullOrWhiteSpace(nume.Text) && nume.Text != modificat.NumeSectie)
                    {
                        if (nume.Text.Length < 2 || nume.Text.Length > 50)
                            eroare += "Numele sectiei trebuie sa aiba intre 2 si 50 caractere.\n";
                        else
                        {
                            modificat.NumeSectie = nume.Text;
                            modificatMacarUnCamp = true;
                        }
                    }

                    if ((int)etaj.Value != modificat.Etaj)
                    {
                        if ((int)etaj.Value < 0 || (int)etaj.Value > 20)
                            eroare += "Etajul trebuie sa fie intre 0 si 20.\n";
                        else
                        {
                            modificat.Etaj = (int)etaj.Value;
                            modificatMacarUnCamp = true;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(capacitate.Text) && int.TryParse(capacitate.Text, out int capacitateNoua))
                    {
                        if (capacitateNoua < 1 || capacitateNoua > 500)
                            eroare += "Capacitatea trebuie sa fie intre 1 si 500.\n";
                        else if (capacitateNoua != modificat.CapacitateMaxima)
                        {
                            modificat.CapacitateMaxima = capacitateNoua;
                            modificatMacarUnCamp = true;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(nrPacienti.Text) && int.TryParse(nrPacienti.Text, out int nrPacientiNou))
                    {
                        if (nrPacientiNou < 0 || nrPacientiNou > modificat.CapacitateMaxima)
                            eroare += "Numarul de pacienti trebuie sa fie intre 0 si capacitatea maxima.\n";
                        else if (nrPacientiNou != modificat.NrPacientiInternati)
                        {
                            modificat.NrPacientiInternati = nrPacientiNou;
                            modificatMacarUnCamp = true;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(temperatura.Text) && double.TryParse(temperatura.Text, out double tempNoua))
                    {
                        if (tempNoua < 10.0 || tempNoua > 35.0)
                            eroare += "Temperatura trebuie sa fie intre 10.0 si 35.0.\n";
                        else if (tempNoua != modificat.TemperaturaMediu)
                        {
                            modificat.TemperaturaMediu = tempNoua;
                            modificatMacarUnCamp = true;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(suprafata.Text) && double.TryParse(suprafata.Text, out double suprafataNoua))
                    {
                        if (suprafataNoua < 20.0 || suprafataNoua > 1000.0)
                            eroare += "Suprafata trebuie sa fie intre 20.0 si 1000.0.\n";
                        else if (suprafataNoua != modificat.SuprafataSectie)
                        {
                            modificat.SuprafataSectie = suprafataNoua;
                            modificatMacarUnCamp = true;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(buget.Text) && double.TryParse(buget.Text, out double bugetNou))
                    {
                        if (bugetNou < 0)
                            eroare += "Bugetul trebuie sa fie pozitiv.\n";
                        else if (bugetNou != modificat.BugetSectie)
                        {
                            modificat.BugetSectie = bugetNou;
                            modificatMacarUnCamp = true;
                        }
                    }

                    foreach (var ctrl in Controls)
                    {
                        if (ctrl is RadioButton rb && rb.Checked)
                        {
                            if (Enum.TryParse(rb.Text, out StatusFunctionareSectie statusNou) && statusNou != modificat.Status)
                            {
                                modificat.Status = statusNou;
                                modificatMacarUnCamp = true;
                            }
                        }
                    }

                    DotariSectie dotariNou = DotariSectie.Nespecificat;
                    foreach (var ctrl in Controls)
                    {
                        if (ctrl is CheckBox cb && cb.Checked && Enum.TryParse(cb.Text, out DotariSectie dotare))
                        {
                            dotariNou |= dotare;
                        }
                    }
                    if (dotariNou != DotariSectie.Nespecificat && dotariNou != modificat.DotariSec)
                    {
                        modificat.DotariSec = dotariNou;
                        modificatMacarUnCamp = true;
                    }

                    if (!string.IsNullOrEmpty(eroare))
                    {
                        MessageBox.Show(eroare, "Date invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!modificatMacarUnCamp)
                    {
                        MessageBox.Show("Trebuie să modificați cel puțin un câmp!", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    sectii[i] = modificat;
                    File.WriteAllLines(caleFisier, sectii.Select(s => s.ConversieLaSir_PentruFisier()));

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

                    MessageBox.Show("Sectia a fost modificata cu succes.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (!gasit)
            {
                MessageBox.Show("Nu exista o sectie cu acest cod.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
