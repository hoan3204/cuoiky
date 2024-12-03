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
    public partial class BanMangVeForm : Form
    {
        public BanMangVeForm()
        {
            InitializeComponent();
        }

        // Hàm tải danh sách menu từ cơ sở dữ liệu
        private void LoadData()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                // Tải danh sách món ăn
                string queryMenu = "SELECT MaMon, TenMon, Gia FROM Menu";
                SqlDataAdapter daMenu = new SqlDataAdapter(queryMenu, conn);
                DataTable dtMenu = new DataTable();
                daMenu.Fill(dtMenu);
                cmbMenu.DisplayMember = "TenMon";
                cmbMenu.ValueMember = "Gia"; // Binding đơn giá
                cmbMenu.DataSource = dtMenu;
            }
        }

        // Hàm khởi tạo DataGridView
        private void InitializeDataGridView()
        {
            dgvHoaDon.Columns.Clear();
            dgvHoaDon.Columns.Add("TenMon", "Tên Món");
            dgvHoaDon.Columns.Add("SoLuong", "Số Lượng");
            dgvHoaDon.Columns.Add("DonGia", "Đơn Giá");
            dgvHoaDon.Columns.Add("ThanhTien", "Thành Tiền");

            // Định dạng cột Thành Tiền
            dgvHoaDon.Columns["ThanhTien"].DefaultCellStyle.Format = "C2";
        }

        // Sự kiện thêm món ăn vào DataGridView
        private void btnThemMon_Click(object sender, EventArgs e)
        {
            if (cmbMenu.SelectedItem != null && int.TryParse(txtSoLuong.Text, out int soLuong))
            {
                string tenMon = cmbMenu.Text;
                decimal donGia = (decimal)cmbMenu.SelectedValue; // Lấy giá trị đơn giá
                decimal thanhTien = soLuong * donGia;

                dgvHoaDon.Rows.Add(tenMon, soLuong, donGia, thanhTien);

                // Cập nhật tổng tiền
                UpdateTotalAmount();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món ăn và nhập số lượng hợp lệ.");
            }
        }

        // Sự kiện thanh toán
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.Rows.Count > 0)
            {
                DateTime ngayTao = DateTime.Now;
                decimal tongTien = dgvHoaDon.Rows.Cast<DataGridViewRow>()
                                     .Where(row => !row.IsNewRow)
                                     .Sum(row => Convert.ToDecimal(row.Cells["ThanhTien"].Value));

                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // Thêm hóa đơn
                    string insertHoaDonQuery = "INSERT INTO HoaDon (MaBan, NgayTao, TongTien, LoaiHoaDon) OUTPUT INSERTED.MaHoaDon VALUES (NULL, @NgayTao, @TongTien, 'MangVề')";
                    SqlCommand cmd = new SqlCommand(insertHoaDonQuery, conn);
                    cmd.Parameters.AddWithValue("@NgayTao", ngayTao);
                    cmd.Parameters.AddWithValue("@TongTien", tongTien);
                    int maHoaDon = (int)cmd.ExecuteScalar();

                    // Thêm chi tiết hóa đơn
                    foreach (DataGridViewRow row in dgvHoaDon.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string tenMon = row.Cells["TenMon"].Value.ToString();
                        int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                        decimal donGia = Convert.ToDecimal(row.Cells["DonGia"].Value);

                        string insertChiTietQuery = "INSERT INTO ChiTietHoaDon (MaHoaDon, TenMon, SoLuong, DonGia) VALUES (@MaHoaDon, @TenMon, @SoLuong, @DonGia)";
                        SqlCommand cmdChiTiet = new SqlCommand(insertChiTietQuery, conn);
                        cmdChiTiet.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                        cmdChiTiet.Parameters.AddWithValue("@TenMon", tenMon);
                        cmdChiTiet.Parameters.AddWithValue("@SoLuong", soLuong);
                        cmdChiTiet.Parameters.AddWithValue("@DonGia", donGia);
                        cmdChiTiet.ExecuteNonQuery();
                    }

                    MessageBox.Show("Thanh toán thành công!");

                    // Reset giao diện
                    ResetForm();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng thêm món ăn vào hóa đơn.");
            }
        }

        // Cập nhật tổng tiền
        private void UpdateTotalAmount()
        {
            decimal total = dgvHoaDon.Rows.Cast<DataGridViewRow>()
                             .Where(row => !row.IsNewRow)
                             .Sum(row => Convert.ToDecimal(row.Cells["ThanhTien"].Value));

            lblTongTien.Text = "Tổng tiền: " + total.ToString("C");
        }

        // Hàm reset form sau khi thanh toán
        private void ResetForm()
        {
            dgvHoaDon.Rows.Clear();
            lblTongTien.Text = "Tổng tiền: 0";
            cmbMenu.SelectedIndex = -1;
            txtSoLuong.Clear();
        }

        // Sự kiện Load form
        private void BanMangVeForm_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
            LoadData();
        }
    }
}
