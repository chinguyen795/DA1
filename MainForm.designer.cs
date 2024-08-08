namespace Ui_DuAn
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.slideTimer = new System.Windows.Forms.Timer(this.components);
            this.tmNhanVien = new System.Windows.Forms.Timer(this.components);
            this.pnSlideContainer = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.pnContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.btnConViec = new Guna.UI2.WinForms.Guna2Button();
            this.btnThongTin = new Guna.UI2.WinForms.Guna2Button();
            this.btnNhanVien = new Guna.UI2.WinForms.Guna2Button();
            this.btnTaiKhoan = new Guna.UI2.WinForms.Guna2Button();
            this.btnMenu = new Guna.UI2.WinForms.Guna2Button();
            this.lbTenDangNhap = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2PictureBox3 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btnDangXuat = new Guna.UI2.WinForms.Guna2Button();
            this.btnThucDon = new Guna.UI2.WinForms.Guna2Button();
            this.btnMayTinh = new Guna.UI2.WinForms.Guna2Button();
            this.btnKhachHang = new Guna.UI2.WinForms.Guna2Button();
            this.btnHoaDon = new Guna.UI2.WinForms.Guna2Button();
            this.btnThongKe = new Guna.UI2.WinForms.Guna2Button();
            this.pnMain = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.pnSlideContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.pnContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // slideTimer
            // 
            this.slideTimer.Interval = 10;
            this.slideTimer.Tick += new System.EventHandler(this.slideTimer_Tick);
            // 
            // tmNhanVien
            // 
            this.tmNhanVien.Interval = 10;
            this.tmNhanVien.Tick += new System.EventHandler(this.tmNhanVien_Tick);
            // 
            // pnSlideContainer
            // 
            this.pnSlideContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnSlideContainer.Controls.Add(this.guna2PictureBox1);
            this.pnSlideContainer.Controls.Add(this.pnContainer);
            this.pnSlideContainer.Controls.Add(this.btnTaiKhoan);
            this.pnSlideContainer.Controls.Add(this.btnMenu);
            this.pnSlideContainer.Controls.Add(this.lbTenDangNhap);
            this.pnSlideContainer.Controls.Add(this.label1);
            this.pnSlideContainer.Controls.Add(this.guna2PictureBox3);
            this.pnSlideContainer.Controls.Add(this.btnDangXuat);
            this.pnSlideContainer.Controls.Add(this.btnThucDon);
            this.pnSlideContainer.Controls.Add(this.btnMayTinh);
            this.pnSlideContainer.Controls.Add(this.btnKhachHang);
            this.pnSlideContainer.Controls.Add(this.btnHoaDon);
            this.pnSlideContainer.Controls.Add(this.btnThongKe);
            this.pnSlideContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnSlideContainer.FillColor = System.Drawing.Color.RoyalBlue;
            this.pnSlideContainer.FillColor2 = System.Drawing.Color.CornflowerBlue;
            this.pnSlideContainer.FillColor3 = System.Drawing.Color.SlateGray;
            this.pnSlideContainer.FillColor4 = System.Drawing.Color.DarkTurquoise;
            this.pnSlideContainer.Location = new System.Drawing.Point(0, 0);
            this.pnSlideContainer.MaximumSize = new System.Drawing.Size(205, 758);
            this.pnSlideContainer.MinimumSize = new System.Drawing.Size(66, 758);
            this.pnSlideContainer.Name = "pnSlideContainer";
            this.pnSlideContainer.Size = new System.Drawing.Size(205, 758);
            this.pnSlideContainer.TabIndex = 3;
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox1.Image")));
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(61, 5);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(138, 97);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 28;
            this.guna2PictureBox1.TabStop = false;
            // 
            // pnContainer
            // 
            this.pnContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnContainer.Controls.Add(this.btnConViec);
            this.pnContainer.Controls.Add(this.btnThongTin);
            this.pnContainer.Controls.Add(this.btnNhanVien);
            this.pnContainer.Location = new System.Drawing.Point(0, 362);
            this.pnContainer.MaximumSize = new System.Drawing.Size(205, 114);
            this.pnContainer.MinimumSize = new System.Drawing.Size(205, 40);
            this.pnContainer.Name = "pnContainer";
            this.pnContainer.Size = new System.Drawing.Size(205, 40);
            this.pnContainer.TabIndex = 27;
            // 
            // btnConViec
            // 
            this.btnConViec.BackColor = System.Drawing.Color.Transparent;
            this.btnConViec.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(166)))), ((int)(((byte)(231)))));
            this.btnConViec.BorderRadius = 10;
            this.btnConViec.CustomBorderThickness = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnConViec.CustomizableEdges.TopLeft = false;
            this.btnConViec.CustomizableEdges.TopRight = false;
            this.btnConViec.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnConViec.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnConViec.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnConViec.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnConViec.FillColor = System.Drawing.Color.Transparent;
            this.btnConViec.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConViec.ForeColor = System.Drawing.Color.White;
            this.btnConViec.HoverState.BorderColor = System.Drawing.Color.Navy;
            this.btnConViec.HoverState.CustomBorderColor = System.Drawing.Color.Blue;
            this.btnConViec.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnConViec.HoverState.ForeColor = System.Drawing.Color.Black;
            this.btnConViec.Image = ((System.Drawing.Image)(resources.GetObject("btnConViec.Image")));
            this.btnConViec.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnConViec.ImageOffset = new System.Drawing.Point(8, 0);
            this.btnConViec.Location = new System.Drawing.Point(0, 75);
            this.btnConViec.Name = "btnConViec";
            this.btnConViec.Size = new System.Drawing.Size(205, 37);
            this.btnConViec.TabIndex = 14;
            this.btnConViec.Text = "Công việc";
            this.btnConViec.TextOffset = new System.Drawing.Point(8, 0);
            this.btnConViec.Click += new System.EventHandler(this.btnConViec_Click);
            // 
            // btnThongTin
            // 
            this.btnThongTin.BackColor = System.Drawing.Color.Transparent;
            this.btnThongTin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(166)))), ((int)(((byte)(231)))));
            this.btnThongTin.CustomBorderThickness = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnThongTin.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThongTin.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThongTin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThongTin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThongTin.FillColor = System.Drawing.Color.Transparent;
            this.btnThongTin.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongTin.ForeColor = System.Drawing.Color.White;
            this.btnThongTin.HoverState.BorderColor = System.Drawing.Color.Navy;
            this.btnThongTin.HoverState.CustomBorderColor = System.Drawing.Color.Blue;
            this.btnThongTin.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnThongTin.HoverState.ForeColor = System.Drawing.Color.Black;
            this.btnThongTin.Image = ((System.Drawing.Image)(resources.GetObject("btnThongTin.Image")));
            this.btnThongTin.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnThongTin.ImageOffset = new System.Drawing.Point(8, 0);
            this.btnThongTin.Location = new System.Drawing.Point(0, 39);
            this.btnThongTin.Name = "btnThongTin";
            this.btnThongTin.Size = new System.Drawing.Size(205, 39);
            this.btnThongTin.TabIndex = 13;
            this.btnThongTin.Text = "Thông Tin";
            this.btnThongTin.TextOffset = new System.Drawing.Point(8, 0);
            this.btnThongTin.Click += new System.EventHandler(this.btnThongTin_Click);
            // 
            // btnNhanVien
            // 
            this.btnNhanVien.BackColor = System.Drawing.Color.Transparent;
            this.btnNhanVien.CustomBorderThickness = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnNhanVien.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNhanVien.DisabledState.FillColor = System.Drawing.Color.Gainsboro;
            this.btnNhanVien.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNhanVien.FillColor = System.Drawing.Color.Transparent;
            this.btnNhanVien.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNhanVien.ForeColor = System.Drawing.Color.White;
            this.btnNhanVien.HoverState.BorderColor = System.Drawing.Color.Navy;
            this.btnNhanVien.HoverState.CustomBorderColor = System.Drawing.Color.Blue;
            this.btnNhanVien.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnNhanVien.HoverState.ForeColor = System.Drawing.Color.Black;
            this.btnNhanVien.Image = ((System.Drawing.Image)(resources.GetObject("btnNhanVien.Image")));
            this.btnNhanVien.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnNhanVien.ImageOffset = new System.Drawing.Point(8, 0);
            this.btnNhanVien.Location = new System.Drawing.Point(0, 0);
            this.btnNhanVien.Name = "btnNhanVien";
            this.btnNhanVien.Size = new System.Drawing.Size(205, 40);
            this.btnNhanVien.TabIndex = 12;
            this.btnNhanVien.Text = "Nhân viên";
            this.btnNhanVien.TextOffset = new System.Drawing.Point(8, 0);
            this.btnNhanVien.Click += new System.EventHandler(this.btnNhanVien_Click);
            // 
            // btnTaiKhoan
            // 
            this.btnTaiKhoan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTaiKhoan.BackColor = System.Drawing.Color.Transparent;
            this.btnTaiKhoan.CustomBorderThickness = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnTaiKhoan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTaiKhoan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnTaiKhoan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTaiKhoan.FillColor = System.Drawing.Color.Transparent;
            this.btnTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTaiKhoan.ForeColor = System.Drawing.Color.White;
            this.btnTaiKhoan.HoverState.BorderColor = System.Drawing.Color.Navy;
            this.btnTaiKhoan.HoverState.CustomBorderColor = System.Drawing.Color.Blue;
            this.btnTaiKhoan.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(166)))), ((int)(((byte)(231)))));
            this.btnTaiKhoan.HoverState.ForeColor = System.Drawing.Color.Black;
            this.btnTaiKhoan.Image = ((System.Drawing.Image)(resources.GetObject("btnTaiKhoan.Image")));
            this.btnTaiKhoan.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnTaiKhoan.ImageOffset = new System.Drawing.Point(8, 0);
            this.btnTaiKhoan.Location = new System.Drawing.Point(0, 620);
            this.btnTaiKhoan.Name = "btnTaiKhoan";
            this.btnTaiKhoan.Size = new System.Drawing.Size(205, 40);
            this.btnTaiKhoan.TabIndex = 26;
            this.btnTaiKhoan.Text = "Tài khoản";
            this.btnTaiKhoan.Click += new System.EventHandler(this.btnTaiKhoan_Click);
            // 
            // btnMenu
            // 
            this.btnMenu.BackColor = System.Drawing.Color.Transparent;
            this.btnMenu.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.btnMenu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMenu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMenu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMenu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMenu.FillColor = System.Drawing.Color.Transparent;
            this.btnMenu.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenu.ForeColor = System.Drawing.Color.Black;
            this.btnMenu.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(166)))), ((int)(((byte)(231)))));
            this.btnMenu.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnMenu.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnMenu.Image = ((System.Drawing.Image)(resources.GetObject("btnMenu.Image")));
            this.btnMenu.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnMenu.ImageOffset = new System.Drawing.Point(11, 0);
            this.btnMenu.Location = new System.Drawing.Point(0, 5);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(66, 74);
            this.btnMenu.TabIndex = 25;
            this.btnMenu.TextOffset = new System.Drawing.Point(8, 0);
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // lbTenDangNhap
            // 
            this.lbTenDangNhap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTenDangNhap.AutoSize = true;
            this.lbTenDangNhap.BackColor = System.Drawing.Color.Transparent;
            this.lbTenDangNhap.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTenDangNhap.Location = new System.Drawing.Point(121, 731);
            this.lbTenDangNhap.Name = "lbTenDangNhap";
            this.lbTenDangNhap.Size = new System.Drawing.Size(22, 14);
            this.lbTenDangNhap.TabIndex = 24;
            this.lbTenDangNhap.Text = "Go";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(61, 731);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 14);
            this.label1.TabIndex = 23;
            this.label1.Text = "Xin chào: ";
            // 
            // guna2PictureBox3
            // 
            this.guna2PictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.guna2PictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox3.Image")));
            this.guna2PictureBox3.ImageRotate = 0F;
            this.guna2PictureBox3.Location = new System.Drawing.Point(12, 722);
            this.guna2PictureBox3.Name = "guna2PictureBox3";
            this.guna2PictureBox3.Size = new System.Drawing.Size(32, 32);
            this.guna2PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.guna2PictureBox3.TabIndex = 22;
            this.guna2PictureBox3.TabStop = false;
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDangXuat.BackColor = System.Drawing.Color.Transparent;
            this.btnDangXuat.CustomBorderThickness = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnDangXuat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDangXuat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnDangXuat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDangXuat.FillColor = System.Drawing.Color.Transparent;
            this.btnDangXuat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDangXuat.ForeColor = System.Drawing.Color.White;
            this.btnDangXuat.HoverState.BorderColor = System.Drawing.Color.Navy;
            this.btnDangXuat.HoverState.CustomBorderColor = System.Drawing.Color.Blue;
            this.btnDangXuat.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(166)))), ((int)(((byte)(231)))));
            this.btnDangXuat.HoverState.ForeColor = System.Drawing.Color.Black;
            this.btnDangXuat.Image = ((System.Drawing.Image)(resources.GetObject("btnDangXuat.Image")));
            this.btnDangXuat.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDangXuat.ImageOffset = new System.Drawing.Point(8, 0);
            this.btnDangXuat.Location = new System.Drawing.Point(0, 666);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(205, 40);
            this.btnDangXuat.TabIndex = 21;
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // btnThucDon
            // 
            this.btnThucDon.BackColor = System.Drawing.Color.Transparent;
            this.btnThucDon.CustomBorderThickness = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnThucDon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThucDon.DisabledState.FillColor = System.Drawing.Color.Gainsboro;
            this.btnThucDon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThucDon.FillColor = System.Drawing.Color.Transparent;
            this.btnThucDon.Font = new System.Drawing.Font("Bahnschrift", 9F);
            this.btnThucDon.ForeColor = System.Drawing.Color.White;
            this.btnThucDon.HoverState.CustomBorderColor = System.Drawing.Color.Blue;
            this.btnThucDon.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnThucDon.HoverState.ForeColor = System.Drawing.Color.Black;
            this.btnThucDon.Image = ((System.Drawing.Image)(resources.GetObject("btnThucDon.Image")));
            this.btnThucDon.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnThucDon.ImageOffset = new System.Drawing.Point(8, 0);
            this.btnThucDon.Location = new System.Drawing.Point(0, 178);
            this.btnThucDon.Name = "btnThucDon";
            this.btnThucDon.Size = new System.Drawing.Size(205, 40);
            this.btnThucDon.TabIndex = 20;
            this.btnThucDon.Text = "Thực đơn";
            this.btnThucDon.TextOffset = new System.Drawing.Point(8, 0);
            this.btnThucDon.Click += new System.EventHandler(this.btnThucDon_Click);
            // 
            // btnMayTinh
            // 
            this.btnMayTinh.BackColor = System.Drawing.Color.Transparent;
            this.btnMayTinh.CustomBorderThickness = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnMayTinh.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMayTinh.DisabledState.FillColor = System.Drawing.Color.Gainsboro;
            this.btnMayTinh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMayTinh.FillColor = System.Drawing.Color.Transparent;
            this.btnMayTinh.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMayTinh.ForeColor = System.Drawing.Color.White;
            this.btnMayTinh.HoverState.BorderColor = System.Drawing.Color.Navy;
            this.btnMayTinh.HoverState.CustomBorderColor = System.Drawing.Color.Blue;
            this.btnMayTinh.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnMayTinh.HoverState.ForeColor = System.Drawing.Color.Black;
            this.btnMayTinh.Image = ((System.Drawing.Image)(resources.GetObject("btnMayTinh.Image")));
            this.btnMayTinh.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnMayTinh.ImageOffset = new System.Drawing.Point(8, 0);
            this.btnMayTinh.Location = new System.Drawing.Point(0, 224);
            this.btnMayTinh.Name = "btnMayTinh";
            this.btnMayTinh.Size = new System.Drawing.Size(205, 40);
            this.btnMayTinh.TabIndex = 19;
            this.btnMayTinh.Text = "Máy tính";
            this.btnMayTinh.TextOffset = new System.Drawing.Point(8, 0);
            this.btnMayTinh.Click += new System.EventHandler(this.btnMayTinh_Click);
            // 
            // btnKhachHang
            // 
            this.btnKhachHang.BackColor = System.Drawing.Color.Transparent;
            this.btnKhachHang.CustomBorderThickness = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnKhachHang.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnKhachHang.DisabledState.FillColor = System.Drawing.Color.Gainsboro;
            this.btnKhachHang.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnKhachHang.FillColor = System.Drawing.Color.Transparent;
            this.btnKhachHang.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKhachHang.ForeColor = System.Drawing.Color.White;
            this.btnKhachHang.HoverState.BorderColor = System.Drawing.Color.Navy;
            this.btnKhachHang.HoverState.CustomBorderColor = System.Drawing.Color.Blue;
            this.btnKhachHang.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnKhachHang.HoverState.ForeColor = System.Drawing.Color.Black;
            this.btnKhachHang.Image = ((System.Drawing.Image)(resources.GetObject("btnKhachHang.Image")));
            this.btnKhachHang.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnKhachHang.ImageOffset = new System.Drawing.Point(8, 0);
            this.btnKhachHang.Location = new System.Drawing.Point(0, 270);
            this.btnKhachHang.Name = "btnKhachHang";
            this.btnKhachHang.Size = new System.Drawing.Size(205, 40);
            this.btnKhachHang.TabIndex = 18;
            this.btnKhachHang.Text = "Khách hàng";
            this.btnKhachHang.TextOffset = new System.Drawing.Point(8, 0);
            this.btnKhachHang.Click += new System.EventHandler(this.btnKhachHang_Click);
            // 
            // btnHoaDon
            // 
            this.btnHoaDon.BackColor = System.Drawing.Color.Transparent;
            this.btnHoaDon.CustomBorderThickness = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnHoaDon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHoaDon.DisabledState.FillColor = System.Drawing.Color.Gainsboro;
            this.btnHoaDon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHoaDon.FillColor = System.Drawing.Color.Transparent;
            this.btnHoaDon.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnHoaDon.HoverState.BorderColor = System.Drawing.Color.Navy;
            this.btnHoaDon.HoverState.CustomBorderColor = System.Drawing.Color.Blue;
            this.btnHoaDon.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnHoaDon.HoverState.ForeColor = System.Drawing.Color.Black;
            this.btnHoaDon.Image = ((System.Drawing.Image)(resources.GetObject("btnHoaDon.Image")));
            this.btnHoaDon.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnHoaDon.ImageOffset = new System.Drawing.Point(8, 0);
            this.btnHoaDon.Location = new System.Drawing.Point(0, 316);
            this.btnHoaDon.Name = "btnHoaDon";
            this.btnHoaDon.Size = new System.Drawing.Size(205, 40);
            this.btnHoaDon.TabIndex = 17;
            this.btnHoaDon.Text = "Hóa đơn";
            this.btnHoaDon.TextOffset = new System.Drawing.Point(8, 0);
            this.btnHoaDon.Click += new System.EventHandler(this.btnHoaDon_Click);
            // 
            // btnThongKe
            // 
            this.btnThongKe.BackColor = System.Drawing.Color.Transparent;
            this.btnThongKe.CustomBorderThickness = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnThongKe.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThongKe.DisabledState.FillColor = System.Drawing.Color.Gainsboro;
            this.btnThongKe.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThongKe.FillColor = System.Drawing.Color.Transparent;
            this.btnThongKe.Font = new System.Drawing.Font("Bahnschrift", 9F);
            this.btnThongKe.ForeColor = System.Drawing.Color.White;
            this.btnThongKe.HoverState.BorderColor = System.Drawing.Color.Navy;
            this.btnThongKe.HoverState.CustomBorderColor = System.Drawing.Color.Blue;
            this.btnThongKe.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(166)))), ((int)(((byte)(231)))));
            this.btnThongKe.HoverState.ForeColor = System.Drawing.Color.Black;
            this.btnThongKe.Image = ((System.Drawing.Image)(resources.GetObject("btnThongKe.Image")));
            this.btnThongKe.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnThongKe.ImageOffset = new System.Drawing.Point(8, 0);
            this.btnThongKe.Location = new System.Drawing.Point(0, 132);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(205, 40);
            this.btnThongKe.TabIndex = 16;
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.TextOffset = new System.Drawing.Point(8, 0);
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // pnMain
            // 
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.FillColor2 = System.Drawing.Color.RoyalBlue;
            this.pnMain.FillColor3 = System.Drawing.Color.Honeydew;
            this.pnMain.Location = new System.Drawing.Point(205, 0);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(999, 758);
            this.pnMain.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 758);
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.pnSlideContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.pnSlideContainer.ResumeLayout(false);
            this.pnSlideContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.pnContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
        private System.Windows.Forms.Timer slideTimer;
        private System.Windows.Forms.Timer tmNhanVien;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel pnSlideContainer;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2Panel pnContainer;
        private Guna.UI2.WinForms.Guna2Button btnConViec;
        private Guna.UI2.WinForms.Guna2Button btnThongTin;
        private Guna.UI2.WinForms.Guna2Button btnNhanVien;
        private Guna.UI2.WinForms.Guna2Button btnTaiKhoan;
        private Guna.UI2.WinForms.Guna2Button btnMenu;
        private System.Windows.Forms.Label lbTenDangNhap;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox3;
        private Guna.UI2.WinForms.Guna2Button btnDangXuat;
        private Guna.UI2.WinForms.Guna2Button btnThucDon;
        private Guna.UI2.WinForms.Guna2Button btnMayTinh;
        private Guna.UI2.WinForms.Guna2Button btnKhachHang;
        private Guna.UI2.WinForms.Guna2Button btnHoaDon;
        private Guna.UI2.WinForms.Guna2Button btnThongKe;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel pnMain;
    }
}