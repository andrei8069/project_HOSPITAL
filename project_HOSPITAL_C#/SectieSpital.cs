using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_HOSPITAL_C_
{
    class SectieSpital
    {
        private string numeSectie; //public string NumeSectie{get; set;} - pot seta si private set , acces doar printr-o metoda
        private int etaj;
        private int capacitateMaxima;
        private int nrPacientiInternati;
        private double temperaturaMediu;
        private double suprafataSectie;
        private double bugetSectie;
        public int codSectie;
        public static int codSectieStatic = 0;

        public SectieSpital()
        {
            numeSectie = string.Empty;
            etaj = 0;
            capacitateMaxima = 0;
            nrPacientiInternati = 0;
            temperaturaMediu = 0.0;
            suprafataSectie = 0.0;
            bugetSectie = 0.0;
            codSectieStatic++;
            codSectie = codSectieStatic;

        }
        public SectieSpital( string numeSectie, int etaj , int capacitateMaxima , int nrPacientiInternati , double temperaturaMediu , double suprafataSectie , double bugetSectie)
        {
            codSectieStatic++;
            codSectie = codSectieStatic;
            this.numeSectie = numeSectie;
            this.etaj = etaj;
            this.capacitateMaxima = capacitateMaxima;
            this.nrPacientiInternati = nrPacientiInternati;
            this.temperaturaMediu = temperaturaMediu;
            this.suprafataSectie = suprafataSectie;
            this.bugetSectie = bugetSectie;
           
        }









        public string NumeSectie
        {
            get { return numeSectie; }
            set
            {
                numeSectie = value;
            }
        }

        public int Etaj
        {
            get { return etaj; }
            set
            {
                etaj = value;
            }
        }
        public int CapacitateMaxima
        {
            get { return capacitateMaxima; }
            set
            {
                capacitateMaxima = value;
            }
        }
        public int NrPacientiInternati
        {
            get { return nrPacientiInternati; }
            set
            {
                nrPacientiInternati = value;
            }
        }
        public double TemperaturaMediu
        {
            get { return temperaturaMediu; }
            set
            {
                temperaturaMediu = value;
            }
        }
        public double SuprafataSectie
        {
            get { return suprafataSectie; }
            set
            {
                suprafataSectie = value;
            }
        }
        public double BugetSectie
        {
            get { return bugetSectie; }
            set
            {
                bugetSectie = value;
            }
        }


















        public string toScreenSectie()
        {
            return ($"Sectie: CodSectie -> {codSectie} Nume -> {numeSectie} Etaj -> {etaj} CapacitateMaxima -> {capacitateMaxima} NrPacientiInternati -> {nrPacientiInternati} TemperaturaMediu -> {temperaturaMediu} SuprafataSectie -> {suprafataSectie} BugetSectie -> {bugetSectie}");
        }
    }
}
