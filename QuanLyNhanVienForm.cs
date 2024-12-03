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
    public partial class QuanLyNhanVienForm : Form
    {
        public QuanLyNhanVienForm()
        {
            InitializeComponent();
        }
        private void LoadNhanVienData()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MaNguoiDung, TenDangNhap, VaiTro FROM NguoiDung";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dgvNhanVien.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách nhân viên: " + ex.Message);
                }
            }
        }
        private void QuanLyNhanVienForm_Load(object sender, EventArgs e)
        {
            // Thêm vai trò vào ComboBox (VaiTro)
            cmbVaiTro.Items.Clear();
            cmbVaiTro.Items.Add("Admin");
            cmbVaiTro.Items.Add("Staff");

            // Chọn giá trị mặc định
            cmbVaiTro.SelectedIndex = 0; // Chọn "Admin" mặc định

            // Tải danh sách nhân viên
            LoadNhanVienData();
        }

        private void cmbVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btnThemNhanVien_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text;
            string matKhau = txtMatKhau.Text;
            string vaiTro = cmbVaiTro.SelectedItem.ToString(); // Lấy vai trò từ ComboBox

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO NguoiDung (TenDangNhap, MatKhau, VaiTro) VALUES (@TenDangNhap, @MatKhau, @VaiTro)";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    command.Parameters.AddWithValue("@MatKhau", matKhau);
                    command.Parameters.AddWithValue("@VaiTro", vaiTro);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Thêm nhân viên thành công!");
                    LoadNhanVienData();  // Cập nhật lại danh sách nhân viên
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message);
                }
            }
        }
        private void btnSuaNhanVien_Click(object sender, EventArgs e)
        {
            int maNguoiDung = (int)dgvNhanVien.SelectedRows[0].Cells["MaNguoiDung"].Value;
            string tenDangNhap = txtTenDangNhap.Text;
            string matKhau = txtMatKhau.Text;
            string vaiTro = cmbVaiTro.SelectedItem.ToString(); // Lấy vai trò từ ComboBox

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE NguoiDung SET TenDangNhap = @TenDangNhap, MatKhau = @MatKhau, VaiTro = @VaiTro WHERE MaNguoiDung = @MaNguoiDung";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                    command.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    command.Parameters.AddWithValue("@MatKhau", matKhau);
                    command.Parameters.AddWithValue("@VaiTro", vaiTro);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật nhân viên thành công!");
                    LoadNhanVienData();  // Cập nhật lại danh sách nhân viên
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa thông tin nhân viên: " + ex.Message);
                }
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Điền thông tin vào các TextBox và ComboBox
                txtTenDangNhap.Text = dgvNhanVien.Rows[e.RowIndex].Cells["TenDangNhap"].Value.ToString();
                txtMatKhau.Text = dgvNhanVien.Rows[e.RowIndex].Cells["MatKhau"].Value.ToString();
                cmbVaiTro.SelectedItem = dgvNhanVien.Rows[e.RowIndex].Cells["VaiTro"].Value.ToString();
            }
        }
        private void btnXoaNhanVien_Click(object sender, EventArgs e)
        {
            int maNguoiDung = (int)dgvNhanVien.SelectedRows[0].Cells["MaNguoiDung"].Value;

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM NguoiDung WHERE MaNguoiDung = @MaNguoiDung";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Xóa nhân viên thành công!");
                    LoadNhanVienData();  // Cập nhật lại danh sách nhân viên
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message);
                }
            }
        }
    }
}
