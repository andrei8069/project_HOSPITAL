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


        //refresh date
        private Button btnRefreshDate;

        //PACIENTI 
        Pacienti_FISIERTEXT adminPacienti;
        private Label lblTitluPacienti;

        //buton adauga pacient
        private Button btnAdaugaPacienti;



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

        //introducem datele in fiecare camp creat 
        private TextBox txtNume;
        private TextBox txtPrenume;
        private TextBox txtCnp;
        private TextBox txtVarsta;
        private TextBox txtGreutate;
        private TextBox txtInaltime;
        private TextBox txtTemperaturaCorp;

        private ComboBox txtGrupaSange;
        private ComboBox txtAfectiuni;


        //SECTII
        Sectii_FISIERTEXT adminSectii;

        private Label lblTitluSectii;

        //buton adauga sectie
        private Button btnAdaugaSectii;

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

        private TextBox txtNumeSectie;
        private TextBox txtEtaj;
        private TextBox txtCapacitateMaxima;
        private TextBox txtNrPacientiInternati;
        private TextBox txtTemperaturaMediu;
        private TextBox txtSuprafataSectie;
        private TextBox txtBugetSectie;

        private ComboBox txtStatusFunctionare;
        private ComboBox txtDotari;

        // constante pt valori bune pacienti
        private const int VARSTA_MIN = 0;
        private const int VARSTA_MAX = 100;
        private const double GREUTATE_MIN = 2.0;
        private const double GREUTATE_MAX = 300.0;
        private const double INALTIME_MIN = 30.0;
        private const double INALTIME_MAX = 250.0;
        private const double TEMP_MIN = 30.0;
        private const double TEMP_MAX = 45.0;
        private const int CNP_LUNGIME = 13;


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

        public Form1()
        {

            InitializeComponent();
            InitializareInterfata();
         
        }




        private void InitializareInterfata()
        {
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


            this.Size = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.StartPosition = FormStartPosition.Manual;
            //this.Location = new Point(50, 50);
            this.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.ForeColor = Color.DarkSlateGray;
            this.Text = "Pacienți & Secții";
            this.BackColor = Color.FromArgb(230, 245, 255);

            //button refresh
            btnRefreshDate = new Button();
            btnRefreshDate.Width = LATIME_CONTROL;
            btnRefreshDate.Text = "Refresh";
            btnRefreshDate.Left = 11 * DIMENSIUNE_PAS_X;
            btnRefreshDate.AutoSize = true;
            btnRefreshDate.Click += BtnRefreshDate_Click;
            this.Controls.Add(btnRefreshDate);
            //PACIENTII


            lblTitluPacienti = new Label();
            lblTitluPacienti.Text = "Pacienți:";
            lblTitluPacienti.Left = 0;
            lblTitluPacienti.AutoSize = true;
            this.Controls.Add(lblTitluPacienti);

            btnAdaugaPacienti = new Button();
            btnAdaugaPacienti.Width = LATIME_CONTROL;
            btnAdaugaPacienti.Text = "Adauga";
            btnAdaugaPacienti.Left = 0;
            btnAdaugaPacienti.Top = 1 * DIMENSIUNE_PAS_Y;
            btnAdaugaPacienti.AutoSize = true;
            btnAdaugaPacienti.Click += BtnAdaugaPacienti_Click;
            this.Controls.Add(btnAdaugaPacienti);



            lblCodPacient = new Label();
            lblCodPacient.Width = LATIME_CONTROL;
            lblCodPacient.Text = "Cod";
            lblCodPacient.Left = 1 * DIMENSIUNE_PAS_X;
            lblCodPacient.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblCodPacient);




            lblNume = new Label();
            lblNume.Width = LATIME_CONTROL;
            lblNume.Text = "Nume";
            lblNume.Left = 2 * DIMENSIUNE_PAS_X;
            lblNume.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblNume);


            txtNume = new TextBox();
            txtNume.Left = 2 * DIMENSIUNE_PAS_X;
            txtNume.Top = 1 * DIMENSIUNE_PAS_Y;
            txtNume.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(txtNume);


            lblPrenume = new Label();
            lblPrenume.Width = LATIME_CONTROL;
            lblPrenume.Text = "Prenume";
            lblPrenume.Left = 3 * DIMENSIUNE_PAS_X;
            lblPrenume.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblPrenume);

            txtPrenume = new TextBox();
            txtPrenume.Left = 3 * DIMENSIUNE_PAS_X;
            txtPrenume.Top = 1 * DIMENSIUNE_PAS_Y;
            txtPrenume.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(txtPrenume);

            lblCnp = new Label();
            lblCnp.Width = LATIME_CONTROL;
            lblCnp.Text = "Cnp";
            lblCnp.Left = 4 * DIMENSIUNE_PAS_X;
            lblCnp.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblCnp);

            txtCnp = new TextBox();
            txtCnp.Left = 4 * DIMENSIUNE_PAS_X;
            txtCnp.Top = 1 * DIMENSIUNE_PAS_Y;
            txtCnp.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(txtCnp);


            lblVarsta = new Label();
            lblVarsta.Width = LATIME_CONTROL;
            lblVarsta.Text = "Varsta";
            lblVarsta.Left = 5 * DIMENSIUNE_PAS_X;
            lblVarsta.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblVarsta);

            txtVarsta = new TextBox();
            txtVarsta.Left = 5 * DIMENSIUNE_PAS_X;
            txtVarsta.Top = 1 * DIMENSIUNE_PAS_Y;
            txtVarsta.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(txtVarsta);


            lblGreutate = new Label();
            lblGreutate.Width = LATIME_CONTROL;
            lblGreutate.Text = "Greutate";
            lblGreutate.Left = 6 * DIMENSIUNE_PAS_X;
            lblGreutate.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblGreutate);

            txtGreutate = new TextBox();
            txtGreutate.Left = 6 * DIMENSIUNE_PAS_X;
            txtGreutate.Top = 1 * DIMENSIUNE_PAS_Y;
            txtGreutate.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(txtGreutate);

            lblInaltime = new Label();
            lblInaltime.Width = LATIME_CONTROL;
            lblInaltime.Text = "Inaltime";
            lblInaltime.Left = 7 * DIMENSIUNE_PAS_X;
            lblInaltime.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblInaltime);

            txtInaltime = new TextBox();
            txtInaltime.Left = 7 * DIMENSIUNE_PAS_X;
            txtInaltime.Top = 1 * DIMENSIUNE_PAS_Y;
            txtInaltime.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(txtInaltime);

            lblTemperaturaCorp = new Label();
            lblTemperaturaCorp.Width = LATIME_CONTROL;
            lblTemperaturaCorp.Text = "Temperatura";
            lblTemperaturaCorp.Left = 8 * DIMENSIUNE_PAS_X;
            lblTemperaturaCorp.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblTemperaturaCorp);

            txtTemperaturaCorp = new TextBox();
            txtTemperaturaCorp.Left = 8 * DIMENSIUNE_PAS_X;
            txtTemperaturaCorp.Top = 1 * DIMENSIUNE_PAS_Y;
            txtTemperaturaCorp.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(txtTemperaturaCorp);

            lblGrupaSange = new Label();
            lblGrupaSange.Width = LATIME_CONTROL;
            lblGrupaSange.Text = "GrupaSange";
            lblGrupaSange.Left = 9 * DIMENSIUNE_PAS_X;
            lblGrupaSange.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblGrupaSange);

            txtGrupaSange = new ComboBox();
            txtGrupaSange.Left = 9 * DIMENSIUNE_PAS_X;
            txtGrupaSange.Top = 1 * DIMENSIUNE_PAS_Y;
            txtGrupaSange.ForeColor = Color.DarkSlateGray;
            txtGrupaSange.Items.AddRange(Enum.GetNames(typeof(GrupaSangePacient)));
            this.Controls.Add(txtGrupaSange);

            lblAfectiuni = new Label();
            lblAfectiuni.Width = LATIME_CONTROL;
            lblAfectiuni.Text = "Afectiuni";
            lblAfectiuni.Left = 10 * DIMENSIUNE_PAS_X;
            lblAfectiuni.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblAfectiuni);

            txtAfectiuni = new ComboBox();
            txtAfectiuni.Left = 10 * DIMENSIUNE_PAS_X;
            txtAfectiuni.Top = 1 * DIMENSIUNE_PAS_Y;
            txtAfectiuni.ForeColor = Color.DarkSlateGray;
            txtAfectiuni.Items.AddRange(Enum.GetNames(typeof(AfectiuniMedicale)));
            this.Controls.Add(txtAfectiuni);

            //SECTIILE




            int axaY = nrPacienti + 2;  //in functie de nr de pacienti alegem cat de jos sa mutam etichetele de la sectiespital ,deoarece se suprapun

            lblTitluSectii = new Label();
            lblTitluSectii.Text = "Secții Spital:";
            lblTitluSectii.Left = 0;
            lblTitluSectii.AutoSize = true;
            lblTitluSectii.Top = axaY * DIMENSIUNE_PAS_Y;
            this.Controls.Add(lblTitluSectii);


            btnAdaugaSectii = new Button();
            btnAdaugaSectii.Width = LATIME_CONTROL;
            btnAdaugaSectii.Text = "Adauga";
            btnAdaugaSectii.Left = 0;
            btnAdaugaSectii.Top = (axaY + 1) * DIMENSIUNE_PAS_Y;
            btnAdaugaSectii.AutoSize = true;
            btnAdaugaSectii.Click += BtnAdaugaSectii_Click;
            this.Controls.Add(btnAdaugaSectii);

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


            txtNumeSectie = new TextBox();
            txtNumeSectie.Left = 2 * DIMENSIUNE_PAS_X;
            txtNumeSectie.Top = (axaY + 1) * DIMENSIUNE_PAS_Y;
            txtNumeSectie.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(txtNumeSectie);

            lblEtaj = new Label();
            lblEtaj.Width = LATIME_CONTROL;
            lblEtaj.Text = "Etaj";
            lblEtaj.Left = 3 * DIMENSIUNE_PAS_X;
            lblEtaj.Top = axaY * DIMENSIUNE_PAS_Y;
            lblEtaj.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblEtaj);


            txtEtaj = new TextBox();
            txtEtaj.Left = 3 * DIMENSIUNE_PAS_X;
            txtEtaj.Top = (axaY + 1) * DIMENSIUNE_PAS_Y;
            txtEtaj.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(txtEtaj);

            lblCapacitateMaxima = new Label();
            lblCapacitateMaxima.Width = LATIME_CONTROL;
            lblCapacitateMaxima.Text = "Capacitate";
            lblCapacitateMaxima.Left = 4 * DIMENSIUNE_PAS_X;
            lblCapacitateMaxima.Top = axaY * DIMENSIUNE_PAS_Y;
            lblCapacitateMaxima.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblCapacitateMaxima);


            txtCapacitateMaxima = new TextBox();
            txtCapacitateMaxima.Left = 4 * DIMENSIUNE_PAS_X;
            txtCapacitateMaxima.Top = (axaY + 1) * DIMENSIUNE_PAS_Y;
            txtCapacitateMaxima.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(txtCapacitateMaxima);

            lblNrPacientiInternati = new Label();
            lblNrPacientiInternati.Width = LATIME_CONTROL;
            lblNrPacientiInternati.Text = "NrPacienti";
            lblNrPacientiInternati.Left = 5 * DIMENSIUNE_PAS_X;
            lblNrPacientiInternati.Top = axaY * DIMENSIUNE_PAS_Y;
            lblNrPacientiInternati.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblNrPacientiInternati);


            txtNrPacientiInternati = new TextBox();
            txtNrPacientiInternati.Left = 5 * DIMENSIUNE_PAS_X;
            txtNrPacientiInternati.Top = (axaY + 1) * DIMENSIUNE_PAS_Y;
            txtNrPacientiInternati.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(txtNrPacientiInternati);

            lblTemperaturaMediu = new Label();
            lblTemperaturaMediu.Width = LATIME_CONTROL;
            lblTemperaturaMediu.Text = "Temperatura";
            lblTemperaturaMediu.Left = 6 * DIMENSIUNE_PAS_X;
            lblTemperaturaMediu.Top = axaY * DIMENSIUNE_PAS_Y;
            lblTemperaturaMediu.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblTemperaturaMediu);


            txtTemperaturaMediu = new TextBox();
            txtTemperaturaMediu.Left = 6 * DIMENSIUNE_PAS_X;
            txtTemperaturaMediu.Top = (axaY + 1) * DIMENSIUNE_PAS_Y;
            txtTemperaturaMediu.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(txtTemperaturaMediu);

            lblSuprafataSectie = new Label();
            lblSuprafataSectie.Width = LATIME_CONTROL;
            lblSuprafataSectie.Text = "Suprafata";
            lblSuprafataSectie.Left = 7 * DIMENSIUNE_PAS_X;
            lblSuprafataSectie.Top = axaY * DIMENSIUNE_PAS_Y;
            lblSuprafataSectie.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblSuprafataSectie);


            txtSuprafataSectie = new TextBox();
            txtSuprafataSectie.Left = 7 * DIMENSIUNE_PAS_X;
            txtSuprafataSectie.Top = (axaY + 1) * DIMENSIUNE_PAS_Y;
            txtSuprafataSectie.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(txtSuprafataSectie);

            lblBugetSectie = new Label();
            lblBugetSectie.Width = LATIME_CONTROL;
            lblBugetSectie.Text = "Buget";
            lblBugetSectie.Left = 8 * DIMENSIUNE_PAS_X;
            lblBugetSectie.Top = axaY * DIMENSIUNE_PAS_Y;
            lblBugetSectie.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblBugetSectie);


            txtBugetSectie = new TextBox();
            txtBugetSectie.Left = 8 * DIMENSIUNE_PAS_X;
            txtBugetSectie.Top = (axaY + 1) * DIMENSIUNE_PAS_Y;
            txtBugetSectie.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(txtBugetSectie);

            lblStatusFunctionare = new Label();
            lblStatusFunctionare.Width = LATIME_CONTROL;
            lblStatusFunctionare.Text = "Status";
            lblStatusFunctionare.Left = 9 * DIMENSIUNE_PAS_X;
            lblStatusFunctionare.Top = axaY * DIMENSIUNE_PAS_Y;
            lblStatusFunctionare.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblStatusFunctionare);


            txtStatusFunctionare = new ComboBox();
            txtStatusFunctionare.Left = 9 * DIMENSIUNE_PAS_X;
            txtStatusFunctionare.Top = (axaY + 1) * DIMENSIUNE_PAS_Y;
            txtStatusFunctionare.ForeColor = Color.DarkSlateGray;
            txtStatusFunctionare.Items.AddRange(Enum.GetNames(typeof(StatusFunctionareSectie)));
            this.Controls.Add(txtStatusFunctionare);

            lblDotari = new Label();
            lblDotari.Width = LATIME_CONTROL;
            lblDotari.Text = "Dotari";
            lblDotari.Left = 10 * DIMENSIUNE_PAS_X;
            lblDotari.Top = axaY * DIMENSIUNE_PAS_Y;
            lblDotari.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblDotari);

            txtDotari = new ComboBox();
            txtDotari.Left = 10 * DIMENSIUNE_PAS_X;
            txtDotari.Top = (axaY + 1) * DIMENSIUNE_PAS_Y;
            txtDotari.ForeColor = Color.DarkSlateGray;
            txtDotari.Items.AddRange(Enum.GetNames(typeof(DotariSectie)));
            this.Controls.Add(txtDotari);


        }





        private void BtnRefreshDate_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializareInterfata();
            AfiseazaPacienti();
            AfiseazaSectii();

        }






        private void BtnAdaugaPacienti_Click(object sender, EventArgs e)
        {
            string eroare = "";
            lblNume.ForeColor = Color.DarkSlateGray;
            lblPrenume.ForeColor = Color.DarkSlateGray;
            lblCnp.ForeColor = Color.DarkSlateGray;
            lblVarsta.ForeColor = Color.DarkSlateGray;
            lblGreutate.ForeColor = Color.DarkSlateGray;
            lblInaltime.ForeColor = Color.DarkSlateGray;
            lblTemperaturaCorp.ForeColor = Color.DarkSlateGray;
            lblGrupaSange.ForeColor = Color.DarkSlateGray;
            lblAfectiuni.ForeColor = Color.DarkSlateGray;

            string nume = txtNume.Text.Trim();
            if (nume.Length < 2)
            {
                eroare += "Numele trebuie să aibă cel puțin 2 caractere.\n";
                lblNume.ForeColor = Color.Red;
            }
            else lblNume.ForeColor = Color.DarkSlateGray;

            string prenume = txtPrenume.Text.Trim();
            if (prenume.Length < 2)
            {
                eroare += "Prenumele trebuie să aibă cel puțin 2 caractere.\n";
                lblPrenume.ForeColor = Color.Red;
            }
            else lblPrenume.ForeColor = Color.DarkSlateGray;

            string cnp = txtCnp.Text.Trim();
            if (cnp.Length != CNP_LUNGIME)
            {
                eroare += "CNP-ul trebuie să aibă 13 caractere.\n";
                lblCnp.ForeColor = Color.Red;
            }
            else lblCnp.ForeColor = Color.DarkSlateGray;

            
            int varsta;
            if (!int.TryParse(txtVarsta.Text.Trim(), out varsta) || varsta < VARSTA_MIN || varsta > VARSTA_MAX)
            {
                eroare += $"Vârsta trebuie să fie între {VARSTA_MIN} și {VARSTA_MAX}.\n";
                lblVarsta.ForeColor = Color.Red;
            }
            else lblVarsta.ForeColor = Color.DarkSlateGray;

            
            double greutate;
            if (!double.TryParse(txtGreutate.Text.Trim(), out greutate) || greutate < GREUTATE_MIN || greutate > GREUTATE_MAX)
            {
                eroare += $"Greutatea trebuie să fie între {GREUTATE_MIN} și {GREUTATE_MAX}.\n";
                lblGreutate.ForeColor = Color.Red;
            }
            else lblGreutate.ForeColor = Color.DarkSlateGray;

            
            double inaltime;
            if (!double.TryParse(txtInaltime.Text.Trim(), out inaltime) || inaltime < INALTIME_MIN || inaltime > INALTIME_MAX)
            {
                eroare += $"Înălțimea trebuie să fie între {INALTIME_MIN} și {INALTIME_MAX}.\n";
                lblInaltime.ForeColor = Color.Red;
            }
            else lblInaltime.ForeColor = Color.DarkSlateGray;

            
            double temperaturaCorp;
            if (!double.TryParse(txtTemperaturaCorp.Text.Trim(), out temperaturaCorp) || temperaturaCorp < TEMP_MIN || temperaturaCorp > TEMP_MAX)
            {
                eroare += $"Temperatura corpului trebuie să fie între {TEMP_MIN} și {TEMP_MAX}.\n";
                lblTemperaturaCorp.ForeColor = Color.Red;
            }
            else lblTemperaturaCorp.ForeColor = Color.DarkSlateGray;


            GrupaSangePacient grupaSange = GrupaSangePacient.Nespecificat; 
            AfectiuniMedicale afectiuni = AfectiuniMedicale.Nespecificat; 


            if (txtGrupaSange.SelectedIndex == -1)
            {
                eroare += "Selectati grupa de sange.\n";
                lblGrupaSange.ForeColor = Color.Red;
            }
            else
            {
                grupaSange = (GrupaSangePacient)Enum.Parse(typeof(GrupaSangePacient), txtGrupaSange.Text);
            }

            if (txtAfectiuni.SelectedIndex == -1)
            {
                eroare += "Selectati o afectiune.\n";
                lblAfectiuni.ForeColor = Color.Red;
            }
            else
            {
                afectiuni = (AfectiuniMedicale)Enum.Parse(typeof(AfectiuniMedicale), txtAfectiuni.Text);
            }



            
            if (eroare != "")
            {   //afiseaza textul , titul ferestrei , un buton ok si un icon de avertizare
                MessageBox.Show(eroare, "Date invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            int nrPacienti = 0;
            Pacient[] pacienti = adminPacienti.GetPacienti(out nrPacienti);

            Pacient pacient = new Pacient(nrPacienti + 1, nume, prenume, cnp, varsta, greutate, inaltime, temperaturaCorp, grupaSange, afectiuni);
            adminPacienti.AddPacient(pacient);

            txtNume.ResetText();
            txtPrenume.ResetText();
            txtCnp.ResetText();
            txtVarsta.ResetText();
            txtGreutate.ResetText();
            txtInaltime.ResetText();
            txtTemperaturaCorp.ResetText();
            txtGrupaSange.ResetText();
            txtAfectiuni.ResetText();

           
        }

        private void BtnAdaugaSectii_Click(object sender, EventArgs e)
        {

            string eroare = "";

            lblNumeSectie.ForeColor = Color.DarkSlateGray;
            lblEtaj.ForeColor = Color.DarkSlateGray;
            lblCapacitateMaxima.ForeColor = Color.DarkSlateGray;
            lblNrPacientiInternati.ForeColor = Color.DarkSlateGray;
            lblTemperaturaMediu.ForeColor = Color.DarkSlateGray;
            lblSuprafataSectie.ForeColor = Color.DarkSlateGray;
            lblBugetSectie.ForeColor = Color.DarkSlateGray;
            lblStatusFunctionare.ForeColor = Color.DarkSlateGray;
            lblDotari.ForeColor = Color.DarkSlateGray;


            
            string numeSectie = txtNumeSectie.Text.Trim();
            if (numeSectie.Length < LUNGIME_MIN_NUME_SECTIE || numeSectie.Length > LUNGIME_MAX_NUME_SECTIE)
            {
                eroare += $"Numele sectiei trebuie să aibă între {LUNGIME_MIN_NUME_SECTIE} și {LUNGIME_MAX_NUME_SECTIE} caractere.\n";
                lblNumeSectie.ForeColor = Color.Red;
            }
            else lblNumeSectie.ForeColor = Color.DarkSlateGray;

           
            int etaj;
            if (!int.TryParse(txtEtaj.Text.Trim(), out etaj) || etaj < ETAJ_MIN || etaj > ETAJ_MAX)
            {
                eroare += $"Etajul trebuie să fie între {ETAJ_MIN} și {ETAJ_MAX}.\n";
                lblEtaj.ForeColor = Color.Red;
            }
            else lblEtaj.ForeColor = Color.DarkSlateGray;

            
            int capacitate;
            if (!int.TryParse(txtCapacitateMaxima.Text.Trim(), out capacitate) || capacitate < CAPACITATE_MIN || capacitate > CAPACITATE_MAX)
            {
                eroare += $"Capacitatea trebuie să fie între {CAPACITATE_MIN} și {CAPACITATE_MAX}.\n";
                lblCapacitateMaxima.ForeColor = Color.Red;
            }
            else lblCapacitateMaxima.ForeColor = Color.DarkSlateGray;

            
            int nrPacienti;
            if (!int.TryParse(txtNrPacientiInternati.Text.Trim(), out nrPacienti) || nrPacienti < NR_PACIENTI_MIN || nrPacienti > capacitate)
            {
                eroare += $"Numărul de pacienți trebuie să fie între {NR_PACIENTI_MIN} și capacitatea maximă ({capacitate}).\n";
                lblNrPacientiInternati.ForeColor = Color.Red;
            }
            else lblNrPacientiInternati.ForeColor = Color.DarkSlateGray;

        
            double temperatura;
            if (!double.TryParse(txtTemperaturaMediu.Text.Trim(), out temperatura) || temperatura < TEMPERATURA_MIN || temperatura > TEMPERATURA_MAX)
            {
                eroare += $"Temperatura trebuie să fie între {TEMPERATURA_MIN} și {TEMPERATURA_MAX} grade.\n";
                lblTemperaturaMediu.ForeColor = Color.Red;
            }
            else lblTemperaturaMediu.ForeColor = Color.DarkSlateGray;

           
            double suprafata;
            if (!double.TryParse(txtSuprafataSectie.Text.Trim(), out suprafata) || suprafata < SUPRAFATA_MIN || suprafata > SUPRAFATA_MAX)
            {
                eroare += $"Suprafața trebuie să fie între {SUPRAFATA_MIN} și {SUPRAFATA_MAX} mp.\n";
                lblSuprafataSectie.ForeColor = Color.Red;
            }
            else lblSuprafataSectie.ForeColor = Color.DarkSlateGray;

            
            double buget;
            if (!double.TryParse(txtBugetSectie.Text.Trim(), out buget) || buget < BUGET_MIN)
            {
                eroare += $"Bugetul trebuie să fie pozitiv.\n";
                lblBugetSectie.ForeColor = Color.Red;
            }
            else lblBugetSectie.ForeColor = Color.DarkSlateGray;


            StatusFunctionareSectie status = StatusFunctionareSectie.Nespecificat;
            DotariSectie dotari = DotariSectie.Nespecificat;

            if (txtStatusFunctionare.SelectedIndex == -1)
            {
                eroare += "Selectati statusul sectiei.\n";
                lblStatusFunctionare.ForeColor = Color.Red;
            }
            else
            {
                status = (StatusFunctionareSectie)Enum.Parse(typeof(StatusFunctionareSectie), txtStatusFunctionare.Text);
            }

            if (txtDotari.SelectedIndex == -1)
            {
                eroare += "Selectati dotarile sectiei.\n";
                lblDotari.ForeColor = Color.Red;
            }
            else
            {
                dotari = (DotariSectie)Enum.Parse(typeof(DotariSectie), txtDotari.Text);
            }

            if (eroare != "")
            {
                MessageBox.Show(eroare, "Date invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int nrSectii = 0;
            SectieSpital[] sectii = adminSectii.GetSectie(out nrSectii);

            SectieSpital sectie = new SectieSpital(nrSectii + 1, numeSectie, etaj, capacitate, nrPacienti, temperatura, suprafata, buget, status, dotari);
            adminSectii.AddSectii(sectie);

            txtNumeSectie.ResetText();
            txtEtaj.ResetText();
            txtCapacitateMaxima.ResetText();
            txtNrPacientiInternati.ResetText();
            txtTemperaturaMediu.ResetText();
            txtSuprafataSectie.ResetText();
            txtBugetSectie.ResetText();
            txtStatusFunctionare.ResetText();
            txtDotari.ResetText();

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
            foreach (Pacient pacient in pacienti)
            {
                lblsCodPacient[i] = new Label();
                lblsCodPacient[i].Width = LATIME_CONTROL;
                lblsCodPacient[i].Text = pacient.CodPacient.ToString();
                lblsCodPacient[i].Left = 1 * DIMENSIUNE_PAS_X;
                lblsCodPacient[i].Top = (i + 2) * DIMENSIUNE_PAS_Y;
                lblsCodPacient[i].BackColor = Color.White; //background alb in spate
                lblsCodPacient[i].BorderStyle = BorderStyle.FixedSingle; //border , sa arate ca un tabel
                this.Controls.Add(lblsCodPacient[i]);

                lblsNume[i] = new Label();
                lblsNume[i].Width = LATIME_CONTROL;
                lblsNume[i].Text = pacient.Nume;
                lblsNume[i].Left = 2 * DIMENSIUNE_PAS_X;
                lblsNume[i].Top = (i + 2) * DIMENSIUNE_PAS_Y;
                lblsNume[i].BackColor = Color.White;
                lblsNume[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsNume[i]);


                lblsPrenume[i] = new Label();
                lblsPrenume[i].Width = LATIME_CONTROL;
                lblsPrenume[i].Text = pacient.Prenume;
                lblsPrenume[i].Left = 3 * DIMENSIUNE_PAS_X;
                lblsPrenume[i].Top = (i + 2) * DIMENSIUNE_PAS_Y;
                lblsPrenume[i].BackColor = Color.White;
                lblsPrenume[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsPrenume[i]);

                lblsCnp[i] = new Label();
                lblsCnp[i].Width = LATIME_CONTROL;
                lblsCnp[i].Text = pacient.Cnp;
                lblsCnp[i].Left = 4 * DIMENSIUNE_PAS_X;
                lblsCnp[i].Top = (i + 2) * DIMENSIUNE_PAS_Y;
                lblsCnp[i].BackColor = Color.White;
                lblsCnp[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsCnp[i]);

                lblsVarsta[i] = new Label();
                lblsVarsta[i].Width = LATIME_CONTROL;
                lblsVarsta[i].Text = pacient.Varsta.ToString();
                lblsVarsta[i].Left = 5 * DIMENSIUNE_PAS_X;
                lblsVarsta[i].Top = (i + 2) * DIMENSIUNE_PAS_Y;
                lblsVarsta[i].BackColor = Color.White;
                lblsVarsta[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsVarsta[i]);


                lblsGreutate[i] = new Label();
                lblsGreutate[i].Width = LATIME_CONTROL;
                lblsGreutate[i].Text = pacient.Greutate.ToString();
                lblsGreutate[i].Left = 6 * DIMENSIUNE_PAS_X;
                lblsGreutate[i].Top = (i + 2) * DIMENSIUNE_PAS_Y;
                lblsGreutate[i].BackColor = Color.White;
                lblsGreutate[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsGreutate[i]);


                lblsInaltime[i] = new Label();
                lblsInaltime[i].Width = LATIME_CONTROL;
                lblsInaltime[i].Text = pacient.Inaltime.ToString();
                lblsInaltime[i].Left = 7 * DIMENSIUNE_PAS_X;
                lblsInaltime[i].Top = (i + 2) * DIMENSIUNE_PAS_Y;
                lblsInaltime[i].BackColor = Color.White;
                lblsInaltime[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsInaltime[i]);


                lblsTemperaturaCorp[i] = new Label();
                lblsTemperaturaCorp[i].Width = LATIME_CONTROL;
                lblsTemperaturaCorp[i].Text = pacient.TemperaturaCorp.ToString();
                lblsTemperaturaCorp[i].Left = 8 * DIMENSIUNE_PAS_X;
                lblsTemperaturaCorp[i].Top = (i + 2) * DIMENSIUNE_PAS_Y;
                lblsTemperaturaCorp[i].BackColor = Color.White;
                lblsTemperaturaCorp[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsTemperaturaCorp[i]);

                lblsGrupaSange[i] = new Label();
                lblsGrupaSange[i].Width = LATIME_CONTROL;
                lblsGrupaSange[i].Text = pacient.Grupa.ToString();
                lblsGrupaSange[i].Left = 9 * DIMENSIUNE_PAS_X;
                lblsGrupaSange[i].Top = (i + 2) * DIMENSIUNE_PAS_Y;
                lblsGrupaSange[i].BackColor = Color.White;
                lblsGrupaSange[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsGrupaSange[i]);

                lblsAfectiuni[i] = new Label();
                lblsAfectiuni[i].Width = LATIME_CONTROL;
                lblsAfectiuni[i].Text = pacient.AfectiuniMed.ToString();
                lblsAfectiuni[i].Left = 10 * DIMENSIUNE_PAS_X;
                lblsAfectiuni[i].Top = (i + 2) * DIMENSIUNE_PAS_Y;
                lblsAfectiuni[i].BackColor = Color.White;
                lblsAfectiuni[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsAfectiuni[i]);

                i++;




            }
        }
        private void AfiseazaSectii()
        {

            SectieSpital[] sectii = adminSectii.GetSectie(out int nrSectii);


            Pacient[] pacienti = adminPacienti.GetPacienti(out int nrPacienti);
            int axaY = nrPacienti + 4; //+2 pentru spatiu extra


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
