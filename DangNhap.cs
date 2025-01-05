using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using test.model;

namespace test
{
    public partial class DangNhap : Form
    {
        private QLCaPheEntities12 QL = new QLCaPheEntities12();
        public int check = 0;
        public string TenNguoi = "";
        public string Quyen = "";

        public DangNhap()
        {
            InitializeComponent();
            // Gắn sự kiện KeyDown cho Form
            this.KeyPreview = true; // Cho phép Form nhận sự kiện KeyDown
            this.KeyDown += new KeyEventHandler(DangNhap_KeyDown);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var CheckĐN = QL.LOAD_TK_DE_LOGIN3();
            foreach (var item in CheckĐN)
            {
                if (item.TENTK == textBox_TK.Text && item.MATKHAU == textBox_MK.Text)
                {
                    TenNguoi = item.TENNV;
                    Quyen = item.QUYEN;

                    if (Quyen == "NHANVIEN")
                    {
                        Quyen = "Nhân viên";
                    }

                    check = 1;

                    // Ẩn form DangNhap thay vì đóng
                    this.Hide();

                    // Hiển thị form Ban
                    Ban newBan = new Ban(TenNguoi, Quyen);
                    newBan.ShowDialog();

                    // Khi form Ban đóng, thoát chương trình
                    this.Close();
                    return;
                }
            }

            // Hiển thị thông báo nếu đăng nhập sai
            MessageBox.Show("Sai tài khoản hoặc mật khẩu");
            textBox_MK.Text = "";
        }



        private void DangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            // Kiểm tra phím Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Gọi lại sự kiện button1_Click
                button1_Click(sender, e);
            }
        }
    }
}
