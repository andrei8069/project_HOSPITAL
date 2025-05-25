using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibrarieModele
{
    public class SectieSpital
    {
        private const char SEPARATOR_PRINCIPAL_FISIER = ';';

        private const int ID = 0;
        private const int NUMESECTIE = 1;
        private const int ETAJ = 2;
        private const int CAPACITATEMAXIMA = 3;
        private const int NRPACIENTIINTERNATI = 4;
        private const int TEMPERATURAMEDIU = 5;
        private const int SUPRAFATASECTIE = 6;
        private const int BUGETSECTIE = 7;
        private const int STATUSSECTIE = 8;
        private const int DOTARI = 9;

        public string NumeSectie { get; set; }
        public int Etaj { get; set; }
        public int CapacitateMaxima { get; set; }
        public int NrPacientiInternati { get; set; }
        public double TemperaturaMediu { get; set; }
        public double SuprafataSectie { get; set; }
        public double BugetSectie { get; set; }
        public int CodSectie { get; set; }

        public StatusFunctionareSectie Status { get; set; }
        public DotariSectie DotariSec { get; set; }


        public SectieSpital()
        {
            

        }
        public SectieSpital(int codSectie,string numeSectie, int etaj, int capacitateMaxima, int nrPacientiInternati, double temperaturaMediu, double suprafataSectie, double bugetSectie,StatusFunctionareSectie status, DotariSectie dotari)
        {

            this.CodSectie = codSectie;
            this.NumeSectie = numeSectie;
            this.Etaj = etaj;
            this.CapacitateMaxima = capacitateMaxima;
            this.NrPacientiInternati = nrPacientiInternati;
            this.TemperaturaMediu = temperaturaMediu;
            this.SuprafataSectie = suprafataSectie;
            this.BugetSectie = bugetSectie;
            this.Status = status;
            this.DotariSec = dotari;

        }

        public SectieSpital(string linieFisier)
        {
            var dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);
            this.CodSectie = Convert.ToInt32(dateFisier[ID]);
            this.NumeSectie = dateFisier[NUMESECTIE];
            this.Etaj = Convert.ToInt32(dateFisier[ETAJ]);
            this.CapacitateMaxima = Convert.ToInt32(dateFisier[CAPACITATEMAXIMA]);
            this.NrPacientiInternati = Convert.ToInt32(dateFisier[NRPACIENTIINTERNATI]);
            this.TemperaturaMediu = Convert.ToDouble(dateFisier[TEMPERATURAMEDIU]);
            this.SuprafataSectie = Convert.ToDouble(dateFisier[SUPRAFATASECTIE]);
            this.BugetSectie = Convert.ToDouble(dateFisier[BUGETSECTIE]);
            if (Enum.TryParse(dateFisier[STATUSSECTIE],out StatusFunctionareSectie statusResult)){
                this.Status = statusResult;
            }
            if (Enum.TryParse(dateFisier[DOTARI],out DotariSectie dotariResult))
            {
                this.DotariSec = dotariResult;
            }
        }


        public string toScreenSectie()
        {
            return ($"Sectie: CodSectie -> {CodSectie} Nume -> {NumeSectie} Etaj -> {Etaj} CapacitateMaxima -> {CapacitateMaxima} NrPacientiInternati -> {NrPacientiInternati} TemperaturaMediu -> {TemperaturaMediu} SuprafataSectie -> {SuprafataSectie} BugetSectie -> {BugetSectie} StatusSectie -> {Status} DotariSectie -> {DotariSec}");
        }

        public string ConversieLaSir_PentruFisier()
        {
            string obiectSectieSpitalPentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}",
                SEPARATOR_PRINCIPAL_FISIER,
                CodSectie.ToString(),
                (NumeSectie ?? "Necunoscut"),
                Etaj,
                CapacitateMaxima,
                NrPacientiInternati,
                TemperaturaMediu,
                SuprafataSectie,
                BugetSectie,
                Status,
                DotariSec
                );
            return obiectSectieSpitalPentruFisier;
        }


    }
}
