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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        //Tạo chuỗi kết nối đến SQL
        SqlConnection connect = new SqlConnection(@"Data Source=JACKL\SQLEXPRESS;Initial Catalog=QLTK;Integrated Security=True");
        public Register()
        {
            InitializeComponent();
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

        //Hàm tạo tài khoản người dùng
        void CreateAccount()
        {
            //Mở kết nối tới SQL
            connect.Open();
            //Tạo câu tru vấn sql
            string sql = ("insert NGUOIDUNG values(N'" + txtName.Text + "','" + txtAccount.Text + "','" + txtPassword.Password + "','"+txtPhone.Text+"','"+txtEmail.Text+"')");

            //Thực hiện câu lệnh sql
            SqlCommand cmd = new SqlCommand(sql, connect);

            //Cho phép đổ dữ liệu vào một DataSet và cập nhật thay đổi vào database
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Tạo đối tượng chứa dữ liệu, nó có tên, các dòng, cột qua đó nó là ánh xạ của một bảng (Table) của CSDL
            DataTable dt = new DataTable();

            //Đổ dữ liệu DataSet
            da.Fill(dt);

            //Đóng kết nối với SQL
            connect.Close();
         
        }

        //Xóa các textbox để tạo thêm tài khoản
        void ClearTextBox()
        {
            txtAccount.Clear();
            txtPassword.Clear();
            txtConfiPassword.Clear();
            txtName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            passwordConfirm.Clear();
            passwordTxtBox.Clear();
        }
        //Kiểm tra tài khoản có tồn tài không
        int checkAccount(string Account)
        {
            //tạo câu truy vấn sql
            string sql = "select *from NGUOIDUNG where TaiKhoan = '" + Account + "'";

            DataTable result = ExecuteQuery(sql);
            return result.Rows.Count;
        }
       private void createAccount(object sender, RoutedEventArgs e)
        {
            //Kiểm tra rỗng của các nút TextBox
            if (txtAccount.Text =="")
            {
                MessageBox.Show("Hãy nhập tên tài khoản ", "Thông báo");
            }
            else if (txtPassword.Password== "")
            {
                MessageBox.Show("Hãy nhập mật khẩu ", "Thông báo");
            }
            else if (txtConfiPassword.Password == "")
            {
                MessageBox.Show("Hãy nhập xác nhận mật khẩu ", "Thông báo");
            }
            else if (txtName.Text == "")
            {
                MessageBox.Show("Hãy nhập tên người dùng ", "Thông báo");
            }
            else if (txtEmail.Text == "")
            {
                MessageBox.Show("Hãy nhập Email", "Thông báo");
            }
            else if (txtPhone.Text == "")
            {
                MessageBox.Show("Hãy nhập số điện thoại", "Thông báo");
            }
            //Kiểm tra rỗng sẽ đến điều kiện xử lý đăng ký
            else
            {
                //Kiểm tra tài khoản đã tồn tại
                if (checkAccount(txtAccount.Text) != 0)
                {
                    MessageBox.Show("Tài khoản đã tồn tại", "Thông báo");
                }
                //Kiểm tra mật khẩu 2 lần để chắc mình nhập đúng
                else if (txtPassword.Password != txtConfiPassword.Password)
                {
                    MessageBox.Show("Mật khẩu không trùng khớp", "Thông Báo");
                }
                //Nếu không nằm các điều kiện trên sẽ đăng ký được được tài khoản
                else
                { 
                CreateAccount();
                MessageBox.Show("Tạo tài khoản thành công", "Thông Báo");
                    ClearTextBox();
                }
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

        //Hiện mật khẩu
        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            passwordTxtBox.Text = txtPassword.Password;
            txtPassword.Visibility = Visibility.Collapsed;
            passwordTxtBox.Visibility = Visibility.Visible;
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            txtPassword.Password = passwordTxtBox.Text;
            passwordTxtBox.Visibility = Visibility.Collapsed;
            txtPassword.Visibility = Visibility.Visible;
        }
        //Hiện mật khẩu xác nhận
        private void ShowPassword_Checked2(object sender, RoutedEventArgs e)
        {
            passwordConfirm.Text = txtConfiPassword.Password;
            txtConfiPassword.Visibility = Visibility.Collapsed;
            passwordConfirm.Visibility = Visibility.Visible;
        }

        private void ShowPassword_Unchecked2(object sender, RoutedEventArgs e)
        {
            txtConfiPassword.Password = passwordConfirm.Text;
            passwordConfirm.Visibility = Visibility.Collapsed;
            txtConfiPassword.Visibility = Visibility.Visible;
        }
    }
}
