using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_HOSPITAL_C_
{
    class Pacient
    {
        private string nume;  //public string Nume{get; set;} - pot seta si private set , acces doar printr-o metoda
        private string prenume;
        private string cnp;
        private int varsta;
        private double greutate;
        private double inaltime;
        private double temperaturaCorp;
        public int codPacient;
        public static int codPacientStatic = 0;

        
        
        public Pacient()
        {
            nume = string.Empty;
            prenume = string.Empty;
            cnp = string.Empty;
            varsta = 0;
            greutate = 0.0;
            inaltime = 0.0;
            temperaturaCorp = 0.0;
            codPacientStatic++;
            codPacient = codPacientStatic;
        }
        public Pacient(string nume , string prenume , string cnp ,int varsta , double greutate , double inaltime , double temperaturaCorp)
        {
            this.nume = nume;
            this.prenume = prenume;
            this.cnp = cnp;
            this.varsta = varsta;
            this.greutate = greutate;
            this.inaltime = inaltime;
            this.temperaturaCorp = temperaturaCorp;
            codPacientStatic++;
            codPacient = codPacientStatic;
        }








        public string Nume
        {
            get { return nume; }
            set { if(value.Substring(0,1) == value.Substring(0,1).ToUpper())
                {
                    nume = value;
                }
                    
                }
        }
        public string Prenume
        {
            get { return prenume; }
            set
            {
                if (value.Substring(0, 1) == value.Substring(0, 1).ToUpper())
                {
                    prenume = value;
                }

            }
        }
        public string Cnp
        {
            get { return cnp; }
            set
            {
                    if(value.Length == 13)
                {
                    cnp = value;
                } else
                {
                    cnp = "NEDEFINIT";
                }
                   
                

            }
        }
        public int Varsta
        {
            get { return varsta; }
            set
            {
                
                    varsta = value;
                

            }
        }
        public double Greutate
        {
            get { return greutate; }
            set
            {

                greutate = value;


            }
        }
        public double Inaltime
        {
            get { return inaltime; }
            set
            {

                inaltime = value;


            }
        }
        public double TemperaturaCorp
        {
            get { return temperaturaCorp; }
            set
            {

                temperaturaCorp = value;


            }
        }









        public string toScreenPacient()
        {
            return ($"Pacient: CodPacient -> {codPacient} Nume -> {nume} Prenume -> {prenume} CNP -> {cnp} Varsta -> {varsta} Greutate -> {greutate} Inaltime -> {inaltime} TemperaturaCorp -> {temperaturaCorp}");
        }
    }
}
