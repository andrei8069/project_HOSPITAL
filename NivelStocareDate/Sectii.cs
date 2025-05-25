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
            sectieSpitalNoua.CodSectie = GetNextCodSectie();
            listaSectii.Add(sectieSpitalNoua);
        }

        public int GetNextCodSectie()
        {
            if (listaSectii.Count == 0) return 1;
            return listaSectii.Max(s => s.CodSectie) + 1;
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
            foreach (SectieSpital sectie in listaSectii)
            {
                Console.WriteLine(sectie.toScreenSectie());
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
