using LibrarieModele;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NivelStocareDate
{
    public class Pacienti_FISIERTEXT
    {
        private const int NR_MAX_PACIENTI = 500;
        private string numeFisier;

        public Pacienti_FISIERTEXT(string numeFisier)
        {
            this.numeFisier = numeFisier;

            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        public void AddPacient(Pacient pacient)
        {
            using(StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, true))
            {
                streamWriterFisierText.WriteLine(pacient.ConversieLaSir_PentruFisier());
            }
        }
        public Pacient[] GetPacienti(out int nrPacienti)
        {
            Pacient[] pacienti = new Pacient[NR_MAX_PACIENTI];
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                nrPacienti = 0;
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    pacienti[nrPacienti++] = new Pacient(linieFisier);
                    //Console.WriteLine(pozitieVectorPacient);
                    //Console.WriteLine("getPacienti");
                }
            }
            Array.Resize(ref pacienti, nrPacienti);
            return pacienti;
        }
        public Pacient FindCNP(string cnp)
        {
            Pacient pacient;
            using (StreamReader citireFisier = new StreamReader(numeFisier))
            {
                string linieFisier;
                while ((linieFisier = citireFisier.ReadLine()) != null)
                {
                    pacient = new Pacient(linieFisier);
                    if (pacient.Cnp == cnp)
                    {
                        return pacient;
                    }
                }
            }
            return null;

        }

    }
}
