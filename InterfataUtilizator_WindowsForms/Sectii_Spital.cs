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
        private Button btnInapoi;

        //cauta dupa id
        private Button btnSearchID;

        //refresh date
        private Button btnRefreshDate;

        //modifica sectie aleasa
        private Button btnModifySectie;

        //SECTII
        Sectii_FISIERTEXT adminSectii;

       // private MetroLabel lblTitluSectii;

        //buton adauga sectie
        private Button btnAdaugaSectii;

        //buton asociere pacient-sectie
        private Button btnAsocierePacientSectie;

        //buton sterge sectie
        private Button btnStergeSectie;



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

        private DataGridView dataGridViewSectii;
        public Sectii_Spital()
        {

            InitializeComponent();
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


            List<SectieSpital> sectii = adminSectii.GetSectii();
            int nrSectii = sectii.Count;

            this.Theme = MetroThemeStyle.Light;
            this.Style = MetroColorStyle.Default;

            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.StartPosition = FormStartPosition.Manual;
            //this.Location = new Point(50, 50);
            this.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.ForeColor = Color.DarkSlateGray;
            this.Text = "Secții";
            this.BackColor = Color.FromArgb(230, 245, 255);

            //Button refresh
            btnRefreshDate = new Button();
            btnRefreshDate.Width = LATIME_CONTROL;
            btnRefreshDate.Text = "Refresh";
            btnRefreshDate.Left = 0 * DIMENSIUNE_PAS_X;
            btnRefreshDate.Top = 3 * DIMENSIUNE_PAS_Y;
            btnRefreshDate.AutoSize = true;
            btnRefreshDate.BackColor = Color.SteelBlue;
            btnRefreshDate.ForeColor = Color.White;
            btnRefreshDate.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnRefreshDate.FlatStyle = FlatStyle.Flat;
            btnRefreshDate.FlatAppearance.BorderSize = 0;
            btnRefreshDate.Cursor = Cursors.Hand;
            btnRefreshDate.Click += BtnRefreshDate_Click;
            this.Controls.Add(btnRefreshDate);


            btnSearchID = new Button();
            btnSearchID.Width = LATIME_CONTROL;
            btnSearchID.Text = "Căutare";
            btnSearchID.Left = 0;
            btnSearchID.Top = 4 * DIMENSIUNE_PAS_Y;
            btnSearchID.AutoSize = true;
            btnSearchID.BackColor = Color.SteelBlue;
            btnSearchID.ForeColor = Color.White;
            btnSearchID.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnSearchID.FlatStyle = FlatStyle.Flat;
            btnSearchID.FlatAppearance.BorderSize = 0;
            btnSearchID.Cursor = Cursors.Hand;
            btnSearchID.Click += BtnSearchID_Click;
            this.Controls.Add(btnSearchID);


            

            btnModifySectie = new Button();
            btnModifySectie.Width = LATIME_CONTROL;
            btnModifySectie.Text = "Modifică";
            btnModifySectie.Left = 0;
            btnModifySectie.Top = 5 * DIMENSIUNE_PAS_Y;
            btnModifySectie.AutoSize = true;
            btnModifySectie.BackColor = Color.SteelBlue;
            btnModifySectie.ForeColor = Color.White;
            btnModifySectie.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnModifySectie.FlatStyle = FlatStyle.Flat;
            btnModifySectie.FlatAppearance.BorderSize = 0;
            btnModifySectie.Cursor = Cursors.Hand;
            btnModifySectie.Click += BtnModifySectie_Click;
            this.Controls.Add(btnModifySectie);

            btnAsocierePacientSectie = new Button();
            btnAsocierePacientSectie.Width = LATIME_CONTROL;
            btnAsocierePacientSectie.Text = "Asociere";
            btnAsocierePacientSectie.Left = 0;
            btnAsocierePacientSectie.Top = 6 * DIMENSIUNE_PAS_Y;
            btnAsocierePacientSectie.AutoSize = true;
            btnAsocierePacientSectie.BackColor = Color.SteelBlue;
            btnAsocierePacientSectie.ForeColor = Color.White;
            btnAsocierePacientSectie.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnAsocierePacientSectie.FlatStyle = FlatStyle.Flat;
            btnAsocierePacientSectie.FlatAppearance.BorderSize = 0;
            btnAsocierePacientSectie.Cursor = Cursors.Hand;
            btnAsocierePacientSectie.Click += BtnAsocierePacientSectie_Click;
            this.Controls.Add(btnAsocierePacientSectie);

            btnStergeSectie = new Button();
            btnStergeSectie.Width = LATIME_CONTROL;
            btnStergeSectie.Text = "Șterge";
            btnStergeSectie.Left = 0;
            btnStergeSectie.Top = 7 * DIMENSIUNE_PAS_Y;
            btnStergeSectie.AutoSize = true;
            btnStergeSectie.BackColor = Color.Red;
            btnStergeSectie.ForeColor = Color.White;
            btnStergeSectie.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnStergeSectie.FlatStyle = FlatStyle.Flat;
            btnStergeSectie.FlatAppearance.BorderSize = 0;
            btnStergeSectie.Cursor = Cursors.Hand;
            btnStergeSectie.Click += BtnStergeSectie_Click;
            this.Controls.Add(btnStergeSectie);

            btnInapoi = new Button();
            btnInapoi.Width = LATIME_CONTROL;
            btnInapoi.Text = "Înapoi";
            btnInapoi.Left = 0;
            btnInapoi.Top = 8 * DIMENSIUNE_PAS_Y;
            btnInapoi.AutoSize = true;
            btnInapoi.BackColor = Color.Crimson;
            btnInapoi.ForeColor = Color.White;
            btnInapoi.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnInapoi.FlatStyle = FlatStyle.Flat;
            btnInapoi.FlatAppearance.BorderSize = 0;
            btnInapoi.Cursor = Cursors.Hand;
            btnInapoi.Click += BtnInapoi_Click;
            this.Controls.Add(btnInapoi);

           


            //SECTIILE



            btnAdaugaSectii = new Button();
            btnAdaugaSectii.Width = LATIME_CONTROL;
            btnAdaugaSectii.Text = "Adaugă";
            btnAdaugaSectii.Left = 0;
            btnAdaugaSectii.Top = 2 * DIMENSIUNE_PAS_Y;
            btnAdaugaSectii.AutoSize = true;
            btnAdaugaSectii.BackColor = Color.SteelBlue;
            btnAdaugaSectii.ForeColor = Color.White;
            btnAdaugaSectii.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnAdaugaSectii.FlatStyle = FlatStyle.Flat;
            btnAdaugaSectii.FlatAppearance.BorderSize = 0;
            btnAdaugaSectii.Cursor = Cursors.Hand;
            btnAdaugaSectii.Click += BtnAdaugaSectii_Click;
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

        private void BtnAsocierePacientSectie_Click(object sender, EventArgs e)
        {
            AsocierePacientSectie formAsociere = new AsocierePacientSectie();
            formAsociere.Show();
        }

        private void BtnStergeSectie_Click(object sender, EventArgs e)
        {
            StergeSectie formSterge = new StergeSectie();
            formSterge.Show();
        }

        

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
            List<SectieSpital> sectii = adminSectii.GetSectii();
            int nrSectii = sectii.Count;




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


    

