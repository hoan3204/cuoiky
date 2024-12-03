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
    public partial class QuanLyKhachHangForm : Form
    {
        public QuanLyKhachHangForm()
        {
            InitializeComponent();
        }
        private void LoadKhachHangData()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM KhachHang";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvKhachHang.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu khách hàng: " + ex.Message);
                }
            }
        }
        private void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            string tenKhachHang = txtTenKhachHang.Text;
            string soDienThoai = txtSoDienThoai.Text;
            string diaChi = txtDiaChi.Text;
            string email = txtEmail.Text;

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO KhachHang (TenKhachHang, SoDienThoai, DiaChi, Email) VALUES (@TenKhachHang, @SoDienThoai, @DiaChi, @Email)";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@TenKhachHang", tenKhachHang);
                    command.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                    command.Parameters.AddWithValue("@DiaChi", diaChi);
                    command.Parameters.AddWithValue("@Email", email);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Thêm khách hàng thành công!");
                    LoadKhachHangData();  // Cập nhật lại danh sách khách hàng
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message);
                }
            }
        }
        private void btnSuaKhachHang_Click(object sender, EventArgs e)
        {
            int maKhachHang = (int)dgvKhachHang.SelectedRows[0].Cells["MaKhachHang"].Value;
            string tenKhachHang = txtTenKhachHang.Text;
            string soDienThoai = txtSoDienThoai.Text;
            string diaChi = txtDiaChi.Text;
            string email = txtEmail.Text;

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE KhachHang SET TenKhachHang = @TenKhachHang, SoDienThoai = @SoDienThoai, DiaChi = @DiaChi, Email = @Email WHERE MaKhachHang = @MaKhachHang";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                    command.Parameters.AddWithValue("@TenKhachHang", tenKhachHang);
                    command.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                    command.Parameters.AddWithValue("@DiaChi", diaChi);
                    command.Parameters.AddWithValue("@Email", email);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật khách hàng thành công!");
                    LoadKhachHangData();  // Cập nhật lại danh sách khách hàng
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa khách hàng: " + ex.Message);
                }
            }
        }
        private void btnXoaKhachHang_Click(object sender, EventArgs e)
        {
            int maKhachHang = (int)dgvKhachHang.SelectedRows[0].Cells["MaKhachHang"].Value;

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM KhachHang WHERE MaKhachHang = @MaKhachHang";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Xóa khách hàng thành công!");
                    LoadKhachHangData();  // Cập nhật lại danh sách khách hàng
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa khách hàng: " + ex.Message);
                }
            }
        }
        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Điền thông tin khách hàng vào các TextBox
                txtTenKhachHang.Text = dgvKhachHang.Rows[e.RowIndex].Cells["TenKhachHang"].Value.ToString();
                txtSoDienThoai.Text = dgvKhachHang.Rows[e.RowIndex].Cells["SoDienThoai"].Value.ToString();
                txtDiaChi.Text = dgvKhachHang.Rows[e.RowIndex].Cells["DiaChi"].Value.ToString();
                txtEmail.Text = dgvKhachHang.Rows[e.RowIndex].Cells["Email"].Value.ToString();
            }
        }

    }
}
