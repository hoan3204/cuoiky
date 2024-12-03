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
    public partial class ChonMonForm : Form
    {
        private int maBan;
        public ChonMonForm(int maBan)
        {
            InitializeComponent();
            this.maBan = maBan;
        }
        private void ChonMonForm_Load(object sender, EventArgs e)
        {
            LoadLoaiMon(); // Tải các loại món ăn vào ComboBox
            LoadMenuItems(); // Tải danh sách món ăn từ cơ sở dữ liệu
        }

        // Hàm tải các loại món ăn vào ComboBox
        private void LoadLoaiMon()
        {
            // Lấy danh sách loại món ăn từ cơ sở dữ liệu
            var query = "SELECT DISTINCT LoaiMon FROM MonAn"; // Giả sử bạn có cột 'LoaiMon' trong bảng MonAn
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    comboBoxLoaiMon.Items.Clear(); // Xóa dữ liệu cũ trong ComboBox
                    while (reader.Read())
                    {
                        comboBoxLoaiMon.Items.Add(reader["LoaiMon"].ToString());
                    }
                }
            }
        }

        // Hàm tải danh sách món ăn dựa vào loại món đã chọn
        private void LoadMenuItems()
        {
            // Lấy loại món đã chọn từ ComboBox
            var loaiMon = comboBoxLoaiMon.SelectedItem?.ToString();
            var query = "SELECT MaMon, TenMon, DonGia FROM MonAn";

            // Nếu có loại món, thêm điều kiện WHERE để lọc
            if (!string.IsNullOrEmpty(loaiMon))
            {
                query += " WHERE LoaiMon = @LoaiMon";
            }

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(loaiMon))
                    {
                        cmd.Parameters.AddWithValue("@LoaiMon", loaiMon);
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        dataGridViewMenu.Rows.Clear();
                        while (reader.Read())
                        {
                            dataGridViewMenu.Rows.Add(reader["MaMon"], reader["TenMon"], reader["DonGia"]);
                        }
                    }
                }
            }
        }

        // Sự kiện khi thay đổi lựa chọn trong ComboBox
        private void comboBoxLoaiMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMenuItems(); // Lọc lại món ăn theo loại đã chọn
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng có chọn món từ DataGridView hay không
            foreach (DataGridViewRow row in dataGridViewMenu.SelectedRows)
            {
                var maMon = row.Cells["MaMon"].Value;
                var tenMon = row.Cells["TenMon"].Value.ToString();
                var donGia = row.Cells["DonGia"].Value;
                var soLuong = int.Parse(txtSoLuong.Text);  // Lấy số lượng từ TextBox hoặc đặt mặc định là 1

                // Thêm món vào chi tiết hóa đơn
                var query = "INSERT INTO ChiTietHoaDon (MaHoaDon, MaMon, SoLuong, DonGia) VALUES (@MaHoaDon, @MaMon, @SoLuong, @DonGia)";
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHoaDon", maBan);  // Lưu MaHoaDon vào bảng
                        cmd.Parameters.AddWithValue("@MaMon", maMon);
                        cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                        cmd.Parameters.AddWithValue("@DonGia", donGia);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            MessageBox.Show("Món đã được thêm vào hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();  // Đóng form sau khi chọn món
        }

    }
}
