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
        private List<Pacient> listaPacienti;

        public Pacienti()
        {
            listaPacienti = new List<Pacient>();
        }

        public void AdaugarePacienti(Pacient pacientNou)
        {
            pacientNou.CodPacient = listaPacienti.Count + 1;
            listaPacienti.Add(pacientNou);
        }

        public string AfisarePacient(int nrPacient)
        {
            if (nrPacient >= 1 && nrPacient <= listaPacienti.Count)
            {
                return listaPacienti[nrPacient - 1].toScreenPacient();
            }
            else
            {
                return "Pacient inexistent!";
            }
        }

        public void AfisarePacienti()
        {
            foreach (var pacient in listaPacienti)
            {
                Console.WriteLine(pacient.toScreenPacient());
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
