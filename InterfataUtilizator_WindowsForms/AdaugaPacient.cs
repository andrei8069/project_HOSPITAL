using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using LibrarieModele;
using NivelStocareDate;
using System.Linq;

namespace InterfataUtilizator_WindowsForms
{
    public partial class AdaugaPacient : MetroForm
    {
        // Controale
        private MetroTextBox txtNume, txtPrenume, txtCnp, txtVarsta, txtGreutate, txtInaltime, txtTemperatura;
        private ComboBox cmbGrupaSange;
        private ListBox lstAfectiuni;
        private Button btnAdauga, btnInapoi;

        // Label-uri
        private MetroLabel lblNume, lblPrenume, lblCnp, lblVarsta, lblGreutate, lblInaltime, lblTemperatura, lblGrupa, lblAfectiuni;

        private const int LABEL_WIDTH = 140;
        private const int CONTROL_WIDTH = 200;
        private const int LINE_HEIGHT = 40;


        private Pacienti_FISIERTEXT adminPacienti;

        public AdaugaPacient()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = "";
            MetroLabel lblTitlu = new MetroLabel()
            {
                Text = "Adaugă Pacient",
                FontSize = MetroFramework.MetroLabelSize.Tall,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Left = 40,
                Top = 20,
                AutoSize = true
            };
            this.Controls.Add(lblTitlu);

