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
    /// Interaction logic for ListAccount.xaml
    /// </summary>
    public partial class ListAccount : Window
    {
        //Tạo chuỗi kết nối đến SQL
        SqlConnection connect = new SqlConnection(@"Data Source=JACKL\SQLEXPRESS;Initial Catalog=QLTK;Integrated Security=True");
        public ListAccount()
        {
            InitializeComponent();
            //Gọi lại hàm load để khi vào form sẽ hiện ra
            LoadData();
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
                Close();
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

        void LoadData()
        {
            //Mở kết nối tới SQL
            connect.Open();

            //Tạo câu truy vấn sql
            string sql = ("select MAND as 'MAND', HoTen as N'Họ&Tên', TaiKhoan as N'Tài khoản', MatKhau as N'Mật khẩu',DienThoai as N'Điện Thoại',Email from NGUOIDUNG");

            //Thực hiện câu lệnh sql
            SqlCommand cmd = new SqlCommand(sql, connect);


            //Cho phép đổ dữ liệu vào một DataSet và cập nhật thay đổi vào database
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Tạo đối tượng chứa dữ liệu, nó có tên, các dòng, cột qua đó nó là ánh xạ của một bảng (Table) của CSDL
            DataTable dt = new DataTable();

            //Đổ dữ liệu DataSet
            da.Fill(dt);

            dataListAccount.ItemsSource = dt.DefaultView;
            //Đóng kết nối SQL
            connect.Close();
        }
        int CheckMaND(string Id)
        {
            //tạo câu truy vấn sql
            string sql = "select *from NGUOIDUNG where MaND = '" + Id + "'";

            DataTable result = ExecuteQuery(sql);
            return result.Rows.Count;
        }
        void deleteAccount()
        {
            //Mở kết nối tới SQL
            connect.Open();

            //Tạo câu truy vấn sql
            string sql = ("delete NGUOIDUNG where MaND ='"+ MaND.Text+ "'");

            //Thực hiện câu lệnh sql
            SqlCommand cmd = new SqlCommand(sql, connect);


            //Cho phép đổ dữ liệu vào một DataSet và cập nhật thay đổi vào database
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Tạo đối tượng chứa dữ liệu, nó có tên, các dòng, cột qua đó nó là ánh xạ của một bảng (Table) của CSDL
            DataTable dt = new DataTable();

            //Đổ dữ liệu DataSet
            da.Fill(dt);


            //Đóng kết nối SQL
            connect.Close();
        }
        //Hàm thực hiện xóa các tài khoản
        private void DeleteAccount(object sender, RoutedEventArgs e)
        { 
            //Kiểm tra nếu cái TextBox đó rỗng yêu cầu nhập
            if(MaND.Text == "")
            {
                MessageBox.Show("Hãy nhập mã người dùng để xóa", "Thông báo");
            }
            //Kiểm tra mã người dùng có tồn tại hay không
            else if (CheckMaND(MaND.Text) == 0)
            {
                MessageBox.Show("Không thấy mã người dùng cần xóa", "Thông báo");
            }

            //Nếu không nằm các điều kiện trên sẽ xóa được tài khoản
            else
            {
                MessageBox.Show("Đã xóa tài khoản người dùng thành công", "Thông báo");
                deleteAccount();
                LoadData();
            }

        }
            private DataTable ExecuteQuery(string query, object[] parameter = null)
            {
            string con = @"Data Source=JACKL\SQLEXPRESS;Initial Catalog=QLTK;Integrated Security=True";
            DataTable data = new DataTable();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                //Tạo biến cmd để thực thi Excute câu query, connection để biết kết nối đến dtb nào
                SqlCommand cmd = new SqlCommand(query, connect);
                //Lấy dữ liệu ra (cmd.ExecuteReader)

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                //Lấy dữ liệu xuất ra từ cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //Cho dữ liệu từ adapter vào dữ liệu bản data
                adapter.Fill(data);
                connect.Close();
            }

            return data;
        }


    }
}
