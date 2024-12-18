using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using test.model;

namespace test
{
    public partial class DangKy : Form
    {
        private QLCaPheEntities13 QL = new QLCaPheEntities13();
        public DangKy()
        {

            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy"; // Định dạng ngày tháng tiếng Việt
            dateTimePicker1.Value = DateTime.Now;
            CultureInfo viCulture = new CultureInfo("vi-VN");
            System.Threading.Thread.CurrentThread.CurrentCulture = viCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = viCulture;
            GioiTinh_comboBox.Items.Add("Nam");
            GioiTinh_comboBox.Items.Add("Nữ");
            GioiTinh_comboBox.SelectedIndex = 0;
            Quyen_comboBox.Items.Add("Admin");
           Quyen_comboBox.Enabled = false;  
            Quyen_comboBox.SelectedIndex = 0;
        }

        private void DangKy_Load(object sender, EventArgs e)
        {

        }

        private void DK_button_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các ô nhập liệu
            string resultQuyen = Quyen_comboBox.SelectedItem.ToString();
            DateTime selectedDate = dateTimePicker1.Value;

            // Gọi hàm thêm dữ liệu vào cơ sở dữ liệu
            QL.INSERT_IN_FIRST_AVAILABLE_POSITION_NV_TK(
                Ten_textBox.Text,
                selectedDate,
                SĐTtextBox.Text,
                GioiTinh_comboBox.SelectedItem.ToString(),
                TaiKhoan_textBox.Text,
                MatKhau_textBox.Text,
                resultQuyen
            );

            // Hiển thị thông báo thành công
            MessageBox.Show("Đăng ký thành công!");

            // Mở form đăng nhập
            DangNhap dn = new DangNhap();
            this.Hide();  // Ẩn form đăng ký
            dn.ShowDialog();  // Hiển thị form đăng nhập theo kiểu modal

            // Đóng form đăng ký sau khi form đăng nhập đóng
            this.Close();
        }

    }
}
