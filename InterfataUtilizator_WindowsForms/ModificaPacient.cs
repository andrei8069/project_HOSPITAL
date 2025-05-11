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
        private const int VARSTA_MIN = 0;
        private const int VARSTA_MAX = 100;
        private const double GREUTATE_MIN = 2.0;
        private const double GREUTATE_MAX = 300.0;
        private const double INALTIME_MIN = 30.0;
        private const double INALTIME_MAX = 250.0;
        private const double TEMP_MIN = 30.0;
        private const double TEMP_MAX = 45.0;
        private const int CNP_LUNGIME = 13;
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
            int nrPacienti = pacienti.Count;


            bool gasit = false;
            for (int i = 0; i < nrPacienti; i++)
            {
                if (pacienti[i].CodPacient == codPacient)
                {
                    gasit = true;
                    Pacient modificat = new Pacient
                    {
                        CodPacient = pacienti[i].CodPacient,
                        Nume = pacienti[i].Nume,
                        Prenume = pacienti[i].Prenume,
                        Cnp = pacienti[i].Cnp,
                        Varsta = pacienti[i].Varsta,
                        Greutate = pacienti[i].Greutate,
                        Inaltime = pacienti[i].Inaltime,
                        TemperaturaCorp = pacienti[i].TemperaturaCorp,
                        Grupa = pacienti[i].Grupa,
                        AfectiuniMed = pacienti[i].AfectiuniMed
                    };

                    bool modificatMacarUnCamp = false;
                    string eroare = "";

                    if (!string.IsNullOrWhiteSpace(nume.Text) && nume.Text != modificat.Nume)
                    {
                        if (nume.Text.Length < 2)
                        {
                            eroare += "Numele trebuie sa aiba cel puțin 2 caractere.\n";
                        }
                        else
                        {
                            modificat.Nume = nume.Text;
                            modificatMacarUnCamp = true;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(prenume.Text) && prenume.Text != modificat.Prenume)
                    {
                        if (prenume.Text.Length < 2)
                        {
                            eroare += "Prenumele trebuie sa aiba cel puțin 2 caractere.\n";
                        }
                        else
                        {
                            modificat.Prenume = prenume.Text;
                            modificatMacarUnCamp = true;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(cnp.Text) && cnp.Text != modificat.Cnp)
                    {
                        if (cnp.Text.Length != CNP_LUNGIME)
                        {
                            eroare += "CNP-ul trebuie sa aiba 13 caractere.\n";
                        }
                        else
                        {
                            modificat.Cnp = cnp.Text;
                            modificatMacarUnCamp = true;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(varsta.Text) && int.TryParse(varsta.Text.Trim(), out int varstaNoua))
                    {
                        if (varstaNoua < VARSTA_MIN || varstaNoua > VARSTA_MAX)
                        {
                            eroare += $"Varsta trebuie sa fie intre {VARSTA_MIN} si {VARSTA_MAX}.\n";
                        }
                        else if (varstaNoua != modificat.Varsta)
                        {
                            modificat.Varsta = varstaNoua;
                            modificatMacarUnCamp = true;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(greutate.Text) && double.TryParse(greutate.Text.Trim(), out double greutateNoua))
                    {
                        if (greutateNoua < GREUTATE_MIN || greutateNoua > GREUTATE_MAX)
                        {
                            eroare += $"Greutatea trebuie sa fie intre {GREUTATE_MIN} si {GREUTATE_MAX}.\n";
                        }
                        else if (greutateNoua != modificat.Greutate)
                        {
                            modificat.Greutate = greutateNoua;
                            modificatMacarUnCamp = true;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(inaltime.Text) && double.TryParse(inaltime.Text.Trim(), out double inaltimeNoua))
                    {
                        if (inaltimeNoua < INALTIME_MIN || inaltimeNoua > INALTIME_MAX)
                        {
                            eroare += $"Înălțimea trebuie sa fie intre {INALTIME_MIN} si {INALTIME_MAX}.\n";
                        }
                        else if (inaltimeNoua != modificat.Inaltime)
                        {
                            modificat.Inaltime = inaltimeNoua;
                            modificatMacarUnCamp = true;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(temperatura.Text) && double.TryParse(temperatura.Text.Trim(), out double tempNoua))
                    {
                        if (tempNoua < TEMP_MIN || tempNoua > TEMP_MAX)
                        {
                            eroare += $"Temperatura trebuie sa fie intre {TEMP_MIN} si {TEMP_MAX}.\n";
                        }
                        else if (tempNoua != modificat.TemperaturaCorp)
                        {
                            modificat.TemperaturaCorp = tempNoua;
                            modificatMacarUnCamp = true;
                        }
                    }

                    foreach (var ctrl in Controls)
                    {
                        if (ctrl is RadioButton rb && rb.Checked)
                        {
                            if (Enum.TryParse(rb.Text, out GrupaSangePacient grupaNoua) && grupaNoua != modificat.Grupa)
                            {
                                modificat.Grupa = grupaNoua;
                                modificatMacarUnCamp = true;
                            }
                        }
                    }

                    AfectiuniMedicale afNou = AfectiuniMedicale.Nespecificat;
                    foreach (var ctrl in Controls)
                    {
                        if (ctrl is CheckBox cb && cb.Checked && Enum.TryParse(cb.Text, out AfectiuniMedicale af))
                        {
                            afNou |= af;
                        }
                    }
                    if (afNou != AfectiuniMedicale.Nespecificat && afNou != modificat.AfectiuniMed)
                    {
                        modificat.AfectiuniMed = afNou;
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

                    pacienti[i] = modificat;
                    File.WriteAllLines(caleFisier, pacienti.Select(p => p.ConversieLaSir_PentruFisier()));

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
                    return;
                }
            }

            if (!gasit)
            {
                MessageBox.Show("Nu există un pacient cu acest cod.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





    }
}
