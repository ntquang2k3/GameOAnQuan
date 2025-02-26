using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TroChoiOAnQuan
{
    public partial class MainChinh : Form
    {
        //Khởi tạo 1 cái bàn cờ ban đầu
        public OAnQuan BanCoMoi = new OAnQuan();
        
        public MainChinh()
        {
            InitializeComponent();
        }
        public void loadComboboxPlayer1()
        {
            for (int i = 1; i < 6; i++)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.SelectedIndex = 0;
        }
        public void loadComboboxPlayer2()
        {
            for (int i = 7; i < 12; i++)
            {
                comboBox2.Items.Add(i);
            }
            comboBox2.SelectedIndex = 0;
        }
        private void MainChinh_Load(object sender, EventArgs e)
        {
            loadComboboxPlayer1();
            loadComboboxPlayer2();
            rdoRight1.Checked = true;
            rdoRight2.Checked = true;
            btnRaiQuan1.Enabled = false;
            btnRaiQuan2.Enabled = false;
            btnThucHien2.Enabled = false;
            //Hiển thị bàn cờ
            HienThiBanCo(BanCoMoi);
        }
        public void HienThiBanCo(OAnQuan state)
        {
            for (int i = 0; i < 12; i++)
            {
                // Tìm PictureBox theo tên để hiện thị lên Form MainChinh
                string pictureBoxName = "anhDan" + i;
                PictureBox pictureBox = this.Controls.Find(pictureBoxName, true).FirstOrDefault() as PictureBox;

                if (pictureBox != null)
                {
                    int soDan = state.LstO[i].SoDan;
                    string imagePath = @"E:\Code_SinhVienNam3\THTTNT\OAnQuanNhaLam\TroChoiOAnQuan\TroChoiOAnQuan\Resources\" + soDan + "quan.png";
                    // Điều chỉnh đường dẫn tương ứng

                    if (System.IO.File.Exists(imagePath))
                    {
                        Image image = Image.FromFile(imagePath);
                        pictureBox.Image = image;
                    }
                }
            }
            //Xử lí riêng cho 2 ô quan
            OQuan oquan0 = new OQuan();
                    oquan0 = (OQuan)state.LstO[0];
                    if (oquan0.CoQuan == 0)
                    {
                        anhQuan0.Image = null;
                    }
            OQuan oquan6 = new OQuan();
                    oquan6 = (OQuan)state.LstO[6];
                    if (oquan6.CoQuan == 0)
                    {
                        anhQuan6.Image = null;
                    }
        }

        private void HienThiDiem()
        {
            //Tính điểm cho người chơi 1
            label_player1.Text = "Player 1: Ăn " + BanCoMoi.Player1.SoQuanDaAn + " quan " + BanCoMoi.Player1.SoDanDaAn + " dân";
            //Tính điểm cho người chơi 2
            label_player2.Text = "Player 2: Ăn " + BanCoMoi.Player2.SoQuanDaAn + " quan " + BanCoMoi.Player2.SoDanDaAn + " dân";
        }

        private void btnThucHien1_Click(object sender, EventArgs e)
        {
            if (rdoRight1.Checked)
            {
                NuocDiChuyen nuocdi = new NuocDiChuyen((int)comboBox1.SelectedItem,"Right");
                BanCoMoi = ConnectOAnQuan.ThucHienNuocDi(BanCoMoi, nuocdi);
            }
            else if (rdoLeft1.Checked)
            {
                NuocDiChuyen nuocdi = new NuocDiChuyen((int)comboBox1.SelectedItem, "Left");
                BanCoMoi = ConnectOAnQuan.ThucHienNuocDi(BanCoMoi, nuocdi);
            }
            //Sau khi chơi xong
            btnThucHien2.Enabled = true;
            btnThucHien1.Enabled = false;
            //Tính điểm cho người chơi
            HienThiDiem();

            HienThiBanCo(BanCoMoi);
            if (ConnectOAnQuan.IsGameOver(BanCoMoi))
            {
                for (int i = 1; i <= 5; i++)
                {
                    BanCoMoi.Player1.SoDanDaAn += BanCoMoi.LstO[i].SoDan;
                }
                for (int i = 7; i <= 11; i++)
                {
                    BanCoMoi.Player2.SoDanDaAn += BanCoMoi.LstO[i].SoDan;
                }
                HienThiDiem();
                MessageBox.Show(ConnectOAnQuan.ThongBaoNguoiThang(BanCoMoi));
                //Console.WriteLine(ConnectOAnQuan.ThongBaoNguoiThang(Player1, player2));
            }
            //Đến lượt người tiếp theo
            //Kiểm tra thử người chơi 2 có cần rải quân không
            if (ConnectOAnQuan.mustRai5ODan(BanCoMoi))
            //Nếu phải rải thì hiện cái nút bấm rải lên cho người ta rải
            {
                btnRaiQuan2.Enabled = true;
                btnThucHien2.Enabled = false;
            }
            //Khi người ta nhấn nút rải thì bắt đầu thực hiện rải
            //Rải quân xong bắt đầu lại chơi tiếp
        }

        private void btnThucHien2_Click(object sender, EventArgs e)
        {
            if (rdoRight2.Checked)
            {
                NuocDiChuyen nuocdi = new NuocDiChuyen((int)comboBox2.SelectedItem, "Left");
                BanCoMoi = ConnectOAnQuan.ThucHienNuocDi(BanCoMoi, nuocdi);
            }
            else if (rdoLeft2.Checked)
            {
                NuocDiChuyen nuocdi = new NuocDiChuyen((int)comboBox2.SelectedItem, "Right");
                BanCoMoi = ConnectOAnQuan.ThucHienNuocDi(BanCoMoi, nuocdi);
            }
            //Sau khi chơi xong
            btnThucHien1.Enabled = true;
            btnThucHien2.Enabled = false;
            //Tính điểm cho người chơi 2
            HienThiDiem();
            HienThiBanCo(BanCoMoi);
            if (ConnectOAnQuan.IsGameOver(BanCoMoi))
            {
                for (int i = 1; i <= 5; i++)
                {
                    BanCoMoi.Player1.SoDanDaAn += BanCoMoi.LstO[i].SoDan;
                }
                for (int i = 7; i <= 11; i++)
                {
                    BanCoMoi.Player2.SoDanDaAn += BanCoMoi.LstO[i].SoDan;
                }
                HienThiDiem();
                MessageBox.Show(ConnectOAnQuan.ThongBaoNguoiThang(BanCoMoi));
            }
            //Đến lượt người tiếp theo
            //Kiểm tra thử người chơi 1 có cần rải quân không
            if (ConnectOAnQuan.mustRai5ODan(BanCoMoi))
            //Nếu phải rải thì hiện cái nút bấm rải lên cho người chơi rải
            {
                btnRaiQuan1.Enabled = true;
                btnThucHien1.Enabled = false;
            }
            
        }

        private void btnRaiQuan1_Click(object sender, EventArgs e)
        {
            ConnectOAnQuan.Phat5ODan(BanCoMoi);
            HienThiBanCo(BanCoMoi);
            btnRaiQuan1.Enabled = false;
            btnThucHien1.Enabled = true;
            //Tính điểm cho người chơi 1
            label_player1.Text = "Player 1: Ăn " + BanCoMoi.Player1.SoQuanDaAn + " quan " + BanCoMoi.Player1.SoDanDaAn + " dân";
        }

        private void btnRaiQuan2_Click(object sender, EventArgs e)
        {
            ConnectOAnQuan.Phat5ODan(BanCoMoi);
            HienThiBanCo(BanCoMoi);
            btnRaiQuan2.Enabled = false;
            btnThucHien2.Enabled = true;
            //Tính điểm cho người chơi 2
            label_player2.Text = "Player 2: Ăn " + BanCoMoi.Player2.SoQuanDaAn + " quan " + BanCoMoi.Player2.SoDanDaAn + " dân";
        }
    }
}
