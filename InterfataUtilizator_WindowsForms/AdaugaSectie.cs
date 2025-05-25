using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using LibrarieModele;
using NivelStocareDate;
using System.Collections.Generic;

namespace InterfataUtilizator_WindowsForms
{
    public partial class AdaugaSectie : MetroForm
    {
        private MetroTextBox txtNume, txtEtaj, txtCapacitate, txtNrPacienti, txtTemperatura, txtSuprafata, txtBuget;
        private ComboBox cmbStatus;
        private ListBox lstDotari;
        private Button btnAdauga, btnInapoi;
        private MetroLabel lblNume, lblEtaj, lblCapacitate, lblNrPacienti, lblTemperatura, lblSuprafata, lblBuget, lblStatus, lblDotari;

        private const int LABEL_WIDTH = 140;
        private const int CONTROL_WIDTH = 200;
        private const int LINE_HEIGHT = 40;

        private const int MIN_NUME = 2, MAX_NUME = 50;
        private const int ETAJ_MIN = 0, ETAJ_MAX = 20;
        private const int CAPACITATE_MIN = 1, CAPACITATE_MAX = 500;
        private const int NR_PAC_MIN = 0;
        private const double TEMP_MIN = 10.0, TEMP_MAX = 35.0;
        private const double SUPRAF_MIN = 20.0, SUPRAF_MAX = 1000.0;
        private const double BUGET_MIN = 0.0;

        private Sectii_FISIERTEXT adminSectii;

        public AdaugaSectie()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = "";
            this.Size = new Size(650, 750);
            this.StartPosition = FormStartPosition.CenterScreen;

            MetroLabel lblTitlu = new MetroLabel()
            {
                Text = "Adaugă Secție",
                FontSize = MetroLabelSize.Tall,
                FontWeight = MetroLabelWeight.Bold,
                Left = 40,
                Top = 20,
                AutoSize = true
            };
            this.Controls.Add(lblTitlu);

            string locatie = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string fisier = ConfigurationManager.AppSettings["NumeFisierSectii"];
            adminSectii = new Sectii_FISIERTEXT(Path.Combine(locatie, fisier));

            int xLabel = 40, xInput = xLabel + LABEL_WIDTH + 10, y = 80;

            lblNume = AddLabel("Nume Secție:", xLabel, y);
            txtNume = AddTextBox(xInput, y);

            y += LINE_HEIGHT;
            lblEtaj = AddLabel("Etaj:", xLabel, y);
            txtEtaj = AddTextBox(xInput, y);

            y += LINE_HEIGHT;
            lblCapacitate = AddLabel("Capacitate Max:", xLabel, y);
            txtCapacitate = AddTextBox(xInput, y);

            y += LINE_HEIGHT;
            lblNrPacienti = AddLabel("Nr. Pacienți:", xLabel, y);
            txtNrPacienti = AddTextBox(xInput, y);

            y += LINE_HEIGHT;
            lblTemperatura = AddLabel("Temperatură:", xLabel, y);
            txtTemperatura = AddTextBox(xInput, y);

            y += LINE_HEIGHT;
            lblSuprafata = AddLabel("Suprafață:", xLabel, y);
            txtSuprafata = AddTextBox(xInput, y);

            y += LINE_HEIGHT;
            lblBuget = AddLabel("Buget:", xLabel, y);
            txtBuget = AddTextBox(xInput, y);

