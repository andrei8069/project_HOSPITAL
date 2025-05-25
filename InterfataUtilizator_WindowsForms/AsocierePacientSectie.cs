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
using MetroFramework;
using System.Configuration;
using System.IO;
using LibrarieModele;
using NivelStocareDate;

namespace InterfataUtilizator_WindowsForms
{
    public partial class AsocierePacientSectie : MetroForm
    {
        private Pacienti_FISIERTEXT adminPacienti;
        private Sectii_FISIERTEXT adminSectii;

        // Controale UI
        private ComboBox cmbPacienti;
        private ComboBox cmbSectii;
        private Button btnAsociaza;
        private Button btnDeconecteaza;
        private Button btnInapoi;
        private ListBox lstAsocieri;
        private MetroLabel lblPacient;
        private MetroLabel lblSectie;
        private MetroLabel lblAsocieriCurente;

        public AsocierePacientSectie()
        {
            InitializeComponent();
        }

        private void InitializareInterfata()
        {

            
            // Initializare administratori fisiere
            string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            
            string numeFisierPacienti = ConfigurationManager.AppSettings["NumeFisierPacienti"];
            string caleCompletaFisierPacienti = locatieFisierSolutie + "\\" + numeFisierPacienti;
            adminPacienti = new Pacienti_FISIERTEXT(caleCompletaFisierPacienti);

            string numeFisierSectii = ConfigurationManager.AppSettings["NumeFisierSectii"];
            string caleCompletaFisierSectii = locatieFisierSolutie + "\\" + numeFisierSectii;
            adminSectii = new Sectii_FISIERTEXT(caleCompletaFisierSectii);

            // Configurare form
            this.Theme = MetroThemeStyle.Light;
            this.Style = MetroColorStyle.Default;
            this.Size = new Size(400, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.ForeColor = Color.DarkSlateGray;
            this.Text = "Asociere Pacient - Secție";
            this.BackColor = Color.FromArgb(230, 245, 255);
            this.MaximizeBox = false;
            this.Resizable = false;

            // Label pentru pacienti
            lblPacient = new MetroLabel();
            lblPacient.Text = "Selectează Pacient:";
            lblPacient.Location = new Point(30, 80);
            lblPacient.Size = new Size(150, 25);
            lblPacient.ForeColor = Color.DarkSlateGray;
            lblPacient.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.Controls.Add(lblPacient);

            // ComboBox pacienti
            cmbPacienti = new ComboBox();
            cmbPacienti.Location = new Point(30, 110);
            cmbPacienti.Size = new Size(250, 30);
            cmbPacienti.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPacienti.Font = new Font("Segoe UI", 10);
            this.Controls.Add(cmbPacienti);

            // Label pentru sectii
            lblSectie = new MetroLabel();
            lblSectie.Text = "Selectează Secție:";
            lblSectie.Location = new Point(320, 80);
            lblSectie.Size = new Size(150, 25);
            lblSectie.ForeColor = Color.DarkSlateGray;
            lblSectie.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.Controls.Add(lblSectie);

            // ComboBox sectii
            cmbSectii = new ComboBox();
            cmbSectii.Location = new Point(320, 110);
            cmbSectii.Size = new Size(250, 30);
            cmbSectii.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSectii.Font = new Font("Segoe UI", 10);
            this.Controls.Add(cmbSectii);

            // Buton Asociaza
            btnAsociaza = new Button();
            btnAsociaza.Text = "Asociază";
            btnAsociaza.Location = new Point(150, 160);
            btnAsociaza.Size = new Size(120, 35);
            btnAsociaza.BackColor = Color.SteelBlue;
            btnAsociaza.ForeColor = Color.White;
            btnAsociaza.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnAsociaza.FlatStyle = FlatStyle.Flat;
            btnAsociaza.FlatAppearance.BorderSize = 0;
            btnAsociaza.Cursor = Cursors.Hand;
            btnAsociaza.Click += BtnAsociaza_Click;
            this.Controls.Add(btnAsociaza);

            // Buton Deconecteaza
            btnDeconecteaza = new Button();
            btnDeconecteaza.Text = "Deconectează";
            btnDeconecteaza.Location = new Point(290, 160);
            btnDeconecteaza.Size = new Size(120, 35);
            btnDeconecteaza.BackColor = Color.Orange;
            btnDeconecteaza.ForeColor = Color.White;
            btnDeconecteaza.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnDeconecteaza.FlatStyle = FlatStyle.Flat;
            btnDeconecteaza.FlatAppearance.BorderSize = 0;
            btnDeconecteaza.Cursor = Cursors.Hand;
            btnDeconecteaza.Click += BtnDeconecteaza_Click;
            this.Controls.Add(btnDeconecteaza);

            // Label pentru asocieri curente
            lblAsocieriCurente = new MetroLabel();
            lblAsocieriCurente.Text = "Asocieri Curente:";
            lblAsocieriCurente.Location = new Point(30, 220);
            lblAsocieriCurente.Size = new Size(200, 25);
            lblAsocieriCurente.ForeColor = Color.DarkSlateGray;
            lblAsocieriCurente.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.Controls.Add(lblAsocieriCurente);

            // ListBox pentru asocieri
            lstAsocieri = new ListBox();
            lstAsocieri.Location = new Point(30, 250);
            lstAsocieri.Size = new Size(540, 150);
            lstAsocieri.Font = new Font("Segoe UI", 9);
            lstAsocieri.BackColor = Color.White;
            this.Controls.Add(lstAsocieri);

            // Buton Inapoi
            btnInapoi = new Button();
            btnInapoi.Text = "Înapoi";
            btnInapoi.Location = new Point(250, 420);
            btnInapoi.Size = new Size(100, 35);
            btnInapoi.BackColor = Color.Crimson;
            btnInapoi.ForeColor = Color.White;
            btnInapoi.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnInapoi.FlatStyle = FlatStyle.Flat;
            btnInapoi.FlatAppearance.BorderSize = 0;
            btnInapoi.Cursor = Cursors.Hand;
            btnInapoi.Click += BtnInapoi_Click;
            this.Controls.Add(btnInapoi);

            // Populeaza combobox-urile si lista
            PopuleazaControale();
        }

        private void PopuleazaControale()
        {
            // Clear si populeaza pacienti
            cmbPacienti.Items.Clear();
            var pacienti = adminPacienti.GetPacienti();
            foreach (Pacient pacient in pacienti)
            {
                cmbPacienti.Items.Add($"{pacient.CodPacient} - {pacient.Nume} {pacient.Prenume}");
            }

            // Clear si populeaza sectii
            cmbSectii.Items.Clear();
            var sectii = adminSectii.GetSectii();
            foreach (SectieSpital sectie in sectii)
            {
                cmbSectii.Items.Add($"{sectie.CodSectie} - {sectie.NumeSectie}");
            }

            // Actualizeaza lista asocierilor
            ActualizeazaListaAsocieri();
        }

        private void BtnAsociaza_Click(object sender, EventArgs e)
        {
            if (cmbPacienti.SelectedIndex >= 0 && cmbSectii.SelectedIndex >= 0)
            {
                var pacienti = adminPacienti.GetPacienti();
                var sectii = adminSectii.GetSectii();
                
                int codPacient = pacienti[cmbPacienti.SelectedIndex].CodPacient;
                int codSectie = sectii[cmbSectii.SelectedIndex].CodSectie;
                var sectie = sectii[cmbSectii.SelectedIndex];
                var pacient = pacienti[cmbPacienti.SelectedIndex];

                // Verifica daca pacientul este deja internat in alta sectie
                if (pacient.CodSectieInternare != 0)
                {
                    var sectieActuala = sectii.Find(s => s.CodSectie == pacient.CodSectieInternare);
                    string numeSectieActuala = sectieActuala?.NumeSectie ?? "Secție necunoscută";
                    MessageBox.Show($"Pacientul {pacient.Nume} {pacient.Prenume} este deja internat în secția {numeSectieActuala}. " +
                        $"Vă rugăm să îl deconectați mai întâi de la secția curentă!", 
                        "Pacient deja internat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verifica statusul sectiei
                if (sectie.Status != StatusFunctionareSectie.Open)
                {
                    MessageBox.Show($"Secția {sectie.NumeSectie} nu este deschisă pentru internări! " +
                        $"Status curent: {sectie.Status}. Doar secțiile cu status 'Open' pot primi pacienți.", 
                        "Secție indisponibilă", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verifica capacitatea sectiei
                if (sectie.NrPacientiInternati >= sectie.CapacitateMaxima)
                {
                    MessageBox.Show($"Secția {sectie.NumeSectie} este la capacitate maximă! " +
                        $"Capacitate: {sectie.CapacitateMaxima}, Pacienți internați: {sectie.NrPacientiInternati}", 
                        "Capacitate depășită", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verifica din nou capacitatea folosind metoda de verificare
                if (adminSectii.VerificaCapacitateDisponibila(codSectie))
                {
                    adminPacienti.AsocierePacientLaSectie(codPacient, codSectie);
                    
                    // Actualizeaza numarul de pacienti in sectie
                    var pacientiInSectie = adminPacienti.GetPacientiInternatiiInSectie(codSectie);
                    adminSectii.ActualizeazaNrPacieniiInSectie(codSectie, pacientiInSectie.Count);

                    MessageBox.Show($"Pacientul {pacient.Nume} {pacient.Prenume} a fost internat cu succes în secția {sectie.NumeSectie}! " +
                        $"Locuri rămase: {sectie.CapacitateMaxima - pacientiInSectie.Count}", 
                        "Internare reușită", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    ActualizeazaListaAsocieri();
                }
                else
                {
                    MessageBox.Show($"Secția {sectie.NumeSectie} nu mai are locuri disponibile!", 
                        "Capacitate epuizată", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Selectează un pacient și o secție!", "Atenție", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDeconecteaza_Click(object sender, EventArgs e)
        {
            if (cmbPacienti.SelectedIndex >= 0 && cmbSectii.SelectedIndex >= 0)
            {
                var pacienti = adminPacienti.GetPacienti();
                var sectii = adminSectii.GetSectii();
                int codPacient = pacienti[cmbPacienti.SelectedIndex].CodPacient;
                int codSectieSelectata = sectii[cmbSectii.SelectedIndex].CodSectie;
                var pacient = pacienti.Find(p => p.CodPacient == codPacient);
                var sectieSelectata = sectii[cmbSectii.SelectedIndex];
                
                if (pacient.CodSectieInternare != 0)
                {
                    // Verifica daca pacientul este intr-adevar in sectia selectata
                    if (pacient.CodSectieInternare != codSectieSelectata)
                    {
                        var sectieActuala = sectii.Find(s => s.CodSectie == pacient.CodSectieInternare);
                        string numeSectieActuala = sectieActuala?.NumeSectie ?? "Secție necunoscută";
                        
                        MessageBox.Show($"Eroare! Pacientul {pacient.Nume} {pacient.Prenume} nu este internat în secția {sectieSelectata.NumeSectie}! " +
                            $"Pacientul este internat în secția {numeSectieActuala}. " +
                            $"Vă rugăm să selectați secția corectă din lista de secții.", 
                            "Secție incorectă", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    int codSectieVeche = pacient.CodSectieInternare;
                    var sectieVeche = sectii.Find(s => s.CodSectie == codSectieVeche);
                    string numeSectieVeche = sectieVeche?.NumeSectie ?? "Secție necunoscută";
                    
                    // Afiseaza un dialog de confirmare cu informatii despre sectia curenta
                    DialogResult result = MessageBox.Show(
                        $"Pacientul {pacient.Nume} {pacient.Prenume} este internat în secția {numeSectieVeche}. " +
                        $"Doriți să îl deconectați din această secție?", 
                        "Confirmare deconectare", 
                        MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        adminPacienti.DeconecteazaPacientDinSectie(codPacient);
                        
                        // Actualizează numărul de pacienți în secția veche
                        var pacientiInSectieVeche = adminPacienti.GetPacientiInternatiiInSectie(codSectieVeche);
                        adminSectii.ActualizeazaNrPacieniiInSectie(codSectieVeche, pacientiInSectieVeche.Count);

                        MessageBox.Show($"Pacientul {pacient.Nume} {pacient.Prenume} a fost deconectat cu succes din secția {numeSectieVeche}!", 
                            "Deconectare reușită", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        ActualizeazaListaAsocieri();
                    }
                }
                else
                {
                    MessageBox.Show($"Pacientul {pacient.Nume} {pacient.Prenume} nu este internat în nicio secție!", 
                        "Pacient neinternat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Selectează un pacient și secția din care vrei să îl deconectezi!", "Atenție", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnInapoi_Click(object sender, EventArgs e)
        {
            try
            {
                Form formPacienti = Application.OpenForms["Form1"];
                Form formSectii = Application.OpenForms["Sectii_Spital"];
                
                if (formPacienti != null) 
                {
                    formPacienti.Show();
                    formPacienti.BringToFront();
                }
                else if (formSectii != null) 
                {
                    formSectii.Show();
                    formSectii.BringToFront();
                }
                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare: {ex.Message}");
                this.Close();
            }
        }

        private void ActualizeazaListaAsocieri()
        {
            lstAsocieri.Items.Clear();
            
            var pacienti = adminPacienti.GetPacienti();
            var sectii = adminSectii.GetSectii();
            
            var pacientiAsociati = pacienti.Where(p => p.CodSectieInternare != 0).ToList();
            
            if (pacientiAsociati.Count == 0)
            {
                lstAsocieri.Items.Add("Nu există asocieri curente.");
            }
            else
            {
                foreach (var pacient in pacientiAsociati)
                {
                    var sectie = sectii.Find(s => s.CodSectie == pacient.CodSectieInternare);
                    string numeSectie = sectie?.NumeSectie ?? "Secție necunoscută";
                    lstAsocieri.Items.Add($"• {pacient.Nume} {pacient.Prenume} → {numeSectie}");
                }
            }
        }

        private void AsocierePacientSectie_Load(object sender, EventArgs e)
        {
            InitializareInterfata();
        }
    }
} 