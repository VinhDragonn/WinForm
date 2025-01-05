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
        public partial class DoanhThu : Form
        {
            private QLCaPheEntities12 QL = new QLCaPheEntities12();

            public DoanhThu()
            {
                InitializeComponent();
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "dd/MM/yyyy"; // Định dạng ngày tháng tiếng Việt
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Format = DateTimePickerFormat.Custom;
                dateTimePicker2.CustomFormat = "dd/MM/yyyy"; // Định dạng ngày tháng tiếng Việt
                dateTimePicker2.Value = DateTime.Now;

                // Đặt ngôn ngữ là tiếng Việt
                CultureInfo viCulture = new CultureInfo("vi-VN");
                System.Threading.Thread.CurrentThread.CurrentCulture = viCulture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = viCulture;

                loadListView(); // Load toàn bộ dữ liệu khi khởi động
            }

            // Method to load ListView with data based on selected dates
            public void loadListView()
            {
                listView1.Items.Clear(); // Clear previous items

                // Get all data (no filtering initially)
                var list = QL.LOAD_HOA_DON_THANH_TOAN().ToList();

                // Add data to ListView
                foreach (var item in list)
                {
                    ListViewItem listViewItem = new ListViewItem(item.IDBAN.ToString());
                    listViewItem.SubItems.Add(item.TENNV.ToString());
                    listViewItem.SubItems.Add(item.TENKH.ToString());
                    listViewItem.SubItems.Add(item.TENMONAN.ToString());
                    listViewItem.SubItems.Add(item.TONGTIEN.ToString("N0", new CultureInfo("vi-VN")));
                    DateTime ngayLap = DateTime.Parse(item.NGAYLAP.ToString());
                    listViewItem.SubItems.Add(ngayLap.ToString("dd/MM/yyyy"));

                    // Add the item to ListView
                    listView1.Items.Add(listViewItem);
                }
            }

            private void DoanhThu_button_Click(object sender, EventArgs e)
            {
                DateTime date1 = dateTimePicker1.Value.Date; // Start date
                DateTime date2 = dateTimePicker2.Value.Date; // End date

                // Check if end date is earlier than start date
                if (date2 < date1)
                {
                    MessageBox.Show("Ngày kết thúc không được nhỏ hơn ngày bắt đầu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Clear current ListView items
                listView1.Items.Clear();

                // Get filtered data based on selected dates
                var filteredList = QL.LOAD_HOA_DON_THANH_TOAN()
                                     .Where(item => item.NGAYLAP.HasValue &&
                                                    item.NGAYLAP.Value.Date >= date1 &&
                                                    item.NGAYLAP.Value.Date <= date2)
                                     .ToList();

                // Add filtered data to ListView
                foreach (var item in filteredList)
                {
                    ListViewItem listViewItem = new ListViewItem(item.IDBAN.ToString());
                    listViewItem.SubItems.Add(item.TENNV.ToString());
                    listViewItem.SubItems.Add(item.TENKH.ToString());
                    listViewItem.SubItems.Add(item.TENMONAN.ToString());
                    listViewItem.SubItems.Add(item.TONGTIEN.ToString("N0", new CultureInfo("vi-VN")));
                    DateTime ngayLap = DateTime.Parse(item.NGAYLAP.ToString());
                    listViewItem.SubItems.Add(ngayLap.ToString("dd/MM/yyyy"));

                    // Add the filtered item to ListView
                    listView1.Items.Add(listViewItem);
                }

                // Optionally, you can show the total sum of filtered items
                int totalAmount = filteredList.Sum(item => item.TONGTIEN);
                label2.Text = $"Tổng tiền từ {date1:dd/MM/yyyy} đến {date2:dd/MM/yyyy} là: {totalAmount:N0} VND";
            }

        private void In_button_Click(object sender, EventArgs e)
        {
            string userDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string wordFilePath = Path.Combine(userDocumentsPath, "HoaDonCaPhe.docx");

            try
            {
                using (var document = DocX.Create(wordFilePath))
                {
                    // Tiêu đề
                    document.InsertParagraph("HÓA ĐƠN")
                        .FontSize(24)
                        .Bold()
                        .Color(Color.Blue)
                        .SpacingAfter(20)
                        .Alignment = Alignment.center;

                    // Lấy dữ liệu từ ListView
                    var listViewItems = listView1.Items.Cast<ListViewItem>().ToList();

                    // Tạo bảng
                    var table = document.AddTable(listViewItems.Count + 1, 6);
                    table.Design = TableDesign.MediumGrid1Accent2;

                    // Tiêu đề bảng
                    table.Rows[0].Cells[0].Paragraphs[0].Append("ID Bàn").Bold().Color(Color.White).Alignment = Alignment.center;
                    table.Rows[0].Cells[1].Paragraphs[0].Append("Tên Nhân Viên").Bold().Color(Color.White).Alignment = Alignment.center;
                    table.Rows[0].Cells[2].Paragraphs[0].Append("Tên Khách Hàng").Bold().Color(Color.White).Alignment = Alignment.center;
                    table.Rows[0].Cells[3].Paragraphs[0].Append("Tên Món Ăn").Bold().Color(Color.White).Alignment = Alignment.center;
                    table.Rows[0].Cells[4].Paragraphs[0].Append("Tổng Tiền").Bold().Color(Color.White).Alignment = Alignment.center;
                    table.Rows[0].Cells[5].Paragraphs[0].Append("Ngày Lập").Bold().Color(Color.White).Alignment = Alignment.center;

                    // Đặt màu nền cho các tiêu đề
                    table.Rows[0].Cells[0].FillColor = Color.DarkBlue;
                    table.Rows[0].Cells[1].FillColor = Color.DarkBlue;
                    table.Rows[0].Cells[2].FillColor = Color.DarkBlue;
                    table.Rows[0].Cells[3].FillColor = Color.DarkBlue;
                    table.Rows[0].Cells[4].FillColor = Color.DarkBlue;
                    table.Rows[0].Cells[5].FillColor = Color.DarkBlue;

                    // Nội dung bảng
                    for (int i = 0; i < listViewItems.Count; i++)
                    {
                        var item = listViewItems[i];
                        table.Rows[i + 1].Cells[0].Paragraphs[0].Append(item.SubItems[0].Text).Alignment = Alignment.center;
                        table.Rows[i + 1].Cells[1].Paragraphs[0].Append(item.SubItems[1].Text).Alignment = Alignment.center;
                        table.Rows[i + 1].Cells[2].Paragraphs[0].Append(item.SubItems[2].Text).Alignment = Alignment.center;
                        table.Rows[i + 1].Cells[3].Paragraphs[0].Append(item.SubItems[3].Text).Alignment = Alignment.center;
                        table.Rows[i + 1].Cells[4].Paragraphs[0].Append(item.SubItems[4].Text).Alignment = Alignment.center;
                        table.Rows[i + 1].Cells[5].Paragraphs[0].Append(item.SubItems[5].Text).Alignment = Alignment.center;
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


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = Tim_textBox.Text.ToLower();  // Chuyển sang chữ thường

            // Chuyển kết quả thành List trước khi xử lý
            var customers = QL.LOAD_HOA_DON_THANH_TOAN().ToList();  // Lưu kết quả thành List

            if (customers == null || !customers.Any())
            {
                MessageBox.Show("Không có dữ liệu!");
                return;
            }

            var sortedCustomers = customers
                .Where(c => c.TENNV.ToString().ToLower().Contains(searchText) ||
                       c.IDBAN.ToString().ToLower().Contains(searchText) ||
                        c.NGAYLAP.ToString().ToLower().Contains(searchText))
                .Select(c => new
                {
                    
                    c.TENNV,
                    c.TENMONAN,
                    c.IDBAN,
                    c.TONGTIEN,
                    c.NGAYLAP,
                    c.TENKH
                   
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
                ListViewItem listViewItem = new ListViewItem(item.IDBAN.ToString());
                listViewItem.SubItems.Add(item.TENNV.ToString());
                listViewItem.SubItems.Add(item.TENKH.ToString());
                listViewItem.SubItems.Add(item.TENMONAN.ToString());
                listViewItem.SubItems.Add(item.TONGTIEN.ToString("N0", new CultureInfo("vi-VN")));
                DateTime ngayLap = DateTime.Parse(item.NGAYLAP.ToString());
                listViewItem.SubItems.Add(ngayLap.ToString("dd/MM/yyyy"));

                listView1.Items.Add(listViewItem);
            }

        }
    }

}
