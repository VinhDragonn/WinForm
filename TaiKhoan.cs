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
    public partial class TaiKhoan : Form
    {
        private QLCaPheEntities12 QL = new QLCaPheEntities12();
        private int selectedIdHD = 0;
        public TaiKhoan()
        {
            InitializeComponent();
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
            Quyen_comboBox.Items.Add("NHANVIEN");
            Quyen_comboBox.SelectedIndex = 0;
            DateTime selectedDate = dateTimePicker1.Value;
            loadListView();
        }

        private void TaiKhoan_Load(object sender, EventArgs e)
        {

        }
        public void loadListView()
        {
            listView1.Items.Clear();
            var list = QL.LOADTAIKHOANVANHANVIEN4()
                         .OrderBy(item => item.IDNV) // Sắp xếp danh sách theo IDNV
                         .ToList();

            foreach (var item in list)
            {
                ListViewItem listViewItem = new ListViewItem(item.IDNV.ToString());
                listViewItem.SubItems.Add(item.TENNV.ToString());
                listViewItem.SubItems.Add(item.NGAYSINH.ToString("dd/MM/yyyy"));
                listViewItem.SubItems.Add(item.SDT.ToString());
                listViewItem.SubItems.Add(item.GIOITINH.ToString());
                listViewItem.SubItems.Add(item.TENTK.ToString());
                listViewItem.SubItems.Add(item.MATKHAU.ToString());
                listViewItem.SubItems.Add(item.QUYEN.ToString());

                listView1.Items.Add(listViewItem);
            }

            // Kích hoạt chế độ sắp xếp
            listView1.Sorting = SortOrder.Ascending;
           // Sắp xếp theo cột ID (cột 0)
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0) // Kiểm tra nếu có mục được chọn
            {
                // Lấy giá trị từ mục được chọn
                string id = listView1.SelectedItems[0].Text.ToString();
                selectedIdHD = Convert.ToInt32(id);
                Ten_textBox.Text = listView1.SelectedItems[0].SubItems[1].Text.ToString();
                SĐTtextBox.Text = listView1.SelectedItems[0].SubItems[3].Text.ToString();
                TaiKhoan_textBox.Text = listView1.SelectedItems[0].SubItems[5].Text.ToString();
                MatKhau_textBox.Text = listView1.SelectedItems[0].SubItems[6].Text.ToString();
                string gioiTinh = listView1.SelectedItems[0].SubItems[4].Text.Trim();
                if (GioiTinh_comboBox.Items.Contains(gioiTinh))
                {
                    GioiTinh_comboBox.SelectedItem = gioiTinh;
                }
                else
                {
                    MessageBox.Show($"Giới tính '{gioiTinh}' không có trong danh sách!");
                }

                string quyen = listView1.SelectedItems[0].SubItems[7].Text.Trim();
                if (Quyen_comboBox.Items.Contains(quyen))
                {
                    Quyen_comboBox.SelectedItem = quyen;
                }
                else
                {
                    MessageBox.Show($"Quyền '{quyen}' không có trong danh sách!");
                }

            }

            else
            {
                MessageBox.Show("Không có giá trị cột thứ hai!", "Thông báo");
            }
        }
        public void UnloadListView()
        {
            listView1.Clear();
        }
        private void Them_button_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(Ten_textBox.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dateTimePicker1.Value == null)
            {
                MessageBox.Show("Vui lòng chọn ngày sinh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(SĐTtextBox.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(TaiKhoan_textBox.Text))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
           
            if (string.IsNullOrWhiteSpace(MatKhau_textBox.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (GioiTinh_comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Quyen_comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn quyền!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Nếu dữ liệu hợp lệ, thực hiện thêm vào cơ sở dữ liệu
            string resultQuyen = Quyen_comboBox.SelectedItem.ToString();
            DateTime selectedDate = dateTimePicker1.Value;
            try
            {
                QL.INSERT_IN_FIRST_AVAILABLE_POSITION_NV_TK(
                    Ten_textBox.Text,
                    selectedDate,
                    SĐTtextBox.Text,
                    GioiTinh_comboBox.SelectedItem.ToString(),
                    TaiKhoan_textBox.Text,
                    MatKhau_textBox.Text,
                    resultQuyen
                );

                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Làm mới danh sách và giao diện
                loadListView();
                Ten_textBox.Text = SĐTtextBox.Text = TaiKhoan_textBox.Text = MatKhau_textBox.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                GioiTinh_comboBox.SelectedIndex = 0;
                Quyen_comboBox.SelectedIndex = 0;
                errorProvider1.SetError(MatKhau_textBox, string.Empty);
                errorProvider1.SetError(TaiKhoan_textBox, string.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }






        private void Xoa_button_Click(object sender, EventArgs e)
        {
            if(selectedIdHD == 0)
            {
                MessageBox.Show("Chưa chọn nhân viên để xóa!");
                return;
            }
            Ten_textBox.Text = SĐTtextBox.Text = TaiKhoan_textBox.Text = MatKhau_textBox.Text = "";
            dateTimePicker1.Refresh();
            GioiTinh_comboBox.SelectedIndex = -1;
            Quyen_comboBox.SelectedIndex = -1;
            QL.DELETE_NHAN_VIEN_VA_TK(selectedIdHD);
            loadListView();
            MessageBox.Show("Đã xóa");
            errorProvider1.SetError(MatKhau_textBox, string.Empty);
            errorProvider1.SetError(TaiKhoan_textBox, string.Empty);
        }

        private void Sua_button_Click(object sender, EventArgs e)
        {
            if (selectedIdHD == 0)
            {
                MessageBox.Show("Chưa chọn nhân viên để sửa!");
                return;
            }
            string resultQuyen = Quyen_comboBox.SelectedItem.ToString();
            DateTime selectedDate = dateTimePicker1.Value;
            QL.UPDATE_NV_TK(selectedIdHD, Ten_textBox.Text,
                selectedDate, SĐTtextBox.Text, GioiTinh_comboBox.SelectedItem.ToString(),
                TaiKhoan_textBox.Text, MatKhau_textBox.Text, resultQuyen);
            Ten_textBox.Text = SĐTtextBox.Text = TaiKhoan_textBox.Text = MatKhau_textBox.Text = "";
            dateTimePicker1.Refresh();
            GioiTinh_comboBox.SelectedIndex = -1;
            Quyen_comboBox.SelectedIndex = -1;
            MessageBox.Show("Đã sửa");
            loadListView();
            Ban newB = new Ban(Ten_textBox.Text, resultQuyen);
            errorProvider1.SetError(MatKhau_textBox, string.Empty);
            errorProvider1.SetError(TaiKhoan_textBox, string.Empty);
        }

        private void Tim_textBox_TextChanged(object sender, EventArgs e)
        {
            string searchText = Tim_textBox.Text.ToLower();  // Chuyển sang chữ thường

            // Chuyển kết quả thành List trước khi xử lý
            var customers = QL.LOAD_TK_NHAN_VIEN_DE_TIM().ToList();  // Lưu kết quả thành List

            if (customers == null || !customers.Any())
            {
                MessageBox.Show("Không có dữ liệu!");
                return;
            }

            var sortedCustomers = customers
                .Where(c => c.TENNV.ToString().ToLower().Contains(searchText) ||
                       c.IDNV_A.ToString().ToLower().Contains(searchText) ||
                        c.TENTK.ToString().ToLower().Contains(searchText) )
                .Select(c => new
                {
                    c.IDNV_A,
                    c.TENNV,
                    c.NGAYSINH,
                    c.SDT,
                    c.GIOITINH,
                    c.TENTK,
                    c.MATKHAU,
                    c.QUYEN
                })
                .ToList();  // Materialize the query results

            // Làm mới ListView
            listView1.Items.Clear(); // Xóa các item cũ

            // Kiểm tra nếu không có kết quả tìm kiếm
            if (sortedCustomers.Count == 0)
            {
                MessageBox.Show("Không tìm thấy kết quả!");
            }

            // Thêm dữ liệu mới
            foreach (var item in sortedCustomers)
            {
                ListViewItem listViewItem = new ListViewItem(item.IDNV_A.ToString());   // Dùng TENNV thay cho IDNV
                listViewItem.SubItems.Add(item.TENNV.ToString());
                listViewItem.SubItems.Add(item.NGAYSINH.ToString("dd/MM/yyyy")); // Định dạng ngày/tháng/năm
                listViewItem.SubItems.Add(item.SDT.ToString());
                listViewItem.SubItems.Add(item.GIOITINH.ToString());
                listViewItem.SubItems.Add(item.TENTK.ToString());
                listViewItem.SubItems.Add(item.MATKHAU.ToString());
                listViewItem.SubItems.Add(item.QUYEN.ToString());

                listView1.Items.Add(listViewItem);
            }
        }

        private void MatKhau_textBox_TextChanged(object sender, EventArgs e)
        {
            string matKhau = MatKhau_textBox.Text;

            // Kiểm tra mật khẩu có ít nhất 8 ký tự không
            if (matKhau.Length < 8)
            {          
                errorProvider1.SetError(MatKhau_textBox, "Mật khẩu phải có ít nhất 8 ký tự.");
                return;
            }
            // Kiểm tra mật khẩu có chứa ít nhất 1 ký tự in hoa không
            else if (!matKhau.Any(c => Char.IsUpper(c)))
            {      
                errorProvider1.SetError(MatKhau_textBox, "Mật khẩu phải chứa ít nhất 1 ký tự in hoa.");
                return;
            }
            // Kiểm tra mật khẩu có chứa ít nhất 1 ký tự số không
            else if (!matKhau.Any(c => Char.IsDigit(c)))
            {
               
                errorProvider1.SetError(MatKhau_textBox, "Mật khẩu phải chứa ít nhất 1 ký tự số.");
                return;
            }
            // Kiểm tra mật khẩu có chứa ít nhất 1 ký tự đặc biệt không (ví dụ: @, #, $, %)
            else if (!matKhau.Any(c => "!@#$%^&*()_+[]{}|;:'\",.<>?/".Contains(c)))
            {
               
                errorProvider1.SetError(MatKhau_textBox, "Mật khẩu phải chứa ít nhất 1 ký tự đặc biệt.");
                return;
            }
            else
            {
                // Nếu mật khẩu hợp lệ, xóa thông báo lỗi
                errorProvider1.SetError(MatKhau_textBox, string.Empty);
               
            }
        }

        private void TaiKhoan_textBox_TextChanged(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(TaiKhoan_textBox.Text, "^[a-zA-Z0-9]+$"))
            {
                errorProvider1.SetError(TaiKhoan_textBox, "Tên tài khoản ko được chứa dấu.");
                
            }
            else {
                errorProvider1.SetError(TaiKhoan_textBox, string.Empty);
            }
        }
    }
}
