using LibrarieModele;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NivelStocareDate
{
    public class Sectii_FISIERTEXT
    {

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
        private const int NR_MAX_SECTII = 200;
        private string numeFisier;
        public Sectii_FISIERTEXT(string numeFisier)
        {
            this.numeFisier = numeFisier;
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }
        public void AddSectii(SectieSpital sectie)
        {
            using(StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, true))
            {
                streamWriterFisierText.WriteLine(sectie.ConversieLaSir_PentruFisier());
            }
        }


        public List<SectieSpital> GetSectii()
        {
            List<SectieSpital> sectii = new List<SectieSpital>();

            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    sectii.Add(new SectieSpital(linieFisier));
                }
            }

            return sectii;
        }

        public SectieSpital FindSectie(string id)
        {
            SectieSpital sectieSpital;
            using (StreamReader citireFisier = new StreamReader(numeFisier))
            {
                string linieFisier;
                while((linieFisier = citireFisier.ReadLine())!= null)
                {
                    sectieSpital = new SectieSpital(linieFisier);
                    if(sectieSpital.CodSectie.ToString() == id)
                    {
                        return sectieSpital;
                    }
                }
            }
            return null;

        }



        public List<SectieSpital> FindSectieDupaNume(string numeSectie)
        {
            List<SectieSpital> sectiiGasite = new List<SectieSpital>();
            using (StreamReader citireFisier = new StreamReader(numeFisier))
            {
                string linieFisier;
                while ((linieFisier = citireFisier.ReadLine()) != null) { 
                    SectieSpital sectieSpital = new SectieSpital(linieFisier);
                    if(sectieSpital.NumeSectie.ToLower() == numeSectie.ToLower())
                    {
                        sectiiGasite.Add(sectieSpital);
                    }
                }
            }
            return sectiiGasite;
        }


        public (bool valid, string mesaj) VerificaDateSectie(
    string nume, string etajStr, string capacitateStr, string nrPacientiStr,
    string temperaturaStr, string suprafataStr, string bugetStr,
    string status, List<string> dotariSelectate,
    List<SectieSpital> sectiiExistente, int? codSectie = null)
        {

            string mesaj = "";

            if (string.IsNullOrWhiteSpace(nume) || nume.Length < LUNGIME_MIN_NUME_SECTIE || nume.Length > LUNGIME_MAX_NUME_SECTIE)
                mesaj += $"Numele trebuie să aibă între {LUNGIME_MIN_NUME_SECTIE} și {LUNGIME_MAX_NUME_SECTIE} caractere.\n";

            if (!int.TryParse(etajStr, out int etaj) || etaj < ETAJ_MIN || etaj > ETAJ_MAX)
                mesaj += $"Etajul trebuie să fie între {ETAJ_MIN} și {ETAJ_MAX}.\n";

            if (!int.TryParse(capacitateStr, out int capacitate) || capacitate < CAPACITATE_MIN || capacitate > CAPACITATE_MAX)
                mesaj += $"Capacitatea trebuie să fie între {CAPACITATE_MIN} și {CAPACITATE_MAX}.\n";

            if (!int.TryParse(nrPacientiStr, out int nrPacienti) || nrPacienti < NR_PACIENTI_MIN || nrPacienti > capacitate)
                mesaj += "Numărul de pacienți trebuie să fie între 0 și capacitatea maximă.\n";

            if (!double.TryParse(temperaturaStr, out double temperatura) || temperatura < TEMPERATURA_MIN || temperatura > TEMPERATURA_MAX)
                mesaj += $"Temperatura trebuie să fie între {TEMPERATURA_MIN} și {TEMPERATURA_MAX}.\n";

            if (!double.TryParse(suprafataStr, out double suprafata) || suprafata < SUPRAFATA_MIN || suprafata > SUPRAFATA_MAX)
                mesaj += $"Suprafața trebuie să fie între {SUPRAFATA_MIN} și {SUPRAFATA_MAX}.\n";

            if (!double.TryParse(bugetStr, out double buget) || buget < BUGET_MIN)
                mesaj += "Bugetul trebuie să fie pozitiv.\n";

            if (string.IsNullOrEmpty(status) || !Enum.IsDefined(typeof(StatusFunctionareSectie), status))
                mesaj += "Statusul este invalid sau lipsă.\n";

            if (dotariSelectate == null || dotariSelectate.Count == 0)
                mesaj += "Selectează cel puțin o dotare.\n";

            if (sectiiExistente.Any(s => s.NumeSectie.Equals(nume, StringComparison.OrdinalIgnoreCase) && (!codSectie.HasValue || s.CodSectie != codSectie)))
                mesaj += "Există deja o altă secție cu acest nume.\n";

            return (string.IsNullOrEmpty(mesaj), mesaj);
        }


        public (bool valid, string mesaj) VerificaCampuriModificateSectie(
    string nume, string etajStr, string capacitateStr, string nrPacientiStr,
    string temperaturaStr, string suprafataStr, string bugetStr,
    string status, List<string> dotariSelectate,
    List<SectieSpital> sectiiExistente, int codSectie)
        {
            string mesaj = "";

            if (!string.IsNullOrWhiteSpace(nume))
            {
                if (nume.Length < LUNGIME_MIN_NUME_SECTIE || nume.Length > LUNGIME_MAX_NUME_SECTIE)
                    mesaj += $"Numele trebuie să aibă între {LUNGIME_MIN_NUME_SECTIE} și {LUNGIME_MAX_NUME_SECTIE} caractere.\n";
                else if (sectiiExistente.Any(s => s.NumeSectie.Equals(nume, StringComparison.OrdinalIgnoreCase) && s.CodSectie != codSectie))
                    mesaj += "Există deja o altă secție cu acest nume.\n";
            }

            if (!string.IsNullOrWhiteSpace(etajStr))
                if (!int.TryParse(etajStr, out int etaj) || etaj < ETAJ_MIN || etaj > ETAJ_MAX)
                    mesaj += $"Etajul trebuie să fie între {ETAJ_MIN} și {ETAJ_MAX}.\n";

            if (!string.IsNullOrWhiteSpace(capacitateStr))
                if (!int.TryParse(capacitateStr, out int capacitate) || capacitate < CAPACITATE_MIN || capacitate > CAPACITATE_MAX)
                    mesaj += $"Capacitatea trebuie să fie între {CAPACITATE_MIN} și {CAPACITATE_MAX}.\n";

            if (!string.IsNullOrWhiteSpace(nrPacientiStr) && int.TryParse(capacitateStr, out int capValid))
                if (!int.TryParse(nrPacientiStr, out int nrPacienti) || nrPacienti < NR_PACIENTI_MIN || nrPacienti > capValid)
                    mesaj += "Numărul de pacienți trebuie să fie între 0 și capacitatea maximă.\n";

            if (!string.IsNullOrWhiteSpace(temperaturaStr))
                if (!double.TryParse(temperaturaStr, out double temperatura) || temperatura < TEMPERATURA_MIN || temperatura > TEMPERATURA_MAX)
                    mesaj += $"Temperatura trebuie să fie între {TEMPERATURA_MIN} și {TEMPERATURA_MAX}.\n";

            if (!string.IsNullOrWhiteSpace(suprafataStr))
                if (!double.TryParse(suprafataStr, out double suprafata) || suprafata < SUPRAFATA_MIN || suprafata > SUPRAFATA_MAX)
                    mesaj += $"Suprafața trebuie să fie între {SUPRAFATA_MIN} și {SUPRAFATA_MAX}.\n";

            if (!string.IsNullOrWhiteSpace(bugetStr))
                if (!double.TryParse(bugetStr, out double buget) || buget < BUGET_MIN)
                    mesaj += "Bugetul trebuie să fie pozitiv.\n";

            if (!string.IsNullOrWhiteSpace(status) && !Enum.IsDefined(typeof(StatusFunctionareSectie), status))
                mesaj += "Statusul este invalid.\n";

            if (dotariSelectate != null && dotariSelectate.Any(d => !Enum.IsDefined(typeof(DotariSectie), d)))
                mesaj += "Există dotări invalide.\n";

            return (string.IsNullOrEmpty(mesaj), mesaj);
        }

        public void ActualizeazaNrPacieniiInSectie(int codSectie, int nrPacienti)
        {
            List<SectieSpital> sectii = GetSectii();
            SectieSpital sectie = sectii.Find(s => s.CodSectie == codSectie);
            if (sectie != null)
            {
                sectie.NrPacientiInternati = nrPacienti;
                SalveazaToateSectiile(sectii);
            }
        }

        private void SalveazaToateSectiile(List<SectieSpital> sectii)
        {
            using (StreamWriter streamWriter = new StreamWriter(numeFisier, false))
            {
                foreach (SectieSpital sectie in sectii)
                {
                    streamWriter.WriteLine(sectie.ConversieLaSir_PentruFisier());
                }
            }
        }

        public bool VerificaCapacitateDisponibila(int codSectie)
        {
            SectieSpital sectie = GetSectii().Find(s => s.CodSectie == codSectie);
            return sectie != null && sectie.NrPacientiInternati < sectie.CapacitateMaxima;
        }

        public void StergeSectie(int codSectie)
        {
            List<SectieSpital> sectii = GetSectii();
            SectieSpital sectieDesters = sectii.Find(s => s.CodSectie == codSectie);
            
            if (sectieDesters != null)
            {
                sectii.Remove(sectieDesters);
                SalveazaToateSectiile(sectii);
            }
        }

        public int GetNextCodSectie()
        {
            var sectii = GetSectii();
            if (sectii.Count == 0) return 1;
            return sectii.Max(s => s.CodSectie) + 1;
        }

    }
}
