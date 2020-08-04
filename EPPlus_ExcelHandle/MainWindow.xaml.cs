using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using OfficeOpenXml.Style;

namespace EPPlus_ExcelHandle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            // tạo ra danh sách UserInfo rỗng để hứng dữ liệu.
            List<UserInfo> userList = new List<UserInfo>();
            try
            {
                // mở file excel
                //var package = new ExcelPackage(new FileInfo(System.IO.Path.Combine(
                //    System.IO.Path.GetFullPath(@"..\..\data\"), 
                //    "ImportData.xlsx")));
                var package = new ExcelPackage(new FileInfo("ImportData.xlsx"));
                //var str = System.IO.Path.GetFullPath(@"..\..\");

                // lấy ra sheet đầu tiên để thao tác
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];

                // duyệt tuần tự từ dòng thứ 2 đến dòng cuối cùng của file. lưu ý file excel bắt đầu từ số 1 không phải số 0
                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    try
                    {
                        // biến j biểu thị cho một column trong file
                        int j = 1;

                        // lấy ra cột họ tên tương ứng giá trị tại vị trí [i, 1]. i lần đầu là 2
                        // tăng j lên 1 đơn vị sau khi thực hiện xong câu lệnh
                        string name = workSheet.Cells[i, j++].Value.ToString();

                        // lấy ra cột ngày sinh tương ứng giá trị tại vị trí [i, 2]. i lần đầu là 2
                        // tăng j lên 1 đơn vị sau khi thực hiện xong câu lệnh
                        // lấy ra giá trị ngày tháng và ép kiểu thành DateTime                      
                        var birthdayTemp = workSheet.Cells[i, j++].Value;
                        DateTime birthday = new DateTime();
                        if (birthdayTemp != null)
                        {
                            birthday = (DateTime)birthdayTemp;
                        }

                        /*                         

                        Đừng lười biến mà dùng đoạn code này sẽ gây ra lỗi nếu giá trị value không thỏa kiểu DateTime

                        DateTime birthday = (DateTime)workSheet.Cells[i, j++].Value;

                         */


                        // tạo UserInfo từ dữ liệu đã lấy được
                        UserInfo user = new UserInfo()
                        {
                            Name = name,
                            Birthday = birthday
                        };

                        // add UserInfo vào danh sách userList
                        userList.Add(user);

                    }
                    catch (Exception exe)
                    {

                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Error!");
            }

            dtgExcel.ItemsSource = userList;
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class UserInfo
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}
