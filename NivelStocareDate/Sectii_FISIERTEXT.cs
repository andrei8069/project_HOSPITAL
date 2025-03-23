using LibrarieModele;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NivelStocareDate
{
    public class Sectii_FISIERTEXT
    {
        private const int NR_MAX_SECTII = 200;
        private string numeFisier;
        public Sectii_FISIERTEXT(string numeFisier)
        {
            this.numeFisier = numeFisier;
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }
        public void AddSectii(SectieSpital sectie)
        {
            using(StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, true))
            {
                streamWriterFisierText.WriteLine(sectie.ConversieLaSir_PentruFisier());
            }
        }

        public SectieSpital[] GetSectie(out int pozitieVectorSpital)
        {
            SectieSpital[] vectorSectieSpital = new SectieSpital[NR_MAX_SECTII];
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                pozitieVectorSpital = 0;
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    vectorSectieSpital[pozitieVectorSpital++] = new SectieSpital(linieFisier);

                }
            }
            return vectorSectieSpital;
        }
        public SectieSpital FindNumeSectie(string numeSectie)
        {
            SectieSpital sectieSpital;
            using (StreamReader citireFisier = new StreamReader(numeFisier))
            {
                string linieFisier;
                while((linieFisier = citireFisier.ReadLine())!= null)
                {
                    sectieSpital = new SectieSpital(linieFisier);
                    if(sectieSpital.NumeSectie == numeSectie)
                    {
                        return sectieSpital;
                    }
                }
            }
            return null;

        }
    }
}
