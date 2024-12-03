namespace hoanchinh
{
    partial class QuanLyHoaDonForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnXoaHoaDon = new System.Windows.Forms.Button();
            this.btnThemHoaDon = new System.Windows.Forms.Button();
            this.dgvHoaDon = new System.Windows.Forms.DataGridView();
            this.lblMaBan = new System.Windows.Forms.Label();
            this.lblNgayTao = new System.Windows.Forms.Label();
            this.lblMaHoaDon = new System.Windows.Forms.Label();
            this.txtMaBan = new System.Windows.Forms.TextBox();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.dtpNgayTao = new System.Windows.Forms.DateTimePicker();
            this.btnXemChiTietHoaDon = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnXoaHoaDon
            // 
            this.btnXoaHoaDon.Location = new System.Drawing.Point(345, 167);
            this.btnXoaHoaDon.Name = "btnXoaHoaDon";
            this.btnXoaHoaDon.Size = new System.Drawing.Size(75, 23);
            this.btnXoaHoaDon.TabIndex = 13;
            this.btnXoaHoaDon.Text = "Xóa";
            this.btnXoaHoaDon.UseVisualStyleBackColor = true;
            this.btnXoaHoaDon.Click += new System.EventHandler(this.btnXoaHoaDon_Click);
            // 
            // btnThemHoaDon
            // 
            this.btnThemHoaDon.Location = new System.Drawing.Point(104, 167);
            this.btnThemHoaDon.Name = "btnThemHoaDon";
            this.btnThemHoaDon.Size = new System.Drawing.Size(87, 23);
            this.btnThemHoaDon.TabIndex = 12;
            this.btnThemHoaDon.Text = "Thêm";
            this.btnThemHoaDon.UseVisualStyleBackColor = true;
            this.btnThemHoaDon.Click += new System.EventHandler(this.btnThemHoaDon_Click);
            // 
            // dgvHoaDon
            // 
            this.dgvHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoaDon.Location = new System.Drawing.Point(104, 249);
            this.dgvHoaDon.Name = "dgvHoaDon";
            this.dgvHoaDon.RowHeadersWidth = 51;
            this.dgvHoaDon.RowTemplate.Height = 24;
            this.dgvHoaDon.Size = new System.Drawing.Size(596, 150);
            this.dgvHoaDon.TabIndex = 11;
            // 
            // lblMaBan
            // 
            this.lblMaBan.AutoSize = true;
            this.lblMaBan.Location = new System.Drawing.Point(101, 105);
            this.lblMaBan.Name = "lblMaBan";
            this.lblMaBan.Size = new System.Drawing.Size(66, 16);
            this.lblMaBan.TabIndex = 10;
            this.lblMaBan.Text = "Tổng tiền:";
            // 
            // lblNgayTao
            // 
            this.lblNgayTao.AutoSize = true;
            this.lblNgayTao.Location = new System.Drawing.Point(315, 52);
            this.lblNgayTao.Name = "lblNgayTao";
            this.lblNgayTao.Size = new System.Drawing.Size(65, 16);
            this.lblNgayTao.TabIndex = 9;
            this.lblNgayTao.Text = "Ngày tạo:";
            // 
            // lblMaHoaDon
            // 
            this.lblMaHoaDon.AutoSize = true;
            this.lblMaHoaDon.Location = new System.Drawing.Point(101, 52);
            this.lblMaHoaDon.Name = "lblMaHoaDon";
            this.lblMaHoaDon.Size = new System.Drawing.Size(55, 16);
            this.lblMaHoaDon.TabIndex = 7;
            this.lblMaHoaDon.Text = "Mã bàn:";
            // 
            // txtMaBan
            // 
            this.txtMaBan.Location = new System.Drawing.Point(189, 52);
            this.txtMaBan.Name = "txtMaBan";
            this.txtMaBan.Size = new System.Drawing.Size(100, 22);
            this.txtMaBan.TabIndex = 14;
            // 
            // txtTongTien
            // 
            this.txtTongTien.Location = new System.Drawing.Point(189, 105);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.Size = new System.Drawing.Size(100, 22);
            this.txtTongTien.TabIndex = 15;
            // 
            // dtpNgayTao
            // 
            this.dtpNgayTao.Location = new System.Drawing.Point(398, 52);
            this.dtpNgayTao.Name = "dtpNgayTao";
            this.dtpNgayTao.Size = new System.Drawing.Size(200, 22);
            this.dtpNgayTao.TabIndex = 18;
            // 
            // btnXemChiTietHoaDon
            // 
            this.btnXemChiTietHoaDon.Location = new System.Drawing.Point(226, 167);
            this.btnXemChiTietHoaDon.Name = "btnXemChiTietHoaDon";
            this.btnXemChiTietHoaDon.Size = new System.Drawing.Size(87, 23);
            this.btnXemChiTietHoaDon.TabIndex = 19;
            this.btnXemChiTietHoaDon.Text = "Chi tiết";
            this.btnXemChiTietHoaDon.UseVisualStyleBackColor = true;
            this.btnXemChiTietHoaDon.Click += new System.EventHandler(this.btnXemChiTietHoaDon_Click);
            // 
            // QuanLyHoaDonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnXemChiTietHoaDon);
            this.Controls.Add(this.dtpNgayTao);
            this.Controls.Add(this.txtTongTien);
            this.Controls.Add(this.txtMaBan);
            this.Controls.Add(this.btnXoaHoaDon);
            this.Controls.Add(this.btnThemHoaDon);
            this.Controls.Add(this.dgvHoaDon);
            this.Controls.Add(this.lblMaBan);
            this.Controls.Add(this.lblNgayTao);
            this.Controls.Add(this.lblMaHoaDon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "QuanLyHoaDonForm";
            this.Text = "QuanLyHoaDonForm";
            this.Load += new System.EventHandler(this.QuanLyHoaDonForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnXoaHoaDon;
        private System.Windows.Forms.Button btnThemHoaDon;
        private System.Windows.Forms.DataGridView dgvHoaDon;
        private System.Windows.Forms.Label lblMaBan;
        private System.Windows.Forms.Label lblNgayTao;
        private System.Windows.Forms.Label lblMaHoaDon;
        private System.Windows.Forms.TextBox txtMaBan;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.DateTimePicker dtpNgayTao;
        private System.Windows.Forms.Button btnXemChiTietHoaDon;
    }
}