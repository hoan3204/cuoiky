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
    public partial class QuanLyBanForm : Form
    {
        public QuanLyBanForm()
        {
            InitializeComponent();
        }
        private void QuanLyBanForm_Load(object sender, EventArgs e)
        {
            LoadDanhSachBan();
        }

        // Hàm này sẽ tải danh sách bàn từ SQL Server vào DataGridView
        private void LoadDanhSachBan()
        {
            dataGridViewBan.Rows.Clear();  // Xóa tất cả các dòng hiện tại trong DataGridView

            var query = "SELECT MaBan, TenBan, TrangThai FROM Ban";
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    // Đọc dữ liệu từ SQL và thêm vào DataGridView
                    while (reader.Read())
                    {
                        var item = new DataGridViewRow();
                        item.Cells.Add(new DataGridViewTextBoxCell() { Value = reader["MaBan"] });
                        item.Cells.Add(new DataGridViewTextBoxCell() { Value = reader["TenBan"] });
                        item.Cells.Add(new DataGridViewTextBoxCell() { Value = reader["TrangThai"] });
                        dataGridViewBan.Rows.Add(item);
                    }
                }
            }
        }

        // Sự kiện khi nhấn nút "Thêm bàn"
        private void btnThemBan_Click(object sender, EventArgs e)
        {
            var tenBan = txtTenBan.Text;  // Giả sử bạn có một TextBox cho tên bàn
            var trangThai = "Trống";      // Mặc định trạng thái bàn là "Trống"

            // Query SQL để thêm bàn mới vào bảng "Ban"
            var query = "INSERT INTO Ban (TenBan, TrangThai) VALUES (@TenBan, @TrangThai)";
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenBan", tenBan);
                    cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadDanhSachBan();  // Cập nhật lại danh sách bàn sau khi thêm
        }

        // Sự kiện khi nhấn nút "Cập nhật trạng thái"
        private void btnCapNhatTrangThai_Click(object sender, EventArgs e)
        {
            if (dataGridViewBan.SelectedRows.Count > 0)
            {
                // Lấy mã bàn được chọn
                var maBan = (int)dataGridViewBan.SelectedRows[0].Cells[0].Value;
                // Kiểm tra trạng thái hiện tại của bàn để chuyển sang trạng thái khác
                var trangThaiMoi = (dataGridViewBan.SelectedRows[0].Cells[2].Value.ToString() == "Trống") ? "Đang sử dụng" : "Trống";

                // Cập nhật trạng thái bàn trong cơ sở dữ liệu
                var query = "UPDATE Ban SET TrangThai = @TrangThai WHERE MaBan = @MaBan";
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TrangThai", trangThaiMoi);
                        cmd.Parameters.AddWithValue("@MaBan", maBan);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadDanhSachBan();  // Cập nhật lại danh sách bàn sau khi cập nhật trạng thái
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một bàn để cập nhật trạng thái.");
            }
        }

        // Sự kiện khi nhấn nút "Xóa bàn"
        private void btnXoaBan_Click(object sender, EventArgs e)
        {
            if (dataGridViewBan.SelectedRows.Count > 0)
            {
                var maBan = (int)dataGridViewBan.SelectedRows[0].Cells[0].Value;

                // Xóa bàn khỏi cơ sở dữ liệu
                var query = "DELETE FROM Ban WHERE MaBan = @MaBan";
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaBan", maBan);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadDanhSachBan();  // Cập nhật lại danh sách bàn sau khi xóa
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một bàn để xóa.");
            }
        }
    }
}
