using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TroChoiOAnQuan
{
    public class NguoiChoi
    {
        int soDanDaAn;
        string whoPlayer;

        public string WhoPlayer
        {
            get { return whoPlayer; }
            set { whoPlayer = value; }
        }
        public int SoDanDaAn
        {
            get { return soDanDaAn; }
            set { soDanDaAn = value; }
        }
        int soQuanDaAn;

        public int SoQuanDaAn
        {
            get { return soQuanDaAn; }
            set
            {
                soQuanDaAn = value;
            }
        }

        public NguoiChoi()
        {
            WhoPlayer = "";
            SoDanDaAn = 0;
            SoQuanDaAn = 0;
        }
        public NguoiChoi Clone()
        {
            NguoiChoi copy = new NguoiChoi();
            copy.SoDanDaAn = this.SoDanDaAn;
            copy.soQuanDaAn = this.SoQuanDaAn;
            copy.WhoPlayer = this.WhoPlayer;
            return copy;
        }
        public NguoiChoi(string name, int a, int b)
        {
            WhoPlayer = name;
            SoDanDaAn = a;
            SoQuanDaAn = b;
        }
    }
}
