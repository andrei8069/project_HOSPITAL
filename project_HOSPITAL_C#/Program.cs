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

            string choice;
            do
            {
                Console.WriteLine("RP: Introducere pacient de la tastatura"); //read pacient
                Console.WriteLine("RS: Introducere sectie de la tastatura"); //read sectie

                Console.WriteLine("SP: Afisarea pacientului dorit introdus de la tastatura"); //screen pacient
                Console.WriteLine("SS: Afisarea sectiei dorite introduse de la tastatura"); //screen sectie

                Console.WriteLine("TP: Afiseaza toti pacientii"); //afisare totala pacienti
                Console.WriteLine("TS: Afiseaza toate sectiile"); //afisare totala sectii


                Console.WriteLine("RECP: căutarea după anumite criterii pentru pacienti");
                Console.WriteLine("RECS: căutarea după anumite criterii pentru sectie spital");


                Console.WriteLine("Q: Quit program");  // quit

                Console.WriteLine("Introduceti optiunea dorita: ");
                choice = Console.ReadLine();

                switch (choice.ToUpper())
                {
                    case "RP":
                        Pacient pacientNou;
                        pacientNou = CitirePacientTastatura();
                        HostPacienti.AdaugarePacienti(pacientNou);
       

                        break;

                    case "RS":
                        SectieSpital sectieSpitalNoua;
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

                    case "RECP":
                        Console.WriteLine("Introduceti numele sectiunii unde doriti sa faceti modificari");
                        string sectiunePacient = Console.ReadLine();
                        switch(sectiunePacient.ToUpper())
                        {
                            case "NUME":
                                for(int i =0; i < HostPacienti.pozitieVectorPacient; i++)
                                {
                                    if(HostPacienti.vectorPacient[i].Nume == "Maria")
                                    {
                                        Console.WriteLine("SUCCES! COD FUNCTIONAL");
                                    }
                                }
                               

                                break;
                            case "PRENUME":
                                for (int i = 0; i < HostPacienti.pozitieVectorPacient; i++)
                                {
                                    if (HostPacienti.vectorPacient[i].Prenume == "Maria")
                                    {

                                    }
                                }
                                
                                break;
                            case "CNP":
                                for (int i = 0; i < HostPacienti.pozitieVectorPacient; i++)
                                {
                                    if (HostPacienti.vectorPacient[i].Cnp == "11212212122112")
                                    {

                                    }
                                }
                                
                                break;
                            case "VARSTA":
                                for (int i = 0; i < HostPacienti.pozitieVectorPacient; i++)
                                {
                                    if (HostPacienti.vectorPacient[i].Varsta == 12)
                                    {

                                    }
                                }
                                
                                break;
                            case "GREUTATE":
                                for (int i = 0; i < HostPacienti.pozitieVectorPacient; i++)
                                {
                                    if (HostPacienti.vectorPacient[i].Greutate == 0.0)
                                    {

                                    }
                                }
                                
                                break;
                            case "INALTIME":
                                for (int i = 0; i < HostPacienti.pozitieVectorPacient; i++)
                                {
                                    if ( HostPacienti.vectorPacient[i].Inaltime == 0.0)
                                    {

                                    }
                                }
                               
                                break;
                            case "TEMPERATURACORP":
                                for (int i = 0; i < HostPacienti.pozitieVectorPacient; i++)
                                {
                                    if (HostPacienti.vectorPacient[i].TemperaturaCorp == 0.0)
                                    {

                                    }
                                }
                                
                                break;
                            default:
                                Console.WriteLine("Optiunea este invalida");
                                break;
                        }

                        break;
                    case "RECS":
                        Console.WriteLine("Introduceti numele sectiunii unde doriti sa faceti modificari");
                        string sectiuneSectie = Console.ReadLine();
                        switch (sectiuneSectie.ToUpper())
                        {

                            case "NUMESECTIE":
                                for (int i = 0; i < HostSectii.pozitieVectorSpital; i++)
                                {
                                    if (HostSectii.vectorSectieSpital[i].NumeSectie == "Cardiologie")
                                    {
                                        Console.WriteLine("SUCCES! COD FUNCTIONAL");
                                    }
                                }
                                
                                break;
                            case "ETAJ":
                                for (int i = 0; i < HostSectii.pozitieVectorSpital; i++)
                                {
                                    if (HostSectii.vectorSectieSpital[i].Etaj == 0)
                                    {

                                    }
                                }
                                
                                break;
                            case "CAPACITATEMAXIMA":
                                for (int i = 0; i < HostSectii.pozitieVectorSpital; i++)
                                {
                                    if (HostSectii.vectorSectieSpital[i].CapacitateMaxima == 0)
                                    {

                                    }
                                }
                                
                                break;
                            case "NRPACIENTIINTERNATI":
                                for (int i = 0; i < HostSectii.pozitieVectorSpital; i++)
                                {
                                    if (HostSectii.vectorSectieSpital[i].NrPacientiInternati == 0)
                                    {

                                    }
                                }
                                
                                break;
                            case "TEMPERATURAMEDIU":
                                for (int i = 0; i < HostSectii.pozitieVectorSpital; i++)
                                {
                                    if (HostSectii.vectorSectieSpital[i].TemperaturaMediu == 0.0)
                                    {

                                    }
                                }
                                
                                break;
                            case "SUPRAFATASECTIE":
                                for (int i = 0; i < HostSectii.pozitieVectorSpital; i++)
                                {
                                    if (HostSectii.vectorSectieSpital[i].SuprafataSectie == 0.0)
                                    {

                                    }
                                }
                                
                                break;
                            case "BUGETSECTIE":
                                for (int i = 0; i < HostSectii.pozitieVectorSpital; i++)
                                {
                                    if (HostSectii.vectorSectieSpital[i].BugetSectie == 0.0)
                                    {

                                    }
                                }
                                
                                break;
                            default:
                                Console.WriteLine("Optiunea este invalida");
                                break;
                        }
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
