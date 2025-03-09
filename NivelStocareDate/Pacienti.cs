using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarieModele;


namespace NivelStocareDate
{
    public class Pacienti
    {
        const int NR_MAX_PACIENTI = 500;
        public int pozitieVectorPacient = 0;
        public Pacient[] vectorPacient = new Pacient[NR_MAX_PACIENTI];


        public void AdaugarePacienti(Pacient pacientNou)
        {
            vectorPacient[pozitieVectorPacient] = pacientNou;
            pozitieVectorPacient++;
        }
        public string AfisarePacient(int nrPacient)
        {
            return vectorPacient[nrPacient - 1].toScreenPacient();
        }

        public void AfisarePacienti()
        {
            for (int i = 0; i < pozitieVectorPacient; i++)
            {
                Console.WriteLine(vectorPacient[i].toScreenPacient());
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }
}
