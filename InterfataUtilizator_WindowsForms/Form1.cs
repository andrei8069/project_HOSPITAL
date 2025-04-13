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

    public partial class Form1 : MetroForm
    {
        //button inapoi
        private MetroButton btnInapoi;

        //cauta dupa cnp
        private MetroButton btnSearchCNP;

        //refresh date
        private MetroButton btnRefreshDate;

        //PACIENTI 
        Pacienti_FISIERTEXT adminPacienti;
       // private MetroLabel lblTitluPacienti;

        //buton adauga pacient
        private MetroButton btnAdaugaPacienti;



        //afiseaza doar numele la etichete
        private MetroLabel lblCodPacient;
        private MetroLabel lblNume;
        private MetroLabel lblPrenume;
        private MetroLabel lblCnp;
        private MetroLabel lblVarsta;
        private MetroLabel lblGreutate;
        private MetroLabel lblInaltime;
        private MetroLabel lblTemperaturaCorp;

        private MetroLabel lblGrupaSange;
        private MetroLabel lblAfectiuni;

        //stocam datele pe care le afisam
        private MetroLabel[] lblsCodPacient;
        private MetroLabel[] lblsNume;
        private MetroLabel[] lblsPrenume;
        private MetroLabel[] lblsCnp;
        private MetroLabel[] lblsVarsta;
        private MetroLabel[] lblsGreutate;
        private MetroLabel[] lblsInaltime;
        private MetroLabel[] lblsTemperaturaCorp;


        private MetroLabel[] lblsGrupaSange;
        private MetroLabel[] lblsAfectiuni;

        //introducem datele in fiecare camp creat 
        private MetroTextBox txtNume;
        private MetroTextBox txtPrenume;
        private MetroTextBox txtCnp;
        private MetroTextBox txtVarsta;
        private MetroTextBox txtGreutate;
        private MetroTextBox txtInaltime;
        private MetroTextBox txtTemperaturaCorp;

        private ComboBox txtGrupaSange;
        private ComboBox txtAfectiuni;

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

        //dimensiuni pt pacienti si pentru sectii
        private const int LATIME_CONTROL = 120; //latimea pentru fiecare coloana , latime de 200 pt nume , prenume
        private const int DIMENSIUNE_PAS_Y = 50; // distanta dintre pacienti
        private const int DIMENSIUNE_PAS_X = 135; //distanta dintre coloane gen distanta dintre nume si prenume sa fie de 200px

        public Form1()
        {
            InitializeComponent();

            // adaugam MetroStyleManager
            this.StyleManager = new MetroStyleManager();
            this.StyleManager.Theme = MetroThemeStyle.Light;
            this.StyleManager.Style = MetroColorStyle.Blue;

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




            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.StartPosition = FormStartPosition.Manual;
            //this.Location = new Point(50, 50);
            this.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.ForeColor = Color.DarkSlateGray;
            this.Text = "Pacienți";
            this.BackColor = Color.FromArgb(230, 245, 255);

            //MetroButton refresh
            btnRefreshDate = new MetroButton();
            btnRefreshDate.Width = LATIME_CONTROL;
            btnRefreshDate.Text = "Refresh";
            btnRefreshDate.Left = 0;
            btnRefreshDate.Top = 3 * DIMENSIUNE_PAS_Y;
            btnRefreshDate.AutoSize = true;
            btnRefreshDate.Click += BtnRefreshDate_Click;
            this.Controls.Add(btnRefreshDate);

            btnSearchCNP = new MetroButton();
            btnSearchCNP.Width = LATIME_CONTROL;
            btnSearchCNP.Text = "Cautare";
            btnSearchCNP.Left = 0;
            btnSearchCNP.Top = 4 * DIMENSIUNE_PAS_Y;
            btnSearchCNP.AutoSize = true;
            btnSearchCNP.Click += BtnSearchCNP_Click;
            this.Controls.Add(btnSearchCNP);

            btnInapoi = new MetroButton();
            btnInapoi.Width = LATIME_CONTROL;
            btnInapoi.Text = "Inapoi";
            btnInapoi.Left = 0;
            btnInapoi.Top = 5 * DIMENSIUNE_PAS_Y;
            btnInapoi.AutoSize = true;
            btnInapoi.Click += BtnInapoi_Click;
            this.Controls.Add(btnInapoi);

            //metrostyle pt buton refresh
            btnRefreshDate.Theme = MetroThemeStyle.Light;
            btnRefreshDate.Style = MetroColorStyle.Blue;



            //PACIENTII


            //lblTitluPacienti = new MetroLabel();
            //lblTitluPacienti.Text = "Pacienți:";
            //lblTitluPacienti.Left = 0;
            //lblTitluPacienti.AutoSize = true;
            //lblTitluPacienti.Theme = MetroThemeStyle.Light;
            //lblTitluPacienti.Style = MetroColorStyle.Blue;
            //this.Controls.Add(lblTitluPacienti);

            btnAdaugaPacienti = new MetroButton();
            btnAdaugaPacienti.Width = LATIME_CONTROL;
            btnAdaugaPacienti.Text = "Adauga";
            btnAdaugaPacienti.Left = 0;
            btnAdaugaPacienti.Top = 2 * DIMENSIUNE_PAS_Y;
            btnAdaugaPacienti.AutoSize = true;
            btnAdaugaPacienti.Click += BtnAdaugaPacienti_Click;
            btnAdaugaPacienti.Theme = MetroThemeStyle.Light;
            btnAdaugaPacienti.Style = MetroColorStyle.Blue;
            this.Controls.Add(btnAdaugaPacienti);



            lblCodPacient = new MetroLabel();
            lblCodPacient.Width = LATIME_CONTROL;
            lblCodPacient.Text = "Cod";
            lblCodPacient.Left = 1 * DIMENSIUNE_PAS_X;
            lblCodPacient.Top = 1 * DIMENSIUNE_PAS_Y;
            lblCodPacient.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblCodPacient);

            lblNume = new MetroLabel();
            lblNume.Width = LATIME_CONTROL;
            lblNume.Text = "Nume";
            lblNume.Left = 2 * DIMENSIUNE_PAS_X;
            lblNume.Top = 1 * DIMENSIUNE_PAS_Y;
            lblNume.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblNume);

            txtNume = new MetroTextBox();
            txtNume.Left = 2 * DIMENSIUNE_PAS_X;
            txtNume.Top = 2 * DIMENSIUNE_PAS_Y;
            txtNume.Width = LATIME_CONTROL;
            txtNume.Theme = MetroThemeStyle.Light;
            txtNume.Style = MetroColorStyle.Blue;
            txtNume.UseStyleColors = true;
            this.Controls.Add(txtNume);

            lblPrenume = new MetroLabel();
            lblPrenume.Width = LATIME_CONTROL;
            lblPrenume.Text = "Prenume";
            lblPrenume.Left = 3 * DIMENSIUNE_PAS_X;
            lblPrenume.Top = 1 * DIMENSIUNE_PAS_Y;
            lblPrenume.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblPrenume);

            txtPrenume = new MetroTextBox();
            txtPrenume.Left = 3 * DIMENSIUNE_PAS_X;
            txtPrenume.Top = 2 * DIMENSIUNE_PAS_Y;
            txtPrenume.Width = LATIME_CONTROL;
            txtPrenume.Theme = MetroThemeStyle.Light;
            txtPrenume.Style = MetroColorStyle.Blue;
            txtPrenume.UseStyleColors = true;
            this.Controls.Add(txtPrenume);

            lblCnp = new MetroLabel();
            lblCnp.Width = LATIME_CONTROL;
            lblCnp.Text = "Cnp";
            lblCnp.Left = 4 * DIMENSIUNE_PAS_X;
            lblCnp.Top = 1 * DIMENSIUNE_PAS_Y;
            lblCnp.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblCnp);

            txtCnp = new MetroTextBox();
            txtCnp.Left = 4 * DIMENSIUNE_PAS_X;
            txtCnp.Top = 2 * DIMENSIUNE_PAS_Y;
            txtCnp.Width = LATIME_CONTROL;
            txtCnp.Theme = MetroThemeStyle.Light;
            txtCnp.Style = MetroColorStyle.Blue;
            txtCnp.UseStyleColors = true;
            this.Controls.Add(txtCnp);

            lblVarsta = new MetroLabel();
            lblVarsta.Width = LATIME_CONTROL;
            lblVarsta.Text = "Varsta";
            lblVarsta.Left = 5 * DIMENSIUNE_PAS_X;
            lblVarsta.Top = 1 * DIMENSIUNE_PAS_Y;
            lblVarsta.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblVarsta);

            txtVarsta = new MetroTextBox();
            txtVarsta.Left = 5 * DIMENSIUNE_PAS_X;
            txtVarsta.Top = 2 * DIMENSIUNE_PAS_Y;
            txtVarsta.Width = LATIME_CONTROL;
            txtVarsta.Theme = MetroThemeStyle.Light;
            txtVarsta.Style = MetroColorStyle.Blue;
            txtVarsta.UseStyleColors = true;
            this.Controls.Add(txtVarsta);

            lblGreutate = new MetroLabel();
            lblGreutate.Width = LATIME_CONTROL;
            lblGreutate.Text = "Greutate";
            lblGreutate.Left = 6 * DIMENSIUNE_PAS_X;
            lblGreutate.Top = 1 * DIMENSIUNE_PAS_Y;
            lblGreutate.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblGreutate);

            txtGreutate = new MetroTextBox();
            txtGreutate.Left = 6 * DIMENSIUNE_PAS_X;
            txtGreutate.Top = 2 * DIMENSIUNE_PAS_Y;
            txtGreutate.Width = LATIME_CONTROL;
            txtGreutate.Theme = MetroThemeStyle.Light;
            txtGreutate.Style = MetroColorStyle.Blue;
            txtGreutate.UseStyleColors = true;
            this.Controls.Add(txtGreutate);

            lblInaltime = new MetroLabel();
            lblInaltime.Width = LATIME_CONTROL;
            lblInaltime.Text = "Inaltime";
            lblInaltime.Left = 7 * DIMENSIUNE_PAS_X;
            lblInaltime.Top = 1 * DIMENSIUNE_PAS_Y;
            lblInaltime.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblInaltime);

            txtInaltime = new MetroTextBox();
            txtInaltime.Left = 7 * DIMENSIUNE_PAS_X;
            txtInaltime.Top = 2 * DIMENSIUNE_PAS_Y;
            txtInaltime.Width = LATIME_CONTROL;
            txtInaltime.Theme = MetroThemeStyle.Light;
            txtInaltime.Style = MetroColorStyle.Blue;
            txtInaltime.UseStyleColors = true;
            this.Controls.Add(txtInaltime);

            lblTemperaturaCorp = new MetroLabel();
            lblTemperaturaCorp.Width = LATIME_CONTROL;
            lblTemperaturaCorp.Text = "Temperatura";
            lblTemperaturaCorp.Left = 8 * DIMENSIUNE_PAS_X;
            lblTemperaturaCorp.Top = 1 * DIMENSIUNE_PAS_Y;
            lblTemperaturaCorp.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblTemperaturaCorp);

            txtTemperaturaCorp = new MetroTextBox();
            txtTemperaturaCorp.Left = 8 * DIMENSIUNE_PAS_X;
            txtTemperaturaCorp.Top = 2 * DIMENSIUNE_PAS_Y;
            txtTemperaturaCorp.Width = LATIME_CONTROL;
            txtTemperaturaCorp.Theme = MetroThemeStyle.Light;
            txtTemperaturaCorp.Style = MetroColorStyle.Blue;
            txtTemperaturaCorp.UseStyleColors = true;
            this.Controls.Add(txtTemperaturaCorp);

            lblGrupaSange = new MetroLabel();
            lblGrupaSange.Width = LATIME_CONTROL;
            lblGrupaSange.Text = "GrupaSange";
            lblGrupaSange.Left = 9 * DIMENSIUNE_PAS_X;
            lblGrupaSange.Top = 1 * DIMENSIUNE_PAS_Y;
            lblGrupaSange.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblGrupaSange);

            txtGrupaSange = new ComboBox();
            txtGrupaSange.Left = 9 * DIMENSIUNE_PAS_X;
            txtGrupaSange.Top = 2 * DIMENSIUNE_PAS_Y;
            txtGrupaSange.Width = LATIME_CONTROL;
            txtGrupaSange.Items.AddRange(Enum.GetNames(typeof(GrupaSangePacient)));
            this.Controls.Add(txtGrupaSange);

            lblAfectiuni = new MetroLabel();
            lblAfectiuni.Width = LATIME_CONTROL;
            lblAfectiuni.Text = "Afectiuni";
            lblAfectiuni.Left = 10 * DIMENSIUNE_PAS_X;
            lblAfectiuni.Top = 1 * DIMENSIUNE_PAS_Y;
            lblAfectiuni.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblAfectiuni);

            txtAfectiuni = new ComboBox();
            txtAfectiuni.Left = 10 * DIMENSIUNE_PAS_X;
            txtAfectiuni.Top = 2 * DIMENSIUNE_PAS_Y;
            txtAfectiuni.Width = LATIME_CONTROL;
            txtAfectiuni.Items.AddRange(Enum.GetNames(typeof(AfectiuniMedicale)));
            this.Controls.Add(txtAfectiuni);



        }





        private void BtnRefreshDate_Click(object sender, EventArgs e)
        {
            //this.Controls.Clear();
           // InitializareInterfata();
            AfiseazaPacienti();


        }
        private void BtnSearchCNP_Click(object sender, EventArgs e)
        {
            Cautare_Dupa_CNP cautatorCNP = new Cautare_Dupa_CNP();
            cautatorCNP.Show();
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






        private void BtnAdaugaPacienti_Click(object sender, EventArgs e)
        {

            string eroare = "";

            lblNume.Style = MetroColorStyle.Blue;
            lblNume.UseStyleColors = true;

            lblPrenume.Style = MetroColorStyle.Blue;
            lblPrenume.UseStyleColors = true;

            lblCnp.Style = MetroColorStyle.Blue;
            lblCnp.UseStyleColors = true;

            lblVarsta.Style = MetroColorStyle.Blue;
            lblVarsta.UseStyleColors = true;

            lblGreutate.Style = MetroColorStyle.Blue;
            lblGreutate.UseStyleColors = true;

            lblInaltime.Style = MetroColorStyle.Blue;
            lblInaltime.UseStyleColors = true;

            lblTemperaturaCorp.Style = MetroColorStyle.Blue;
            lblTemperaturaCorp.UseStyleColors = true;

            lblGrupaSange.Style = MetroColorStyle.Blue;
            lblGrupaSange.UseStyleColors = true;

            lblAfectiuni.Style = MetroColorStyle.Blue;
            lblAfectiuni.UseStyleColors = true;


            // Nume
            string nume = txtNume.Text.Trim();
            if (nume.Length < 2)
            {
                eroare += "Numele trebuie sa aiba cel putin 2 caractere.\n";

                lblNume.Style = MetroColorStyle.Red;
                lblNume.UseStyleColors = true;
            }


            // Prenume
            string prenume = txtPrenume.Text.Trim();
            if (prenume.Length < 2)
            {
                eroare += "Prenumele trebuie sa aiba cel putin 2 caractere.\n";

                lblPrenume.Style = MetroColorStyle.Red;
                lblPrenume.UseStyleColors = true;
            }


            // CNP
            string cnp = txtCnp.Text.Trim();
            if (cnp.Length != CNP_LUNGIME)
            {
                eroare += "CNP-ul trebuie sa aiba 13 caractere.\n";

                lblCnp.Style = MetroColorStyle.Red;
                lblCnp.UseStyleColors = true;
            }


            // Varsta
            int varsta;
            if (!int.TryParse(txtVarsta.Text.Trim(), out varsta) || varsta < VARSTA_MIN || varsta > VARSTA_MAX)
            {
                eroare += $"Varsta trebuie sa fie intre {VARSTA_MIN} si {VARSTA_MAX}.\n";

                lblVarsta.Style = MetroColorStyle.Red;
                lblVarsta.UseStyleColors = true;
            }


            // Greutate
            double greutate;
            if (!double.TryParse(txtGreutate.Text.Trim(), out greutate) || greutate < GREUTATE_MIN || greutate > GREUTATE_MAX)
            {
                eroare += $"Greutatea trebuie sa fie intre {GREUTATE_MIN} si {GREUTATE_MAX}.\n";

                lblGreutate.Style = MetroColorStyle.Red;
                lblGreutate.UseStyleColors = true;
            }


            // Inaltime
            double inaltime;
            if (!double.TryParse(txtInaltime.Text.Trim(), out inaltime) || inaltime < INALTIME_MIN || inaltime > INALTIME_MAX)
            {
                eroare += $"Inaltimea trebuie sa fie intre {INALTIME_MIN} si {INALTIME_MAX}.\n";

                lblInaltime.Style = MetroColorStyle.Red;
                lblInaltime.UseStyleColors = true;
            }


            // Temperatura corp
            double temperaturaCorp;
            if (!double.TryParse(txtTemperaturaCorp.Text.Trim(), out temperaturaCorp) || temperaturaCorp < TEMP_MIN || temperaturaCorp > TEMP_MAX)
            {
                eroare += $"Temperatura corpului trebuie sa fie intre {TEMP_MIN} si {TEMP_MAX}.\n";

                lblTemperaturaCorp.Style = MetroColorStyle.Red;
                lblTemperaturaCorp.UseStyleColors = true;
            }


            // Grupa sange
            GrupaSangePacient grupaSange = GrupaSangePacient.Nespecificat;
            if (Enum.TryParse(txtGrupaSange.Text, out grupaSange)==false)
            {
                eroare += "Grupa de sange selectata este invalida.\n";
                lblGrupaSange.Style = MetroColorStyle.Red;
                lblGrupaSange.UseStyleColors = true;
            }



            // Afectiuni
            AfectiuniMedicale afectiuni = AfectiuniMedicale.Nespecificat;
            if (Enum.TryParse(txtAfectiuni.Text, out afectiuni)==false)
            {
                eroare += "Afectiunea selectata este invalida.\n";
                lblAfectiuni.Style = MetroColorStyle.Red;
                lblAfectiuni.UseStyleColors = true;
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


        private void Form1_Load(object sender, EventArgs e)
        {

            AfiseazaPacienti();

        }
        private void AfiseazaPacienti()
        {


            Pacient[] pacienti = adminPacienti.GetPacienti(out int nrPacienti);
            //MessageBox.Show($"nrPacienti: {nrPacienti}, null? {pacienti == null}");
            //MessageBox.Show($"Lungime array: {pacienti.Length}, nrPacienti: {nrPacienti}");

            lblsCodPacient = new MetroLabel[nrPacienti];
            lblsNume = new MetroLabel[nrPacienti];
            lblsPrenume = new MetroLabel[nrPacienti];
            lblsCnp = new MetroLabel[nrPacienti];
            lblsVarsta = new MetroLabel[nrPacienti];
            lblsGreutate = new MetroLabel[nrPacienti];
            lblsInaltime = new MetroLabel[nrPacienti];
            lblsTemperaturaCorp = new MetroLabel[nrPacienti];
            lblsGrupaSange = new MetroLabel[nrPacienti];
            lblsAfectiuni = new MetroLabel[nrPacienti];

            int i = 0;
            foreach (Pacient pacient in pacienti)
            {
                lblsCodPacient[i] = new MetroLabel();
                lblsCodPacient[i].Width = LATIME_CONTROL;
                lblsCodPacient[i].Text = pacient.CodPacient.ToString();
                lblsCodPacient[i].Left = 1 * DIMENSIUNE_PAS_X;
                lblsCodPacient[i].Top = (i + 3) * DIMENSIUNE_PAS_Y;
                lblsCodPacient[i].BackColor = Color.White; //background alb in spate
                                                           // lblsCodPacient[i].BorderStyle = BorderStyle.FixedSingle; //border , sa arate ca un tabel
                this.Controls.Add(lblsCodPacient[i]);

                lblsNume[i] = new MetroLabel();
                lblsNume[i].Width = LATIME_CONTROL;
                lblsNume[i].Text = pacient.Nume;
                lblsNume[i].Left = 2 * DIMENSIUNE_PAS_X;
                lblsNume[i].Top = (i + 3) * DIMENSIUNE_PAS_Y;
                lblsNume[i].BackColor = Color.White;
                // lblsNume[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsNume[i]);


                lblsPrenume[i] = new MetroLabel();
                lblsPrenume[i].Width = LATIME_CONTROL;
                lblsPrenume[i].Text = pacient.Prenume;
                lblsPrenume[i].Left = 3 * DIMENSIUNE_PAS_X;
                lblsPrenume[i].Top = (i + 3) * DIMENSIUNE_PAS_Y;
                lblsPrenume[i].BackColor = Color.White;
                // lblsPrenume[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsPrenume[i]);

                lblsCnp[i] = new MetroLabel();
                lblsCnp[i].Width = LATIME_CONTROL;
                lblsCnp[i].Text = pacient.Cnp;
                lblsCnp[i].Left = 4 * DIMENSIUNE_PAS_X;
                lblsCnp[i].Top = (i + 3) * DIMENSIUNE_PAS_Y;
                lblsCnp[i].BackColor = Color.White;
                // lblsCnp[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsCnp[i]);

                lblsVarsta[i] = new MetroLabel();
                lblsVarsta[i].Width = LATIME_CONTROL;
                lblsVarsta[i].Text = pacient.Varsta.ToString();
                lblsVarsta[i].Left = 5 * DIMENSIUNE_PAS_X;
                lblsVarsta[i].Top = (i + 3) * DIMENSIUNE_PAS_Y;
                lblsVarsta[i].BackColor = Color.White;
                // lblsVarsta[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsVarsta[i]);


                lblsGreutate[i] = new MetroLabel();
                lblsGreutate[i].Width = LATIME_CONTROL;
                lblsGreutate[i].Text = pacient.Greutate.ToString();
                lblsGreutate[i].Left = 6 * DIMENSIUNE_PAS_X;
                lblsGreutate[i].Top = (i + 3) * DIMENSIUNE_PAS_Y;
                lblsGreutate[i].BackColor = Color.White;
                //  lblsGreutate[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsGreutate[i]);


                lblsInaltime[i] = new MetroLabel();
                lblsInaltime[i].Width = LATIME_CONTROL;
                lblsInaltime[i].Text = pacient.Inaltime.ToString();
                lblsInaltime[i].Left = 7 * DIMENSIUNE_PAS_X;
                lblsInaltime[i].Top = (i + 3) * DIMENSIUNE_PAS_Y;
                lblsInaltime[i].BackColor = Color.White;
                //   lblsInaltime[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsInaltime[i]);


                lblsTemperaturaCorp[i] = new MetroLabel();
                lblsTemperaturaCorp[i].Width = LATIME_CONTROL;
                lblsTemperaturaCorp[i].Text = pacient.TemperaturaCorp.ToString();
                lblsTemperaturaCorp[i].Left = 8 * DIMENSIUNE_PAS_X;
                lblsTemperaturaCorp[i].Top = (i + 3) * DIMENSIUNE_PAS_Y;
                lblsTemperaturaCorp[i].BackColor = Color.White;
                //   lblsTemperaturaCorp[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsTemperaturaCorp[i]);

                lblsGrupaSange[i] = new MetroLabel();
                lblsGrupaSange[i].Width = LATIME_CONTROL;
                lblsGrupaSange[i].Text = pacient.Grupa.ToString();
                lblsGrupaSange[i].Left = 9 * DIMENSIUNE_PAS_X;
                lblsGrupaSange[i].Top = (i + 3) * DIMENSIUNE_PAS_Y;
                lblsGrupaSange[i].BackColor = Color.White;
                //    lblsGrupaSange[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsGrupaSange[i]);

                lblsAfectiuni[i] = new MetroLabel();
                lblsAfectiuni[i].Width = LATIME_CONTROL;
                lblsAfectiuni[i].Text = pacient.AfectiuniMed.ToString();
                lblsAfectiuni[i].Left = 10 * DIMENSIUNE_PAS_X;
                lblsAfectiuni[i].Top = (i + 3) * DIMENSIUNE_PAS_Y;
                lblsAfectiuni[i].BackColor = Color.White;
                //  lblsAfectiuni[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lblsAfectiuni[i]);

                i++;




            }
        }
       
        }
    }
