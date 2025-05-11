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

        //public SectieSpital[] GetSectie(out int nrSectii)
        //{
        //    SectieSpital[] sectii = new SectieSpital[NR_MAX_SECTII];
        //    using (StreamReader streamReader = new StreamReader(numeFisier))
        //    {
        //        string linieFisier;
        //        nrSectii = 0;
        //        while ((linieFisier = streamReader.ReadLine()) != null)
        //        {
        //            sectii[nrSectii++] = new SectieSpital(linieFisier);

        //        }
        //    }
        //    Array.Resize(ref sectii, nrSectii);
        //    return sectii;
        //}

        public List<SectieSpital> GetSectii()
        {
            List<SectieSpital> sectii = new List<SectieSpital>();

            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    sectii.Add(new SectieSpital(linieFisier));
                }
            }

            return sectii;
        }

        public SectieSpital FindSectie(string id)
        {
            SectieSpital sectieSpital;
            using (StreamReader citireFisier = new StreamReader(numeFisier))
            {
                string linieFisier;
                while((linieFisier = citireFisier.ReadLine())!= null)
                {
                    sectieSpital = new SectieSpital(linieFisier);
                    if(sectieSpital.CodSectie.ToString() == id)
                    {
                        return sectieSpital;
                    }
                }
            }
            return null;

        }
    }
}
