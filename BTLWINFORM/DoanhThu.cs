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
    public partial class DoanhThu : Form
    {
        private QLCaPheEntities13 QL = new QLCaPheEntities13();

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
            MessageBox.Show($"Tổng tiền từ {date1:dd/MM/yyyy} đến {date2:dd/MM/yyyy} là: {totalAmount:N0} VND", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

}
