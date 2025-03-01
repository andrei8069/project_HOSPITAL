using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_HOSPITAL_C_
{
    class Program
    {
        static void Main(string[] args)
        {
            Pacient pacient1 = new Pacient();
            Pacient pacient2 = new Pacient();

            SectieSpital sectieSpital1 = new SectieSpital();
            SectieSpital sectieSpital2 = new SectieSpital();

            Console.WriteLine(pacient1.toScreenPacient());
            Console.WriteLine(); 

            Console.WriteLine(sectieSpital1.toScreenSectie());
            Console.WriteLine();

            Console.WriteLine(pacient2.toScreenPacient());
            Console.WriteLine();

            Console.WriteLine(sectieSpital2.toScreenSectie());
            Console.WriteLine();


            Pacient pacient3 = new Pacient("Andrei", "Bogdan", "1212111222", 20, 75.5, 1.85, 36);
            Console.WriteLine(pacient3.toScreenPacient());
            Console.WriteLine();

            Console.WriteLine("CNP Pacinet: " + pacient3.Cnp);
            Console.WriteLine();


            SectieSpital sectieSpital3 = new SectieSpital("Cardiologie", 3, 200, 19, 24, 400, 10000000.987);
            Console.WriteLine(sectieSpital3.toScreenSectie());
            Console.WriteLine();

            Console.WriteLine("BUGET SectieSpital: " + sectieSpital3.BugetSectie);
            Console.WriteLine();




        }
    }
}
