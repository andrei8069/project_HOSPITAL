using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.ComponentModel;
using LibrarieModele;
using NivelStocareDate;
using System.Text.RegularExpressions;




namespace project_HOSPITAL_C_
{

    class Program
    {


        

        static void Main(string[] args)
        {
            Pacienti HostPacienti = new Pacienti();
            Sectii HostSectii = new Sectii();

            string numeFisierPacienti = ConfigurationManager.AppSettings["NumeFisierPacienti"];
            string numeFisierSectii = ConfigurationManager.AppSettings["NumeFisierSectii"];
            string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string caleFisierPacienti = locatieFisierSolutie + "\\" + numeFisierPacienti;
            string caleFisierSectii = locatieFisierSolutie + "\\" + numeFisierSectii;


            Pacienti_FISIERTEXT adminPacienti = new Pacienti_FISIERTEXT(caleFisierPacienti);

            Sectii_FISIERTEXT adminSectii = new Sectii_FISIERTEXT(caleFisierSectii);

            Pacient pacientNou = new Pacient();
            SectieSpital sectieSpitalNoua = new SectieSpital();
            //int pozitieVectorPacient =0;
            //int pozitieVectorSpital =0;
            int nrPacienti = 0;
            int nrSectii = 0;
            adminPacienti.GetPacienti(out nrPacienti);
            adminSectii.GetSectie(out nrSectii);

            string choice;
            do
            {
                Console.WriteLine("A: Introducere pacient de la tastatura"); //read pacient
                Console.WriteLine("B: Introducere sectie de la tastatura"); //read sectie

                Console.WriteLine("C: Afisarea pacientului dorit introdus de la tastatura"); //screen pacient
                Console.WriteLine("D: Afisarea sectiei dorite introduse de la tastatura"); //screen sectie

                Console.WriteLine("E: Afiseaza toti pacientii"); //afisare totala pacienti
                Console.WriteLine("F: Afiseaza toate sectiile"); //afisare totala sectii

                Console.WriteLine("G: SALVARE PACIENT IN FISIER"); 
                Console.WriteLine("H: AFISARE PACIENTI DIN FISIER"); 

                Console.WriteLine("I: SALVARE SECTIE IN FISIER"); 
                Console.WriteLine("J: AFISARE SECTII DIN FISIER");

                Console.WriteLine("K: cautare pacient dupa cnp in fisier");
                Console.WriteLine("L: cautare sectie dupa id in fisier");

                Console.WriteLine("Q: Quit program");  // quit

                Console.WriteLine("Introduceti optiunea dorita: ");
                choice = Console.ReadLine();

                switch (choice.ToUpper())
                {
                    
                    case "A":
                        pacientNou = CitirePacientTastatura();
                        HostPacienti.AdaugarePacienti(pacientNou);
       

                        break;

                    case "B":
                        sectieSpitalNoua = CitireSectieTastatura();

                        HostSectii.AdaugareSectii(sectieSpitalNoua);
                        break;

                    case "C":
                        Console.WriteLine("Introduceti numarul pacientului pe care doriti sa fie afisat pe ecran:");
                        bool valueNrPacient = Int32.TryParse(Console.ReadLine(), out int nrPacient);
                        

                        Console.WriteLine(HostPacienti.AfisarePacient(nrPacient));
                        break;

                    case "D":
                        Console.WriteLine("Introduceti numarul sectiei pe care doriti sa fie afisata pe ecran:");
                        bool valueNrSectie = Int32.TryParse(Console.ReadLine(), out int nrSectie);


                        Console.WriteLine(HostSectii.AfisareSectie(nrSectie));
                        break;
                    case "E":
                        HostPacienti.AfisarePacienti();
                        
                        break;
                    case "F":
                        HostSectii.AfisareSectii();
                        break;
                    case "J":
                        SectieSpital[] vectorSectieSpital = adminSectii.GetSectie(out nrSectii);
                        AfisareSectii(vectorSectieSpital,nrSectii);
                        break;
                    case "I":
                        //int codSectie = ++pozitieVectorSpital;
                        int codSectie = ++nrSectii;
                        sectieSpitalNoua.CodSectie = codSectie;

                        adminSectii.AddSectii(sectieSpitalNoua);
                        break;
                    case "H":
                        Pacient[] vectorPacient = adminPacienti.GetPacienti(out nrPacienti);
                        //Console.WriteLine("menu");
                        //Console.WriteLine(pozitieVectorPacient);
                        AfisarePacienti(vectorPacient, nrPacienti);
                        break;
                    case "G":
                        //int codPacient = ++pozitieVectorPacient;
                        int codPacient = ++nrPacienti;
                        pacientNou.CodPacient = codPacient;
                        adminPacienti.AddPacient(pacientNou);


                        break;

                    case "K":
                        Console.WriteLine("Introduceti CNP-ul pacientului pe care doriti sa il afisati ");
                        
                        Console.WriteLine(adminPacienti.FindCNP(Console.ReadLine()).toScreenPacient());
                        
                        break;
                    case "L":
                        Console.WriteLine("Introduceti id-ul sectiei pe care doriti sa o afisati ");
                        
                        Console.WriteLine(adminSectii.FindSectie(Console.ReadLine()).toScreenSectie());
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
        public static void AfisareSectii(SectieSpital[] vectorSectieSpital , int nrSectii )
        {
            Console.WriteLine("Sectiile sunt:");
            for(int contor =0; contor < nrSectii; contor++)
            {
                string infoSectieSpital = vectorSectieSpital[contor].toScreenSectie();
                Console.WriteLine(infoSectieSpital);
            }
        }
        public static void AfisarePacienti(Pacient[] vectorPacient , int nrPacienti)
        {
            Console.WriteLine("Pacientii sunt:");
            for (int contor =0; contor < nrPacienti; contor++)
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

            Console.WriteLine("Selectati grupa de sange a pacientului:");
            foreach(GrupaSangePacient grupa in Enum.GetValues(typeof(GrupaSangePacient)))
            {
                if (grupa != GrupaSangePacient.Nimic)
                {
                    Console.WriteLine($"{(int)grupa} - {grupa}");
                }
                
            }
            bool valid = int.TryParse(Console.ReadLine(), out int choiceGrupa);
            GrupaSangePacient grupaSelectata = GrupaSangePacient.Nimic;
            if(valid == true && Enum.IsDefined(typeof(GrupaSangePacient), choiceGrupa)) // se verifica daca e un nr introdus si se mai verifica daca numarul este definit in cadrul enum
            {
                grupaSelectata = (GrupaSangePacient)choiceGrupa;
                Console.WriteLine($"Ai selectat grupa de sange: {grupaSelectata}");
            }
            else
            {
                Console.WriteLine("Alegere invalida!");
            }

            Console.WriteLine("Selectati afectiunea pe care o are pacientul:");
            foreach(AfectiuniMedicale afectiuni in Enum.GetValues(typeof(AfectiuniMedicale)))
            {
                if(afectiuni!= AfectiuniMedicale.Nimic)
                {
                    Console.WriteLine($"{(int)afectiuni} - {afectiuni}");
                }
                
            }
            Console.WriteLine("Introduceti numerele afectiunilor separate prin virgula:");
            string input = Console.ReadLine();
            AfectiuniMedicale afectiuniSelectate = AfectiuniMedicale.Nimic;
            string[] optiuni = input.Split(',');

            foreach(string opt in optiuni)
            {
                if(int.TryParse(opt.Trim(),out int valoare) && Enum.IsDefined(typeof(AfectiuniMedicale), valoare)){
                    //afectiuniSelectate |= (AfectiuniMedicale)valoare;
                    afectiuniSelectate = afectiuniSelectate | (AfectiuniMedicale)valoare; // | e sau pt biti
                } //trim elimina spatiile de la inceput si sfarsit

            }
            Console.WriteLine($"Ai selectat: {afectiuniSelectate}");


            Pacient pacient = new Pacient(0,nume, prenume, cnp, varsta, greutate, inaltime, temperaturaCorp , grupaSelectata,afectiuniSelectate);
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

            Console.WriteLine("Selectati statusul de functionare al sectiei:");
            foreach (StatusFunctionareSectie status in Enum.GetValues(typeof(StatusFunctionareSectie)))
            {
                if (status != StatusFunctionareSectie.Nimic)
                {
                    Console.WriteLine($"{(int)status} - {status}");

                }
            }
            bool valid = int.TryParse(Console.ReadLine(), out int choiceStatus);
            StatusFunctionareSectie statusSelectat = StatusFunctionareSectie.Nimic;
            if (valid == true && Enum.IsDefined(typeof(StatusFunctionareSectie), choiceStatus)) // se verifica daca e un nr introdus si se mai verifica daca numarul este definit in cadrul enum
            {
                statusSelectat = (StatusFunctionareSectie)choiceStatus;
                Console.WriteLine($"Ai selectat statul de functionare: {statusSelectat}");
            }
            else
            {
                Console.WriteLine("Alegere invalida!");
            }


            Console.WriteLine("Selectati dotarile sectiei din spital:");
            foreach (DotariSectie dotari in Enum.GetValues(typeof(DotariSectie)))
            {
                if (dotari != DotariSectie.Nimic)
                {
                    Console.WriteLine($"{(int)dotari} - {dotari}");
                }

            }
            Console.WriteLine("Introduceti numerele dotarilor din sectie separate prin virgula");
            string input = Console.ReadLine();
            DotariSectie dotariSelectate = DotariSectie.Nimic;
            string[] optiuni = input.Split(',');

            foreach (string opt in optiuni)
            {
                if (int.TryParse(opt.Trim(), out int valoare) && Enum.IsDefined(typeof(DotariSectie), valoare))
                {
                    dotariSelectate |= (DotariSectie)valoare;
                } //trim elimina spatiile de la inceput si sfarsit

            }
            Console.WriteLine($"Ai selectat: {dotariSelectate}");



            SectieSpital sectieSpital = new SectieSpital(0,numeSectie,etaj,capacitateMaxima,nrPacientiInternati,temperaturaMediu,suprafataSectie,bugetSectie,statusSelectat , dotariSelectate);
            return sectieSpital;


        }
        
       

    }
}
