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
        private const int GRUPASANGE = 8;
        private const int AFECTIUNI = 9;
        
        public string Nume {get; set;}
        public string Prenume {get; set;}
        public string Cnp {get; set;}
        public int Varsta {get; set;}
        public double Greutate {get; set;}
        public double Inaltime {get; set;}
        public double TemperaturaCorp {get; set;}
        public int CodPacient {get; set;}

        public GrupaSangePacient Grupa { get; set; }

        public AfectiuniMedicale AfectiuniMed { get; set; }

        //private static int codPacientStatic = 0;

      
        //public static void SeteazaUltimulCod(int cod)
        //{
        //    codPacientStatic = cod;
        //}
        public Pacient()
        {
            Nume = string.Empty;
            Prenume = string.Empty;
            Cnp = string.Empty;
            Varsta = 0;
            Greutate = 0.0;
            Inaltime = 0.0;
            TemperaturaCorp = 0.0;
            //codPacientStatic++;
            //CodPacient = codPacientStatic;
        }
        public Pacient(int codPacient,string nume , string prenume , string cnp ,int varsta , double greutate , double inaltime , double temperaturaCorp, GrupaSangePacient grupa , AfectiuniMedicale afectiuni)
        {
            
            this.Nume = nume;
            this.Prenume = prenume;
            this.Cnp = cnp;
            this.Varsta = varsta;
            this.Greutate = greutate;
            this.Inaltime = inaltime;
            this.TemperaturaCorp = temperaturaCorp;
            this.Grupa = grupa;
            this.AfectiuniMed = afectiuni;
            //codPacientStatic++;
            //CodPacient = codPacientStatic;
            this.CodPacient = codPacient;
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
            if (Enum.TryParse(dateFisier[GRUPASANGE] , out GrupaSangePacient grupaResult)){
                this.Grupa = grupaResult;
            }
            if (Enum.TryParse(dateFisier[AFECTIUNI],out AfectiuniMedicale afectiuniResult))
            {
                this.AfectiuniMed = afectiuniResult;
            }




        }

        public string toScreenPacient()
        {
            return ($"Pacient: CodPacient -> {CodPacient} Nume -> {Nume} Prenume -> {Prenume} CNP -> {Cnp} Varsta -> {Varsta} Greutate -> {Greutate} Inaltime -> {Inaltime} TemperaturaCorp -> {TemperaturaCorp} GrupaPacient -> {Grupa} Afectiuni -> {AfectiuniMed}");
        }

        public string ConversieLaSir_PentruFisier()
        {
            string obiectPacientPentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}",
                    SEPARATOR_PRINCIPAL_FISIER,
                    CodPacient.ToString(),
                    (Nume ?? "Necunoscut"),
                    (Prenume ?? "Necunoscut"),
                    (Cnp ?? "Necunoscut"),
                    Varsta,
                    Greutate,
                    Inaltime,
                    TemperaturaCorp,
                    Grupa,
                    //Enum.GetName(typeof(GrupaSangePacient),Grupa),
                    //Enum.GetName(typeof(AfectiuniMedicale),AfectiuniMed)
                    //AfectiuniMed.ToString()
                    AfectiuniMed
                );
            return obiectPacientPentruFisier;
        }
    }
}
