using System;
using System.Collections.Generic;
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
    public partial class Sectii_Spital : MetroForm
    {
        //button inapoi
        private MetroButton btnInapoi;

        //cauta dupa id
        private MetroButton btnSearchID;

        //refresh date
        private MetroButton btnRefreshDate;

        //modifica sectie aleasa
        private MetroButton btnModifySectie;

        //SECTII
        Sectii_FISIERTEXT adminSectii;

       // private MetroLabel lblTitluSectii;

        //buton adauga sectie
        private MetroButton btnAdaugaSectii;

        //afiseaza doar numele la etichete
        private MetroLabel lblCodSectie;
        private MetroLabel lblNumeSectie;
        private MetroLabel lblEtaj;
        private MetroLabel lblCapacitateMaxima;
        private MetroLabel lblNrPacientiInternati;
        private MetroLabel lblTemperaturaMediu;
        private MetroLabel lblSuprafataSectie;
        private MetroLabel lblBugetSectie;

        private MetroLabel lblStatusFunctionare;
        private MetroLabel lblDotari;

        //stocam datele pe care le afisam
        private MetroLabel[] lblsCodSectie;
        private MetroLabel[] lblsNumeSectie;
        private MetroLabel[] lblsEtaj;
        private MetroLabel[] lblsCapacitateMaxima;
        private MetroLabel[] lblsNrPacientiInternati;
        private MetroLabel[] lblsTemperaturaMediu;
        private MetroLabel[] lblsSuprafataSectie;
        private MetroLabel[] lblsBugetSectie;

        private MetroLabel[] lblsStatusFunctionare;
        private MetroLabel[] lblsDotari;

        //private MetroTextBox txtNumeSectie;
        //private MetroTextBox txtEtaj;
        //private MetroTextBox txtCapacitateMaxima;
        //private MetroTextBox txtNrPacientiInternati;
        //private MetroTextBox txtTemperaturaMediu;
        //private MetroTextBox txtSuprafataSectie;
        //private MetroTextBox txtBugetSectie;

        //private ComboBox txtStatusFunctionare;
        //private ListBox txtDotari;

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


        //dimensiuni pt pacienti si pentru sectii
        private const int LATIME_CONTROL = 120; //latimea pentru fiecare coloana , latime de 200 pt nume , prenume
        private const int DIMENSIUNE_PAS_Y = 50; // distanta dintre pacienti
        private const int DIMENSIUNE_PAS_X = 135; //distanta dintre coloane gen distanta dintre nume si prenume sa fie de 200px

        public Sectii_Spital()
        {

            InitializeComponent();

            // adaugam MetroStyleManager
            this.StyleManager = new MetroStyleManager();
            this.StyleManager.Theme = MetroThemeStyle.Light;
            this.StyleManager.Style = MetroColorStyle.Blue;

            InitializareInterfata();
        }



        private void Sectii_Spital_Load(object sender, EventArgs e)
        {
            AfiseazaSectii();
        }

        private void InitializareInterfata()
        {
            string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            //linia de sus ia locatia folder-ului principal al solutiei


            string numeFisierSectii = ConfigurationManager.AppSettings["NumeFisierSectii"];
            string caleCompletaFisierSectii = locatieFisierSolutie + "\\" + numeFisierSectii;
            //MessageBox.Show("CALE SECTII: " + caleCompletaFisierSectii);

            adminSectii = new Sectii_FISIERTEXT(caleCompletaFisierSectii);


            int nrSectii = 0;
            SectieSpital[] sectii = adminSectii.GetSectie(out nrSectii);


            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.StartPosition = FormStartPosition.Manual;
            //this.Location = new Point(50, 50);
            this.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.ForeColor = Color.DarkSlateGray;
            this.Text = "Secții";
            this.BackColor = Color.FromArgb(230, 245, 255);

            //MetroButton refresh
            btnRefreshDate = new MetroButton();
            btnRefreshDate.Width = LATIME_CONTROL;
            btnRefreshDate.Text = "Refresh";
            btnRefreshDate.Left = 0 * DIMENSIUNE_PAS_X;
            btnRefreshDate.Top = 3 * DIMENSIUNE_PAS_Y;
            btnRefreshDate.AutoSize = true;
            btnRefreshDate.Click += BtnRefreshDate_Click;
            this.Controls.Add(btnRefreshDate);


            btnSearchID = new MetroButton();
            btnSearchID.Width = LATIME_CONTROL;
            btnSearchID.Text = "Cautare";
            btnSearchID.Left = 0;
            btnSearchID.Top = 4 * DIMENSIUNE_PAS_Y;
            btnSearchID.AutoSize = true;
            btnSearchID.Click += BtnSearchID_Click;
            this.Controls.Add(btnSearchID);


            

            btnModifySectie = new MetroButton();
            btnModifySectie.Width = LATIME_CONTROL;
            btnModifySectie.Text = "Modifica";
            btnModifySectie.Left = 0;
            btnModifySectie.Top = 5 * DIMENSIUNE_PAS_Y;
            btnModifySectie.AutoSize = true;
            btnModifySectie.Click += BtnModifySectie_Click;
            this.Controls.Add(btnModifySectie);


            btnInapoi = new MetroButton();
            btnInapoi.Width = LATIME_CONTROL;
            btnInapoi.Text = "Inapoi";
            btnInapoi.Left = 0;
            btnInapoi.Top = 6 * DIMENSIUNE_PAS_Y;
            btnInapoi.AutoSize = true;
            btnInapoi.Click += BtnInapoi_Click;
            this.Controls.Add(btnInapoi);

            //metrostyle pt buton refresh
            btnRefreshDate.Theme = MetroThemeStyle.Light;
            btnRefreshDate.Style = MetroColorStyle.Blue;

            

            //SECTIILE

            //lblTitluSectii = new MetroLabel();
            //lblTitluSectii.Text = "Secții Spital:";
            //lblTitluSectii.Left = 0;
            //lblTitluSectii.AutoSize = true;
            //lblTitluSectii.Top = axaY * DIMENSIUNE_PAS_Y;
            //lblTitluSectii.Theme = MetroThemeStyle.Light;
            //lblTitluSectii.Style = MetroColorStyle.Blue;
            //this.Controls.Add(lblTitluSectii);

            btnAdaugaSectii = new MetroButton();
            btnAdaugaSectii.Width = LATIME_CONTROL;
            btnAdaugaSectii.Text = "Adauga";
            btnAdaugaSectii.Left = 0;
            btnAdaugaSectii.Top = 2 * DIMENSIUNE_PAS_Y;
            btnAdaugaSectii.AutoSize = true;
            btnAdaugaSectii.Click += BtnAdaugaSectii_Click;
            btnAdaugaSectii.Theme = MetroThemeStyle.Light;
            btnAdaugaSectii.Style = MetroColorStyle.Blue;
            this.Controls.Add(btnAdaugaSectii);


            // Etichete pentru sectii
            lblCodSectie = new MetroLabel();
            lblCodSectie.Width = LATIME_CONTROL;
            lblCodSectie.Text = "Cod";
            lblCodSectie.Left = 1 * DIMENSIUNE_PAS_X;
            lblCodSectie.Top = 1 * DIMENSIUNE_PAS_Y;
            lblCodSectie.Theme = MetroThemeStyle.Light;
            lblCodSectie.Style = MetroColorStyle.Blue;
            this.Controls.Add(lblCodSectie);

            lblNumeSectie = new MetroLabel();
            lblNumeSectie.Width = LATIME_CONTROL;
            lblNumeSectie.Text = "Nume";
            lblNumeSectie.Left = 2 * DIMENSIUNE_PAS_X;
            lblNumeSectie.Top = 1 * DIMENSIUNE_PAS_Y;
            lblNumeSectie.Theme = MetroThemeStyle.Light;
            lblNumeSectie.Style = MetroColorStyle.Blue;
            this.Controls.Add(lblNumeSectie);

            lblEtaj = new MetroLabel();
            lblEtaj.Width = LATIME_CONTROL;
            lblEtaj.Text = "Etaj";
            lblEtaj.Left = 3 * DIMENSIUNE_PAS_X;
            lblEtaj.Top = 1 * DIMENSIUNE_PAS_Y;
            lblEtaj.Theme = MetroThemeStyle.Light;
            lblEtaj.Style = MetroColorStyle.Blue;
            this.Controls.Add(lblEtaj);

            lblCapacitateMaxima = new MetroLabel();
            lblCapacitateMaxima.Width = LATIME_CONTROL;
            lblCapacitateMaxima.Text = "Capacitate";
            lblCapacitateMaxima.Left = 4 * DIMENSIUNE_PAS_X;
            lblCapacitateMaxima.Top = 1 * DIMENSIUNE_PAS_Y;
            lblCapacitateMaxima.Theme = MetroThemeStyle.Light;
            lblCapacitateMaxima.Style = MetroColorStyle.Blue;
            this.Controls.Add(lblCapacitateMaxima);

            lblNrPacientiInternati = new MetroLabel();
            lblNrPacientiInternati.Width = LATIME_CONTROL;
            lblNrPacientiInternati.Text = "NrPacienti";
            lblNrPacientiInternati.Left = 5 * DIMENSIUNE_PAS_X;
            lblNrPacientiInternati.Top = 1 * DIMENSIUNE_PAS_Y;
            lblNrPacientiInternati.Theme = MetroThemeStyle.Light;
            lblNrPacientiInternati.Style = MetroColorStyle.Blue;
            this.Controls.Add(lblNrPacientiInternati);

            lblTemperaturaMediu = new MetroLabel();
            lblTemperaturaMediu.Width = LATIME_CONTROL;
            lblTemperaturaMediu.Text = "Temperatura";
            lblTemperaturaMediu.Left = 6 * DIMENSIUNE_PAS_X;
            lblTemperaturaMediu.Top = 1 * DIMENSIUNE_PAS_Y;
            lblTemperaturaMediu.Theme = MetroThemeStyle.Light;
            lblTemperaturaMediu.Style = MetroColorStyle.Blue;
            this.Controls.Add(lblTemperaturaMediu);

            lblSuprafataSectie = new MetroLabel();
            lblSuprafataSectie.Width = LATIME_CONTROL;
            lblSuprafataSectie.Text = "Suprafata";
            lblSuprafataSectie.Left = 7 * DIMENSIUNE_PAS_X;
            lblSuprafataSectie.Top = 1 * DIMENSIUNE_PAS_Y;
            lblSuprafataSectie.Theme = MetroThemeStyle.Light;
            lblSuprafataSectie.Style = MetroColorStyle.Blue;
            this.Controls.Add(lblSuprafataSectie);

            lblBugetSectie = new MetroLabel();
            lblBugetSectie.Width = LATIME_CONTROL;
            lblBugetSectie.Text = "Buget";
            lblBugetSectie.Left = 8 * DIMENSIUNE_PAS_X;
            lblBugetSectie.Top = 1 * DIMENSIUNE_PAS_Y;
            lblBugetSectie.Theme = MetroThemeStyle.Light;
            lblBugetSectie.Style = MetroColorStyle.Blue;
            this.Controls.Add(lblBugetSectie);

            lblStatusFunctionare = new MetroLabel();
            lblStatusFunctionare.Width = LATIME_CONTROL;
            lblStatusFunctionare.Text = "Status";
            lblStatusFunctionare.Left = 9 * DIMENSIUNE_PAS_X;
            lblStatusFunctionare.Top = 1 * DIMENSIUNE_PAS_Y;
            lblStatusFunctionare.Theme = MetroThemeStyle.Light;
            lblStatusFunctionare.Style = MetroColorStyle.Blue;
            this.Controls.Add(lblStatusFunctionare);

            lblDotari = new MetroLabel();
            lblDotari.Width = LATIME_CONTROL;
            lblDotari.Text = "Dotari";
            lblDotari.Left = 10 * DIMENSIUNE_PAS_X;
            lblDotari.Top = 1 * DIMENSIUNE_PAS_Y;
            lblDotari.Theme = MetroThemeStyle.Light;
            lblDotari.Style = MetroColorStyle.Blue;
            this.Controls.Add(lblDotari);

            // Controale pentru sectii

            //txtNumeSectie = new MetroTextBox();
            //txtNumeSectie.Left = 2 * DIMENSIUNE_PAS_X;
            //txtNumeSectie.Top = 2 * DIMENSIUNE_PAS_Y;
            //txtNumeSectie.Width = LATIME_CONTROL;
            //txtNumeSectie.Theme = MetroThemeStyle.Light;
            //txtNumeSectie.Style = MetroColorStyle.Blue;
            //txtNumeSectie.UseStyleColors = true;
            //this.Controls.Add(txtNumeSectie);

            //txtEtaj = new MetroTextBox();
            //txtEtaj.Left = 3 * DIMENSIUNE_PAS_X;
            //txtEtaj.Top = 2 * DIMENSIUNE_PAS_Y;
            //txtEtaj.Width = LATIME_CONTROL;
            //txtEtaj.Theme = MetroThemeStyle.Light;
            //txtEtaj.Style = MetroColorStyle.Blue;
            //txtEtaj.UseStyleColors = true;
            //this.Controls.Add(txtEtaj);

            //txtCapacitateMaxima = new MetroTextBox();
            //txtCapacitateMaxima.Left = 4 * DIMENSIUNE_PAS_X;
            //txtCapacitateMaxima.Top = 2 * DIMENSIUNE_PAS_Y;
            //txtCapacitateMaxima.Width = LATIME_CONTROL;
            //txtCapacitateMaxima.Theme = MetroThemeStyle.Light;
            //txtCapacitateMaxima.Style = MetroColorStyle.Blue;
            //txtCapacitateMaxima.UseStyleColors = true;
            //this.Controls.Add(txtCapacitateMaxima);

            //txtNrPacientiInternati = new MetroTextBox();
            //txtNrPacientiInternati.Left = 5 * DIMENSIUNE_PAS_X;
            //txtNrPacientiInternati.Top = 2 * DIMENSIUNE_PAS_Y;
            //txtNrPacientiInternati.Width = LATIME_CONTROL;
            //txtNrPacientiInternati.Theme = MetroThemeStyle.Light;
            //txtNrPacientiInternati.Style = MetroColorStyle.Blue;
            //txtNrPacientiInternati.UseStyleColors = true;
            //this.Controls.Add(txtNrPacientiInternati);

            //txtTemperaturaMediu = new MetroTextBox();
            //txtTemperaturaMediu.Left = 6 * DIMENSIUNE_PAS_X;
            //txtTemperaturaMediu.Top = 2 * DIMENSIUNE_PAS_Y;
            //txtTemperaturaMediu.Width = LATIME_CONTROL;
            //txtTemperaturaMediu.Theme = MetroThemeStyle.Light;
            //txtTemperaturaMediu.Style = MetroColorStyle.Blue;
            //txtTemperaturaMediu.UseStyleColors = true;
            //this.Controls.Add(txtTemperaturaMediu);

            //txtSuprafataSectie = new MetroTextBox();
            //txtSuprafataSectie.Left = 7 * DIMENSIUNE_PAS_X;
            //txtSuprafataSectie.Top = 2 * DIMENSIUNE_PAS_Y;
            //txtSuprafataSectie.Width = LATIME_CONTROL;
            //txtSuprafataSectie.Theme = MetroThemeStyle.Light;
            //txtSuprafataSectie.Style = MetroColorStyle.Blue;
            //txtSuprafataSectie.UseStyleColors = true;
            //this.Controls.Add(txtSuprafataSectie);

            //txtBugetSectie = new MetroTextBox();
            //txtBugetSectie.Left = 8 * DIMENSIUNE_PAS_X;
            //txtBugetSectie.Top = 2 * DIMENSIUNE_PAS_Y;
            //txtBugetSectie.Width = LATIME_CONTROL;
            //txtBugetSectie.Theme = MetroThemeStyle.Light;
            //txtBugetSectie.Style = MetroColorStyle.Blue;
            //txtBugetSectie.UseStyleColors = true;
            //this.Controls.Add(txtBugetSectie);

            //txtStatusFunctionare = new ComboBox();
            //txtStatusFunctionare.Left = 9 * DIMENSIUNE_PAS_X;
            //txtStatusFunctionare.Top = 2 * DIMENSIUNE_PAS_Y;
            //txtStatusFunctionare.Width = LATIME_CONTROL;
            //txtStatusFunctionare.Items.AddRange(Enum.GetNames(typeof(StatusFunctionareSectie)));
            //this.Controls.Add(txtStatusFunctionare);


            //txtDotari = new ListBox();
            //txtDotari.Left = 10 * DIMENSIUNE_PAS_X;
            //txtDotari.Top = 2 * DIMENSIUNE_PAS_Y;
            //txtDotari.Width = LATIME_CONTROL;
            //txtDotari.Height = 3 * DIMENSIUNE_PAS_Y;
            //txtDotari.SelectionMode = SelectionMode.MultiSimple;
            //txtDotari.Items.AddRange(Enum.GetNames(typeof(DotariSectie)));
            //this.Controls.Add(txtDotari);


        }





        private void BtnRefreshDate_Click(object sender, EventArgs e)
        {

            AfiseazaSectii();

        }

        private void BtnSearchID_Click(object obj, EventArgs e)
        {
            Cautare_Dupa_ID cautatorID = new Cautare_Dupa_ID();
            cautatorID.Show();
        }

        private void BtnModifySectie_Click(object sender, EventArgs e)
        {
            ModificaSectie modificatorSectie=new ModificaSectie();
            modificatorSectie.Show();
        }


        private void BtnInapoi_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form mainMenu = Application.OpenForms["HospitalMenu"];

            if (mainMenu != null)
            {
                mainMenu.Show(); 
            }
            else
            {
                //meniu nou doar daca nu exista
                HospitalMenu meniuNou = new HospitalMenu(); 
                meniuNou.Show();
            }

            this.Close();
        }

        private void BtnAdaugaSectii_Click(object sender, EventArgs e)
        {
            AdaugaSectie adaugatorSectie = new AdaugaSectie();
            adaugatorSectie.Show();
        }

        //private void BtnAdaugaSectii_Click(object sender, EventArgs e)
        //{
        //    string eroare = "";

        //    lblNumeSectie.Style = MetroColorStyle.Blue;
        //    lblNumeSectie.UseStyleColors = true;

        //    lblEtaj.Style = MetroColorStyle.Blue;
        //    lblEtaj.UseStyleColors = true;

        //    lblCapacitateMaxima.Style = MetroColorStyle.Blue;
        //    lblCapacitateMaxima.UseStyleColors = true;

        //    lblNrPacientiInternati.Style = MetroColorStyle.Blue;
        //    lblNrPacientiInternati.UseStyleColors = true;

        //    lblTemperaturaMediu.Style = MetroColorStyle.Blue;
        //    lblTemperaturaMediu.UseStyleColors = true;

        //    lblSuprafataSectie.Style = MetroColorStyle.Blue;
        //    lblSuprafataSectie.UseStyleColors = true;

        //    lblBugetSectie.Style = MetroColorStyle.Blue;
        //    lblBugetSectie.UseStyleColors = true;

        //    lblStatusFunctionare.Style = MetroColorStyle.Blue;
        //    lblStatusFunctionare.UseStyleColors = true;

        //    lblDotari.Style = MetroColorStyle.Blue;
        //    lblDotari.UseStyleColors = true;


        //    // Nume sectie
        //    string numeSectie = txtNumeSectie.Text.Trim();
        //    if (numeSectie.Length < LUNGIME_MIN_NUME_SECTIE || numeSectie.Length > LUNGIME_MAX_NUME_SECTIE)
        //    {
        //        eroare += $"Numele sectiei trebuie sa aiba intre {LUNGIME_MIN_NUME_SECTIE} si {LUNGIME_MAX_NUME_SECTIE} caractere.\n";

        //        lblNumeSectie.Style = MetroColorStyle.Red;
        //        lblNumeSectie.UseStyleColors = true;
        //    }


        //    // Etaj
        //    int etaj;
        //    if (!int.TryParse(txtEtaj.Text.Trim(), out etaj) || etaj < ETAJ_MIN || etaj > ETAJ_MAX)
        //    {
        //        eroare += $"Etajul trebuie sa fie intre {ETAJ_MIN} si {ETAJ_MAX}.\n";

        //        lblEtaj.Style = MetroColorStyle.Red;
        //        lblEtaj.UseStyleColors = true;
        //    }


        //    // Capacitate
        //    int capacitate;
        //    if (!int.TryParse(txtCapacitateMaxima.Text.Trim(), out capacitate) || capacitate < CAPACITATE_MIN || capacitate > CAPACITATE_MAX)
        //    {
        //        eroare += $"Capacitatea trebuie sa fie intre {CAPACITATE_MIN} si {CAPACITATE_MAX}.\n";

        //        lblCapacitateMaxima.Style = MetroColorStyle.Red;
        //        lblCapacitateMaxima.UseStyleColors = true;
        //    }


        //    // Numar pacienti
        //    int nrPacienti;
        //    if (!int.TryParse(txtNrPacientiInternati.Text.Trim(), out nrPacienti) || nrPacienti < NR_PACIENTI_MIN || nrPacienti > capacitate)
        //    {
        //        eroare += $"Numarul de pacienti trebuie sa fie intre {NR_PACIENTI_MIN} si capacitatea maxima ({capacitate}).\n";

        //        lblNrPacientiInternati.Style = MetroColorStyle.Red;
        //        lblNrPacientiInternati.UseStyleColors = true;
        //    }


        //    // Temperatura
        //    double temperatura;
        //    if (!double.TryParse(txtTemperaturaMediu.Text.Trim(), out temperatura) || temperatura < TEMPERATURA_MIN || temperatura > TEMPERATURA_MAX)
        //    {
        //        eroare += $"Temperatura trebuie sa fie intre {TEMPERATURA_MIN} si {TEMPERATURA_MAX} grade.\n";

        //        lblTemperaturaMediu.Style = MetroColorStyle.Red;
        //        lblTemperaturaMediu.UseStyleColors = true;
        //    }


        //    // Suprafata
        //    double suprafata;
        //    if (!double.TryParse(txtSuprafataSectie.Text.Trim(), out suprafata) || suprafata < SUPRAFATA_MIN || suprafata > SUPRAFATA_MAX)
        //    {
        //        eroare += $"Suprafata trebuie sa fie intre {SUPRAFATA_MIN} si {SUPRAFATA_MAX} mp.\n";

        //        lblSuprafataSectie.Style = MetroColorStyle.Red;
        //        lblSuprafataSectie.UseStyleColors = true;
        //    }


        //    // Buget
        //    double buget;
        //    if (!double.TryParse(txtBugetSectie.Text.Trim(), out buget) || buget < BUGET_MIN)
        //    {
        //        eroare += "Bugetul trebuie sa fie pozitiv.\n";

        //        lblBugetSectie.Style = MetroColorStyle.Red;
        //        lblBugetSectie.UseStyleColors = true;
        //    }


        //    // Status Functionare
        //    StatusFunctionareSectie status = StatusFunctionareSectie.Nespecificat;
        //    if (!Enum.TryParse(txtStatusFunctionare.Text, out status))
        //    {
        //        eroare += "Selectati statusul sectiei.\n";
        //        lblStatusFunctionare.Style = MetroColorStyle.Red;
        //        lblStatusFunctionare.UseStyleColors = true;
        //    }



        //    DotariSectie dotari = DotariSectie.Nespecificat;
        //    if (txtDotari.SelectedItems.Count == 0)
        //    {
        //        eroare += "Selecteaza cel putin o dotare pentru sectie.\n";
        //        lblDotari.Style = MetroColorStyle.Red;
        //        lblDotari.UseStyleColors = true;
        //    }
        //    else
        //    {
        //        foreach (var item in txtDotari.SelectedItems)
        //        {
        //            if (Enum.TryParse(item.ToString(), out DotariSectie dot))
        //            {
        //                dotari |= dot;
        //            }
        //        }
        //    }



        //    if (eroare != "")
        //    {
        //        MessageBox.Show(eroare, "Date invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    int nrSectii = 0;
        //    SectieSpital[] sectii = adminSectii.GetSectie(out nrSectii);

        //    SectieSpital sectie = new SectieSpital(nrSectii + 1, numeSectie, etaj, capacitate, nrPacienti, temperatura, suprafata, buget, status, dotari);
        //    adminSectii.AddSectii(sectie);

        //    txtNumeSectie.ResetText();
        //    txtEtaj.ResetText();
        //    txtCapacitateMaxima.ResetText();
        //    txtNrPacientiInternati.ResetText();
        //    txtTemperaturaMediu.ResetText();
        //    txtSuprafataSectie.ResetText();
        //    txtBugetSectie.ResetText();
        //    txtStatusFunctionare.ResetText();
        //    txtDotari.ClearSelected();

        //}

        private void StergeEticheteSectii()
        {
            void RemoveLabels(MetroLabel[] labels)
            {
                if (labels != null)
                {
                    foreach (var lbl in labels)
                    {
                        if (lbl != null)
                            this.Controls.Remove(lbl);
                    }
                }
            }

            RemoveLabels(lblsCodSectie);
            RemoveLabels(lblsNumeSectie);
            RemoveLabels(lblsEtaj);
            RemoveLabels(lblsCapacitateMaxima);
            RemoveLabels(lblsNrPacientiInternati);
            RemoveLabels(lblsTemperaturaMediu);
            RemoveLabels(lblsSuprafataSectie);
            RemoveLabels(lblsBugetSectie);
            RemoveLabels(lblsStatusFunctionare);
            RemoveLabels(lblsDotari);
        }



        private void AfiseazaSectii()
        {
            StergeEticheteSectii();
            SectieSpital[] sectii = adminSectii.GetSectie(out int nrSectii);



            lblsCodSectie = new MetroLabel[nrSectii];
            lblsNumeSectie = new MetroLabel[nrSectii];
            lblsEtaj = new MetroLabel[nrSectii];
            lblsCapacitateMaxima = new MetroLabel[nrSectii];
            lblsNrPacientiInternati = new MetroLabel[nrSectii];
            lblsTemperaturaMediu = new MetroLabel[nrSectii];
            lblsSuprafataSectie = new MetroLabel[nrSectii];
            lblsBugetSectie = new MetroLabel[nrSectii];
            lblsStatusFunctionare = new MetroLabel[nrSectii];
            lblsDotari = new MetroLabel[nrSectii];

            int i = 0;
            int axaY = 2;
            foreach (SectieSpital sectieSpital in sectii)
            {
                lblsCodSectie[i] = new MetroLabel();
                lblsCodSectie[i].Width = LATIME_CONTROL;
                lblsCodSectie[i].Text = sectieSpital.CodSectie.ToString();
                lblsCodSectie[i].Left = 1 * DIMENSIUNE_PAS_X;
                lblsCodSectie[i].Top = (i + axaY) * DIMENSIUNE_PAS_Y;
                lblsCodSectie[i].BackColor = Color.White;
                //   lblsCodSectie[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsCodSectie[i]);

                lblsNumeSectie[i] = new MetroLabel();
                lblsNumeSectie[i].Width = LATIME_CONTROL;
                lblsNumeSectie[i].Text = sectieSpital.NumeSectie;
                lblsNumeSectie[i].Left = 2 * DIMENSIUNE_PAS_X;
                lblsNumeSectie[i].Top = (i + axaY) * DIMENSIUNE_PAS_Y;
                lblsNumeSectie[i].BackColor = Color.White;
                //   lblsNumeSectie[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsNumeSectie[i]);


                lblsEtaj[i] = new MetroLabel();
                lblsEtaj[i].Width = LATIME_CONTROL;
                lblsEtaj[i].Text = sectieSpital.Etaj.ToString();
                lblsEtaj[i].Left = 3 * DIMENSIUNE_PAS_X;
                lblsEtaj[i].Top = (i + axaY) * DIMENSIUNE_PAS_Y;
                lblsEtaj[i].BackColor = Color.White;
                //   lblsEtaj[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsEtaj[i]);

                lblsCapacitateMaxima[i] = new MetroLabel();
                lblsCapacitateMaxima[i].Width = LATIME_CONTROL;
                lblsCapacitateMaxima[i].Text = sectieSpital.CapacitateMaxima.ToString();
                lblsCapacitateMaxima[i].Left = 4 * DIMENSIUNE_PAS_X;
                lblsCapacitateMaxima[i].Top = (i + axaY) * DIMENSIUNE_PAS_Y;
                lblsCapacitateMaxima[i].BackColor = Color.White;
                //   lblsCapacitateMaxima[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsCapacitateMaxima[i]);


                lblsNrPacientiInternati[i] = new MetroLabel();
                lblsNrPacientiInternati[i].Width = LATIME_CONTROL;
                lblsNrPacientiInternati[i].Text = sectieSpital.NrPacientiInternati.ToString();
                lblsNrPacientiInternati[i].Left = 5 * DIMENSIUNE_PAS_X;
                lblsNrPacientiInternati[i].Top = (i + axaY) * DIMENSIUNE_PAS_Y;
                lblsNrPacientiInternati[i].BackColor = Color.White;
                //  lblsNrPacientiInternati[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsNrPacientiInternati[i]);

                lblsTemperaturaMediu[i] = new MetroLabel();
                lblsTemperaturaMediu[i].Width = LATIME_CONTROL;
                lblsTemperaturaMediu[i].Text = sectieSpital.TemperaturaMediu.ToString();
                lblsTemperaturaMediu[i].Left = 6 * DIMENSIUNE_PAS_X;
                lblsTemperaturaMediu[i].Top = (i + axaY) * DIMENSIUNE_PAS_Y;
                lblsTemperaturaMediu[i].BackColor = Color.White;
                //   lblsTemperaturaMediu[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsTemperaturaMediu[i]);


                lblsSuprafataSectie[i] = new MetroLabel();
                lblsSuprafataSectie[i].Width = LATIME_CONTROL;
                lblsSuprafataSectie[i].Text = sectieSpital.SuprafataSectie.ToString();
                lblsSuprafataSectie[i].Left = 7 * DIMENSIUNE_PAS_X;
                lblsSuprafataSectie[i].Top = (i + axaY) * DIMENSIUNE_PAS_Y;
                lblsSuprafataSectie[i].BackColor = Color.White;
                //    lblsSuprafataSectie[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsSuprafataSectie[i]);


                lblsBugetSectie[i] = new MetroLabel();
                lblsBugetSectie[i].Width = LATIME_CONTROL;
                lblsBugetSectie[i].Text = sectieSpital.BugetSectie.ToString();
                lblsBugetSectie[i].Left = 8 * DIMENSIUNE_PAS_X;
                lblsBugetSectie[i].Top = (i + axaY) * DIMENSIUNE_PAS_Y;
                lblsBugetSectie[i].BackColor = Color.White;
                //    lblsBugetSectie[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsBugetSectie[i]);


                //lblsStatusFunctionare 
                // lblsDotari

                lblsStatusFunctionare[i] = new MetroLabel();
                lblsStatusFunctionare[i].Width = LATIME_CONTROL;
                lblsStatusFunctionare[i].Text = sectieSpital.Status.ToString();
                lblsStatusFunctionare[i].Left = 9 * DIMENSIUNE_PAS_X;
                lblsStatusFunctionare[i].Top = (i + axaY) * DIMENSIUNE_PAS_Y;
                lblsStatusFunctionare[i].BackColor = Color.White;
                //   lblsStatusFunctionare[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsStatusFunctionare[i]);


                lblsDotari[i] = new MetroLabel();
                lblsDotari[i].Width = LATIME_CONTROL;
                lblsDotari[i].Text = sectieSpital.DotariSec.ToString();
                lblsDotari[i].Left = 10 * DIMENSIUNE_PAS_X;
                lblsDotari[i].Top = (i + axaY) * DIMENSIUNE_PAS_Y;
                lblsDotari[i].BackColor = Color.White;
                //   lblsDotari[i].BorderStyle = BorderStyle.FixedSingle;
                lblsDotari[i].AutoSize = true;
                this.Controls.Add(lblsDotari[i]);

                i++;




            }
        }
    }
}


    

