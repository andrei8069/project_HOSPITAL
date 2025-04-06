﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarieModele
{
    [Flags]
    public enum AfectiuniMedicale
    {
        Nespecificat = 0,
        Diabet = 1 << 0,          // 1
        Anemie = 1 << 1,   // 2
        Alergii = 1 << 2,         // 4
        Astm = 1 << 3,            // 8
        Raceala = 1 << 4 //16
    }
}
