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
    public partial class BanTaiChoForm : Form
    {
        private int selectedBanId;

        public BanTaiChoForm()
        {
            InitializeComponent();
        }

        // Hàm tải danh sách bàn và menu từ cơ sở dữ liệu
        private void LoadData()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                // Tải danh sách bàn
                string queryBan = "SELECT MaBan, TenBan FROM Ban WHERE TrangThai = 'Trống'";
                SqlDataAdapter daBan = new SqlDataAdapter(queryBan, conn);
                DataTable dtBan = new DataTable();
                daBan.Fill(dtBan);
                cmbBan.DisplayMember = "TenBan";
                cmbBan.ValueMember = "MaBan";
                cmbBan.DataSource = dtBan;

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

        // Hàm khởi tạo các cột trong DataGridView
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
            if (cmbBan.SelectedItem != null && cmbMenu.SelectedItem != null && int.TryParse(txtSoLuong.Text, out int soLuong))
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
            if (cmbBan.SelectedItem != null && dgvHoaDon.Rows.Count > 0)
            {
                int maBan = (int)cmbBan.SelectedValue;
                DateTime ngayTao = DateTime.Now;
                decimal tongTien = dgvHoaDon.Rows.Cast<DataGridViewRow>()
                                     .Where(row => !row.IsNewRow)
                                     .Sum(row => Convert.ToDecimal(row.Cells["ThanhTien"].Value));

                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // Thêm hóa đơn
                    string insertHoaDonQuery = "INSERT INTO HoaDon (MaBan, NgayTao, TongTien) OUTPUT INSERTED.MaHoaDon VALUES (@MaBan, @NgayTao, @TongTien)";
                    SqlCommand cmd = new SqlCommand(insertHoaDonQuery, conn);
                    cmd.Parameters.AddWithValue("@MaBan", maBan);
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

                    // Cập nhật trạng thái bàn
                    UpdateBanStatus(maBan, "Trống");

                    MessageBox.Show("Thanh toán thành công!");

                    // Reset giao diện
                    ResetForm();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn bàn và thêm món ăn vào hóa đơn.");
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

        // Hàm cập nhật trạng thái bàn
        private void UpdateBanStatus(int maBan, string trangThai)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Ban SET TrangThai = @TrangThai WHERE MaBan = @MaBan";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                cmd.Parameters.AddWithValue("@MaBan", maBan);
                cmd.ExecuteNonQuery();
            }
        }

        // Hàm reset form sau khi thanh toán
        private void ResetForm()
        {
            dgvHoaDon.Rows.Clear();
            lblTongTien.Text = "Tổng tiền: 0";
            cmbBan.SelectedIndex = -1;
            cmbMenu.SelectedIndex = -1;
            txtSoLuong.Clear();
        }

        // Sự kiện Load form
        private void BanTaiChoForm_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
            LoadData();
        }

        // Sự kiện thay đổi bàn
        private void cmbBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBan.SelectedItem != null)
            {
                selectedBanId = (int)cmbBan.SelectedValue;
                // Có thể tải dữ liệu liên quan đến bàn được chọn nếu cần
            }
        }
    }
}

