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
        private const char SEPARATOR_PRINCIPAL_FISIER = ';';

        private const int ID = 0;
        private const int NUME = 1;
        private const int PRENUME = 2;
        private const int CNP = 3;
        private const int VARSTA = 4;
        private const int GREUTATE = 5;
        private const int INALTIME = 6;
        private const int TEMPERATURACORP = 7;
        
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


        public Pacient(string linieFisier)
        {
            var dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);

            this.CodPacient = Convert.ToInt32(dateFisier[ID]);
            
            this.Nume = dateFisier[NUME];
            this.Prenume = dateFisier[PRENUME];
            this.Cnp = dateFisier[CNP];
            this.Varsta =  Convert.ToInt32(dateFisier[VARSTA]);
            this.Greutate =  Convert.ToDouble(dateFisier[GREUTATE]);
            this.Inaltime = Convert.ToDouble(dateFisier[INALTIME]);
            this.TemperaturaCorp = Convert.ToDouble(dateFisier[TEMPERATURACORP]);





        }

        public string toScreenPacient()
        {
            return ($"Pacient: CodPacient -> {CodPacient} Nume -> {Nume} Prenume -> {Prenume} CNP -> {Cnp} Varsta -> {Varsta} Greutate -> {Greutate} Inaltime -> {Inaltime} TemperaturaCorp -> {TemperaturaCorp}");
        }

        public string ConversieLaSir_PentruFisier()
        {
            string obiectPacientPentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}",
                    SEPARATOR_PRINCIPAL_FISIER,
                    CodPacient.ToString(),
                    (Nume ?? "Necunoscut"),
                    (Prenume ?? "Necunoscut"),
                    (Cnp ?? "Necunoscut"),
                    Varsta,
                    Greutate,
                    Inaltime,
                    TemperaturaCorp
                );
            return obiectPacientPentruFisier;
        }
    }
}
