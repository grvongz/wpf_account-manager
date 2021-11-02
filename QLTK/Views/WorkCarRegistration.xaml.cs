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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QLTK.Views
{
    /// <summary>
    /// Interaction logic for WorkCarRegistration.xaml
    /// </summary>
    public partial class WorkCarRegistration : Window
    {
        //Tạo chuỗi kết nối đến SQL
        SqlConnection connect = new SqlConnection(@"Data Source=JACKL\SQLEXPRESS;Initial Catalog=QLTK;Integrated Security=True");
        public WorkCarRegistration()
        {
            InitializeComponent();
            LoadCongTac();
        }
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
                System.Windows.MessageBox.Show(ex.Message);
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
                System.Windows.MessageBox.Show(ex.Message);
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


        //Load bảng khi tìm kiếm nơi đến
        void LoadDataSearch()
        {
            //Mở kết nối tới SQL
            connect.Open();

            //Tạo câu truy vấn sql
            string sql = ("select STT as 'STT', City as N'Tỉnh', Address as N'Quận&Huyện' from NOIDEN where City = N'" + Type.Text + "'");

            //Thực hiện câu lệnh sql
            SqlCommand cmd = new SqlCommand(sql, connect);


            //Cho phép đổ dữ liệu vào một DataSet và cập nhật thay đổi vào database
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Tạo đối tượng chứa dữ liệu, nó có tên, các dòng, cột qua đó nó là ánh xạ của một bảng (Table) của CSDL
            DataTable dt = new DataTable();

            //Đổ dữ liệu DataSet
            da.Fill(dt);

            dataListNoiDen.ItemsSource = dt.DefaultView;
            //Đóng kết nối SQL
            connect.Close();
        }

        //Kiểm tra coi địa chỉ hay thành phố đó có trong xe công tác không
        int CheckAddress(string Address)
        {
            //tạo câu truy vấn sql
            string sql = "select *from NOIDEN where City = '" + Address + "'";

            DataTable result = ExecuteQuery(sql);
            return result.Rows.Count;
        }

        //Tìm Kiếm nơi đến
        private void Search(object sender, RoutedEventArgs e)
        {
            //Kiểm tra nếu cái TextBox đó rỗng yêu cầu nhập
            if (Type.Text == "")
            {
                System.Windows.MessageBox.Show("Hãy nhập để tìm kếm", "Thông báo");
            }
            //Kiểm tra mã người dùng có tồn tại hay không
            else if (CheckAddress(Type.Text) != 0)
            {
                System.Windows.MessageBox.Show("Không thấy tỉnh thành", "Thông báo");
            }

            //Nếu không nằm các điều kiện trên sẽ tìm được tỉnh,quận,huyện
            else
            {

                LoadDataSearch();

            }

        }

        //Tạo hàm Load  Table Công Tác

        void LoadCongTac()
        {
            //Mở kết nối tới SQL
            connect.Open();

            //Tạo câu truy vấn sql
            string sql =
            "select MaDK as N'Mã ĐK',TimeGo as N'Thời gian bắt đầu',TimeEnd as N'Thời gian kết thúc',NguoiChuanBi as N'Người chuẩn bị',Noidi as N'Nơi đi',Noiden as N'Nơi đến',ThanhPhan as N'Thành phần',SoKm as N'Số Km', SoGhe as N'Số ghế', NoiDung as N'Nội dung',DonViChuTri as N'Đơn vị chủ trì',GhiChu as N'Ghi chú',MaND as N'Mã người dùng' from CONGTAC";

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
            dataListDangKyCongTac.ItemsSource = dt.DefaultView;
        }
        //Hàm thêm đăng ký xe công tác
        void Add()
        {
            //Mở kết nối SQL
            connect.Open();

            //Câu lệnh truy vấn Sql         
            string sql = "insert into CONGTAC( TimeGo, TimeEnd, NguoiChuanBi, Noidi, Noiden, ThanhPhan, SoKm, SoGhe, NoiDung, DonViChuTri, GhiChu)  values(@TimeGo,@TimeEnd,@NguoiChuanBi,@Noidi,@Noiden,@ThanhPhan,@SoKm,@SoGhe,@NoiDung,@DonViChuTri,@GhiChu)";

            //Thực thi câu lệnh
            SqlCommand cmd = new SqlCommand(sql, connect);
            cmd.Parameters.AddWithValue("TimeGo", DateTime.Parse(dataGo.Text));
            cmd.Parameters.AddWithValue("TimeEnd", DateTime.Parse(dataEnd.Text));
            cmd.Parameters.AddWithValue("NguoiChuanBi", NguoiChuanBi.Text);
            cmd.Parameters.AddWithValue("Noidi", NoiDi.Text);
            cmd.Parameters.AddWithValue("Noiden", NoiDen.Text);
            cmd.Parameters.AddWithValue("ThanhPhan", ThanhPhan.Text);
            cmd.Parameters.AddWithValue("SoKm", SoKm.Text);
            cmd.Parameters.AddWithValue("SoGhe", SoGhe.Text);
            cmd.Parameters.AddWithValue("NoiDung", NoiDung.Text);
            cmd.Parameters.AddWithValue("DonViChuTri", DonViChuTri.Text);
            cmd.Parameters.AddWithValue("GhiChu", GhiChu.Text);

            //Xử lý
            cmd.ExecuteNonQuery();


            //Đóng kết nối
            connect.Close();
        }
        private void AddRegister(object sender, RoutedEventArgs e)
        {
            //Kiểm tra textBox có rỗng không nếu rỗng yêu cầu nhập
            if (dataGo.Text == "")
            {
                System.Windows.MessageBox.Show("Hãy nhập thời gian bắt đầu", "Thông báo");
            }
            else if (dataEnd.Text == "")
            {
                System.Windows.MessageBox.Show("Hãy nhập thời gian kết thúc", "Thông báo");
            }
            else if (NguoiChuanBi.Text == "")
            {
                System.Windows.MessageBox.Show("Hãy nhập người chuẩn bị", "Thông báo");
            }
            else if (NoiDi.Text == "")
            {
                System.Windows.MessageBox.Show("Hãy nhập nơi đi", "Thông báo");
            }
            else if (NoiDen.Text == "")
            {
                System.Windows.MessageBox.Show("Hãy nhập nơi đến", "Thông báo");

            }
            else if (ThanhPhan.Text == "")
            {
                System.Windows.MessageBox.Show("Hãy nhập thành phần", "Thông báo");
            }
            else if (SoKm.Text == "")
            {
                System.Windows.MessageBox.Show("Hãy nhập số km", "Thông báo");
            }
            else if (SoGhe.Text == "")
            {
                System.Windows.MessageBox.Show("Hãy nhập số ghế", "Thông báo");
            }
            else if (NoiDung.Text == "")
            {
                System.Windows.MessageBox.Show("Hãy nhập nội dung", "Thông báo");
            }
            else if (DonViChuTri.Text == "")
            {
                System.Windows.MessageBox.Show("Hãy nhập đơn vị chủ trì", "Thông báo");
            }
            else if (GhiChu.Text == "")
            {
                System.Windows.MessageBox.Show("Hãy nhập ghi chú", "Thông báo");
            }
            else
            if (DonViChuTri.Text == "")
            {
                System.Windows.MessageBox.Show("Hãy nhập đơn vị chủ trì", "Thông báo");
            }
            else
            {
                if (CheckChair(SoGhe.Text) != 0)
                {
                    System.Windows.MessageBox.Show("Ghế đã được đăng ký xin hãy chọn số ghê khác ", "Thông báo");
                }
                else if(int.Parse(SoGhe.Text) >= 40){
                    System.Windows.MessageBox.Show("Hãy nhập số ghế từ 1-40 ", "Thông báo");
                }
                else { 
                    Add();
                    LoadCongTac();
                }
            }
           
        }




        void deleteAccount()
        {
            //Mở kết nối tới SQL
            connect.Open();

            //Tạo câu truy vấn sql
            string sql = ("delete CONGTAC where MaDK = '" + Type.Text + "'");

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



        //Kiểm tra 
        int CheckRegister(string Id)
        {
            //tạo câu truy vấn sql
            string sql = "select *from CONGTAC where MaDK = '" + Id + "'";

            DataTable result = ExecuteQuery(sql);
            return result.Rows.Count;
        }
        int CheckChair(string Chair)
        {
            //tạo câu truy vấn sql
            string sql = "select *from CONGTAC where SoGhe = '" + Chair + "'";

            DataTable result = ExecuteQuery(sql);
            return result.Rows.Count;
        }

        //Xóa 1 bảng
        private void DeleteRegister(object sender, RoutedEventArgs e)
        {

            //Kiểm tra nếu cái TextBox đó rỗng yêu cầu nhập
            if (Type.Text == "")
            {
                System.Windows.MessageBox.Show("Hãy nhập mã đăng ký để xóa", "Thông báo");
            }
            //Kiểm tra mã người dùng có tồn tại hay không
            else if (CheckRegister(Type.Text) == 0)
            {
                System.Windows.MessageBox.Show("Không thấy mã đăng ký công tác cần xóa", "Thông báo");
            }

            //Nếu không nằm các điều kiện trên sẽ xóa được tài khoản
            else
            {
                System.Windows.MessageBox.Show("Đã xóa form đăng ký", "Thông báo");
                deleteAccount();
                LoadCongTac();
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



        int CheckRepair(string Id)
        {
            //tạo câu truy vấn sql
            string sql = "select *from CONGTAC where MaDK = '" + Id + "'";

            DataTable result = ExecuteQuery(sql);
            return result.Rows.Count;
        }
        void Repair()
        {
            //Mở kết nối SQL
            connect.Open();

            //Câu lệnh truy vấn SQL
            string sql = "UPDATE CONGTAC SET  TimeGo =@Timego,TimeEnd =@TimeEnd,NguoiChuanBi =@NguoiChuanBi,Noidi =@Noidi,Noiden = @Noiden,ThanhPhan= @ThanhPhan,SoKm =@SoKm,SoGhe=@SoGhe,NoiDung=@NoiDung,DonViChuTri=@DonViChuTri,GhiChu=@GhiChu  WHERE MaDK = '" + Type.Text + "'";

            //Thực hiện câu lệnh truy vấn
            SqlCommand cmd = new SqlCommand(sql, connect);

            cmd.Parameters.AddWithValue("TimeGo", DateTime.Parse(dataGo.Text));
            cmd.Parameters.AddWithValue("TimeEnd", DateTime.Parse(dataEnd.Text));
            cmd.Parameters.AddWithValue("NguoiChuanBi", NguoiChuanBi.Text);
            cmd.Parameters.AddWithValue("Noidi", NoiDi.Text);
            cmd.Parameters.AddWithValue("Noiden", NoiDen.Text);
            cmd.Parameters.AddWithValue("ThanhPhan", ThanhPhan.Text);
            cmd.Parameters.AddWithValue("SoKm", SoKm.Text);
            cmd.Parameters.AddWithValue("SoGhe", SoGhe.Text);
            cmd.Parameters.AddWithValue("NoiDung", NoiDung.Text);
            cmd.Parameters.AddWithValue("DonViChuTri", DonViChuTri.Text);
            cmd.Parameters.AddWithValue("GhiChu", GhiChu.Text);

            //Xử lý
            cmd.ExecuteNonQuery();

            //Đóng kết nối
            connect.Close();
            LoadCongTac();


        }
        private void btn_Repair(object sender, RoutedEventArgs e)
        {
            //Kiểm tra nếu cái TextBox đó rỗng yêu cầu nhập
            if (Type.Text == "")
            {
                System.Windows.MessageBox.Show("Hãy nhập mã đăng ký", "Thông báo");
            }
            //Kiểm tra mã người dùng có tồn tại hay không
            else if (CheckRepair(Type.Text) == 0)
            {
                System.Windows.MessageBox.Show("Không thấy mã đăng ký công tác cần sửa", "Thông báo");
            }

            //Nếu không nằm các điều kiện trên sẽ xóa được tài khoản
            else
            {
                System.Windows.MessageBox.Show("Đã sửa thành công", "Thông báo");
                Repair();
                LoadCongTac();
            }
            
        }
     }
}
