using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarieModele
{
    [Flags]
    public enum DotariSectie
    {
        Nespecificat = 0,
        Ventilatie = 1 << 0, //1
        Monitorizare = 1 << 1,//2
        Izolare = 1 << 2,//4
        Radiologie = 1 << 3,//8
        Reanimare = 1 << 4,//16
        Internet = 1 << 5//32
    }
}
