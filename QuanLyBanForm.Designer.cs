namespace hoanchinh
{
    partial class QuanLyBanForm
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
            this.btnCapNhatTrangThai = new System.Windows.Forms.Button();
            this.btnXoaBan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTenBan = new System.Windows.Forms.TextBox();
            this.btnThemBan = new System.Windows.Forms.Button();
            this.dataGridViewBan = new System.Windows.Forms.DataGridView();
            this.MaBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBan)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCapNhatTrangThai
            // 
            this.btnCapNhatTrangThai.Location = new System.Drawing.Point(201, 57);
            this.btnCapNhatTrangThai.Name = "btnCapNhatTrangThai";
            this.btnCapNhatTrangThai.Size = new System.Drawing.Size(75, 23);
            this.btnCapNhatTrangThai.TabIndex = 19;
            this.btnCapNhatTrangThai.Text = "Sửa";
            this.btnCapNhatTrangThai.UseVisualStyleBackColor = true;
            this.btnCapNhatTrangThai.Click += new System.EventHandler(this.btnCapNhatTrangThai_Click);
            // 
            // btnXoaBan
            // 
            this.btnXoaBan.Location = new System.Drawing.Point(285, 57);
            this.btnXoaBan.Name = "btnXoaBan";
            this.btnXoaBan.Size = new System.Drawing.Size(75, 23);
            this.btnXoaBan.TabIndex = 18;
            this.btnXoaBan.Text = "Xóa";
            this.btnXoaBan.UseVisualStyleBackColor = true;
            this.btnXoaBan.Click += new System.EventHandler(this.btnXoaBan_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "Tên Bàn:";
            // 
            // txtTenBan
            // 
            this.txtTenBan.Location = new System.Drawing.Point(260, 12);
            this.txtTenBan.Name = "txtTenBan";
            this.txtTenBan.Size = new System.Drawing.Size(100, 22);
            this.txtTenBan.TabIndex = 12;
            // 
            // btnThemBan
            // 
            this.btnThemBan.Location = new System.Drawing.Point(120, 57);
            this.btnThemBan.Name = "btnThemBan";
            this.btnThemBan.Size = new System.Drawing.Size(75, 23);
            this.btnThemBan.TabIndex = 11;
            this.btnThemBan.Text = "Thêm";
            this.btnThemBan.UseVisualStyleBackColor = true;
            this.btnThemBan.Click += new System.EventHandler(this.btnThemBan_Click);
            // 
            // dataGridViewBan
            // 
            this.dataGridViewBan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaBan,
            this.TenBan,
            this.TrangThai});
            this.dataGridViewBan.Location = new System.Drawing.Point(120, 169);
            this.dataGridViewBan.Name = "dataGridViewBan";
            this.dataGridViewBan.RowHeadersWidth = 51;
            this.dataGridViewBan.RowTemplate.Height = 24;
            this.dataGridViewBan.Size = new System.Drawing.Size(428, 269);
            this.dataGridViewBan.TabIndex = 10;
            // 
            // MaBan
            // 
            this.MaBan.HeaderText = "Mã bàn";
            this.MaBan.MinimumWidth = 6;
            this.MaBan.Name = "MaBan";
            this.MaBan.Width = 125;
            // 
            // TenBan
            // 
            this.TenBan.HeaderText = "Tên ";
            this.TenBan.MinimumWidth = 6;
            this.TenBan.Name = "TenBan";
            this.TenBan.Width = 125;
            // 
            // TrangThai
            // 
            this.TrangThai.HeaderText = "Trạng thái";
            this.TrangThai.MinimumWidth = 6;
            this.TrangThai.Name = "TrangThai";
            this.TrangThai.Width = 125;
            // 
            // QuanLyBanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCapNhatTrangThai);
            this.Controls.Add(this.btnXoaBan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTenBan);
            this.Controls.Add(this.btnThemBan);
            this.Controls.Add(this.dataGridViewBan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "QuanLyBanForm";
            this.Text = "QuanLyBanForm";
            this.Load += new System.EventHandler(this.QuanLyBanForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCapNhatTrangThai;
        private System.Windows.Forms.Button btnXoaBan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTenBan;
        private System.Windows.Forms.Button btnThemBan;
        private System.Windows.Forms.DataGridView dataGridViewBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrangThai;
    }
}