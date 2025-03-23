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
        Nimic = 0,
        VentilatieMecanica = 1 << 0, //1
        MonitorizarePacienti = 1 << 1,//2
        CameraIzolare = 1 << 2,//4
        EchipamentRadiologie = 1 << 3,//8
        EchipamentReanimare = 1 << 4,//16
        AccesInternet = 1 << 5//32
    }
}