            this.Size = new Size(600, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            var styleManager = new MetroFramework.Components.MetroStyleManager();
            styleManager.Owner = this;
            styleManager.Theme = MetroThemeStyle.Light;
            styleManager.Style = MetroColorStyle.Blue;
            this.StyleManager = styleManager;

            string locatie = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string fisier = ConfigurationManager.AppSettings["NumeFisierPacienti"];
            adminPacienti = new Pacienti_FISIERTEXT(Path.Combine(locatie, fisier));

            int xLabel = 40;
            int xInput = xLabel + LABEL_WIDTH + 10;
            int y = 80;

            lblNume = AddLabel("Nume:", xLabel, y);
            txtNume = AddTextBox(xInput, y);

            y += LINE_HEIGHT;
            lblPrenume = AddLabel("Prenume:", xLabel, y);
            txtPrenume = AddTextBox(xInput, y);

            y += LINE_HEIGHT;
            lblCnp = AddLabel("CNP:", xLabel, y);
            txtCnp = AddTextBox(xInput, y);

            y += LINE_HEIGHT;
            lblVarsta = AddLabel("Vârstă:", xLabel, y);
            txtVarsta = AddTextBox(xInput, y);

            y += LINE_HEIGHT;
            lblGreutate = AddLabel("Greutate (kg):", xLabel, y);
            txtGreutate = AddTextBox(xInput, y);

            y += LINE_HEIGHT;
            lblInaltime = AddLabel("Înălțime (cm):", xLabel, y);
            txtInaltime = AddTextBox(xInput, y);

            y += LINE_HEIGHT;
            lblTemperatura = AddLabel("Temperatură (°C):", xLabel, y);
            txtTemperatura = AddTextBox(xInput, y);

            y += LINE_HEIGHT;
            lblGrupa = AddLabel("Grupa Sânge:", xLabel, y);
            cmbGrupaSange = new ComboBox()
            {
                Left = xInput,
                Top = y,
                Width = CONTROL_WIDTH,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbGrupaSange.Items.AddRange(Enum.GetNames(typeof(GrupaSangePacient)));
            this.Controls.Add(cmbGrupaSange);

            y += LINE_HEIGHT;
            lblAfectiuni = AddLabel("Afecțiuni:", xLabel, y);
            lstAfectiuni = new ListBox()
            {
                Left = xInput,
                Top = y,
                Width = CONTROL_WIDTH,
                Height = 80,
                SelectionMode = SelectionMode.MultiSimple
            };
            lstAfectiuni.Items.AddRange(Enum.GetNames(typeof(AfectiuniMedicale)));
            this.Controls.Add(lstAfectiuni);

            y += 120;
            btnAdauga = new Button()
            {
                Text = "Adaugă",
                Width = 120,
                Height = 50,
                Left = xInput,
                Top = y,
                BackColor = Color.MediumSeaGreen,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAdauga.FlatAppearance.BorderSize = 0;
            btnAdauga.Click += BtnAdauga_Click;
            this.Controls.Add(btnAdauga);

            btnInapoi = new Button()
            {
                Text = "Înapoi",
                Width = 120,
                Height = 50,
                Left = xInput + 140,
                Top = y,
                BackColor = Color.Crimson,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnInapoi.FlatAppearance.BorderSize = 0;
            btnInapoi.Click += BtnInapoi_Click;
            this.Controls.Add(btnInapoi);
        }

        private MetroLabel AddLabel(string text, int x, int y)
        {
            var lbl = new MetroLabel()
            {
                Text = text,
                Left = x,
                Top = y,
                Width = LABEL_WIDTH,
                FontSize = MetroFramework.MetroLabelSize.Medium
            };
            this.Controls.Add(lbl);
            return lbl;
        }

        private MetroTextBox AddTextBox(int x, int y)
        {
            var txt = new MetroTextBox()
            {
                Left = x,
                Top = y,
                Width = CONTROL_WIDTH,
                Theme = MetroThemeStyle.Light,
                Style = MetroColorStyle.Blue,
                UseStyleColors = true
            };
            this.Controls.Add(txt);
            return txt;
        }

        private void ColorError(MetroLabel label, bool eroare)
        {
            label.Style = eroare ? MetroColorStyle.Red : MetroColorStyle.Blue;
            label.UseStyleColors = true;
        }

        private void BtnInapoi_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form formPacienti = Application.OpenForms["Form1"];
            if (formPacienti != null) formPacienti.Show();
            else new Form1().Show();
            this.Close();
        }

        private void BtnAdauga_Click(object sender, EventArgs e)
        {
            List<Pacient> pacienti = adminPacienti.GetPacienti();

            string nume = txtNume.Text.Trim();
            string prenume = txtPrenume.Text.Trim();
            string cnp = txtCnp.Text.Trim();
            string varstaStr = txtVarsta.Text.Trim();
            string greutateStr = txtGreutate.Text.Trim();
            string inaltimeStr = txtInaltime.Text.Trim();
            string temperaturaStr = txtTemperatura.Text.Trim();
            string grupa = cmbGrupaSange.SelectedItem?.ToString();
            List<string> afectiuni = lstAfectiuni.SelectedItems.Cast<string>().ToList();

            var (valid, mesaj) = adminPacienti.VerificaDatePacient(
                nume, prenume, cnp, varstaStr, greutateStr, inaltimeStr, temperaturaStr,
                grupa, afectiuni, pacienti
            );

            // Validare 
            ColorError(lblNume, string.IsNullOrWhiteSpace(nume) || nume.Length < 2);
            ColorError(lblPrenume, string.IsNullOrWhiteSpace(prenume) || prenume.Length < 2);
            ColorError(lblCnp, cnp.Length != 13);
            ColorError(lblVarsta, !int.TryParse(varstaStr, out int varsta) || varsta < 0 || varsta > 120);
            ColorError(lblGreutate, !double.TryParse(greutateStr, out double greutate) || greutate < 2 || greutate > 300);
            ColorError(lblInaltime, !double.TryParse(inaltimeStr, out double inaltime) || inaltime < 30 || inaltime > 250);
            ColorError(lblTemperatura, !double.TryParse(temperaturaStr, out double temp) || temp < 30 || temp > 45);
            ColorError(lblGrupa, string.IsNullOrEmpty(grupa));
            ColorError(lblAfectiuni, afectiuni.Count == 0);

            if (!valid)
            {
                MessageBox.Show(mesaj, "Eroare Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GrupaSangePacient grupaSange = (GrupaSangePacient)Enum.Parse(typeof(GrupaSangePacient), grupa);

            AfectiuniMedicale afectiuniMed = AfectiuniMedicale.Nespecificat;
            foreach (var af in afectiuni)
                if (Enum.TryParse(af, out AfectiuniMedicale afVal))
                    afectiuniMed |= afVal;

            Pacient pacient = new Pacient(adminPacienti.GetNextCodPacient(),
                nume, prenume, cnp,
                varsta, greutate, inaltime, temp,
                grupaSange, afectiuniMed);

            adminPacienti.AddPacient(pacient);
            MessageBox.Show("Pacient adăugat cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ResetForm();
        }


        private void ResetForm()
        {
            txtNume.Clear();
            txtPrenume.Clear();
            txtCnp.Clear();
            txtVarsta.Clear();
            txtGreutate.Clear();
            txtInaltime.Clear();
            txtTemperatura.Clear();
            cmbGrupaSange.SelectedIndex = -1;
            lstAfectiuni.ClearSelected();

            ColorError(lblNume, false);
            ColorError(lblPrenume, false);
            ColorError(lblCnp, false);
            ColorError(lblVarsta, false);
            ColorError(lblGreutate, false);
            ColorError(lblInaltime, false);
            ColorError(lblTemperatura, false);
            ColorError(lblGrupa, false);
            ColorError(lblAfectiuni, false);
        }

        private void AdaugaPacient_Load(object sender, EventArgs e) { }
    }
}
