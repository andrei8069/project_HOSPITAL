using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarieModele;
using NivelStocareDate;




namespace project_HOSPITAL_C_
{

    class Program
    {


        

        static void Main(string[] args)
        {
            Pacienti HostPacienti = new Pacienti();
            Sectii HostSectii = new Sectii();

            Pacienti_FISIERTEXT adminPacienti = new Pacienti_FISIERTEXT("Pacienti.txt");
            Sectii_FISIERTEXT adminSectii = new Sectii_FISIERTEXT("Sectii.txt");
            Pacient pacientNou = new Pacient();
            SectieSpital sectieSpitalNoua = new SectieSpital();
            int pozitieVectorPacient =0;
            int pozitieVectorSpital =0;

            string choice;
            do
            {
                Console.WriteLine("RP: Introducere pacient de la tastatura"); //read pacient
                Console.WriteLine("RS: Introducere sectie de la tastatura"); //read sectie

                Console.WriteLine("SP: Afisarea pacientului dorit introdus de la tastatura"); //screen pacient
                Console.WriteLine("SS: Afisarea sectiei dorite introduse de la tastatura"); //screen sectie

                Console.WriteLine("TP: Afiseaza toti pacientii"); //afisare totala pacienti
                Console.WriteLine("TS: Afiseaza toate sectiile"); //afisare totala sectii

                Console.WriteLine("SFP: SALVARE PACIENT IN FISIER"); //afisare totala pacienti
                Console.WriteLine("AFP: AFISARE PACIENTI DIN FISIER"); //afisare totala sectii

                Console.WriteLine("SFS: SALVARE SECTIE IN FISIER"); //afisare totala pacienti
                Console.WriteLine("AFS: AFISARE SECTII DIN FISIER"); //afisare totala sectii

                Console.WriteLine("Q: Quit program");  // quit

                Console.WriteLine("Introduceti optiunea dorita: ");
                choice = Console.ReadLine();

                switch (choice.ToUpper())
                {
                    
                    case "RP":
                        pacientNou = CitirePacientTastatura();
                        HostPacienti.AdaugarePacienti(pacientNou);
       

                        break;

                    case "RS":
                        sectieSpitalNoua = CitireSectieTastatura();

                        HostSectii.AdaugareSectii(sectieSpitalNoua);
                        break;

                    case "SP":
                        Console.WriteLine("Introduceti numarul pacientului pe care doriti sa fie afisat pe ecran:");
                        bool valueNrPacient = Int32.TryParse(Console.ReadLine(), out int nrPacient);
                        

                        Console.WriteLine(HostPacienti.AfisarePacient(nrPacient));
                        break;

                    case "SS":
                        Console.WriteLine("Introduceti numarul sectiei pe care doriti sa fie afisata pe ecran:");
                        bool valueNrSectie = Int32.TryParse(Console.ReadLine(), out int nrSectie);


                        Console.WriteLine(HostSectii.AfisareSectie(nrSectie));
                        break;
                    case "TP":
                        HostPacienti.AfisarePacienti();
                        
                        break;
                    case "TS":
                        HostSectii.AfisareSectii();
                        break;
                    case "AFS":
                        SectieSpital[] vectorSectieSpital = adminSectii.GetSectie(out pozitieVectorSpital);
                        AfisareSectii(vectorSectieSpital, pozitieVectorSpital);
                        break;
                    case "SFS":
                        int codSectie = ++pozitieVectorSpital;
                        sectieSpitalNoua.CodSectie = codSectie;

                        adminSectii.AddSectii(sectieSpitalNoua);
                        break;
                    case "AFP":
                        Pacient[] vectorPacient = adminPacienti.GetPacienti(out pozitieVectorPacient);
                        //Console.WriteLine("menu");
                        //Console.WriteLine(pozitieVectorPacient);
                        AfisarePacienti(vectorPacient, pozitieVectorPacient);
                        break;
                    case "SFP":
                        int codPacient = ++pozitieVectorPacient;
                        pacientNou.CodPacient = codPacient;
                        adminPacienti.AddPacient(pacientNou);


                        break;
                    case "Q":
                        return;

                    default:
                        Console.WriteLine("Optiunea invalida!");
                        break;


                }
            } while (choice.ToUpper() != "Q");


            Console.ReadKey();
        }
        public static void AfisareSectii(SectieSpital[] vectorSectieSpital , int pozitieVectorSpital )
        {
            Console.WriteLine("Sectiile sunt:");
            for(int contor =0; contor < pozitieVectorSpital; contor++)
            {
                string infoSectieSpital = vectorSectieSpital[contor].toScreenSectie();
                Console.WriteLine(infoSectieSpital);
            }
        }
        public static void AfisarePacienti(Pacient[] vectorPacient , int pozitieVectorPacient)
        {
            Console.WriteLine("Pacientii sunt:");
            for (int contor =0; contor < pozitieVectorPacient; contor++)
            {
                
                string infoPacient = vectorPacient[contor].toScreenPacient();
                Console.WriteLine(infoPacient);
            }
        }
        public static Pacient CitirePacientTastatura()
        {
            Console.WriteLine("Introduceti datele aferente pacientului (nume,prenume,cnp,varsta,greutate,inaltime,temperatura) : ");
            string nume = Console.ReadLine();
            string prenume = Console.ReadLine();
            string cnp = Console.ReadLine();
            bool valueVarsta = Int32.TryParse( Console.ReadLine(), out int varsta);
            bool valueGreutate = Double.TryParse(Console.ReadLine(), out double greutate);
            bool valueInaltime = Double.TryParse(Console.ReadLine(), out double inaltime);
            bool valueTemperatura = Double.TryParse(Console.ReadLine(), out double temperaturaCorp);


            Pacient pacient = new Pacient(nume, prenume, cnp, varsta, greutate, inaltime, temperaturaCorp);
            return pacient;
        }
        public static SectieSpital CitireSectieTastatura()
        {
            Console.WriteLine("Introduceti datele aferente sectiei (nume,etaj,capacitateMaxima,pacientiInternati,temperatura,suprafata,buget) : ");
            string numeSectie = Console.ReadLine();
            bool valueEtaj = Int32.TryParse(Console.ReadLine(), out int etaj);
            bool valueCapacitateMaxima = Int32.TryParse(Console.ReadLine(), out int capacitateMaxima);
            bool valueNrPacientiInternati = Int32.TryParse(Console.ReadLine(), out int nrPacientiInternati);
            bool valueTemperaturaMediu = Double.TryParse(Console.ReadLine(), out double temperaturaMediu);
            bool valueSuprafataSectie = Double.TryParse(Console.ReadLine(), out double suprafataSectie);
            bool valueBugetSectie = Double.TryParse(Console.ReadLine(), out double bugetSectie);

            SectieSpital sectieSpital = new SectieSpital(numeSectie,etaj,capacitateMaxima,nrPacientiInternati,temperaturaMediu,suprafataSectie,bugetSectie);
            return sectieSpital;


        }
        
       

    }
}
