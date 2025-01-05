using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using test.model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Security.Cryptography;
using System.Globalization;
using DocumentFormat.OpenXml.Office2010.Excel;
namespace test
{
    public partial class Ban : Form
    {
      
        private QLCaPheEntities12 QL = new QLCaPheEntities12();
        private int DemBan=1;
        private DangNhap newDn = new DangNhap();
        private int selectedIdHD = 0; // Biến toàn cục lưu giá trị idHD
        private string TenKH = "";
        private string Tennguoi = "";

        public Ban(string tenNguoi, string quyen)
        {
            InitializeComponent();
            Tennguoi = tenNguoi;
             // Khởi tạo Form2 và đặt các thuộc tính của nó
             ThucDon thucDon = new ThucDon();
            thucDon.TopLevel = false; // Đặt thành false để nhúng vào TabPage
            thucDon.FormBorderStyle = FormBorderStyle.None; // Bỏ border để trông mượt mà hơn
            thucDon.Dock = DockStyle.Fill; // Cho Form2 lấp đầy TabPage
            thucDon.Show();
            TaiKhoan taiKhoan = new TaiKhoan();
            taiKhoan.TopLevel = false; // Đặt thành false để nhúng vào TabPage
            taiKhoan.FormBorderStyle = FormBorderStyle.None; // Bỏ border để trông mượt mà hơn
            taiKhoan.Dock = DockStyle.Fill; // Cho Form2 lấp đầy TabPage
            taiKhoan.Show();
            DoanhThu doanhThu = new DoanhThu();
            doanhThu.TopLevel = false; // Đặt thành false để nhúng vào TabPage
            doanhThu.FormBorderStyle = FormBorderStyle.None; // Bỏ border để trông mượt mà hơn
            doanhThu.Dock = DockStyle.Fill; // Cho Form2 lấp đầy TabPage
            doanhThu.Show();
            tabPage2.Controls.Add(thucDon);
            tabPage3.Controls.Add(taiKhoan);
            tabPage4.Controls.Add(doanhThu);
            loadTable();
            var z = QL.LOAD_DANHMUC().ToList();
            comboBox_DanhMuc.DataSource = z.Select(item => item.TENDM).ToList();
           
           
            numericUpDown2.Value = 1;
            label2.Text = $"{quyen}: {tenNguoi}";
           

            if (quyen == "Nhân viên")
            {
                // Hạn chế quyền
                taiKhoan.Enabled = false;
                taiKhoan.UnloadListView();
            }
            tabControl1.SelectedIndexChanged += (s, e) =>
            {
                // Kiểm tra nếu tabPage5 (mã của tabPage5 là tabPage5)
                if (tabControl1.SelectedTab == tabPage5)
                {
                    // Đóng form Ban
                    this.Close();

                    // Mở lại form DangNhap
                    DangNhap dangNhapForm = new DangNhap();
                    dangNhapForm.ShowDialog();
                }
                else if (tabControl1.SelectedTab == tabPage4 || tabControl1.SelectedTab == tabPage3 
    || tabControl1.SelectedTab == tabPage2 || tabControl1.SelectedTab == tabPage1)
                {
                    // Gọi hàm load dữ liệu trong các tab
                    doanhThu.loadListView();
                    taiKhoan.loadListView();
                    thucDon.loadListView();
                   
                    comboBox_DanhMuc.DataSource = z.Select(item => item.TENDM).ToList();
                }
            };
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            Button but = (Button)sender;
            KH_textBox.Text = "";
            int id = 0;
            string status = "";
            if (but != null && but.Tag is Tuple<int, string> tagData)
            {
                id = tagData.Item1;
                status = tagData.Item2;
            }          
            buttonTT.Tag = new Tuple<int, string>(id, status);
            buttonThem.Tag = id;
            button_Sua.Tag = id;
            buttonXoa.Tag = id;
            selectedIdHD = id;
            var timKH = QL.Get_ALL_Table().Where(n => n.id == id).ToList();
            TenKH = timKH[0].TenKH;

            KH_textBox.Text = TenKH;
            Ban_label.Text = "Bàn số: " + id.ToString();
            groupBox3.Text = "Danh sách các món ăn của bàn: " + id.ToString() + "  Khách hàng: " + TenKH;

            loadListView(id);

            if (TenKH != null)
            {
                KH_textBox.Enabled = false;
            }
            else
            {
                KH_textBox.Enabled = true;
            }
        }
        private void Btn_MouseDown(object sender, MouseEventArgs e)
           {
               if (e.Button == MouseButtons.Right)
               {
                   Button btn = sender as Button;
                   var tagData = (Tuple<int, string>)btn.Tag;
                int IDBan = tagData.Item1;
                var TimBan = QL.GetTableList().Where(n => n.id == IDBan).ToList();
                string ten = TimBan[0].ten;
                int ban = int.Parse(ten.Substring(4)) ;
                string trangThai = tagData.Item2;
                   if (btn != null)
                   {
                       // Hiển thị menu ngữ cảnh hoặc xử lý hành động khác
                       ContextMenuStrip contextMenu = new ContextMenuStrip();
                       contextMenu.Items.Add("Xem chi tiết").Click += (s, ev) =>
                       {
                           string result = loadListView(IDBan); // Gọi hàm loadListView
                           MessageBox.Show(result, $"Chi tiết bàn {ban}");
                       };
                       contextMenu.Items.Add("Xóa").Click += (s, ev) =>
                       {
                           QL.XOA_TABLE(IDBan);
                           MessageBox.Show($"Đã xóa bàn: {ban}");
                           loadTable();
                       };

                       // Hiển thị menu ngữ cảnh tại vị trí con trỏ chuột
                       contextMenu.Show(Cursor.Position);
                   }
               }
           }
        private void loadTable()
        {
            string ten = "";
            // Xóa các nút hiện có trên flowLayoutPanel1
            flowLayoutPanel1.Controls.Clear();

            // Lấy danh sách bàn từ cơ sở dữ liệu
            var a = QL.GetTableList().ToList();

            // Kiểm tra nếu danh sách bàn không trống
            if (a.Any())
            {
                // Sắp xếp danh sách bàn theo số trong t.ten
                var sortedTables = a.OrderBy(t =>
                {
                    // Trích xuất số từ t.ten
                    int number = int.Parse(new string(t.ten.Where(char.IsDigit).ToArray()));
                    return number;
                }).ToList();

                // Lấy số bàn lớn nhất để cập nhật DemBan
                ten = sortedTables.Last().ten;
                DemBan = int.Parse(new string(ten.Where(char.IsDigit).ToArray())) + 1;

                // Duyệt qua danh sách đã sắp xếp và tạo nút cho từng bàn
                foreach (var t in sortedTables)
                {
                    Button btn = new Button()
                    {
                        Width = 100,
                        Height = 100,
                    };
                    btn.Text = t.ten + Environment.NewLine + t.status;
                    btn.Tag = new Tuple<int, string>(t.id, t.status);
                    btn.Click += Btn_Click;
                    btn.MouseDown += Btn_MouseDown;

                    // Đặt màu nền dựa trên trạng thái bàn
                    switch (t.status)
                    {
                        case "Trống":
                            btn.BackColor = System.Drawing.Color.GreenYellow;
                            break;
                        default:
                            btn.BackColor = System.Drawing.Color.SandyBrown;
                            break;
                    }

                    // Thêm nút vào flowLayoutPanel1
                    flowLayoutPanel1.Controls.Add(btn);
                }
            }
            else
            {
                // Nếu không có bàn nào, gán DemBan = 1
                DemBan = 1;
            }
        }
        private string loadListView(int dem)
        {
            listView1.Items.Clear();
            var list = QL.LOAD_HOA_DON_NEW().ToList();
            if (list.Any(item => item.IDBAN == dem))
            {
                string result = "Danh sách món ăn:\n";
                foreach (var item in list)
                {
                    if (item.IDBAN == dem)
                    {
                        ListViewItem listViewItem = new ListViewItem(item.IDHOADON.ToString());
                        listViewItem.SubItems.Add(item.TenMon.ToString());
                        listViewItem.SubItems.Add(item.SoLuong.ToString());
                        listViewItem.SubItems.Add((item.DonGia ?? 0).ToString("N0", new CultureInfo("vi-VN")));
                        listViewItem.SubItems.Add((item.ThanhTien ?? 0).ToString("N0", new CultureInfo("vi-VN")));

                        // Thêm ListViewItem vào ListView
                        listView1.Items.Add(listViewItem);

                        // Thêm vào chuỗi kết quả
                        result += $"- {item.TenMon} (SL: {item.SoLuong}, Giá: {(item.ThanhTien ?? 0):N0} VNĐ)\n";
                    }
                }
                return result;
            }
            else
            {
                return "Danh sách món ăn trống.";
            }
        }
        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0) // Kiểm tra nếu có mục được chọn
            {
                // Lấy giá trị từ mục được chọn
                string id = listView1.SelectedItems[0].Text.ToString();
                int idHD = Convert.ToInt32(id);
                selectedIdHD = idHD;
                
                // Kiểm tra nếu có đủ SubItems
                if (listView1.SelectedItems[0].SubItems.Count > 1)
                {
                    // Lấy giá trị cột thứ hai (SubItems[1]) từ mục được chọn
                    string selectedValue2 = listView1.SelectedItems[0].SubItems[1].Text.Trim();
                    TenMon_comboBox.Text = selectedValue2;
                   
                }
                else
                {
                    MessageBox.Show("Không có giá trị cột thứ hai!", "Thông báo");
                }

                // Lấy giá trị cột thứ ba (SubItems[2])
                if (listView1.SelectedItems[0].SubItems.Count > 2)
                {
                    string value = listView1.SelectedItems[0].SubItems[2].Text.Trim();

                    // Chuyển đổi giá trị từ chuỗi sang số và gán vào numericUpDown2
                    if (decimal.TryParse(value, out decimal numericValue))
                    {
                        numericUpDown2.Value = numericValue;
                    }
                    else
                    {
                        MessageBox.Show("Giá trị không hợp lệ để gán vào numericUpDown2!", "Lỗi");
                    }
                }
                else
                {
                    MessageBox.Show("Không có giá trị cột thứ ba!", "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Không có mục nào được chọn!", "Thông báo");
            }
        }
        private void button_ThemBan_Click(object sender, EventArgs e)
            {        
            QL.INSERT_IN_FIRST_AVAILABLE_TABLE2();
                DemBan++;
               loadTable(); 
            }
        private void buttonThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedIdHD == 0)
                {
                    MessageBox.Show("Vui lòng chọn bàn trước khi thêm món!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(KH_textBox.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên khách hàng!");
                    return;
                }

                string ten = KH_textBox.Text;
                groupBox3.Text = "Danh sách các món ăn của bàn: " + selectedIdHD.ToString() + "  Khách hàng: " + ten;

                var z = QL.LOAD_DOUONG().ToList();
                var selectedItem = TenMon_comboBox.SelectedItem?.ToString();
                int? donGia = z
                    .Where(item => item.TENDOUONG.Equals(selectedItem))
                    .Select(item => (int?)item.DONGIA)
                    .FirstOrDefault();

                if (selectedItem == null || donGia == null)
                {
                    MessageBox.Show("Vui lòng chọn đồ uống hợp lệ!");
                    return;
                }

                int SoLuong = (int)numericUpDown2.Value;

                // Thêm dữ liệu vào bảng hóa đơn và cập nhật
                QL.Them_DATA_VAO_TABLE(selectedIdHD, ten);
                QL.SP_INSERT_OR_UPDATE_IN_FIRST_AVAILABLE_HOA_DON(selectedIdHD, selectedItem, SoLuong, donGia);

                KH_textBox.Enabled=false;
                
                loadTable();
                loadListView(selectedIdHD);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void buttonTT_Click(object sender, EventArgs e)
        {
            
                Button btn = (Button)sender;

                // Kiểm tra và ép kiểu Tag thành Tuple<int, string>
                var tagData = (Tuple<int, string>)btn.Tag;

                // Nếu tagData là null, thông báo cho người dùng và thoát khỏi hàm
               

                string a = tagData.Item2;
                DateTime TimeNow = DateTime.Now;

                // Nếu có người ngồi bàn
                if (a == "Có người")
                {
                    int IDban = tagData.Item1;
                    List<string> listMonAn = new List<string>();
                    var TenCacMonAn = QL.LOAD_HOA_DON();

                    int tong = 0;

                    foreach (var t in TenCacMonAn)
                    {
                        if (t.IDBAN == IDban)
                        {
                            listMonAn.Add(t.TenMon);
                        }
                        tong = tong + (int)t.ThanhTien;
                    }

                    // Thực hiện các hành động thanh toán
                    QL.ThanhToanTableList2(IDban);
                    QL.DELETE_ALL_HOA_DON(IDban);
                    
                    string chuoiMonAn = string.Join(", ", listMonAn);

                    QL.INSERT_HOADON_THANH_TOAN(IDban,Tennguoi , chuoiMonAn,TenKH, TimeNow, tong);
                    KH_textBox.Clear();
                    KH_textBox.Enabled= true;
                    listView1.Items.Clear();
                    MessageBox.Show("Đã thanh toán");
                loadTable();
            }
                else 
                {
                    MessageBox.Show("Bàn chưa có người. Vui lòng thêm để thanh toán");
                    return;
                }
            
           
        }
        private void buttonXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)buttonXoa.Tag;
                if (selectedIdHD == 0)
                {
                    MessageBox.Show("Vui lòng chọn món để xóa");
                    return;
                }

                QL.DELETE_HOA_DON_VOI_ID(selectedIdHD);
                loadListView((int)buttonXoa.Tag);
                MessageBox.Show("Đã xóa món đã chọn!");
                // Kiểm tra nếu listView không có mục nào
                if (listView1.Items.Count == 0)  // Kiểm tra số lượng item trong listView
                {
                    QL.XOA_MON_CUOI_CUNG_TRONG_LIST_VIEW(id);
                    loadTable();
                    KH_textBox.Clear(); 
                    KH_textBox.Enabled=true;
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn bàn rồi mới xóa món ăn bàn đó được!");
            }
        }
        private void button_Sua_Click(object sender, EventArgs e)
        {
            if (selectedIdHD == 0)
            {
                MessageBox.Show("Chưa chọn bàn để sửa!");
                return;
            }
            var z = QL.LOAD_DOUONG().ToList();
            var selectedItem = TenMon_comboBox.SelectedItem?.ToString();
            int? donGia = z
                .Where(item => item.TENDOUONG.Equals(selectedItem)) // Lọc món ăn dựa trên tên
                .Select(item => (int?)item.DONGIA) // Lấy giá trị đơn giá
                .FirstOrDefault();

            int id = (int)button_Sua.Tag;
            int SoLuong = (int)numericUpDown2.Value;
            QL.EDIT_HOA_DON_NEW(selectedIdHD, TenMon_comboBox.SelectedItem.ToString(), SoLuong, donGia, SoLuong * donGia);
            MessageBox.Show("Đã sửa bàn:" + id.ToString());
            loadListView((int)button_Sua.Tag);
        }
        public void comboBox_DanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            int idDM = comboBox_DanhMuc.SelectedIndex + 1;
            var DoUong = QL.LOAD_DOUONG_ID(idDM);
            TenMon_comboBox.DataSource = DoUong.Select(item => item.TENDOUONG).ToList();
        }
    }
}
