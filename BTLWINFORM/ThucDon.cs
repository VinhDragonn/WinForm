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
using Xceed.Document.NET;
using Xceed.Words.NET;
using System.IO;
namespace test
{
    public partial class ThucDon : Form
    {
        private QLCaPheEntities12 QL = new QLCaPheEntities12();
        private int selectedIdHD=0;
        public ThucDon()
        {
            InitializeComponent();
            var danhMucList = QL.LOAD_DANHMUC().ToList();
            DanhMuc_comboBox.DataSource = danhMucList;
            DanhMuc_comboBox.DisplayMember = "TENDM"; // Hiển thị tên danh mục
            DanhMuc_comboBox.ValueMember = "IDDM";   // Giá trị thực sẽ là IDDM
            DanhMuc_comboBox.SelectedIndex = 0;

            loadListView();

        }
        public void loadListView()
        {
            // Lấy danh sách đồ uống từ cơ sở dữ liệu
            var list = QL.LOAD_DOUONG().ToList();

            // Xóa các mục hiện có trong ListView (nếu có)
            listView1.Items.Clear();

            // Duyệt qua danh sách và thêm từng item vào ListView
            foreach (var item in list)
            {
                // Tạo một ListViewItem mới
                ListViewItem listViewItem = new ListViewItem(item.IDDOUONG.ToString());
                listViewItem.SubItems.Add(item.IDDM.ToString()=="1"?"Nước":"Đồ ăn");
                listViewItem.SubItems.Add(item.TENDOUONG);   // Thêm cột Tên đồ uống
                listViewItem.SubItems.Add(item.DONGIA.ToString("N0", new CultureInfo("vi-VN")));

                // Thêm ListViewItem vào ListView
                listView1.Items.Add(listViewItem);
            }
        }
        private void In_button_Click(object sender, EventArgs e)
        {
            string userDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string wordFilePath = Path.Combine(userDocumentsPath, "MenuCaPhe.docx");

            try
            {
                using (var document = DocX.Create(wordFilePath))
                {
                    // Tiêu đề
                    document.InsertParagraph("MENU CÀ PHÊ")
                        .FontSize(24)
                        .Bold()
                        .Color(Color.Blue)
                        .SpacingAfter(20)
                        .Alignment = Alignment.center;

                    // Tạo bảng
                    var list = QL.LOAD_DOUONG().ToList();
                    var table = document.AddTable(list.Count + 1, 4);
                    table.Design = TableDesign.MediumGrid1Accent2;

                    // Tiêu đề bảng
                    table.Rows[0].Cells[0].Paragraphs[0]
                           .Append("ID").Bold().Color(Color.White).Alignment = Alignment.center;
                    table.Rows[0].Cells[1].Paragraphs[0].Append("Loại").Bold().Color(Color.White).Alignment= Alignment.center;
                    table.Rows[0].Cells[2].Paragraphs[0].Append("Tên Đồ Uống").Bold().Color(Color.White).Alignment = Alignment.center;
                    table.Rows[0].Cells[3].Paragraphs[0].Append("Đơn Giá").Bold().Color(Color.White).Alignment = Alignment.center;

                    table.Rows[0].Cells[0].FillColor = Color.DarkBlue;
                    table.Rows[0].Cells[1].FillColor = Color.DarkBlue;
                    table.Rows[0].Cells[2].FillColor = Color.DarkBlue;
                    table.Rows[0].Cells[3].FillColor = Color.DarkBlue;

                    // Nội dung bảng
                    for (int i = 0; i < list.Count; i++)
                    {
                        var item = list[i];
                        table.Rows[i + 1].Cells[0].Paragraphs[0].Append(item.IDDOUONG.ToString()).Alignment = Alignment.center;
                        table.Rows[i + 1].Cells[1].Paragraphs[0].Append(item.IDDM.ToString() == "1" ? "Nước" : "Đồ ăn").Alignment = Alignment.center;
                        table.Rows[i + 1].Cells[2].Paragraphs[0].Append(item.TENDOUONG).Alignment = Alignment.center;
                        table.Rows[i + 1].Cells[3].Paragraphs[0].Append(item.DONGIA.ToString("N0", new CultureInfo("vi-VN"))).Alignment = Alignment.center;
                    }

                    // Chèn bảng vào tài liệu
                    document.InsertTable(table);

                    // Lưu tài liệu
                    document.Save();

                    MessageBox.Show("Xuất dữ liệu thành công! File đã được lưu tại: " + wordFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ThucDon_Load(object sender, EventArgs e)
        {

        }

        private void DanhMuc_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Them_button_Click(object sender, EventArgs e)
        {
            try
            {
                DanhMuc_comboBox.Enabled = true;

                // Kiểm tra giá trị giá tiền
                float tien;
                if (!float.TryParse(Gia_textBox.Text, out tien))
                {
                    MessageBox.Show("Giá tiền không hợp lệ. Vui lòng nhập một số hợp lệ.");
                    return;
                }

                // Kiểm tra giá trị danh mục
                int idDM;
                if (!int.TryParse(DanhMuc_comboBox.SelectedValue?.ToString(), out idDM))
                {
                    MessageBox.Show("Vui lòng chọn danh mục hợp lệ.");
                    return;
                }

                // Gọi stored procedure
                QL.INSERT_IN_FIRST_AVAILABLE_DO_UONG(idDM, Ten_textBox.Text.ToString(), tien);
                QL.SaveChanges();

                // Reset các trường giao diện
                Ten_textBox.Text = Gia_textBox.Text = "";
                DanhMuc_comboBox.Refresh();
                MessageBox.Show("Đã thêm");
                loadListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }



        private void Xoa_button3_Click(object sender, EventArgs e)
        {
            DanhMuc_comboBox.Enabled = true;
            QL.DELETE_DOUONG3(selectedIdHD);
            QL.SaveChanges();
            MessageBox.Show("Đã xóa món:" + selectedIdHD.ToString());
            loadListView();
            DanhMuc_comboBox.SelectedIndex = -1;
            Ten_textBox.Clear();
            Gia_textBox.Clear();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       private void listView1_Click(object sender, EventArgs e)
{
    try
    {
        if (listView1.SelectedItems.Count > 0) // Kiểm tra nếu có mục được chọn
        {
            // Lấy giá trị từ mục được chọn
            string id = listView1.SelectedItems[0].Text.ToString();

            // Kiểm tra số lượng SubItems
            if (listView1.SelectedItems[0].SubItems.Count > 3)
            {
                   DanhMuc_comboBox.SelectedIndex = listView1.SelectedItems[0].SubItems.Count > 3 &&
                    listView1.SelectedItems[0].SubItems[1].Text.Trim() == "Nước"? 0 : 1;
               
                        Ten_textBox.Text = listView1.SelectedItems[0].SubItems[2].Text.Trim();
                Gia_textBox.Text = listView1.SelectedItems[0].SubItems[3].Text.Trim();
            }

            // Kiểm tra và chuyển đổi ID
            if (int.TryParse(id, out int parsedId))
            {
                selectedIdHD = parsedId;
            }
            else
            {
                MessageBox.Show("Dữ liệu ID không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        else
        {
            // Đặt lại các điều khiển nếu không có mục được chọn
            DanhMuc_comboBox.SelectedIndex = -1;
            Ten_textBox.Clear();
            Gia_textBox.Clear();
        }
    }
    catch (Exception ex)
    {
        // Hiển thị thông báo lỗi
        MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

        private void Sua_button_Click(object sender, EventArgs e)
        {
            
            QL.UPDATE_DOUONG2(selectedIdHD,Ten_textBox.Text, Convert.ToDouble((Gia_textBox.Text)));
            DanhMuc_comboBox.SelectedIndex = -1;
            Ten_textBox.Clear();
            Gia_textBox.Clear();
            MessageBox.Show("Đã sửa món: " + selectedIdHD);
            loadListView();
        }

        private void Tim_textBox_TextChanged(object sender, EventArgs e)
        {
            string searchText = Tim_textBox.Text.ToLower();

            // Lọc dữ liệu
            var sortedCustomers = QL.LOAD_DOUONG()
                .Where(c => c.IDDOUONG.ToString().Contains(searchText) ||
                             c.TENDM.ToLower().Contains(searchText) ||
                             c.TENDOUONG.ToLower().Contains(searchText) ||
                             c.DONGIA.ToString().ToLower().Contains(searchText))
                .Select(c => new
                {
                    c.IDDOUONG,
                    c.TENDM,
                    c.TENDOUONG,
                    c.DONGIA
                })
                .ToList();

            // Làm mới ListView
            listView1.Items.Clear(); // Xóa các item cũ

            // Thêm dữ liệu mới
            foreach (var customer in sortedCustomers)
            {
                ListViewItem item = new ListViewItem(customer.IDDOUONG.ToString());
                item.SubItems.Add(customer.TENDM);
                item.SubItems.Add(customer.TENDOUONG);
                item.SubItems.Add(customer.DONGIA.ToString("N0", new CultureInfo("vi-VN")));

                listView1.Items.Add(item);
            }
        }
       
    }
}
