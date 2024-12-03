using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hoanchinh
{
    public partial class ChiTietHoaDonForm : Form
    {
        private int maHoaDon;

        public ChiTietHoaDonForm(int maHoaDon)
        {
            InitializeComponent();
            this.maHoaDon = maHoaDon;
        }
        private void LoadMenuItems()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT TenMon, Gia FROM Menu";
                    SqlCommand command = new SqlCommand(query, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        cmbMenu.Items.Add(reader["TenMon"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách món ăn: " + ex.Message);
                }
            }
        }

        // Hàm thêm món ăn vào chi tiết hóa đơn
        private void btnThemMon_Click(object sender, EventArgs e)
        {
            string tenMon = cmbMenu.SelectedItem.ToString();
            int soLuong = (int)numSoLuong.Value;

            // Lấy giá món ăn từ menu
            float donGia = GetDonGia(tenMon);
            float thanhTien = donGia * soLuong;

            // Thêm vào bảng ChiTietHoaDon
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO ChiTietHoaDon (MaHoaDon, TenMon, SoLuong, DonGia) VALUES (@MaHoaDon, @TenMon, @SoLuong, @DonGia)";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    command.Parameters.AddWithValue("@TenMon", tenMon);
                    command.Parameters.AddWithValue("@SoLuong", soLuong);
                    command.Parameters.AddWithValue("@DonGia", donGia);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Thêm món ăn vào hóa đơn thành công!");
                    LoadChiTietHoaDonData();  // Cập nhật lại danh sách chi tiết hóa đơn
                    UpdateTongTien();  // Cập nhật tổng tiền
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm món ăn: " + ex.Message);
                }
            }
        }

        // Hàm lấy giá của món ăn từ bảng Menu
        private float GetDonGia(string tenMon)
        {
            float donGia = 0;

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Gia FROM Menu WHERE TenMon = @TenMon";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@TenMon", tenMon);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        donGia = Convert.ToSingle(reader["Gia"]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy giá món ăn: " + ex.Message);
                }
            }

            return donGia;
        }

        // Hàm tải chi tiết hóa đơn vào DataGridView
        private void LoadChiTietHoaDonData()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT TenMon, SoLuong, DonGia, (SoLuong * DonGia) AS ThanhTien FROM ChiTietHoaDon WHERE MaHoaDon = @MaHoaDon";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dgvChiTietHoaDon.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải chi tiết hóa đơn: " + ex.Message);
                }
            }
        }

        // Hàm cập nhật tổng tiền của hóa đơn
        private void UpdateTongTien()
        {
            float tongTien = 0;

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT SUM(SoLuong * DonGia) AS TongTien FROM ChiTietHoaDon WHERE MaHoaDon = @MaHoaDon";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        tongTien = Convert.ToSingle(reader["TongTien"]);
                    }

                    // Cập nhật tổng tiền vào TextBox hoặc Label
                    lblTongTien.Text = "Tổng tiền: " + tongTien.ToString("C");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật tổng tiền: " + ex.Message);
                }
            }
        }

        // Sự kiện load form
        private void ChiTietHoaDonForm_Load(object sender, EventArgs e)
        {
            LoadMenuItems();  // Tải danh sách món ăn
            LoadChiTietHoaDonData();  // Tải chi tiết hóa đơn
        }
    }
}
