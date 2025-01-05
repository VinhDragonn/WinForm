
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using test.model;

namespace test
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Tạo đối tượng của DbContext
            using (var QL = new QLCaPheEntities12())
            {
                // Kiểm tra xem có dữ liệu trong bảng tài khoản không
                var CheckĐN = QL.LOAD_TK_DE_LOGIN3().ToList();
                if (CheckĐN.Count == 0)
                {
                    // Nếu không có tài khoản, mở form đăng ký
                    Application.Run(new DangKy());
                }
                else
                {
                    // Nếu có tài khoản, mở form đăng nhập
                    Application.Run(new DangNhap());
                }
            }

        }

    }
}
