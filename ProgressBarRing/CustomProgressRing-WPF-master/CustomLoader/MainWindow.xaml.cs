using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CustomLoader {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow:Window {
	public MainWindow() {
	  InitializeComponent();
	}

	private void loaderPlay_Click(object sender,RoutedEventArgs e) {
	  Loader.Visibility = Visibility.Visible;

	  Async1("3","4");
	}

	private void loaderStop_Click(object sender,RoutedEventArgs e) {
	  Loader.Visibility = Visibility.Collapsed;
	}
	private string s;

	private async void Async1(string thamso1,string thamso2) {
	  Func<object,string> myfunc = (object thamso) => {
		dynamic ts = thamso;
		for(int i = 1;i <= 15;i++) {

		  s= $"{Thread.CurrentThread.ManagedThreadId,3} {ts.x,10} {i,5} {ts.y}";
		  Thread.Sleep(500);
		}
		return $"Kết thúc! {ts.x}";
	  };

	  Task<string> task = new Task<string>(myfunc,new { x = thamso1,y = thamso2 });
	  task.Start();

	  Thread.Sleep(1000);
	  this.Title= "Làm gì đó khi task đang chạy ...";

	  await task;     // Gọi Async1 sẽ quay về chỗ gọi nó từ đây


	  // Từ đây là code sau await (trong Async1) sẽ chỉ thi hành khi task kết thúc
	  string ketqua = task.Result;       // Đọc kết quả trả về của task - không phải lo block thread gọi Async1
	  this.Title ="Làm gì đó khi task đã kết thúc";
	  this.Title=ketqua;          // In kết quả trả về của task
	}

  }
}
