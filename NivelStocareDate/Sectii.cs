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
        private SectieSpital[] vectorSectieSpital = new SectieSpital[NR_MAX_SECTII];
        private int nrSectii = 0;

        public Sectii()
        {
            nrSectii = 0;
            vectorSectieSpital = new SectieSpital[NR_MAX_SECTII];
        }
        public void AdaugareSectii(SectieSpital sectieSpitalNoua)
        {
            sectieSpitalNoua.CodSectie = nrSectii + 1;
            vectorSectieSpital[nrSectii] = sectieSpitalNoua;
            nrSectii++;
        }

        public string AfisareSectie(int nrSectie)
        {
            return vectorSectieSpital[nrSectie - 1].toScreenSectie();
        }

        public void AfisareSectii()
        {
            for (int i = 0; i < nrSectii; i++)
            {
                Console.WriteLine(vectorSectieSpital[i].toScreenSectie());
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
