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

namespace InterfataUtilizator_WindowsForms
{
    public partial class AdaugaPacient : MetroForm
    {
        // Controale
        private MetroTextBox txtNume, txtPrenume, txtCnp, txtVarsta, txtGreutate, txtInaltime, txtTemperatura;
        private ComboBox cmbGrupaSange;
        private ListBox lstAfectiuni;
        private MetroButton btnAdauga, btnInapoi;

        // Label-uri
        private MetroLabel lblNume, lblPrenume, lblCnp, lblVarsta, lblGreutate, lblInaltime, lblTemperatura, lblGrupa, lblAfectiuni;

        private const int LABEL_WIDTH = 140;
        private const int CONTROL_WIDTH = 200;
        private const int LINE_HEIGHT = 40;

        // Validare
        private const int CNP_LUNGIME = 13;
        private const int VARSTA_MIN = 0, VARSTA_MAX = 120;
        private const double GREUTATE_MIN = 2.0, GREUTATE_MAX = 300.0;
        private const double INALTIME_MIN = 30.0, INALTIME_MAX = 250.0;
        private const double TEMP_MIN = 30.0, TEMP_MAX = 45.0;

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
            btnAdauga = new MetroButton()
            {
                Text = "Adaugă",
                Width = 120,
                Height = 35,
                Left = xInput,
                Top = y
            };
            btnAdauga.Click += BtnAdauga_Click;
            this.Controls.Add(btnAdauga);

            btnInapoi = new MetroButton()
            {
                Text = "Înapoi",
                Width = 120,
                Height = 35,
                Left = xInput + 140,
                Top = y
            };
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
            string mesaj = "";

            ColorError(lblNume, txtNume.Text.Length < 2);
            if (txtNume.Text.Length < 2) mesaj += "Nume invalid.\n";

            ColorError(lblPrenume, txtPrenume.Text.Length < 2);
            if (txtPrenume.Text.Length < 2) mesaj += "Prenume invalid.\n";

            ColorError(lblCnp, txtCnp.Text.Length != CNP_LUNGIME);
            if (txtCnp.Text.Length != CNP_LUNGIME) mesaj += "CNP invalid.\n";

            ColorError(lblVarsta, !int.TryParse(txtVarsta.Text, out int varsta) || varsta < VARSTA_MIN || varsta > VARSTA_MAX);
            if (varsta < VARSTA_MIN || varsta > VARSTA_MAX) mesaj += $"Vârsta între {VARSTA_MIN}-{VARSTA_MAX}.\n";

            ColorError(lblGreutate, !double.TryParse(txtGreutate.Text, out double greutate) || greutate < GREUTATE_MIN || greutate > GREUTATE_MAX);
            if (greutate < GREUTATE_MIN || greutate > GREUTATE_MAX) mesaj += $"Greutatea între {GREUTATE_MIN}-{GREUTATE_MAX} kg.\n";

            ColorError(lblInaltime, !double.TryParse(txtInaltime.Text, out double inaltime) || inaltime < INALTIME_MIN || inaltime > INALTIME_MAX);
            if (inaltime < INALTIME_MIN || inaltime > INALTIME_MAX) mesaj += $"Înălțimea între {INALTIME_MIN}-{INALTIME_MAX} cm.\n";

            ColorError(lblTemperatura, !double.TryParse(txtTemperatura.Text, out double temp) || temp < TEMP_MIN || temp > TEMP_MAX);
            if (temp < TEMP_MIN || temp > TEMP_MAX) mesaj += $"Temperatura între {TEMP_MIN}-{TEMP_MAX} °C.\n";

            ColorError(lblGrupa, cmbGrupaSange.SelectedIndex == -1);
            if (cmbGrupaSange.SelectedIndex == -1) mesaj += "Selectează grupa de sânge.\n";

            ColorError(lblAfectiuni, lstAfectiuni.SelectedItems.Count == 0);
            if (lstAfectiuni.SelectedItems.Count == 0) mesaj += "Selectează cel puțin o afecțiune.\n";

            if (mesaj != "")
            {
                MessageBox.Show(mesaj, "Eroare Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<Pacient> listaPacienti = adminPacienti.GetPacienti();
            int nrPacienti = listaPacienti.Count;
            GrupaSangePacient grupa = (GrupaSangePacient)Enum.Parse(typeof(GrupaSangePacient), cmbGrupaSange.Text);
            AfectiuniMedicale afectiuni = AfectiuniMedicale.Nespecificat;
            foreach (var item in lstAfectiuni.SelectedItems)
                if (Enum.TryParse(item.ToString(), out AfectiuniMedicale af))
                    afectiuni |= af;

            var pacient = new Pacient(nrPacienti + 1,
                txtNume.Text.Trim(), txtPrenume.Text.Trim(), txtCnp.Text.Trim(),
                varsta, greutate, inaltime, temp,
                grupa, afectiuni);

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
