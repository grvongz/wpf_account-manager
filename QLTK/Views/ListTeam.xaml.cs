using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace QLTK.Views
{
    /// <summary>
    /// Interaction logic for ListTeam.xaml
    /// </summary>
    public partial class ListTeam : Window
    {
        //Tạo chuỗi kết nối đến SQL
        SqlConnection connect = new SqlConnection(@"Data Source=JACKL\SQLEXPRESS;Initial Catalog=QLTK;Integrated Security=True");
        public ListTeam()
        {
            InitializeComponent();
            //Gọi lại hàm load để khi vào form sẽ hiện ra
            loadData();
        }
        //Sử dụng chuột để di chuyển Form
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        //Đóng chương trình
        private void closeApp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //Thu nhỏ chương trình

        private void minimiseApp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Phóng to và trở lại bình thường của chương trình
        private void maximiseApp(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }
        

        //Tạo hàm load danh sách nhóm
        void loadData()
        {
            //Mở kết nối tới SQL
            connect.Open();

            //Tạo câu truy vấn sql
            string sql = ("select STT as 'STT', HoTen as N'Họ&Tên', MaSV as N'Mã sinh viên', PhanChiaCongViec as N'Phân chia công việc',ThucHien as N'Thực hiện' from NHOM");

            //Thực hiện câu lệnh sql
            SqlCommand cmd = new SqlCommand(sql, connect);


            //Cho phép đổ dữ liệu vào một DataSet và cập nhật thay đổi vào database
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Tạo đối tượng chứa dữ liệu, nó có tên, các dòng, cột qua đó nó là ánh xạ của một bảng (Table) của CSDL
            DataTable dt = new DataTable();

            //Đổ dữ liệu DataSet
            da.Fill(dt);

            dataListTeam.ItemsSource = dt.DefaultView;
            //Đóng kết nối SQL
            connect.Close();
        }
        
    }
}
