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
        private List<SectieSpital> listaSectii;

        public Sectii()
        {
            listaSectii = new List<SectieSpital>();
        }

        public void AdaugareSectii(SectieSpital sectieSpitalNoua)
        {
            sectieSpitalNoua.CodSectie = listaSectii.Count + 1;
            listaSectii.Add(sectieSpitalNoua);
        }

        public string AfisareSectie(int nrSectie)
        {
            if (nrSectie >= 1 && nrSectie <= listaSectii.Count)
            {
                return listaSectii[nrSectie - 1].toScreenSectie();
            }
            else
            {
                return "Sectie inexistenta!";
            }
        }

        public void AfisareSectii()
        {
            foreach (var sectie in listaSectii)
            {
                Console.WriteLine(sectie.toScreenSectie());
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
