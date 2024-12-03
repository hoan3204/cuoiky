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
    public partial class QuanLyHoaDonForm : Form
    {
        public QuanLyHoaDonForm()
        {
            InitializeComponent();
        }
        private void LoadHoaDonData()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM HoaDon";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dgvHoaDon.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }

        // Hàm thêm hóa đơn mới
        private void btnThemHoaDon_Click(object sender, EventArgs e)
        {
            int maBan = Convert.ToInt32(txtMaBan.Text);  // Mã bàn
            DateTime ngayTao = dtpNgayTao.Value;  // Ngày tạo
            float tongTien = Convert.ToSingle(txtTongTien.Text);  // Tổng tiền

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO HoaDon (MaBan, NgayTao, TongTien) VALUES (@MaBan, @NgayTao, @TongTien)";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@MaBan", maBan);
                    command.Parameters.AddWithValue("@NgayTao", ngayTao);
                    command.Parameters.AddWithValue("@TongTien", tongTien);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Thêm hóa đơn thành công!");
                    LoadHoaDonData();  // Tải lại dữ liệu
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm hóa đơn: " + ex.Message);
                }
            }
        }

        // Hàm xóa hóa đơn
        private void btnXoaHoaDon_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                int maHoaDon = Convert.ToInt32(dgvHoaDon.SelectedRows[0].Cells["MaHoaDon"].Value);

                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM HoaDon WHERE MaHoaDon = @MaHoaDon";
                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                        command.ExecuteNonQuery();

                        MessageBox.Show("Xóa hóa đơn thành công!");
                        LoadHoaDonData();  // Tải lại dữ liệu
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa hóa đơn: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để xóa.");
            }
        }

        // Hàm xem chi tiết hóa đơn
        private void btnXemChiTietHoaDon_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                int maHoaDon = Convert.ToInt32(dgvHoaDon.SelectedRows[0].Cells["MaHoaDon"].Value);
                ChiTietHoaDonForm chiTietForm = new ChiTietHoaDonForm(maHoaDon);
                chiTietForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để xem chi tiết.");
            }
        }

        // Sự kiện load form
        private void QuanLyHoaDonForm_Load(object sender, EventArgs e)
        {
            LoadHoaDonData();  // Tải dữ liệu khi form được load
        }
    }
}
