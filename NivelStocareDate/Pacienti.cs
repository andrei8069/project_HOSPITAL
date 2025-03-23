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
        private int nrPacienti = 0;
        private Pacient[] vectorPacient = new Pacient[NR_MAX_PACIENTI];


        public Pacienti()
        {
            nrPacienti = 0;
            vectorPacient = new Pacient[NR_MAX_PACIENTI];
        }
        public void AdaugarePacienti(Pacient pacientNou)
        {
            pacientNou.CodPacient = nrPacienti + 1;
            vectorPacient[nrPacienti] = pacientNou;
            nrPacienti++;
        }
        public string AfisarePacient(int nrPacient)
        {
            return vectorPacient[nrPacient - 1].toScreenPacient();
        }

        public void AfisarePacienti()
        {
            for (int i = 0; i < nrPacienti; i++)
            {
                Console.WriteLine(vectorPacient[i].toScreenPacient());
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }
}
