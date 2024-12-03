using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hoanchinh
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void LoadForm(Form form)
        {
            // Xóa các Form cũ trong Panel
            panelContent.Controls.Clear();

            // Đặt Form mới vào Panel
            form.TopLevel = false; // Form này là Form con
            form.Dock = DockStyle.Fill; // Tự động căn chỉnh kích thước
            panelContent.Controls.Add(form); // Thêm vào Panel
            form.Show(); // Hiển thị Form
        }

        private void menuItemBanTaiCho_Click(object sender, EventArgs e)
        {
            LoadForm(new BanTaiChoForm());
        }



        private void menuItemBanMangVe_Click(object sender, EventArgs e)
        {
            LoadForm(new BanMangVeForm());
        }


        private void menuItemQuanLyBan_Click(object sender, EventArgs e)
        {
            LoadForm(new QuanLyBanForm());
        }



        private void menuItemThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void quảnLýMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new QuanLyMenuForm());
        }

        private void bánHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void quảnLýHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new QuanLyHoaDonForm());
        }

        private void quảnLýNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new QuanLyNhanVienForm());
        }

        private void quảnLýKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new QuanLyKhachHangForm());
        }
    }
}
