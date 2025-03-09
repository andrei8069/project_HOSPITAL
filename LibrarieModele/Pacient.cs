using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace LibrarieModele
{
     public class Pacient
    {
        
        public string Nume {get; set;}
        public string Prenume {get; set;}
        public string Cnp {get; set;}
        public int Varsta {get; set;}
        public double Greutate {get; set;}
        public double Inaltime {get; set;}
        public double TemperaturaCorp {get; set;}
        public int CodPacient {get; set;}

        private static int codPacientStatic = 0;

      
        
        public Pacient()
        {
            Nume = string.Empty;
            Prenume = string.Empty;
            Cnp = string.Empty;
            Varsta = 0;
            Greutate = 0.0;
            Inaltime = 0.0;
            TemperaturaCorp = 0.0;
            codPacientStatic++;
            CodPacient = codPacientStatic;
        }
        public Pacient(string nume , string prenume , string cnp ,int varsta , double greutate , double inaltime , double temperaturaCorp)
        {
            this.Nume = nume;
            this.Prenume = prenume;
            this.Cnp = cnp;
            this.Varsta = varsta;
            this.Greutate = greutate;
            this.Inaltime = inaltime;
            this.TemperaturaCorp = temperaturaCorp;
            codPacientStatic++;
            CodPacient = codPacientStatic;
        }


        public string toScreenPacient()
        {
            return ($"Pacient: CodPacient -> {CodPacient} Nume -> {Nume} Prenume -> {Prenume} CNP -> {Cnp} Varsta -> {Varsta} Greutate -> {Greutate} Inaltime -> {Inaltime} TemperaturaCorp -> {TemperaturaCorp}");
        }
    }
}
