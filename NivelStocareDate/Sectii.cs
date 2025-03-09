using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarieModele;

namespace NivelStocareDate
{
    public class Sectii
    {
        const int NR_MAX_SECTII = 200;
        public SectieSpital[] vectorSectieSpital = new SectieSpital[NR_MAX_SECTII];
        public int pozitieVectorSpital = 0;

        public void AdaugareSectii(SectieSpital sectieSpitalNoua)
        {
            vectorSectieSpital[pozitieVectorSpital] = sectieSpitalNoua;
            pozitieVectorSpital++;
        }

        public string AfisareSectie(int nrSectie)
        {
            return vectorSectieSpital[nrSectie - 1].toScreenSectie();
        }

        public void AfisareSectii()
        {
            for (int i = 0; i < pozitieVectorSpital; i++)
            {
                Console.WriteLine(vectorSectieSpital[i].toScreenSectie());
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
