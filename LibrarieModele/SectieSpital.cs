using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibrarieModele
{
    public class SectieSpital
    {

        public string NumeSectie { get; set; }
        public int Etaj { get; set; }
        public int CapacitateMaxima { get; set; }
        public int NrPacientiInternati { get; set; }
        public double TemperaturaMediu { get; set; }
        public double SuprafataSectie { get; set; }
        public double BugetSectie { get; set; }
        public int CodSectie { get; set; }

        private static int codSectieStatic = 0;

        public SectieSpital()
        {
            NumeSectie = string.Empty;
            Etaj = 0;
            CapacitateMaxima = 0;
            NrPacientiInternati = 0;
            TemperaturaMediu = 0.0;
            SuprafataSectie = 0.0;
            BugetSectie = 0.0;
            codSectieStatic++;
            CodSectie = codSectieStatic;

        }
        public SectieSpital(string numeSectie, int etaj, int capacitateMaxima, int nrPacientiInternati, double temperaturaMediu, double suprafataSectie, double bugetSectie)
        {
            codSectieStatic++;
            CodSectie = codSectieStatic;
            this.NumeSectie = numeSectie;
            this.Etaj = etaj;
            this.CapacitateMaxima = capacitateMaxima;
            this.NrPacientiInternati = nrPacientiInternati;
            this.TemperaturaMediu = temperaturaMediu;
            this.SuprafataSectie = suprafataSectie;
            this.BugetSectie = bugetSectie;

        }



        public string toScreenSectie()
        {
            return ($"Sectie: CodSectie -> {CodSectie} Nume -> {NumeSectie} Etaj -> {Etaj} CapacitateMaxima -> {CapacitateMaxima} NrPacientiInternati -> {NrPacientiInternati} TemperaturaMediu -> {TemperaturaMediu} SuprafataSectie -> {SuprafataSectie} BugetSectie -> {BugetSectie}");
        }
    }
}
