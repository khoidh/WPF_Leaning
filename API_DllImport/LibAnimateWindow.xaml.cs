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

//
namespace API_DllImport
{
    /// <summary>
    /// Interaction logic for AnimateWindowLib.xaml
    /// </summary>
    public partial class AnimateWindowLib : Window
    {
        public AnimateWindowLib()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern bool AnimateWindow(IntPtr hWnd, int time, AnimateWindowFlags flags);
        //Hàm AnimateWindow chúng ta sẽ truyền vào ba tham số:
        //Tham số 1: Là Handle của đối tượng chúng ta cần thực hiện hiệu ứng
        //Tham số 2: Thời gian delay của hiệu ứng(mili giây)   
        //Tham số 3: Là các cờ hiệu ứng mà chúng ta khai báo ở Enum bên dưới

        //khai báo một Enum AnimateWindowFlags chứa các hiệu ứng của windows.
        enum AnimateWindowFlags : uint
        {
            AW_HOR_POSITIVE = 0x00000001,
            AW_HOR_NEGATIVE = 0x00000002,
            AW_VER_POSITIVE = 0x00000004,
            AW_VER_NEGATIVE = 0x00000008,
            AW_CENTER = 0x00000010,
            AW_HIDE = 0x00010000,
            AW_ACTIVATE = 0x00020000,
            AW_SLIDE = 0x00040000,
            AW_BLEND = 0x00080000
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //            AnimateWindowLib frm = new Form1();
            //AnimateWindow(frm.Handle, 1000, AnimateWindowFlags.AW_ACTIVATE | AnimateWindowFlags.AW_BLEND);
        
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
