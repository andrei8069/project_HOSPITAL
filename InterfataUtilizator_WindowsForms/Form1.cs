using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Configuration;
using System.IO;
using LibrarieModele;
using NivelStocareDate;
using static System.Collections.Specialized.BitVector32;


namespace InterfataUtilizator_WindowsForms
{
    
    public partial class Form1 : Form
    {

       


        //PACIENTI 
        Pacienti_FISIERTEXT adminPacienti;
        private Label lblTitluPacienti;
     
        //afiseaza doar numele la etichete
        private Label lblCodPacient;
        private Label lblNume;
        private Label lblPrenume;
        private Label lblCnp;
        private Label lblVarsta;
        private Label lblGreutate;
        private Label lblInaltime;
        private Label lblTemperaturaCorp;

        private Label lblGrupaSange;
        private Label lblAfectiuni;

        //stocam datele pe care le afisam
        private Label[] lblsCodPacient;
        private Label[] lblsNume;
        private Label[] lblsPrenume;
        private Label[] lblsCnp;
        private Label[] lblsVarsta;
        private Label[] lblsGreutate;
        private Label[] lblsInaltime;
        private Label[] lblsTemperaturaCorp;


        private Label[] lblsGrupaSange;
        private Label[] lblsAfectiuni;


        //SECTII
        Sectii_FISIERTEXT adminSectii;

        private Label lblTitluSectii;

        //afiseaza doar numele la etichete
        private Label lblCodSectie;
        private Label lblNumeSectie;
        private Label lblEtaj;
        private Label lblCapacitateMaxima;
        private Label lblNrPacientiInternati;
        private Label lblTemperaturaMediu;
        private Label lblSuprafataSectie;
        private Label lblBugetSectie;

        private Label lblStatusFunctionare;
        private Label lblDotari;

        //stocam datele pe care le afisam
        private Label[] lblsCodSectie;
        private Label[] lblsNumeSectie;
        private Label[] lblsEtaj;
        private Label[] lblsCapacitateMaxima;
        private Label[] lblsNrPacientiInternati;
        private Label[] lblsTemperaturaMediu;
        private Label[] lblsSuprafataSectie;
        private Label[] lblsBugetSectie;

        private Label[] lblsStatusFunctionare;
        private Label[] lblsDotari;



        //dimensiuni pt pacienti si pentru sectii
        private const int LATIME_CONTROL = 120; //latimea pentru fiecare coloana , latime de 200 pt nume , prenume
        private const int DIMENSIUNE_PAS_Y = 50; // distanta dintre pacienti
        private const int DIMENSIUNE_PAS_X = 135; //distanta dintre coloane gen distanta dintre nume si prenume sa fie de 200px

        public Form1()
        {
            InitializeComponent();
       

            string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            //linia de sus ia locatia folder-ului principal al solutiei


            string numeFisierPacienti = ConfigurationManager.AppSettings["NumeFisierPacienti"];
            string caleCompletaFisierPacienti = locatieFisierSolutie + "\\" + numeFisierPacienti;
            adminPacienti = new Pacienti_FISIERTEXT(caleCompletaFisierPacienti);


            int nrPacienti = 0;
            Pacient[] pacienti = adminPacienti.GetPacienti(out nrPacienti);


            string numeFisierSectii = ConfigurationManager.AppSettings["NumeFisierSectii"];   
            string caleCompletaFisierSectii = locatieFisierSolutie + "\\" + numeFisierSectii;
            //MessageBox.Show("CALE SECTII: " + caleCompletaFisierSectii);

            adminSectii = new Sectii_FISIERTEXT(caleCompletaFisierSectii);


            int nrSectii = 0;
            SectieSpital[] sectii = adminSectii.GetSectie(out nrSectii);


            this.Size = new Size(1920, 1080);
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.StartPosition = FormStartPosition.Manual;
            //this.Location = new Point(50, 50);
            this.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.ForeColor = Color.DarkSlateGray;
            this.Text = "Pacienți & Secții";
            this.BackColor = Color.FromArgb(230, 245, 255);


            //PACIENTII


            lblTitluPacienti = new Label();
            lblTitluPacienti.Text = "Pacienți:";
            lblTitluPacienti.Left = 0;
            lblTitluPacienti.AutoSize = true;
            this.Controls.Add(lblTitluPacienti);

            lblCodPacient = new Label();
            lblCodPacient.Width = LATIME_CONTROL;
            lblCodPacient.Text = "Cod";
            lblCodPacient.Left =1* DIMENSIUNE_PAS_X;
            lblCodPacient.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblCodPacient);


