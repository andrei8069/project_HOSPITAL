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
        public Pacient[] GetPacienti(out int pozitieVectorPacient)
        {
            Pacient[] vectorPacient = new Pacient[NR_MAX_PACIENTI];
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                pozitieVectorPacient = 0;
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    vectorPacient[pozitieVectorPacient++] = new Pacient(linieFisier);
                    //Console.WriteLine(pozitieVectorPacient);
                    //Console.WriteLine("getPacienti");
                }
            }
            return vectorPacient;
        }
        public Pacient FindNumePacient(string nume)
        {
            Pacient pacient;
            using (StreamReader citireFisier = new StreamReader(numeFisier))
            {
                string linieFisier;
                while ((linieFisier = citireFisier.ReadLine()) != null)
                {
                    pacient = new Pacient(linieFisier);
                    if (pacient.Nume == nume)
                    {
                        return pacient;
                    }
                }
            }
            return null;

        }

    }
}
