using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Runtime.InteropServices;

//http://aptech.fpt.edu.vn/chitiet.php?id=447
namespace API_DllImport
{
    /// <summary>
    /// Interaction logic for APIGetSystemInfo.xaml
    /// </summary>
    public partial class APIGetSystemInfo : Window
    {
        public APIGetSystemInfo()
        {
            InitializeComponent();
        }

        //Khai báo hàm API:
        [DllImport("kernel32")]
        public static extern void GetSystemInfo(ref SYSTEM_INFO pSI);

        //Trong sự kiện Click của button thêm đoạn code

        private void btGetSystemInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SYSTEM_INFO pSI = new SYSTEM_INFO();
                GetSystemInfo(ref pSI);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }

    //Khai báo cấu trúc với những tham số của GetSystemInfo.
    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEM_INFO
    {
        public uint dwOemId;
        public uint dwPageSize;
        public uint lpMinimumApplicationAddress;
        public uint lpMaximumApplicationAddress;
        public uint dwActiveProcessorMask;
        public uint dwNumberOfProcessors;
        public uint dwProcessorType;
        public uint dwAllocationGranulariry;
        public uint dwProcessorLevel;
        public uint dwProcessorRevision;
    }
}