            lblNume = new Label();
            lblNume.Width = LATIME_CONTROL;
            lblNume.Text = "Nume";
            lblNume.Left = 2 * DIMENSIUNE_PAS_X;
            lblNume.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblNume);

            lblPrenume = new Label();
            lblPrenume.Width = LATIME_CONTROL;
            lblPrenume.Text = "Prenume";
            lblPrenume.Left = 3 * DIMENSIUNE_PAS_X;
            lblPrenume.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblPrenume);

            lblCnp = new Label();
            lblCnp.Width = LATIME_CONTROL;
            lblCnp.Text = "Cnp";
            lblCnp.Left = 4* DIMENSIUNE_PAS_X;
            lblCnp.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblCnp);


            lblVarsta = new Label();
            lblVarsta.Width = LATIME_CONTROL;
            lblVarsta.Text = "Varsta";
            lblVarsta.Left =5* DIMENSIUNE_PAS_X;
            lblVarsta.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblVarsta);


            lblGreutate = new Label();
            lblGreutate.Width = LATIME_CONTROL;
            lblGreutate.Text = "Greutate";
            lblGreutate.Left =6* DIMENSIUNE_PAS_X;
            lblGreutate.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblGreutate);

            lblInaltime = new Label();
            lblInaltime.Width = LATIME_CONTROL;
            lblInaltime.Text = "Inaltime";
            lblInaltime.Left =7* DIMENSIUNE_PAS_X;
            lblInaltime.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblInaltime);

            lblTemperaturaCorp = new Label();
            lblTemperaturaCorp.Width = LATIME_CONTROL;
            lblTemperaturaCorp.Text = "Temperatura";
            lblTemperaturaCorp.Left =8* DIMENSIUNE_PAS_X;
            lblTemperaturaCorp.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblTemperaturaCorp);

            lblGrupaSange = new Label();
            lblGrupaSange.Width = LATIME_CONTROL;
            lblGrupaSange.Text = "GrupaSange";
            lblGrupaSange.Left = 9 * DIMENSIUNE_PAS_X;
            lblGrupaSange.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblGrupaSange);

            lblAfectiuni = new Label();
            lblAfectiuni.Width = LATIME_CONTROL;
            lblAfectiuni.Text = "Afectiuni";
            lblAfectiuni.Left = 10 * DIMENSIUNE_PAS_X;
            lblAfectiuni.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblAfectiuni);

            //SECTIILE




            int axaY = nrPacienti +1;  //in functie de nr de pacienti alegem cat de jos sa mutam etichetele de la sectiespital ,deoarece se suprapun

            lblTitluSectii = new Label();
            lblTitluSectii.Text = "Secții Spital:";
            lblTitluSectii.Left = 0;
            lblTitluSectii.AutoSize = true;
            lblTitluSectii.Top = axaY * DIMENSIUNE_PAS_Y;
            this.Controls.Add(lblTitluSectii);

            lblCodSectie = new Label();
            lblCodSectie.Width = LATIME_CONTROL;
            lblCodSectie.Text = "Cod";
            lblCodSectie.Left = 1 * DIMENSIUNE_PAS_X;
            lblCodSectie.Top = axaY * DIMENSIUNE_PAS_Y;
            lblCodSectie.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblCodSectie);

            lblNumeSectie = new Label();
            lblNumeSectie.Width = LATIME_CONTROL;
            lblNumeSectie.Text = "Nume";
            lblNumeSectie.Left = 2 * DIMENSIUNE_PAS_X;
            lblNumeSectie.Top = axaY * DIMENSIUNE_PAS_Y;
            lblNumeSectie.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblNumeSectie);

            lblEtaj = new Label();
            lblEtaj.Width = LATIME_CONTROL;
            lblEtaj.Text = "Etaj";
            lblEtaj.Left = 3 * DIMENSIUNE_PAS_X;
            lblEtaj.Top = axaY * DIMENSIUNE_PAS_Y;
            lblEtaj.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblEtaj);

            lblCapacitateMaxima = new Label();
            lblCapacitateMaxima.Width = LATIME_CONTROL;
            lblCapacitateMaxima.Text = "Capacitate";
            lblCapacitateMaxima.Left = 4 * DIMENSIUNE_PAS_X;
            lblCapacitateMaxima.Top = axaY * DIMENSIUNE_PAS_Y;
            lblCapacitateMaxima.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblCapacitateMaxima);

            lblNrPacientiInternati = new Label();
            lblNrPacientiInternati.Width = LATIME_CONTROL;
            lblNrPacientiInternati.Text = "NrPacienti";
            lblNrPacientiInternati.Left = 5 * DIMENSIUNE_PAS_X;
            lblNrPacientiInternati.Top = axaY * DIMENSIUNE_PAS_Y;
            lblNrPacientiInternati.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblNrPacientiInternati);

            lblTemperaturaMediu = new Label();
            lblTemperaturaMediu.Width = LATIME_CONTROL;
            lblTemperaturaMediu.Text = "Temperatura";
            lblTemperaturaMediu.Left = 6 * DIMENSIUNE_PAS_X;
            lblTemperaturaMediu.Top = axaY * DIMENSIUNE_PAS_Y;
            lblTemperaturaMediu.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblTemperaturaMediu);

            lblSuprafataSectie = new Label();
            lblSuprafataSectie.Width = LATIME_CONTROL;
            lblSuprafataSectie.Text = "Suprafata";
            lblSuprafataSectie.Left = 7 * DIMENSIUNE_PAS_X;
            lblSuprafataSectie.Top = axaY * DIMENSIUNE_PAS_Y;
            lblSuprafataSectie.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblSuprafataSectie);

            lblBugetSectie = new Label();
            lblBugetSectie.Width = LATIME_CONTROL;
            lblBugetSectie.Text = "Buget";
            lblBugetSectie.Left = 8 * DIMENSIUNE_PAS_X;
            lblBugetSectie.Top = axaY * DIMENSIUNE_PAS_Y;
            lblBugetSectie.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblBugetSectie);

            lblStatusFunctionare = new Label();
            lblStatusFunctionare.Width = LATIME_CONTROL;
            lblStatusFunctionare.Text = "Status";
            lblStatusFunctionare.Left = 9 * DIMENSIUNE_PAS_X;
            lblStatusFunctionare.Top = axaY * DIMENSIUNE_PAS_Y;
            lblStatusFunctionare.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblStatusFunctionare);

            lblDotari = new Label();
            lblDotari.Width = LATIME_CONTROL;
            lblDotari.Text = "Dotari";
            lblDotari.Left = 10 * DIMENSIUNE_PAS_X;
            lblDotari.Top = axaY * DIMENSIUNE_PAS_Y;
            lblDotari.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblDotari);


        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            AfiseazaPacienti();
            AfiseazaSectii();
        }
        private void AfiseazaPacienti()
        {
            Pacient[] pacienti = adminPacienti.GetPacienti(out int nrPacienti);
            //MessageBox.Show($"nrPacienti: {nrPacienti}, null? {pacienti == null}");
            //MessageBox.Show($"Lungime array: {pacienti.Length}, nrPacienti: {nrPacienti}");



            lblsCodPacient = new Label[nrPacienti];
            lblsNume = new Label[nrPacienti];
            lblsPrenume = new Label[nrPacienti];
            lblsCnp = new Label[nrPacienti];
            lblsVarsta = new Label[nrPacienti];
            lblsGreutate = new Label[nrPacienti];
            lblsInaltime = new Label[nrPacienti];
            lblsTemperaturaCorp = new Label[nrPacienti];
            lblsGrupaSange = new Label[nrPacienti];
            lblsAfectiuni = new Label[nrPacienti];

            int i = 0;
            foreach(Pacient pacient in pacienti)
            {
                lblsCodPacient[i] = new Label();
                lblsCodPacient[i].Width = LATIME_CONTROL;
                lblsCodPacient[i].Text = pacient.CodPacient.ToString();
                lblsCodPacient[i].Left =1* DIMENSIUNE_PAS_X;
                lblsCodPacient[i].Top = (i + 1) * DIMENSIUNE_PAS_Y;
                lblsCodPacient[i].BackColor = Color.White; //background alb in spate
                lblsCodPacient[i].BorderStyle = BorderStyle.FixedSingle; //border , sa arate ca un tabel
                this.Controls.Add(lblsCodPacient[i]);

                lblsNume[i] = new Label();
                lblsNume[i].Width = LATIME_CONTROL;
                lblsNume[i].Text = pacient.Nume;
                lblsNume[i].Left = 2 * DIMENSIUNE_PAS_X;
                lblsNume[i].Top = (i + 1) * DIMENSIUNE_PAS_Y;
                lblsNume[i].BackColor = Color.White;
                lblsNume[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsNume[i]);


                lblsPrenume[i] = new Label();
                lblsPrenume[i].Width = LATIME_CONTROL;
                lblsPrenume[i].Text = pacient.Prenume;
                lblsPrenume[i].Left = 3 * DIMENSIUNE_PAS_X;
                lblsPrenume[i].Top = (i + 1) * DIMENSIUNE_PAS_Y;
                lblsPrenume[i].BackColor = Color.White;
                lblsPrenume[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsPrenume[i]);

                lblsCnp[i] = new Label();
                lblsCnp[i].Width = LATIME_CONTROL;
                lblsCnp[i].Text = pacient.Cnp;
                lblsCnp[i].Left = 4 * DIMENSIUNE_PAS_X;
                lblsCnp[i].Top = (i + 1) * DIMENSIUNE_PAS_Y;
                lblsCnp[i].BackColor = Color.White;
                lblsCnp[i].BorderStyle = BorderStyle.FixedSingle;
                lblsCnp[i].AutoSize = true;
                this.Controls.Add(lblsCnp[i]);

                lblsVarsta[i] = new Label();
                lblsVarsta[i].Width = LATIME_CONTROL;
                lblsVarsta[i].Text = pacient.Varsta.ToString();
                lblsVarsta[i].Left = 5 * DIMENSIUNE_PAS_X;
                lblsVarsta[i].Top = (i + 1) * DIMENSIUNE_PAS_Y;
                lblsVarsta[i].BackColor = Color.White;
                lblsVarsta[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsVarsta[i]);


                lblsGreutate[i] = new Label();
                lblsGreutate[i].Width = LATIME_CONTROL;
                lblsGreutate[i].Text = pacient.Greutate.ToString();
                lblsGreutate[i].Left = 6 * DIMENSIUNE_PAS_X;
                lblsGreutate[i].Top = (i + 1) * DIMENSIUNE_PAS_Y;
                lblsGreutate[i].BackColor = Color.White;
                lblsGreutate[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsGreutate[i]);


                lblsInaltime[i] = new Label();
                lblsInaltime[i].Width = LATIME_CONTROL;
                lblsInaltime[i].Text = pacient.Inaltime.ToString();
                lblsInaltime[i].Left = 7 * DIMENSIUNE_PAS_X;
                lblsInaltime[i].Top = (i + 1) * DIMENSIUNE_PAS_Y;
                lblsInaltime[i].BackColor = Color.White;
                lblsInaltime[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsInaltime[i]);


                lblsTemperaturaCorp[i] = new Label();
                lblsTemperaturaCorp[i].Width = LATIME_CONTROL;
                lblsTemperaturaCorp[i].Text = pacient.TemperaturaCorp.ToString();
                lblsTemperaturaCorp[i].Left = 8 * DIMENSIUNE_PAS_X;
                lblsTemperaturaCorp[i].Top = (i + 1) * DIMENSIUNE_PAS_Y;
                lblsTemperaturaCorp[i].BackColor = Color.White;
                lblsTemperaturaCorp[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsTemperaturaCorp[i]);

                lblsGrupaSange[i] = new Label();
                lblsGrupaSange[i].Width = LATIME_CONTROL;
                lblsGrupaSange[i].Text = pacient.Grupa.ToString();
                lblsGrupaSange[i].Left = 9 * DIMENSIUNE_PAS_X;
                lblsGrupaSange[i].Top = (i + 1) * DIMENSIUNE_PAS_Y;
                lblsGrupaSange[i].BackColor = Color.White;
                lblsGrupaSange[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsGrupaSange[i]);

                lblsAfectiuni[i] = new Label();
                lblsAfectiuni[i].Width = LATIME_CONTROL;
                lblsAfectiuni[i].Text = pacient.AfectiuniMed.ToString();
                lblsAfectiuni[i].Left = 10 * DIMENSIUNE_PAS_X;
                lblsAfectiuni[i].Top = (i + 1) * DIMENSIUNE_PAS_Y;
                lblsAfectiuni[i].BackColor = Color.White;
                lblsAfectiuni[i].BorderStyle = BorderStyle.FixedSingle;
                lblsAfectiuni[i].AutoSize = true;
                this.Controls.Add(lblsAfectiuni[i]);

                i++;
                



            }
        }
        private void AfiseazaSectii()
        {

           
            SectieSpital[] sectii = adminSectii.GetSectie(out int nrSectii);



            Pacient[] pacienti = adminPacienti.GetPacienti(out int nrPacienti);
            int axaY = nrPacienti + 2; //+2 pentru spatiu extra
            

            lblsCodSectie = new Label[nrSectii];
            lblsNumeSectie = new Label[nrSectii];
            lblsEtaj = new Label[nrSectii];
            lblsCapacitateMaxima = new Label[nrSectii];
            lblsNrPacientiInternati = new Label[nrSectii];
            lblsTemperaturaMediu = new Label[nrSectii];
            lblsSuprafataSectie = new Label[nrSectii];
            lblsBugetSectie = new Label[nrSectii];
            lblsStatusFunctionare = new Label[nrSectii];
            lblsDotari = new Label[nrSectii];

            int i = 0;
            foreach (SectieSpital sectieSpital in sectii)
            {
                lblsCodSectie[i] = new Label();
                lblsCodSectie[i].Width = LATIME_CONTROL;
                lblsCodSectie[i].Text = sectieSpital.CodSectie.ToString();
                lblsCodSectie[i].Left = 1 * DIMENSIUNE_PAS_X;
                lblsCodSectie[i].Top = (i + axaY) * DIMENSIUNE_PAS_Y;
                lblsCodSectie[i].BackColor = Color.White;
                lblsCodSectie[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsCodSectie[i]);

                lblsNumeSectie[i] = new Label();
                lblsNumeSectie[i].Width = LATIME_CONTROL;
                lblsNumeSectie[i].Text = sectieSpital.NumeSectie;
                lblsNumeSectie[i].Left = 2 * DIMENSIUNE_PAS_X;
                lblsNumeSectie[i].Top = (i + axaY) * DIMENSIUNE_PAS_Y;
                lblsNumeSectie[i].BackColor = Color.White;
                lblsNumeSectie[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsNumeSectie[i]);


                lblsEtaj[i] = new Label();
                lblsEtaj[i].Width = LATIME_CONTROL;
                lblsEtaj[i].Text = sectieSpital.Etaj.ToString();
                lblsEtaj[i].Left = 3 * DIMENSIUNE_PAS_X;
                lblsEtaj[i].Top = (i + axaY) * DIMENSIUNE_PAS_Y;
                lblsEtaj[i].BackColor = Color.White;
                lblsEtaj[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsEtaj[i]);

                lblsCapacitateMaxima[i] = new Label();
                lblsCapacitateMaxima[i].Width = LATIME_CONTROL;
                lblsCapacitateMaxima[i].Text = sectieSpital.CapacitateMaxima.ToString();
                lblsCapacitateMaxima[i].Left = 4 * DIMENSIUNE_PAS_X;
                lblsCapacitateMaxima[i].Top = (i + axaY) * DIMENSIUNE_PAS_Y;
                lblsCapacitateMaxima[i].BackColor = Color.White;
                lblsCapacitateMaxima[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsCapacitateMaxima[i]);


                lblsNrPacientiInternati[i] = new Label();
                lblsNrPacientiInternati[i].Width = LATIME_CONTROL;
                lblsNrPacientiInternati[i].Text = sectieSpital.NrPacientiInternati.ToString();
                lblsNrPacientiInternati[i].Left = 5 * DIMENSIUNE_PAS_X;
                lblsNrPacientiInternati[i].Top = (i + axaY) * DIMENSIUNE_PAS_Y;
                lblsNrPacientiInternati[i].BackColor = Color.White;
                lblsNrPacientiInternati[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsNrPacientiInternati[i]);

                lblsTemperaturaMediu[i] = new Label();
                lblsTemperaturaMediu[i].Width = LATIME_CONTROL;
                lblsTemperaturaMediu[i].Text = sectieSpital.TemperaturaMediu.ToString();
                lblsTemperaturaMediu[i].Left = 6 * DIMENSIUNE_PAS_X;
                lblsTemperaturaMediu[i].Top = (i + axaY) * DIMENSIUNE_PAS_Y;
                lblsTemperaturaMediu[i].BackColor = Color.White;
                lblsTemperaturaMediu[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsTemperaturaMediu[i]);


                lblsSuprafataSectie[i] = new Label();
                lblsSuprafataSectie[i].Width = LATIME_CONTROL;
                lblsSuprafataSectie[i].Text = sectieSpital.SuprafataSectie.ToString();
                lblsSuprafataSectie[i].Left = 7 * DIMENSIUNE_PAS_X;
                lblsSuprafataSectie[i].Top = (i + axaY) * DIMENSIUNE_PAS_Y;
                lblsSuprafataSectie[i].BackColor = Color.White;
                lblsSuprafataSectie[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsSuprafataSectie[i]);


                lblsBugetSectie[i] = new Label();
                lblsBugetSectie[i].Width = LATIME_CONTROL;
                lblsBugetSectie[i].Text = sectieSpital.BugetSectie.ToString();
                lblsBugetSectie[i].Left = 8 * DIMENSIUNE_PAS_X;
                lblsBugetSectie[i].Top = (i + axaY) * DIMENSIUNE_PAS_Y;
                lblsBugetSectie[i].BackColor = Color.White;
                lblsBugetSectie[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsBugetSectie[i]);


                //lblsStatusFunctionare 
                // lblsDotari

                lblsStatusFunctionare[i] = new Label();
                lblsStatusFunctionare[i].Width = LATIME_CONTROL;
                lblsStatusFunctionare[i].Text = sectieSpital.Status.ToString();
                lblsStatusFunctionare[i].Left = 9 * DIMENSIUNE_PAS_X;
                lblsStatusFunctionare[i].Top = (i + axaY) * DIMENSIUNE_PAS_Y;
                lblsStatusFunctionare[i].BackColor = Color.White;
                lblsStatusFunctionare[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsStatusFunctionare[i]);


                lblsDotari[i] = new Label();
                lblsDotari[i].Width = LATIME_CONTROL;
                lblsDotari[i].Text = sectieSpital.DotariSec.ToString();
                lblsDotari[i].Left = 10 * DIMENSIUNE_PAS_X;
                lblsDotari[i].Top = (i + axaY) * DIMENSIUNE_PAS_Y;
                lblsDotari[i].BackColor = Color.White;
                lblsDotari[i].BorderStyle = BorderStyle.FixedSingle;
                lblsDotari[i].AutoSize = true;
                this.Controls.Add(lblsDotari[i]);

                i++;




            }
        }
    }
}
