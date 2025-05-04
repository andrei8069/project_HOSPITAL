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

namespace InterfataUtilizator_WindowsForms
{
    public partial class AdaugaSectie : MetroForm
    {
        private MetroTextBox txtNume, txtEtaj, txtCapacitate, txtNrPacienti, txtTemperatura, txtSuprafata, txtBuget;
        private ComboBox cmbStatus;
        private ListBox lstDotari;
        private MetroButton btnAdauga, btnInapoi;
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

            btnAdauga = new MetroButton() { Text = "Adaugă", Width = 120, Left = xInput, Top = y };
            btnAdauga.Click += BtnAdauga_Click;
            this.Controls.Add(btnAdauga);

            btnInapoi = new MetroButton() { Text = "Înapoi", Width = 120, Left = xInput + 140, Top = y };
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
            string eroare = "";
            ResetLabelColors();

            if (txtNume.Text.Length < MIN_NUME || txtNume.Text.Length > MAX_NUME)
            {
                eroare += "Nume invalid (2–50).\n";
                ColorError(lblNume, true);
            }

            if (!int.TryParse(txtEtaj.Text, out int etaj) || etaj < ETAJ_MIN || etaj > ETAJ_MAX)
            {
                eroare += "Etaj invalid.\n";
                ColorError(lblEtaj, true);
            }

            if (!int.TryParse(txtCapacitate.Text, out int capacitate) || capacitate < CAPACITATE_MIN || capacitate > CAPACITATE_MAX)
            {
                eroare += "Capacitate invalidă.\n";
                ColorError(lblCapacitate, true);
            }

            if (!int.TryParse(txtNrPacienti.Text, out int nrPacienti) || nrPacienti < NR_PAC_MIN || nrPacienti > capacitate)
            {
                eroare += "Nr. pacienți invalid.\n";
                ColorError(lblNrPacienti, true);
            }

            if (!double.TryParse(txtTemperatura.Text, out double temperatura) || temperatura < TEMP_MIN || temperatura > TEMP_MAX)
            {
                eroare += "Temperatură invalidă.\n";
                ColorError(lblTemperatura, true);
            }

            if (!double.TryParse(txtSuprafata.Text, out double suprafata) || suprafata < SUPRAF_MIN || suprafata > SUPRAF_MAX)
            {
                eroare += "Suprafață invalidă.\n";
                ColorError(lblSuprafata, true);
            }

            if (!double.TryParse(txtBuget.Text, out double buget) || buget < BUGET_MIN)
            {
                eroare += "Buget invalid.\n";
                ColorError(lblBuget, true);
            }

            if (cmbStatus.SelectedIndex == -1)
            {
                eroare += "Selectează un status.\n";
                ColorError(lblStatus, true);
            }

            if (lstDotari.SelectedItems.Count == 0)
            {
                eroare += "Selectează cel puțin o dotare.\n";
                ColorError(lblDotari, true);
            }

            if (eroare != "")
            {
                MessageBox.Show(eroare, "Erori de validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DotariSectie dotari = DotariSectie.Nespecificat;
            foreach (var item in lstDotari.SelectedItems)
            {
                if (Enum.TryParse(item.ToString(), out DotariSectie dot))
                {
                    dotari |= dot;
                }
            }

            StatusFunctionareSectie status = (StatusFunctionareSectie)Enum.Parse(typeof(StatusFunctionareSectie), cmbStatus.Text);

            int nrSectii;
            adminSectii.GetSectie(out nrSectii);

            var sectie = new SectieSpital(nrSectii + 1, txtNume.Text.Trim(), etaj, capacitate, nrPacienti,
                temperatura, suprafata, buget, status, dotari);

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
