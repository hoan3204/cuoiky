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
    public partial class QuanLyMenuForm : Form
    {
        public QuanLyMenuForm()
        {
            InitializeComponent();
        }
        private void LoadMenu()
        {
            dataGridViewMenu.Rows.Clear(); // Xóa các dòng hiện tại trong DataGridView

            var query = "SELECT MaMon, TenMon, Gia, DanhMuc FROM Menu"; // Lấy dữ liệu món ăn
            using (var conn = DatabaseHelper.GetConnection()) // Sử dụng phương thức GetConnection()
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Thêm từng dòng vào DataGridView
                            dataGridViewMenu.Rows.Add(
                                reader["MaMon"],
                                reader["TenMon"],
                                reader["Gia"],
                                reader["DanhMuc"]
                            );
                        }
                    }
                }
            }
        }
        private void QuanLyMenuForm_Load(object sender, EventArgs e)
        {
            LoadMenu();
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            var tenMon = txtTenMon.Text;
            var gia = decimal.Parse(txtGia.Text);  // Bạn có thể kiểm tra lại input nếu cần
            var danhMuc = txtDanhMuc.Text;

            var query = "INSERT INTO Menu (TenMon, Gia, DanhMuc) VALUES (@TenMon, @Gia, @DanhMuc)";
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenMon", tenMon);
                    cmd.Parameters.AddWithValue("@Gia", gia);
                    cmd.Parameters.AddWithValue("@DanhMuc", danhMuc);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadMenu(); // Cập nhật lại danh sách món ăn sau khi thêm
        }


        private void btnSuaMon_Click(object sender, EventArgs e)
        {
            if (dataGridViewMenu.SelectedRows.Count > 0)
            {
                var maMon = int.Parse(dataGridViewMenu.SelectedRows[0].Cells["MaMon"].Value.ToString());
                var tenMon = txtTenMon.Text;
                var gia = decimal.Parse(txtGia.Text);
                var danhMuc = txtDanhMuc.Text;

                var query = "UPDATE Menu SET TenMon = @TenMon, Gia = @Gia, DanhMuc = @DanhMuc WHERE MaMon = @MaMon";
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenMon", tenMon);
                        cmd.Parameters.AddWithValue("@Gia", gia);
                        cmd.Parameters.AddWithValue("@DanhMuc", danhMuc);
                        cmd.Parameters.AddWithValue("@MaMon", maMon);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadMenu(); // Cập nhật lại danh sách món ăn sau khi sửa
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món ăn để sửa.");
            }
        }


        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (dataGridViewMenu.SelectedRows.Count > 0)
            {
                var maMon = int.Parse(dataGridViewMenu.SelectedRows[0].Cells["MaMon"].Value.ToString());

                var query = "DELETE FROM Menu WHERE MaMon = @MaMon";
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaMon", maMon);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadMenu(); // Cập nhật lại danh sách món ăn sau khi xóa
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món ăn để xóa.");
            }
        }

    }
}
