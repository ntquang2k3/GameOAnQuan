using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TroChoiOAnQuan
{
    public class O
    {
        int soDan;

        public int SoDan
        {
            get { return soDan; }
            set { soDan = value; }
        }
        public O()
        {
            SoDan = 5;
        }
        public O Clone()
        {
            O copy = new O();
            copy.SoDan = this.SoDan;
            return copy;
        }
        public O(int soDan)
        {
            this.SoDan = soDan;
        }
    }
}
