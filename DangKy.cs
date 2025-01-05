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
        private QLCaPheEntities12 QL = new QLCaPheEntities12();
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
            DK_button.Enabled = false;
        }

        private void DangKy_Load(object sender, EventArgs e)
        {

        }
        private void DK_button_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường thông tin có bị thiếu không
            if (string.IsNullOrEmpty(Ten_textBox.Text) ||
                string.IsNullOrEmpty(SĐTtextBox.Text) ||
                string.IsNullOrEmpty(TaiKhoan_textBox.Text) ||
                string.IsNullOrEmpty(MatKhau_textBox.Text))
            {
                // Nếu có trường thiếu, tắt nút đăng ký
                DK_button.Enabled = false;
                MessageBox.Show("Vui lòng nhập đầy đủ các thông tin!");
                return; // Dừng hàm nếu thiếu thông tin
            }

            // Nếu tất cả các trường đều hợp lệ, tiếp tục thực hiện đăng ký
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

        private void CheckInputs()
        {
            // Kiểm tra xem tất cả các trường nhập liệu có đầy đủ không
            bool isFormValid = !string.IsNullOrEmpty(Ten_textBox.Text) &&
                               !string.IsNullOrEmpty(SĐTtextBox.Text) &&
                               !string.IsNullOrEmpty(TaiKhoan_textBox.Text) &&
                               !string.IsNullOrEmpty(MatKhau_textBox.Text);

            // Kiểm tra nếu các trường hợp hợp lệ, bật nút Đăng ký
            DK_button.Enabled = isFormValid;
        }

        private void Ten_textBox_TextChanged(object sender, EventArgs e)
        {
            CheckInputs();
        }

        private void SĐTtextBox_TextChanged(object sender, EventArgs e)
        {
            CheckInputs();
        }

        private void TaiKhoan_textBox_TextChanged(object sender, EventArgs e)
        {
            CheckInputs();
        }

        private void MatKhau_textBox_TextChanged(object sender, EventArgs e)
        {
            string matKhau = MatKhau_textBox.Text;

            // Kiểm tra mật khẩu có ít nhất 8 ký tự không
            if (matKhau.Length < 8)
            {
                DK_button.Enabled = false;
                errorProvider1.SetError(MatKhau_textBox, "Mật khẩu phải có ít nhất 8 ký tự.");
            }
            // Kiểm tra mật khẩu có chứa ít nhất 1 ký tự in hoa không
            else if (!matKhau.Any(c => Char.IsUpper(c)))
            {
                DK_button.Enabled = false;
                errorProvider1.SetError(MatKhau_textBox, "Mật khẩu phải chứa ít nhất 1 ký tự in hoa.");
            }
            // Kiểm tra mật khẩu có chứa ít nhất 1 ký tự số không
            else if (!matKhau.Any(c => Char.IsDigit(c)))
            {
                DK_button.Enabled = false;
                errorProvider1.SetError(MatKhau_textBox, "Mật khẩu phải chứa ít nhất 1 ký tự số.");
            }
            // Kiểm tra mật khẩu có chứa ít nhất 1 ký tự đặc biệt không (ví dụ: @, #, $, %)
            else if (!matKhau.Any(c => "!@#$%^&*()_+[]{}|;:'\",.<>?/".Contains(c)))
            {
                DK_button.Enabled = false;
                errorProvider1.SetError(MatKhau_textBox, "Mật khẩu phải chứa ít nhất 1 ký tự đặc biệt.");
            }
            else
            {
                // Nếu mật khẩu hợp lệ, xóa thông báo lỗi
                errorProvider1.SetError(MatKhau_textBox, string.Empty);
                DK_button.Enabled = true;
            }
        }

        private void MatKhau_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra mật khẩu có đủ điều kiện không
            string matKhau = MatKhau_textBox.Text;

            // Kiểm tra mật khẩu có ít nhất 8 ký tự không
            if (matKhau.Length < 8)
            {
                DK_button.Enabled = false;
                errorProvider1.SetError(MatKhau_textBox, "Mật khẩu phải có ít nhất 8 ký tự.");
            }
            // Kiểm tra mật khẩu có chứa ít nhất 1 ký tự in hoa không
            else if (!matKhau.Any(c => Char.IsUpper(c)))
            {
                DK_button.Enabled = false;
                errorProvider1.SetError(MatKhau_textBox, "Mật khẩu phải chứa ít nhất 1 ký tự in hoa.");
            }
            // Kiểm tra mật khẩu có chứa ít nhất 1 ký tự số không
            else if (!matKhau.Any(c => Char.IsDigit(c)))
            {
                DK_button.Enabled = false;
                errorProvider1.SetError(MatKhau_textBox, "Mật khẩu phải chứa ít nhất 1 ký tự số.");
            }
            // Kiểm tra mật khẩu có chứa ít nhất 1 ký tự đặc biệt không (ví dụ: @, #, $, %)
            else if (!matKhau.Any(c => "!@#$%^&*()_+[]{}|;:'\",.<>?/".Contains(c)))
            {
                DK_button.Enabled = false;
                errorProvider1.SetError(MatKhau_textBox, "Mật khẩu phải chứa ít nhất 1 ký tự đặc biệt.");
            }
            else
            {
                // Nếu mật khẩu hợp lệ, xóa thông báo lỗi
                errorProvider1.SetError(MatKhau_textBox, string.Empty);
                DK_button.Enabled = true;
            }
        }

        private void TaiKhoan_textBox_TextChanged_1(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(TaiKhoan_textBox.Text, "^[a-zA-Z0-9]+$"))
            {
                errorProvider1.SetError(TaiKhoan_textBox, "Tên tài khoản ko được chứa dấu.");

            }
            else
            {
                errorProvider1.SetError(TaiKhoan_textBox, string.Empty);
            }
        }
    }
}