            y += LINE_HEIGHT;
            lblStatus = AddLabel("Status Funcționare:", xLabel, y);
            cmbStatus = new ComboBox() { Left = xInput, Top = y, Width = CONTROL_WIDTH, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbStatus.Items.AddRange(Enum.GetNames(typeof(StatusFunctionareSectie)));
            this.Controls.Add(cmbStatus);

            y += LINE_HEIGHT;
            lblDotari = AddLabel("Dotări:", xLabel, y);
            lstDotari = new ListBox()
            {
                Left = xInput,
                Top = y,
                Width = CONTROL_WIDTH,
                Height = 80,
                SelectionMode = SelectionMode.MultiSimple
            };
            lstDotari.Items.AddRange(Enum.GetNames(typeof(DotariSectie)));
            this.Controls.Add(lstDotari);

            y += 100;

            btnAdauga = new Button() { 
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

            btnInapoi = new Button() { 
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
            btnInapoi.Click += (s, e) => this.Close();
            this.Controls.Add(btnInapoi);
        }

        private MetroLabel AddLabel(string text, int x, int y)
        {
            var lbl = new MetroLabel() { Text = text, Left = x, Top = y, Width = LABEL_WIDTH };
            this.Controls.Add(lbl);
            return lbl;
        }

        private MetroTextBox AddTextBox(int x, int y)
        {
            var txt = new MetroTextBox() { Left = x, Top = y, Width = CONTROL_WIDTH };
            this.Controls.Add(txt);
            return txt;
        }

        private void BtnAdauga_Click(object sender, EventArgs e)
        {
            ResetLabelColors();

            // Obține valori din controale
            string numeSectie = txtNume.Text.Trim();
            string etaj = txtEtaj.Text.Trim();
            string capacitate = txtCapacitate.Text.Trim();
            string nrPacienti = txtNrPacienti.Text.Trim();
            string temperatura = txtTemperatura.Text.Trim();
            string suprafata = txtSuprafata.Text.Trim();
            string buget = txtBuget.Text.Trim();
            string status = cmbStatus.SelectedItem?.ToString();

            List<string> dotariSelectate = new List<string>();
            foreach (var item in lstDotari.SelectedItems)
                dotariSelectate.Add(item.ToString());

            List<SectieSpital> sectii = adminSectii.GetSectii();

            // Apel validare din clasa Sectii_FISIERTEXT
            var rezultat = adminSectii.VerificaDateSectie(
                numeSectie, etaj, capacitate, nrPacienti,
                temperatura, suprafata, buget, status,
                dotariSelectate, sectii
            );

            if (!rezultat.valid)
            {
                // Verifică fiecare câmp individual și colorează label-ul asociat
                if (string.IsNullOrWhiteSpace(numeSectie) || numeSectie.Length < MIN_NUME || numeSectie.Length > MAX_NUME)
                    ColorError(lblNume, true);

                if (!int.TryParse(etaj, out int valEtaj) || valEtaj < ETAJ_MIN || valEtaj > ETAJ_MAX)
                    ColorError(lblEtaj, true);

                if (!int.TryParse(capacitate, out int valCap) || valCap < CAPACITATE_MIN || valCap > CAPACITATE_MAX)
                    ColorError(lblCapacitate, true);

                if (!int.TryParse(nrPacienti, out int valPac) || valPac < NR_PAC_MIN || (valCap > 0 && valPac > valCap))
                    ColorError(lblNrPacienti, true);

                if (!double.TryParse(temperatura, out double valTemp) || valTemp < TEMP_MIN || valTemp > TEMP_MAX)
                    ColorError(lblTemperatura, true);

                if (!double.TryParse(suprafata, out double valSup) || valSup < SUPRAF_MIN || valSup > SUPRAF_MAX)
                    ColorError(lblSuprafata, true);

                if (!double.TryParse(buget, out double valBuget) || valBuget < BUGET_MIN)
                    ColorError(lblBuget, true);

                if (string.IsNullOrWhiteSpace(status) || !Enum.IsDefined(typeof(StatusFunctionareSectie), status))
                    ColorError(lblStatus, true);

                if (dotariSelectate.Count == 0)
                    ColorError(lblDotari, true);

                MessageBox.Show(rezultat.mesaj, "Date invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // Conversie enumuri
            DotariSectie dotari = DotariSectie.Nespecificat;
            foreach (var item in dotariSelectate)
            {
                if (Enum.TryParse(item, out DotariSectie dot))
                    dotari |= dot;
            }

            StatusFunctionareSectie statusEnum = (StatusFunctionareSectie)Enum.Parse(typeof(StatusFunctionareSectie), status);

            // Creează obiectul SectieSpital
            int codNou = adminSectii.GetNextCodSectie();
            var sectie = new SectieSpital(
                codNou,
                numeSectie,
                int.Parse(etaj),
                int.Parse(capacitate),
                int.Parse(nrPacienti),
                double.Parse(temperatura),
                double.Parse(suprafata),
                double.Parse(buget),
                statusEnum,
                dotari
            );

            // Salvare în fișier
            adminSectii.AddSectii(sectie);
            MessageBox.Show("Secție adăugată cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ResetForm();
        }


        private void ColorError(MetroLabel lbl, bool isError)
        {
            lbl.Style = isError ? MetroColorStyle.Red : MetroColorStyle.Blue;
            lbl.UseStyleColors = true;
        }

        private void ResetLabelColors()
        {
            ColorError(lblNume, false);
            ColorError(lblEtaj, false);
            ColorError(lblCapacitate, false);
            ColorError(lblNrPacienti, false);
            ColorError(lblTemperatura, false);
            ColorError(lblSuprafata, false);
            ColorError(lblBuget, false);
            ColorError(lblStatus, false);
            ColorError(lblDotari, false);
        }

        private void ResetForm()
        {
            txtNume.Clear();
            txtEtaj.Clear();
            txtCapacitate.Clear();
            txtNrPacienti.Clear();
            txtTemperatura.Clear();
            txtSuprafata.Clear();
            txtBuget.Clear();
            cmbStatus.SelectedIndex = -1;
            lstDotari.ClearSelected();
        }

        private void AdaugaSectie_Load(object sender, EventArgs e) { }
    }
}
