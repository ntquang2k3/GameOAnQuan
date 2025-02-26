using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TroChoiOAnQuan
{
    public class ConnectOAnQuan
    {
        //Làm lại vị trí hợp lệ
        public static int Repositon(int vitri)
        {
            int index = vitri;
            // Quay lại đầu mảng nếu cần nếu ô đó đi lùi vượt mức 0
            if (index < 0)
            {
                index += 12;
            }
            // Quay lại đầu mảng nếu cần khi đi tới vượt mức 11
            if (index > 11)
            {
                index -= 12;
            }
            return index;
        }

        public static OAnQuan DuyetTang(int vitriChon, OAnQuan state)
        {
            int daCongThem2 = 0;
            OAnQuan thucthi = state;
            //Khai báo số quân đang có là số quân đang lưu trong list
            int soquandangco = thucthi.LstO[vitriChon].SoDan;
            //array = new int[12] { 0, 5, 5, 5, 5, 5, 0, 5, 5, 5, 5, 5 };
            //Khởi tạo một biến tạm để lưu số quân đang có
            int soquancamtrentay = soquandangco;
            //Lấy quân đó đi rải thì quân đó đang rỗng mang giá trị là không
            thucthi.LstO[vitriChon].SoDan = 0;

            //Chuyển đổi bàn cờ cho lượt người chơi tiếp theo
            thucthi.CheckNguoiChoi *= -1;

            int currentIndex = vitriChon;
            //Duyet xuoi
            while (true)
            {
                soquancamtrentay--;
                //soQuanDaDuocRai++;
                currentIndex++;
                // Quay lại đầu mảng nếu cần
                currentIndex = ConnectOAnQuan.Repositon(currentIndex);

                thucthi.LstO[currentIndex].SoDan++;
                
                // Tạo một phương thức bất đồng bộ bên ngoài để thực hiện độ trễ
                //Task.Delay(timeDelay).Wait(); // Sử dụng .Wait() để đợi hoàn thành mà không ảnh hưởng đến giá trị trả về

                if (soquancamtrentay == 0)
                {
                    currentIndex++;
                    // Quay lại đầu mảng nếu cần
                    currentIndex = ConnectOAnQuan.Repositon(currentIndex);
                    //Nếu ô tiếp theo là ô trống
                    while (currentIndex != 0 && currentIndex != 6)
                    {
                        if (thucthi.LstO[currentIndex].SoDan == 0)
                        {
                            //kiểm tra ô tiếp theo nữa là ô đó có trống hay không
                            if (currentIndex == 11)
                            {
                                currentIndex = -1;
                            }
                            if (thucthi.LstO[currentIndex + 1].SoDan == 0)
                            {
                                //Nếu là ô trống ( 2 ô trống liên tục) thì dừng
                                return thucthi;
                            }
                            //Ngược lại nếu ô đó là ô ăn điểm
                            else
                            {
                                OQuan temp = new OQuan();
                                //Nếu ô được ăn là ô quan
                                if (thucthi.LstO[currentIndex + 1] is OQuan)
                                    temp = (OQuan)thucthi.LstO[currentIndex + 1];
                                if (temp.CoQuan == 1 && temp.SoDan >= 5)
                                {
                                    if (state.CheckNguoiChoi != -1)
                                    {
                                        state.Player1.SoQuanDaAn += 1;
                                        state.Player1.SoDanDaAn += temp.SoDan;
                                    }
                                    else
                                    {
                                        state.Player2.SoQuanDaAn += 1;
                                        state.Player2.SoDanDaAn += temp.SoDan;
                                    }
                                    //Ăn xong ô quan thì cho ô quan về 0
                                    OQuan moi = new OQuan(0, 0);
                                    thucthi.LstO[currentIndex + 1] = moi;

                                }
                                else if (temp.CoQuan == 1 && temp.SoDan < 5)
                                {
                                    return thucthi;
                                }
                                else//Là ô quan nhưng quan đã mất \\ có quan = 0 thì húp hết
                                {//Nếu ăn là ô dân 
                                    //Ăn xong thì ô đó về lại giá trị 0
                                    if (state.CheckNguoiChoi != -1)
                                    {
                                        state.Player1.SoDanDaAn += thucthi.LstO[currentIndex + 1].SoDan;
                                    }
                                    else
                                    {
                                        state.Player2.SoDanDaAn += thucthi.LstO[currentIndex + 1].SoDan;
                                    }
                                    thucthi.LstO[currentIndex + 1].SoDan = 0;
                                }
                                //Khi ăn được 1 ô thì có thể chụp nhiều ô liên tục  
                                currentIndex += 2;
                                daCongThem2++;
                                // Quay lại đầu mảng nếu cần
                                currentIndex = ConnectOAnQuan.Repositon(currentIndex);
                            }
                        }
                        else
                        {
                            //Đã cộng thêm 2 để check húp hết thì giờ phải trừ lại
                            if (daCongThem2 != 0)
                            {
                                currentIndex -= 2 * daCongThem2;
                            }
                            // Quay lại đầu mảng nếu cần
                            currentIndex = ConnectOAnQuan.Repositon(currentIndex);
                            break;
                        }
                    }
                    //if (thoat == true && )
                    //{
                    //    return thucthi;
                    //}
                    //Nếu trên tay đã hết quân thì kiểm tra xem ô tiếp theo có phải quân quan hay không
                    if (currentIndex == 0 || currentIndex == 6 || thucthi.LstO[currentIndex].SoDan == 0)
                        //Nếu đúng là ô quan thì dừng
                        return thucthi;
                    //Nếu ô tiếp theo là quân dân thì tiếp tục lấy số quân đó đem đi rải
                    else if (thucthi.LstO[currentIndex].SoDan != 0)
                    {
                        soquancamtrentay = thucthi.LstO[currentIndex].SoDan;
                        thucthi.LstO[currentIndex].SoDan = 0;
                    }
                    //continue;


                }
            }
            //Dòng phụ
        }

        public static OAnQuan DuyetGiam(int vitriChon, OAnQuan state)
        {
            int daTruDi2 = 0; // số lần trừ
            OAnQuan thucthi = state;
            //Khai báo số quân đang có là số quân đang lưu trong list
            int soquandangco = thucthi.LstO[vitriChon].SoDan;
            //array = new int[12] { 0, 5, 5, 5, 5, 5, 0, 5, 5, 5, 5, 5 };
            //Khởi tạo một biến tạm để lưu số quân đang có
            int soquancamtrentay = soquandangco;
            //Lấy quân đó đi rải thì quân đó đang rỗng mang giá trị là không
            thucthi.LstO[vitriChon].SoDan = 0;
            //Chưa rải được quân nào
            //int soQuanDaDuocRai = 0;

            //Chuyển đổi bàn cờ cho lượt người chơi tiếp theo
            thucthi.CheckNguoiChoi *= -1;

            int currentIndex = vitriChon;
            //Duyet ngược
            while (true)
            {
                soquancamtrentay--;
                //soQuanDaDuocRai++;
                currentIndex--;
                // Quay lại đầu mảng nếu cần
                currentIndex = ConnectOAnQuan.Repositon(currentIndex);

                thucthi.LstO[currentIndex].SoDan++;


                if (soquancamtrentay == 0)
                {
                    currentIndex--;
                    // Quay lại đầu mảng nếu cần
                    currentIndex = ConnectOAnQuan.Repositon(currentIndex);
                    //Nếu ô tiếp theo là ô trống
                    while (currentIndex != 0 && currentIndex != 6)
                    {
                        if (thucthi.LstO[currentIndex].SoDan == 0)
                        {
                            //kiểm tra ô tiếp theo nữa là ô đó có trống hay không
                            if (currentIndex == 0)
                            {
                                currentIndex = 12;
                            }
                            if (thucthi.LstO[currentIndex - 1].SoDan == 0)
                            {
                                //Nếu là ô trống ( 2 ô trống liên tục) thì dừng
                                return thucthi;
                            }
                            //Ngược lại nếu ô đó là ô ăn điểm
                            else
                            {
                                OQuan temp = new OQuan();
                                //Nếu ô được ăn là ô quan
                                if (thucthi.LstO[currentIndex - 1] is OQuan)
                                    temp = (OQuan)thucthi.LstO[currentIndex - 1];
                                if (temp.CoQuan == 1 && temp.SoDan >= 5)
                                {
                                    if (state.CheckNguoiChoi != -1)
                                    {
                                        state.Player1.SoQuanDaAn += 1;
                                        state.Player1.SoDanDaAn += temp.SoDan;
                                    }
                                    else
                                    {
                                        state.Player2.SoQuanDaAn += 1;
                                        state.Player2.SoDanDaAn += temp.SoDan;
                                    }
                                    //Ăn xong ô quan thì cho ô quan về 0
                                    OQuan moi = new OQuan(0, 0);
                                    thucthi.LstO[currentIndex - 1] = moi;

                                }
                                else if (temp.CoQuan == 1 && temp.SoDan < 5)
                                {
                                    return thucthi;
                                }
                                else//Nếu là ô quan mà quan mất rồi thì ăn thoải mái
                                {//Nếu ăn là ô dân 
                                    //Ăn xong thì ô đó về lại giá trị 0
                                    if (state.CheckNguoiChoi != -1)
                                    {
                                        state.Player1.SoDanDaAn += thucthi.LstO[currentIndex - 1].SoDan;
                                    }
                                    else
                                    {
                                        state.Player2.SoDanDaAn += thucthi.LstO[currentIndex - 1].SoDan;
                                    }
                                    thucthi.LstO[currentIndex - 1].SoDan = 0;
                                }
                                //Khi ăn được 1 ô thì có thể chụp nhiều ô liên tục  
                                currentIndex -= 2;
                                daTruDi2++;
                                // Quay lại đầu mảng nếu cần
                                currentIndex = ConnectOAnQuan.Repositon(currentIndex);
                            }
                        }
                        else
                        {
                            //Đã trừ đi 2 để check húp hết thì giờ phải cộng lại
                            if (daTruDi2 != 0)
                            {
                                currentIndex += 2 * daTruDi2;
                            }
                            // Quay lại đầu mảng nếu cần
                            currentIndex = ConnectOAnQuan.Repositon(currentIndex);
                            break;
                        }
                    }
                    //if (thoat == true && )
                    //{
                    //    return thucthi;
                    //}
                    //Nếu trên tay đã hết quân thì kiểm tra xem ô tiếp theo có phải quân quan hay không
                    if (currentIndex == 0 || currentIndex == 6 || thucthi.LstO[currentIndex].SoDan == 0)
                        //Nếu đúng là ô quan thì dừng
                        return thucthi;
                    //Nếu ô tiếp theo là quân dân thì tiếp tục lấy số quân đó đem đi rải
                    else if (thucthi.LstO[currentIndex].SoDan != 0)
                    {
                        soquancamtrentay = thucthi.LstO[currentIndex].SoDan;
                        thucthi.LstO[currentIndex].SoDan = 0;
                    }
                    //continue;


                }
            }
            //Dòng phụ
        }

        public static OAnQuan ThucHienNuocDi(OAnQuan state, NuocDiChuyen action)
        {
            if (action.DiTraiPhai == "Right")
            {
                return DuyetTang(action.Vitri, state);
            }
            else // (action.DiTraiPhai == "Left")
            {
                return DuyetGiam(action.Vitri, state);
            }
        }

        public static bool IsGameOver(OAnQuan state)
        {
            //Nếu ở 2 ô quan đều là rỗng thì game kết thúc
            OQuan vitri0 = new OQuan();
            vitri0 = (OQuan)state.LstO[0];
            OQuan vitri6 = new OQuan();
            vitri6 = (OQuan)state.LstO[6];
            //Nếu ở 2 ô quan không có quan và không có dân nào
            if (vitri0.CoQuan == 0 && vitri0.SoDan == 0 && vitri6.CoQuan == 0 && vitri6.SoDan == 0)
            {
                return true;
            }
            else
                return false;
        }

        public static bool mustRai5ODan(OAnQuan state)
        {
            //Nếu là người chơi 1 thì kiểm tra hàng 1 đến 5
            if (state.CheckNguoiChoi == -1)
            {
                int dem = 0;
                for (int i = 1; i <= 5; i++)
                {
                    if (state.LstO[i].SoDan == 0) //Đếm đủ 5 ô đều trống
                    {
                        dem++;
                    }
                }
                if (dem == 5) //đủ 5 ô thì cần phải rải
                    return true;
                else
                    return false;
            }
            else
            {
                int dem = 0;
                for (int i = 7; i <= 11; i++)
                {
                    if (state.LstO[i].SoDan == 0) //Đếm đủ 5 ô đều trống
                    {
                        dem++;
                    }
                }
                if (dem == 5) //đủ 5 ô thì cần phải rải
                    return true;
                else
                    return false;
            }
        }

        public static void Phat5ODan(OAnQuan state)
        {
            //Nếu là người chơi 1 thì phát ô 1 đến 5
            if (state.CheckNguoiChoi == -1)
            {
                for (int i = 1; i <= 5; i++)
                {
                    if (state.LstO[i].SoDan == 0) //Đếm đủ 5 ô đều trống
                    {
                        state.LstO[i].SoDan += 1;
                    }
                }
                state.Player1.SoDanDaAn -= 5;
            }
            else
            {
                for (int i = 7; i <= 11; i++)
                {
                    if (state.LstO[i].SoDan == 0) //Đếm đủ 5 ô đều trống
                    {
                        state.LstO[i].SoDan += 1;
                    }
                }
                state.Player2.SoDanDaAn -= 5;
            }
        }

        public static string ThongBaoNguoiThang(OAnQuan state)
        {
            int diemPlayer1 = state.Player1.SoQuanDaAn * 5 + state.Player1.SoDanDaAn;
            for (int i = 1; i <= 5; i++)
            {
                diemPlayer1 += state.LstO[i].SoDan;
            }
            int diemPlayer2 = state.Player2.SoQuanDaAn * 5 + state.Player2.SoDanDaAn;
            for (int i = 7; i <= 11; i++)
            {
                diemPlayer1 += state.LstO[i].SoDan;
            }
            if (diemPlayer1 > diemPlayer2)
            {
                string thongbao;
                thongbao = "Người chơi thứ nhất đã thắng với \n" + state.Player1.SoQuanDaAn.ToString() + " quan đã ăn và " + state.Player1.SoDanDaAn.ToString() + " dân đã ăn";
                return thongbao;
            }
            else if (diemPlayer1 == diemPlayer2)
            {
                string thongbao;
                thongbao = "Người chơi thứ nhất huề với Người chơi thứ 2 :\n" + state.Player1.SoQuanDaAn.ToString() + " quan đã ăn và " + state.Player1.SoDanDaAn.ToString() + " dân đã ăn";
                return thongbao;
            }
            else
            {
                string thongbao;
                thongbao = "Người chơi thứ hai đã thắng với \n" + state.Player2.SoQuanDaAn.ToString() + " quan đã ăn và " + state.Player2.SoDanDaAn.ToString() + " dân đã ăn";
                return thongbao;
            }
        }

    }
}