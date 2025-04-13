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
    public partial class Cautare_Dupa_ID : MetroForm
    {
        public Cautare_Dupa_ID()
        {
            InitializeComponent();
            this.Text = "Cautare dupa ID";

            this.Theme = MetroThemeStyle.Light;
            this.Style = MetroColorStyle.Teal;

            this.BackColor = Color.FromArgb(240, 248, 255);
            this.Size = new Size(1000, 700);

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Cautare_Dupa_ID_Load(object sender, EventArgs e)
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
            // ID
            string id = metroTextBox1.Text.Trim();
            if (string.IsNullOrEmpty(id))
            {
                eroare += "Introdu un ID corespunzator!\n";

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


            string numeFisierSectii = ConfigurationManager.AppSettings["NumeFisierSectii"];
            string caleCompletaFisierSectii = locatieFisierSolutie + "\\" + numeFisierSectii;

            Sectii_FISIERTEXT adminSectii = new Sectii_FISIERTEXT(caleCompletaFisierSectii);
            SectieSpital sectieGasita = adminSectii.FindSectie(id);
            if (sectieGasita != null)
            {
                MessageBox.Show(
                    $"Sectie gasita:\n\n" +
                    $"Cod Sectie: {sectieGasita.CodSectie}\n" +
                    $"Nume: {sectieGasita.NumeSectie}\n" +
                    $"Etaj: {sectieGasita.Etaj}\n" +
                    $"Capacitate Maxima: {sectieGasita.CapacitateMaxima}\n" +
                    $"Nr. Pacienti Internati: {sectieGasita.NrPacientiInternati}\n" +
                    $"Temperatura Mediu: {sectieGasita.TemperaturaMediu}\n" +
                    $"Suprafata: {sectieGasita.SuprafataSectie}\n" +
                    $"Buget: {sectieGasita.BugetSectie}\n" +
                    $"Status Functionare: {sectieGasita.Status}\n" +
                    $"Dotari: {sectieGasita.DotariSec}",
                    "Sectie gasita",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                MessageBox.Show(
                    "Nu a fost gasita nicio sectie cu ID-ul introdus.",
                    "Sectie inexistenta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

        }
    }
}
