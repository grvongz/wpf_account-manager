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
namespace QLTK.Views
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        
        public Menu()
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

                this.Hide();
               MainWindow mainWindow = new MainWindow();
                mainWindow.ShowDialog();
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

        //Chuyển sang form List Account

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ListAccount list = new ListAccount();
            list.ShowDialog();
            this.Show();
           
        }


        //Chuyển sang form Đăng ký xe công tác
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            WorkCarRegistration workCarRegistration = new WorkCarRegistration();
            workCarRegistration.ShowDialog();
            this.Show();
         


        }
        //Chuyển sang form Danh sách nhóm
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ListTeam listTeam = new ListTeam();
            listTeam.ShowDialog();
            this.Show();
          

        }
    }
}
