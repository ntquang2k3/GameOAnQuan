using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TroChoiOAnQuan
{
    public class OAnQuan
    {
        NguoiChoi player1 = new NguoiChoi("Player1", 0, 0);

        public NguoiChoi Player1
        {
            get { return player1; }
            set { player1 = value; }
        }
        NguoiChoi player2 = new NguoiChoi("Player2", 0, 0);

        public NguoiChoi Player2
        {
            get { return player2; }
            set { player2 = value; }
        }
        int checkNguoiChoi = -1; // -1 là người chơi 1 đi 1 là người chơi 2 đi

        public int CheckNguoiChoi
        {
            get { return checkNguoiChoi; }
            set { checkNguoiChoi = value; }
        }
        //Thuộc tính
        List<O> lstO = new List<O>();

        internal List<O> LstO
        {
            get { return lstO; }
            set { lstO = value; }
        }
        //Khởi tạo không tham số
        public OAnQuan()
        {
            LstO = new List<O>();
            // Thêm các phần tử vào danh sách mảng 1 chiều
            for (int i = 0; i < 12; i++) // Ô ăn Quan gồm có 12 ô
            //Ô 0 và Ô 6 mang giá trị là Ô CHỨA QUAN 
            {
                if (i == 0 || i == 6)
                {
                    O OQuan = new OQuan(0, 1);
                    LstO.Add(OQuan);
                    continue;
                }
                O O = new O(5);
                LstO.Add(O);
            }
            CheckNguoiChoi = -1;
        }
        public OAnQuan(OAnQuan saochep)
        {
            this.Player1 = saochep.Player1;
            this.Player2 = saochep.Player2;
            this.LstO = saochep.LstO;
            this.CheckNguoiChoi = saochep.CheckNguoiChoi;
        }
        public OAnQuan Clone()
        {
            OAnQuan copy = new OAnQuan();

            // Sao chép tất cả dữ liệu quan trọng vào bản sao
            copy.Player1 = this.Player1.Clone();
            copy.Player2 = this.Player2.Clone();
            O OQuan0 = (OQuan)copy.LstO[0];
            OQuan0 = ((OQuan)this.LstO[0]).Clone();
            copy.LstO[1] = this.LstO[1].Clone();
            copy.LstO[2] = this.LstO[2].Clone();
            copy.LstO[3] = this.LstO[3].Clone();
            copy.LstO[4] = this.LstO[4].Clone();
            copy.LstO[5] = this.LstO[5].Clone();
            O OQuan6 = (OQuan)copy.LstO[6];
            OQuan6 = ((OQuan)this.LstO[6]).Clone();
            copy.LstO[7] = this.LstO[7].Clone();
            copy.LstO[8] = this.LstO[8].Clone();
            copy.LstO[9] = this.LstO[9].Clone();
            copy.LstO[10] = this.LstO[10].Clone();
            copy.LstO[11] = this.LstO[11].Clone();
            copy.CheckNguoiChoi = this.CheckNguoiChoi;

            return copy;
        }
        //Khởi tạo có tham số
        //Phương thức
        public List<NuocDiChuyen> Actions()
        {
            List<NuocDiChuyen> nuocCoTheDi = new List<NuocDiChuyen>();

            if (this.CheckNguoiChoi == -1)
            {
                for (int i = 1; i <= 5; i++)
                {
                    if (this.LstO[i].SoDan != 0)
                    {
                        NuocDiChuyen nuocdiphai = new NuocDiChuyen(i, "Right");
                        NuocDiChuyen nuocditrai = new NuocDiChuyen(i, "Left");
                        nuocCoTheDi.Add(nuocdiphai);
                        nuocCoTheDi.Add(nuocditrai);
                    }
                }
            }
            else
            {
                for (int i = 7; i <= 11; i++)
                {
                    if (this.LstO[i].SoDan != 0)
                    {
                        NuocDiChuyen nuocdiphai = new NuocDiChuyen(i, "Right");
                        NuocDiChuyen nuocditrai = new NuocDiChuyen(i, "Left");
                        nuocCoTheDi.Add(nuocdiphai);
                        nuocCoTheDi.Add(nuocditrai);
                    }
                }
            }
            return nuocCoTheDi;
        }
        //Xuất thử giá trị của bàn đã tạo
        public void xuat()
        {
            // Truy cập và hiển thị giá trị từ danh sách hai chiều
            for (int i = 0; i < 12; i++)
            {
                if (i == 0 || i == 6)
                {
                    OQuan OQuan = (OQuan)lstO[i];
                    if (OQuan.CoQuan == 1)
                        Console.WriteLine("Ô Quan hiện đang CÓ QUÂN QUAN và đang có {0} quân", OQuan.SoDan);
                    else
                        Console.WriteLine("Ô Quan hiện đang KHÔNG CÓ QUÂN QUAN và đang có {0} quân", OQuan.SoDan);
                    continue;
                }
                Console.WriteLine("Ô dân hiện đang có {0} quân", lstO[i].SoDan);
            }
            Console.WriteLine("Player1 đã ăn được {0} quan và {1} dân", this.Player1.SoQuanDaAn, this.Player1.SoDanDaAn);
            Console.WriteLine("Player2 đã ăn được {0} quan và {1} dân", this.Player2.SoQuanDaAn, this.Player2.SoDanDaAn);
            Console.WriteLine();
        }

    }
    //Phương thức truyền vào trạng thái của bàn chơi trả về một trạng thái mới đến lượt của người chơi tiếp theo
}
