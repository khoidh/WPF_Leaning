using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
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

namespace ExcelInCsharp
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
                var package = new ExcelPackage(new FileInfo("ImportData.xlsx"));

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
            string filePath = "";
            // tạo SaveFileDialog để lưu file excel
            SaveFileDialog dialog = new SaveFileDialog();

            // chỉ lọc ra các file có định dạng Excel
            dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

            // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
            if (dialog.ShowDialog() == true)
            {
                filePath = dialog.FileName;
            }

            // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
                return;
            }

            try
            {
                using (ExcelPackage p = new ExcelPackage())
                {
                    // đặt tên người tạo file
                    p.Workbook.Properties.Author = "Kteam by K9";

                    // đặt tiêu đề cho file
                    p.Workbook.Properties.Title = "Báo cáo thống kê";

                    //Tạo một sheet để làm việc trên đó
                    p.Workbook.Worksheets.Add("Kteam sheet");

                    // lấy sheet vừa add ra để thao tác
                    ExcelWorksheet ws = p.Workbook.Worksheets[1];

                    // đặt tên cho sheet
                    ws.Name = "Kteam sheet";
                    // fontsize mặc định cho cả sheet
                    ws.Cells.Style.Font.Size = 11; 
                    // font family mặc định cho cả sheet
                    ws.Cells.Style.Font.Name = "Calibri";

                    // Tạo danh sách các column header
                    string[] arrColumnHeader = {    
                                                "Họ tên",
                                                "Năm sinh"                                                
                };

                    // lấy ra số lượng cột cần dùng dựa vào số lượng header
                    var countColHeader = arrColumnHeader.Count();

                    // merge các column lại từ column 1 đến số column header
                    // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                    ws.Cells[1, 1].Value = "Thống kê thông tin User Kteam";
                    ws.Cells[1, 1, 1, countColHeader].Merge = true;
                    // in đậm
                    ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                    // căn giữa
                    ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    int colIndex = 1;
                    int rowIndex = 2;

                    //tạo các header từ column header đã tạo từ bên trên
                    foreach (var item in arrColumnHeader)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];

                        //set màu thành gray
                        var fill = cell.Style.Fill;
                        fill.PatternType = ExcelFillStyle.Solid;
                        fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                        //căn chỉnh các border
                        var border = cell.Style.Border;
                        border.Bottom.Style =
                            border.Top.Style =
                            border.Left.Style =
                            border.Right.Style = ExcelBorderStyle.Thin;

                        //gán giá trị
                        cell.Value = item;

                        colIndex++;
                    }

                    // lấy ra danh sách UserInfo từ ItemSource của DataGrid
                    List<UserInfo> userList = dtgExcel.ItemsSource.Cast<UserInfo>().ToList();

                    // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                    foreach (var item in userList)
                    {
                        // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                        colIndex = 1;

                        // rowIndex tương ứng từng dòng dữ liệu
                        rowIndex++;

                        //gán giá trị cho từng cell                      
                        ws.Cells[rowIndex, colIndex++].Value = item.Name;

                        // lưu ý phải .ToShortDateString để dữ liệu khi in ra Excel là ngày như ta vẫn thấy.Nếu không sẽ ra tổng số :v
                        ws.Cells[rowIndex, colIndex++].Value = item.Birthday.ToShortDateString();
                       
                    }                    

                    //Lưu file lại
                    Byte[] bin = p.GetAsByteArray();
                    File.WriteAllBytes(filePath, bin);
                }
                MessageBox.Show("Xuất excel thành công!");
            }
            catch (Exception EE)
            {
                MessageBox.Show("Có lỗi khi lưu file!");
            }
        }
    }
    public class UserInfo
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}
