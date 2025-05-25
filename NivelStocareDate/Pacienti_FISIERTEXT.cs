using LibrarieModele;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NivelStocareDate
{
    public class Pacienti_FISIERTEXT
    {
        private const int NR_MAX_PACIENTI = 500;
        private string numeFisier;
        const int CNP_LUNGIME = 13;
        const int VARSTA_MIN = 0, VARSTA_MAX = 120;
        const double GREUTATE_MIN = 2.0, GREUTATE_MAX = 300.0;
        const double INALTIME_MIN = 30.0, INALTIME_MAX = 250.0;
        const double TEMP_MIN = 30.0, TEMP_MAX = 45.0;
        public Pacienti_FISIERTEXT(string numeFisier)
        {
            this.numeFisier = numeFisier;

            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        public void AddPacient(Pacient pacient)
        {
            using(StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, true))
            {
                streamWriterFisierText.WriteLine(pacient.ConversieLaSir_PentruFisier());
            }
        }
        public List<Pacient> GetPacienti()
        {
            List<Pacient> pacienti = new List<Pacient>();

            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    pacienti.Add(new Pacient(linieFisier));
                }
            }

            return pacienti;
        }


        public Pacient FindCNP(string cnp)
        {
            Pacient pacient;
            using (StreamReader citireFisier = new StreamReader(numeFisier))
            {
                string linieFisier;
                while ((linieFisier = citireFisier.ReadLine()) != null)
                {
                    pacient = new Pacient(linieFisier);
                    if (pacient.Cnp == cnp)
                    {
                        return pacient;
                    }
                }
            }
            return null;

        }


        public (bool valid, string mesaj) VerificaDatePacient(
    string nume, string prenume, string cnp, string varstaStr, string greutateStr,
    string inaltimeStr, string temperaturaStr, string grupaSange, List<string> afectiuni,
    List<Pacient> pacientiExistenti, int? codPacient = null)
        {
           

            string mesaj = "";

            if (string.IsNullOrWhiteSpace(nume) || nume.Length < 2)
                mesaj += "Numele trebuie să aibă cel puțin 2 caractere.\n";

            if (string.IsNullOrWhiteSpace(prenume) || prenume.Length < 2)
                mesaj += "Prenumele trebuie să aibă cel puțin 2 caractere.\n";

            if (cnp.Length != CNP_LUNGIME)
                mesaj += "CNP-ul trebuie să aibă 13 caractere.\n";
            else if (pacientiExistenti.Any(p => p.Cnp == cnp && (!codPacient.HasValue || p.CodPacient != codPacient)))
                mesaj += "Există deja un alt pacient cu acest CNP!\n";

            if (!int.TryParse(varstaStr, out int varsta) || varsta < VARSTA_MIN || varsta > VARSTA_MAX)
                mesaj += $"Vârsta trebuie să fie între {VARSTA_MIN}-{VARSTA_MAX}.\n";

            if (!double.TryParse(greutateStr, out double greutate) || greutate < GREUTATE_MIN || greutate > GREUTATE_MAX)
                mesaj += $"Greutatea trebuie să fie între {GREUTATE_MIN}-{GREUTATE_MAX} kg.\n";

            if (!double.TryParse(inaltimeStr, out double inaltime) || inaltime < INALTIME_MIN || inaltime > INALTIME_MAX)
                mesaj += $"Înălțimea trebuie să fie între {INALTIME_MIN}-{INALTIME_MAX} cm.\n";

            if (!double.TryParse(temperaturaStr, out double temperatura) || temperatura < TEMP_MIN || temperatura > TEMP_MAX)
                mesaj += $"Temperatura trebuie să fie între {TEMP_MIN}-{TEMP_MAX} °C.\n";

            if (string.IsNullOrEmpty(grupaSange) || !Enum.IsDefined(typeof(GrupaSangePacient), grupaSange))
                mesaj += "Selectează o grupă de sânge validă.\n";

            if (afectiuni == null || afectiuni.Count == 0)
                mesaj += "Selectează cel puțin o afecțiune.\n";

            return (string.IsNullOrEmpty(mesaj), mesaj);
        }


        public (bool valid, string mesaj) VerificaCampuriModificate(
    string nume, string prenume, string cnp, string varstaStr, string greutateStr,
    string inaltimeStr, string temperaturaStr, string grupaSange, List<string> afectiuni,
    List<Pacient> pacientiExistenti, int codPacient)
        {
            string mesaj = "";

            if (!string.IsNullOrWhiteSpace(nume) && nume.Length < 2)
                mesaj += "Numele trebuie să aibă cel puțin 2 caractere.\n";

            if (!string.IsNullOrWhiteSpace(prenume) && prenume.Length < 2)
                mesaj += "Prenumele trebuie să aibă cel puțin 2 caractere.\n";

            if (!string.IsNullOrWhiteSpace(cnp))
            {
                if (cnp.Length != 13)
                    mesaj += "CNP-ul trebuie să aibă 13 caractere.\n";
                else if (pacientiExistenti.Any(p => p.Cnp == cnp && p.CodPacient != codPacient))
                    mesaj += "Există deja un alt pacient cu acest CNP!\n";
            }

            if (!string.IsNullOrWhiteSpace(varstaStr))
                if (!int.TryParse(varstaStr, out int varsta) || varsta < VARSTA_MIN || varsta > VARSTA_MAX)
                    mesaj += $"Vârsta trebuie să fie între {VARSTA_MIN}-{VARSTA_MAX}.\n";

            if (!string.IsNullOrWhiteSpace(greutateStr))
                if (!double.TryParse(greutateStr, out double greutate) || greutate < GREUTATE_MIN || greutate > GREUTATE_MAX)
                    mesaj += $"Greutatea trebuie să fie între {GREUTATE_MIN}-{GREUTATE_MAX} kg.\n";

            if (!string.IsNullOrWhiteSpace(inaltimeStr))
                if (!double.TryParse(inaltimeStr, out double inaltime) || inaltime < INALTIME_MIN || inaltime > INALTIME_MAX)
                    mesaj += $"Înălțimea trebuie să fie între {INALTIME_MIN}-{INALTIME_MAX} cm.\n";

            if (!string.IsNullOrWhiteSpace(temperaturaStr))
                if (!double.TryParse(temperaturaStr, out double temperatura) || temperatura < TEMP_MIN || temperatura > TEMP_MAX)
                    mesaj += $"Temperatura trebuie să fie între {TEMP_MIN}-{TEMP_MAX} °C.\n";

            if (!string.IsNullOrWhiteSpace(grupaSange))
                if (!Enum.IsDefined(typeof(GrupaSangePacient), grupaSange))
                    mesaj += "Selectează o grupă de sânge validă.\n";

            if (afectiuni != null && afectiuni.Count > 0)
            {
                foreach (var af in afectiuni)
                    if (!Enum.IsDefined(typeof(AfectiuniMedicale), af))
                        mesaj += $"Afecțiunea \"{af}\" este invalidă.\n";
            }

            return (string.IsNullOrEmpty(mesaj), mesaj);
        }

        public void AsocierePacientLaSectie(int codPacient, int codSectie)
        {
            List<Pacient> pacienti = GetPacienti();
            Pacient pacient = pacienti.Find(p => p.CodPacient == codPacient);
            if (pacient != null)
            {
                pacient.CodSectieInternare = codSectie;
                SalveazaTotiPacientii(pacienti);
            }
        }

        public void DeconecteazaPacientDinSectie(int codPacient)
        {
            List<Pacient> pacienti = GetPacienti();
            var pacient = pacienti.Find(p => p.CodPacient == codPacient);
            if (pacient != null)
            {
                pacient.CodSectieInternare = 0;
                SalveazaTotiPacientii(pacienti);
            }
        }

        private void SalveazaTotiPacientii(List<Pacient> pacienti)
        {
            using (StreamWriter streamWriter = new StreamWriter(numeFisier, false))
            {
                foreach (Pacient pacient in pacienti)
                {
                    streamWriter.WriteLine(pacient.ConversieLaSir_PentruFisier());
                }
            }
        }

        public List<Pacient> GetPacientiInternatiiInSectie(int codSectie)
        {
            return GetPacienti().FindAll(p => p.CodSectieInternare == codSectie);
        }

        public void StergePacient(int codPacient)
        {
            List<Pacient> pacienti = GetPacienti();
            var pacientDesters = pacienti.Find(p => p.CodPacient == codPacient);
            
            if (pacientDesters != null)
            {
                pacienti.Remove(pacientDesters);
                SalveazaTotiPacientii(pacienti);
            }
        }

        public int GetNextCodPacient()
        {
            var pacienti = GetPacienti();
            if (pacienti.Count == 0) return 1;
            return pacienti.Max(p => p.CodPacient) + 1;
        }
    }
}
