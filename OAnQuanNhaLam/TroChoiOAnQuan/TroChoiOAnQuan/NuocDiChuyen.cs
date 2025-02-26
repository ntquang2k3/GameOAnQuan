using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TroChoiOAnQuan
{
    public class NuocDiChuyen
    {
        int vitri;


        string diTraiPhai;

        public string DiTraiPhai
        {
            get { return diTraiPhai; }
            set { diTraiPhai = value; }
        }

        public int Vitri
        {
            get { return vitri; }
            set { vitri = value; }
        }

        public NuocDiChuyen()
        { 
        }
        public NuocDiChuyen(int vitrichon, string rightOrLeft)
        {
            Vitri = vitrichon;
            DiTraiPhai = rightOrLeft;
        }
    }
}
