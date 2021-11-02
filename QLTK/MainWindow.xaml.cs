using QLTK.Views;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QLTK
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Tạo chuỗi kết nối đến sql
        string strConnection = @"Data Source=JACKL\SQLEXPRESS;Initial Catalog=QLTK;Integrated Security=True";
        //SQl Kết nối
        SqlConnection conn;
        //SQl Thực hiện câu lệnh
        SqlCommand command;
        public MainWindow()
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
       
        //Nút button để chuyển sang Form đăng ký
        private void btn_register(object sender, RoutedEventArgs e)
        {

            this.Hide();
            Register register = new Register();
            register.ShowDialog();
            this.Show();
        }

        //Nút button để đăng nhập vào Form Menu
        private void btn_login(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = "Select Count(*) From NGUOIDUNG Where TaiKhoan=@acc And MatKhau =@pass";
                conn = new SqlConnection(strConnection);
                conn.Open();
                command = new SqlCommand(sql, conn);
                command.Parameters.Add(new SqlParameter("@acc", txtAccount.Text));
                command.Parameters.Add(new SqlParameter("@pass", txtPassword.Password));
             
                int x = (int)command.ExecuteScalar();
                string userName = txtAccount.Text;

                //Kiểm tra rỗng của TextBôx
                if( txtAccount.Text =="")
                {
                    MessageBox.Show("Hãy nhập tên tài khoản", "Thông Báo");

                }
                else if (txtPassword.Password == "")
                {
                    MessageBox.Show("Hãy nhập mật khẩu", "Thông Báo");

                }
                //Kiểm tra rỗng xong sẽ đến điều kiện để thực hiện đăng nhập
                else
                {
                    //Điều kiện đúng xe đăng nhập
                    if (x == 1) 
                    {
                        ClearTextBox();
                        Views.Menu menu = new Views.Menu();
                        this.Close();
                        menu.ShowDialog();
                    
                    }
                    //Ngược lại sẽ báo lỗi về tài khoản hoặc mật khẩu
                    else
                    {
                        MessageBox.Show("Sai tài khoản hoặc mặt khẩu", "Thông Báo");
                    }
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


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
        //Hàm xóa textBox
        void ClearTextBox()
        {
            txtAccount.Clear();
            txtPassword.Clear();
            passwordTxtBox.Clear();
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

       
        //Sử dụng nút enter để đăng nhập
        private void Login_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                try
                {
                    string sql = "Select Count(*) From NGUOIDUNG Where TaiKhoan=@acc And MatKhau =@pass";
                    conn = new SqlConnection(strConnection);
                    conn.Open();
                    command = new SqlCommand(sql, conn);
                    command.Parameters.Add(new SqlParameter("@acc", txtAccount.Text));
                    command.Parameters.Add(new SqlParameter("@pass", txtPassword.Password));

                    int x = (int)command.ExecuteScalar();
                    string userName = txtAccount.Text;

                    if (txtAccount.Text == "")
                    {
                        MessageBox.Show("Hãy nhập tên tài khoản", "Thông Báo");

                    }
                    else if (txtPassword.Password == "")
                    {
                        MessageBox.Show("Hãy nhập mật khẩu", "Thông Báo");

                    }
                    else
                    {
                        if (x == 1)
                        {
                            ClearTextBox();
                            Views.Menu menu = new Views.Menu();
                            this.Close();
                            menu.ShowDialog();

                        }
                        else
                        {
                            MessageBox.Show("Sai tài khoản hoặc mặt khẩu", "Thông Báo");
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
