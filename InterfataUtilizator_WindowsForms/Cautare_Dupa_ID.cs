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
using System.Windows.Forms.VisualStyles;

namespace InterfataUtilizator_WindowsForms
{
    public partial class Cautare_Dupa_ID : MetroForm
    {
        
        private DataGridView rezultateSectie;
        public Cautare_Dupa_ID()
        {
            InitializeComponent();
            this.Text = "Cautare dupa nume";

            this.Theme = MetroThemeStyle.Light;
            this.Style = MetroColorStyle.Teal;

            this.BackColor = Color.FromArgb(240, 248, 255);
            this.Size = new Size(1000, 700);

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Cautare_Dupa_ID_Load(object sender, EventArgs e)
        {
            rezultateSectie = new DataGridView();
            rezultateSectie.Name = "rezultateSectie";
            rezultateSectie.Location = new Point(30, 150);
            rezultateSectie.Size = new Size(900, 500);
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




        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string nume = metroTextBox1.Text.Trim();
            metroTextBox1.Style = MetroColorStyle.Blue;
            metroTextBox1.UseStyleColors = true;

            if(string.IsNullOrEmpty(nume))
            {
                MessageBox.Show("Introduceti un nume de sectie!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                metroTextBox1.Style = MetroColorStyle.Red;
                metroTextBox1.UseStyleColors = true;
                return;
            }

            string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string numeFisierSectii = ConfigurationManager.AppSettings["NumeFisierSectii"];
            string caleCompletaFisierSectii = Path.Combine(locatieFisierSolutie, numeFisierSectii);

            Sectii_FISIERTEXT adminSectii = new Sectii_FISIERTEXT(caleCompletaFisierSectii);
            List<SectieSpital> rezultate = adminSectii.FindSectieDupaNume(nume);

            rezultateSectie.Rows.Clear();

            if (rezultate.Count > 0)
            {
                foreach (SectieSpital sectie in rezultate)
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
            } else
            {
                MessageBox.Show("Nu s-a gasit nicio sectie cu acest nume!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                metroTextBox1.Style = MetroColorStyle.Red;
                metroTextBox1.UseStyleColors = true;
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
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

        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Modifica_Click(object sender, EventArgs e)
        {
            if(rezultateSectie.SelectedRows.Count ==0 )
            {
                MessageBox.Show("Selecteaza o sectie din tabel!","Eroare" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataGridViewRow rand = rezultateSectie.SelectedRows[0];

            SectieSpital sectie = new SectieSpital
            {
                CodSectie = Convert.ToInt32(rand.Cells["CodSectie"].Value),
                NumeSectie = rand.Cells["NumeSectie"].Value.ToString(),
                Etaj = Convert.ToInt32(rand.Cells["Etaj"].Value),
                CapacitateMaxima = Convert.ToInt32(rand.Cells["CapacitateMaxima"].Value),
                NrPacientiInternati = Convert.ToInt32(rand.Cells["NrPacientiInternati"].Value),
                TemperaturaMediu = Convert.ToDouble(rand.Cells["TemperaturaMediu"].Value),
                SuprafataSectie = Convert.ToDouble(rand.Cells["SuprafataSectie"].Value),
                BugetSectie = Convert.ToDouble(rand.Cells["BugetSectie"].Value),
                Status = (StatusFunctionareSectie)Enum.Parse(typeof(StatusFunctionareSectie), rand.Cells["Status"].Value.ToString()),
                DotariSec = (DotariSectie)Enum.Parse(typeof(DotariSectie), rand.Cells["Dotari"].Value.ToString())
            };

            ModificaSectie modifica = new ModificaSectie();
            modifica.SeteazaSectie(sectie);
            modifica.Show();



        }
    }
}
