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
        private Button btnInapoi;

        //cauta dupa cnp
        private Button btnSearchCNP;

        //refresh date
        private Button btnRefreshDate;

        //modifica pacient ales
        private Button btnModifyPacient;

        //PACIENTI 
        Pacienti_FISIERTEXT adminPacienti;
        // private MetroLabel lblTitluPacienti;

        //buton adauga pacient
        private Button btnAdaugaPacienti;

        //buton asociere pacient-sectie
        private Button btnAsocierePacientSectie;

        //buton sterge pacient
        private Button btnStergePacient;

 



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
        private MetroLabel lblSectieInternare;

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
        private MetroLabel[] lblsSectieInternare;


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


            List<Pacient> pacienti = adminPacienti.GetPacienti();
            int nrPacienti = pacienti.Count;

            this.Theme = MetroThemeStyle.Light;
            this.Style = MetroColorStyle.Default;

            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.StartPosition = FormStartPosition.Manual;
            //this.Location = new Point(50, 50);
            this.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.ForeColor = Color.DarkSlateGray;
            this.Text = "Pacienți";
            this.BackColor = Color.FromArgb(230, 245, 255);

            //MetroButton refresh
            btnRefreshDate = new Button();
            btnRefreshDate.Width = LATIME_CONTROL;
            btnRefreshDate.Text = "Refresh";
            btnRefreshDate.Left = 0;
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

            btnSearchCNP = new Button();
            btnSearchCNP.Width = LATIME_CONTROL;
            btnSearchCNP.Text = "Căutare";
            btnSearchCNP.Left = 0;
            btnSearchCNP.Top = 4 * DIMENSIUNE_PAS_Y;
            btnSearchCNP.AutoSize = true;
            btnSearchCNP.BackColor = Color.SteelBlue;
            btnSearchCNP.ForeColor = Color.White;
            btnSearchCNP.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnSearchCNP.FlatStyle = FlatStyle.Flat;
            btnSearchCNP.FlatAppearance.BorderSize = 0;
            btnSearchCNP.Cursor = Cursors.Hand;
            btnSearchCNP.Click += BtnSearchCNP_Click;
            this.Controls.Add(btnSearchCNP);



            btnModifyPacient = new Button();
            btnModifyPacient.Width = LATIME_CONTROL;
            btnModifyPacient.Text = "Modifică";
            btnModifyPacient.Left = 0;
            btnModifyPacient.Top = 5 * DIMENSIUNE_PAS_Y;
            btnModifyPacient.AutoSize = true;
            btnModifyPacient.BackColor = Color.SteelBlue;
            btnModifyPacient.ForeColor = Color.White;
            btnModifyPacient.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnModifyPacient.FlatStyle = FlatStyle.Flat;
            btnModifyPacient.FlatAppearance.BorderSize = 0;
            btnModifyPacient.Cursor = Cursors.Hand;
            btnModifyPacient.Click += BtnModifyPacient_Click;
            this.Controls.Add(btnModifyPacient);

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

            btnStergePacient = new Button();
            btnStergePacient.Width = LATIME_CONTROL;
            btnStergePacient.Text = "Șterge";
            btnStergePacient.Left = 0;
            btnStergePacient.Top = 7 * DIMENSIUNE_PAS_Y;
            btnStergePacient.AutoSize = true;
            btnStergePacient.BackColor = Color.Red;
            btnStergePacient.ForeColor = Color.White;
            btnStergePacient.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnStergePacient.FlatStyle = FlatStyle.Flat;
            btnStergePacient.FlatAppearance.BorderSize = 0;
            btnStergePacient.Cursor = Cursors.Hand;
            btnStergePacient.Click += BtnStergePacient_Click;
            this.Controls.Add(btnStergePacient);

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




            //PACIENTII


            btnAdaugaPacienti = new Button();
            btnAdaugaPacienti.Width = LATIME_CONTROL;
            btnAdaugaPacienti.Text = "Adaugă";
            btnAdaugaPacienti.Left = 0;
            btnAdaugaPacienti.Top = 2 * DIMENSIUNE_PAS_Y;
            btnAdaugaPacienti.AutoSize = true;
            btnAdaugaPacienti.BackColor = Color.SteelBlue;
            btnAdaugaPacienti.ForeColor = Color.White;
            btnAdaugaPacienti.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnAdaugaPacienti.FlatStyle = FlatStyle.Flat;
            btnAdaugaPacienti.FlatAppearance.BorderSize = 0;
            btnAdaugaPacienti.Cursor = Cursors.Hand;
            btnAdaugaPacienti.Click += BtnAdaugaPacienti_Click;
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


            lblPrenume = new MetroLabel();
            lblPrenume.Width = LATIME_CONTROL;
            lblPrenume.Text = "Prenume";
            lblPrenume.Left = 3 * DIMENSIUNE_PAS_X;
            lblPrenume.Top = 1 * DIMENSIUNE_PAS_Y;
            lblPrenume.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblPrenume);



            lblCnp = new MetroLabel();
            lblCnp.Width = LATIME_CONTROL;
            lblCnp.Text = "Cnp";
            lblCnp.Left = 4 * DIMENSIUNE_PAS_X;
            lblCnp.Top = 1 * DIMENSIUNE_PAS_Y;
            lblCnp.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblCnp);


            lblVarsta = new MetroLabel();
            lblVarsta.Width = LATIME_CONTROL;
            lblVarsta.Text = "Varsta";
            lblVarsta.Left = 5 * DIMENSIUNE_PAS_X;
            lblVarsta.Top = 1 * DIMENSIUNE_PAS_Y;
            lblVarsta.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblVarsta);


            lblGreutate = new MetroLabel();
            lblGreutate.Width = LATIME_CONTROL;
            lblGreutate.Text = "Greutate";
            lblGreutate.Left = 6 * DIMENSIUNE_PAS_X;
            lblGreutate.Top = 1 * DIMENSIUNE_PAS_Y;
            lblGreutate.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblGreutate);


            lblInaltime = new MetroLabel();
            lblInaltime.Width = LATIME_CONTROL;
            lblInaltime.Text = "Inaltime";
            lblInaltime.Left = 7 * DIMENSIUNE_PAS_X;
            lblInaltime.Top = 1 * DIMENSIUNE_PAS_Y;
            lblInaltime.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblInaltime);


            lblTemperaturaCorp = new MetroLabel();
            lblTemperaturaCorp.Width = LATIME_CONTROL;
            lblTemperaturaCorp.Text = "Temperatura";
            lblTemperaturaCorp.Left = 8 * DIMENSIUNE_PAS_X;
            lblTemperaturaCorp.Top = 1 * DIMENSIUNE_PAS_Y;
            lblTemperaturaCorp.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblTemperaturaCorp);


            lblGrupaSange = new MetroLabel();
            lblGrupaSange.Width = LATIME_CONTROL;
            lblGrupaSange.Text = "GrupaSange";
            lblGrupaSange.Left = 9 * DIMENSIUNE_PAS_X;
            lblGrupaSange.Top = 1 * DIMENSIUNE_PAS_Y;
            lblGrupaSange.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblGrupaSange);



            lblAfectiuni = new MetroLabel();
            lblAfectiuni.Width = LATIME_CONTROL;
            lblAfectiuni.Text = "Afectiuni";
            lblAfectiuni.Left = 10 * DIMENSIUNE_PAS_X;
            lblAfectiuni.Top = 1 * DIMENSIUNE_PAS_Y;
            lblAfectiuni.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblAfectiuni);

            lblSectieInternare = new MetroLabel();
            lblSectieInternare.Width = LATIME_CONTROL;
            lblSectieInternare.Text = "Sectie";
            lblSectieInternare.Left = 11 * DIMENSIUNE_PAS_X;
            lblSectieInternare.Top = 1 * DIMENSIUNE_PAS_Y;
            lblSectieInternare.ForeColor = Color.DarkSlateGray;
            this.Controls.Add(lblSectieInternare);





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

        private void BtnModifyPacient_Click(object sender, EventArgs e)
        {
            ModificaPacient modificatorPacient = new ModificaPacient();
            modificatorPacient.Show();
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
            AdaugaPacient adaugatorPacient = new AdaugaPacient();
            adaugatorPacient.Show();
        }

        private void BtnAsocierePacientSectie_Click(object sender, EventArgs e)
        {
            AsocierePacientSectie formAsociere = new AsocierePacientSectie();
            formAsociere.Show();
        }

        private void BtnStergePacient_Click(object sender, EventArgs e)
        {
            StergePacient formSterge = new StergePacient();
            formSterge.Show();
        }




        private void Form1_Load(object sender, EventArgs e)
        {

            AfiseazaPacienti();

        }

        private void StergeEtichetePacienti()
        {
            void RemoveLabels(MetroLabel[] labels)
            {
                if (labels != null)
                {
                    foreach (var lbl in labels)
                        this.Controls.Remove(lbl);
                }
            }

            RemoveLabels(lblsCodPacient);
            RemoveLabels(lblsNume);
            RemoveLabels(lblsPrenume);
            RemoveLabels(lblsCnp);
            RemoveLabels(lblsVarsta);
            RemoveLabels(lblsGreutate);
            RemoveLabels(lblsInaltime);
            RemoveLabels(lblsTemperaturaCorp);
            RemoveLabels(lblsGrupaSange);
            RemoveLabels(lblsAfectiuni);
            RemoveLabels(lblsSectieInternare);
        }

        private void AfiseazaPacienti()
        {

            StergeEtichetePacienti();

            List<Pacient> pacienti = adminPacienti.GetPacienti();
            int nrPacienti = pacienti.Count;

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
            lblsSectieInternare = new MetroLabel[nrPacienti];

            int i = 0;
            int valoare = 2;
            foreach (Pacient pacient in pacienti)
            {
                lblsCodPacient[i] = new MetroLabel();
                lblsCodPacient[i].Width = LATIME_CONTROL;
                lblsCodPacient[i].Text = pacient.CodPacient.ToString();
                lblsCodPacient[i].Left = 1 * DIMENSIUNE_PAS_X;
                lblsCodPacient[i].Top = (i + valoare) * DIMENSIUNE_PAS_Y;
                lblsCodPacient[i].BackColor = Color.White; //background alb in spate
                this.Controls.Add(lblsCodPacient[i]);

                lblsNume[i] = new MetroLabel();
                lblsNume[i].Width = LATIME_CONTROL;
                lblsNume[i].Text = pacient.Nume;
                lblsNume[i].Left = 2 * DIMENSIUNE_PAS_X;
                lblsNume[i].Top = (i + valoare) * DIMENSIUNE_PAS_Y;
                lblsNume[i].BackColor = Color.White;
                this.Controls.Add(lblsNume[i]);


                lblsPrenume[i] = new MetroLabel();
                lblsPrenume[i].Width = LATIME_CONTROL;
                lblsPrenume[i].Text = pacient.Prenume;
                lblsPrenume[i].Left = 3 * DIMENSIUNE_PAS_X;
                lblsPrenume[i].Top = (i + valoare) * DIMENSIUNE_PAS_Y;
                lblsPrenume[i].BackColor = Color.White;
                this.Controls.Add(lblsPrenume[i]);

                lblsCnp[i] = new MetroLabel();
                lblsCnp[i].Width = LATIME_CONTROL;
                lblsCnp[i].Text = pacient.Cnp;
                lblsCnp[i].Left = 4 * DIMENSIUNE_PAS_X;
                lblsCnp[i].Top = (i + valoare) * DIMENSIUNE_PAS_Y;
                lblsCnp[i].BackColor = Color.White;
                this.Controls.Add(lblsCnp[i]);

                lblsVarsta[i] = new MetroLabel();
                lblsVarsta[i].Width = LATIME_CONTROL;
                lblsVarsta[i].Text = pacient.Varsta.ToString();
                lblsVarsta[i].Left = 5 * DIMENSIUNE_PAS_X;
                lblsVarsta[i].Top = (i + valoare) * DIMENSIUNE_PAS_Y;
                lblsVarsta[i].BackColor = Color.White;
                this.Controls.Add(lblsVarsta[i]);


                lblsGreutate[i] = new MetroLabel();
                lblsGreutate[i].Width = LATIME_CONTROL;
                lblsGreutate[i].Text = pacient.Greutate.ToString();
                lblsGreutate[i].Left = 6 * DIMENSIUNE_PAS_X;
                lblsGreutate[i].Top = (i + valoare) * DIMENSIUNE_PAS_Y;
                lblsGreutate[i].BackColor = Color.White;
                this.Controls.Add(lblsGreutate[i]);


                lblsInaltime[i] = new MetroLabel();
                lblsInaltime[i].Width = LATIME_CONTROL;
                lblsInaltime[i].Text = pacient.Inaltime.ToString();
                lblsInaltime[i].Left = 7 * DIMENSIUNE_PAS_X;
                lblsInaltime[i].Top = (i + valoare) * DIMENSIUNE_PAS_Y;
                lblsInaltime[i].BackColor = Color.White;
                this.Controls.Add(lblsInaltime[i]);


                lblsTemperaturaCorp[i] = new MetroLabel();
                lblsTemperaturaCorp[i].Width = LATIME_CONTROL;
                lblsTemperaturaCorp[i].Text = pacient.TemperaturaCorp.ToString();
                lblsTemperaturaCorp[i].Left = 8 * DIMENSIUNE_PAS_X;
                lblsTemperaturaCorp[i].Top = (i + valoare) * DIMENSIUNE_PAS_Y;
                lblsTemperaturaCorp[i].BackColor = Color.White;
                this.Controls.Add(lblsTemperaturaCorp[i]);

                lblsGrupaSange[i] = new MetroLabel();
                lblsGrupaSange[i].Width = LATIME_CONTROL;
                lblsGrupaSange[i].Text = pacient.Grupa.ToString();
                lblsGrupaSange[i].Left = 9 * DIMENSIUNE_PAS_X;
                lblsGrupaSange[i].Top = (i + valoare) * DIMENSIUNE_PAS_Y;
                lblsGrupaSange[i].BackColor = Color.White;
                this.Controls.Add(lblsGrupaSange[i]);

                lblsAfectiuni[i] = new MetroLabel();
                lblsAfectiuni[i].Width = LATIME_CONTROL;
                lblsAfectiuni[i].Text = pacient.AfectiuniMed.ToString();
                lblsAfectiuni[i].Left = 10 * DIMENSIUNE_PAS_X;
                lblsAfectiuni[i].Top = (i + valoare) * DIMENSIUNE_PAS_Y;
                lblsAfectiuni[i].BackColor = Color.White;
                this.Controls.Add(lblsAfectiuni[i]);

                // Afisam sectia internare
                lblsSectieInternare[i] = new MetroLabel();
                lblsSectieInternare[i].Width = LATIME_CONTROL;
                string textSectie = pacient.CodSectieInternare == 0 ? "Neasociat" : pacient.CodSectieInternare.ToString();
                lblsSectieInternare[i].Text = textSectie;
                lblsSectieInternare[i].Left = 11 * DIMENSIUNE_PAS_X;
                lblsSectieInternare[i].Top = (i + valoare) * DIMENSIUNE_PAS_Y;
                lblsSectieInternare[i].BackColor = Color.White;
                this.Controls.Add(lblsSectieInternare[i]);

                i++;




            }
        }



    }
}
