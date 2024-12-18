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
        private QLCaPheEntities13 QL = new QLCaPheEntities13();
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
            var list = QL.LOADTAIKHOANVANHANVIEN4().ToList();
            foreach (var item in list)
            {
                ListViewItem listViewItem = new ListViewItem(item.IDNV.ToString());
                listViewItem.SubItems.Add(item.TENNV.ToString());
                listViewItem.SubItems.Add(item.NGAYSINH.ToString("dd/MM/yyyy")); // Định dạng ngày/tháng/năm
                listViewItem.SubItems.Add(item.SDT.ToString());   // Thêm cột Tên đồ uống
                listViewItem.SubItems.Add(item.GIOITINH.ToString()); // Thêm cột Giá (chuyển sang chuỗi)
                listViewItem.SubItems.Add(item.TENTK.ToString());
                listViewItem.SubItems.Add(item.MATKHAU.ToString());
                listViewItem.SubItems.Add(item.QUYEN.ToString());
                // Thêm ListViewItem vào ListView
                listView1.Items.Add(listViewItem);

            }
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
            string resultQuyen = Quyen_comboBox.SelectedItem.ToString();
            DateTime selectedDate = dateTimePicker1.Value;
            QL.INSERT_IN_FIRST_AVAILABLE_POSITION_NV_TK(Ten_textBox.Text,
                selectedDate, SĐTtextBox.Text, GioiTinh_comboBox.SelectedItem.ToString(),
                TaiKhoan_textBox.Text, MatKhau_textBox.Text, resultQuyen);
            MessageBox.Show("Đã thêm");
            Ten_textBox.Text=SĐTtextBox.Text=TaiKhoan_textBox.Text=MatKhau_textBox.Text="";
            dateTimePicker1.Refresh();
            GioiTinh_comboBox.SelectedIndex = -1;
            Quyen_comboBox.SelectedIndex = -1;

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
            MessageBox.Show("Đã xóa");
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



    }
}
