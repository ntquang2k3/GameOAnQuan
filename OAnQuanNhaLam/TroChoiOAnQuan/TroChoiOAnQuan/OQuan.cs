using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TroChoiOAnQuan
{
    public class OQuan : O
    {
        int coQuan;

        public int CoQuan
        {
            get { return coQuan; }
            set
            {
                if (value == 1 || value == 0)
                {
                    coQuan = value;
                }
                else
                {
                    throw new ArgumentException("Giá trị không hợp lệ. Chỉ chấp nhận 1 hoặc 0.");
                }
            }
        }

        public OQuan()
        {
            coQuan = 0;
        }
        public OQuan Clone()
        {
            OQuan copy = new OQuan();
            copy.CoQuan = this.CoQuan;
            copy.SoDan = this.SoDan;
            return copy;
        }
        public OQuan(int soquan, int yesorno)
            : base(soquan)
        {
            coQuan = yesorno;
        }
    }
}
