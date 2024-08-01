using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace UIDuAn1.Models
{
    public partial class QUANLYQUANNETContext : DbContext
    {
        public QUANLYQUANNETContext()
        {
        }

        public QUANLYQUANNETContext(DbContextOptions<QUANLYQUANNETContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CaLam> CaLam { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<HoaDonChiTiet> HoaDonChiTiet { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<MayTinh> MayTinh { get; set; }
        public virtual DbSet<NhanVien> NhanVien { get; set; }
        public virtual DbSet<ThucDon> ThucDon { get; set; }
        public virtual DbSet<VaiTro> VaiTro { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=NGUYEN\\CHINGUYEN;Database=QUANLYQUANNET;Trusted_Connection=True;uid=sa;password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CaLam>(entity =>
            {
                entity.HasKey(e => e.MaCa)
                    .HasName("PK__CaLam__27258E7BFCF73F79");

                entity.Property(e => e.MaCa).HasMaxLength(10);

                entity.Property(e => e.CaLam1)
                    .IsRequired()
                    .HasColumnName("CaLam")
                    .HasMaxLength(20);

                entity.Property(e => e.MaNhanVien).HasMaxLength(10);

                entity.Property(e => e.NgayLam).HasColumnType("datetime");

                entity.Property(e => e.ViPham)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.CaLam)
                    .HasForeignKey(d => d.MaNhanVien)
                    .HasConstraintName("FK__CaLam__MaNhanVie__4E88ABD4");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHoaDon)
                    .HasName("PK__HoaDon__835ED13BEE4DB66B");

                entity.Property(e => e.MaHoaDon).HasMaxLength(10);

                entity.Property(e => e.MaNhanVien)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.TriGia).HasColumnType("money");

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.HoaDon)
                    .HasForeignKey(d => d.MaNhanVien)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoaDon__MaNhanVi__59FA5E80");
            });

            modelBuilder.Entity<HoaDonChiTiet>(entity =>
            {
                entity.HasKey(e => new { e.MaHoaDon, e.MaMonAn })
                    .HasName("PK__HoaDonCh__C84FA05985916B3B");

                entity.Property(e => e.MaHoaDon).HasMaxLength(10);

                entity.Property(e => e.MaMonAn).HasMaxLength(10);

                entity.Property(e => e.MaKhachHang)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.MaHoaDonNavigation)
                    .WithMany(p => p.HoaDonChiTiet)
                    .HasForeignKey(d => d.MaHoaDon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoaDonChi__MaHoa__5CD6CB2B");

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.HoaDonChiTiet)
                    .HasForeignKey(d => d.MaKhachHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoaDonChi__MaKha__5EBF139D");

                entity.HasOne(d => d.MaMonAnNavigation)
                    .WithMany(p => p.HoaDonChiTiet)
                    .HasForeignKey(d => d.MaMonAn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoaDonChi__MaMon__5DCAEF64");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKhachHang)
                    .HasName("PK__KhachHan__88D2F0E5E8415933");

                entity.Property(e => e.MaKhachHang).HasMaxLength(10);

                entity.Property(e => e.MaNhanVien).HasMaxLength(10);

                entity.Property(e => e.MatKhau)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SoTien).HasColumnType("money");

                entity.Property(e => e.TaiKhoan)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.KhachHang)
                    .HasForeignKey(d => d.MaNhanVien)
                    .HasConstraintName("FK__KhachHang__MaNha__5165187F");
            });

            modelBuilder.Entity<MayTinh>(entity =>
            {
                entity.HasKey(e => e.MaMay)
                    .HasName("PK__MayTinh__3A5BBB411AEC7DDD");

                entity.Property(e => e.MaMay).HasMaxLength(10);

                entity.Property(e => e.Cpu)
                    .IsRequired()
                    .HasColumnName("CPU")
                    .HasMaxLength(50);

                entity.Property(e => e.GiaTien).HasColumnType("money");

                entity.Property(e => e.Gpu)
                    .IsRequired()
                    .HasColumnName("GPU")
                    .HasMaxLength(50);

                entity.Property(e => e.MaNhanVien).HasMaxLength(10);

                entity.Property(e => e.Ram)
                    .IsRequired()
                    .HasColumnName("RAM")
                    .HasMaxLength(10);

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.MayTinh)
                    .HasForeignKey(d => d.MaNhanVien)
                    .HasConstraintName("FK__MayTinh__MaNhanV__571DF1D5");
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNhanVien)
                    .HasName("PK__NhanVien__77B2CA47D13C3E88");

                entity.Property(e => e.MaNhanVien).HasMaxLength(10);

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Gmail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HoTen)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MaVaiTro).HasMaxLength(10);

                entity.Property(e => e.MatKhau).HasMaxLength(50);

                entity.Property(e => e.TenVaiTro)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.MaVaiTroNavigation)
                    .WithMany(p => p.NhanVien)
                    .HasForeignKey(d => d.MaVaiTro)
                    .HasConstraintName("FK__NhanVien__MaVaiT__4BAC3F29");
            });

            modelBuilder.Entity<ThucDon>(entity =>
            {
                entity.HasKey(e => e.MaMonAn)
                    .HasName("PK__ThucDon__B11716258E650456");

                entity.Property(e => e.MaMonAn).HasMaxLength(10);

                entity.Property(e => e.Gia).HasColumnType("money");

                entity.Property(e => e.HinhAnh)
                    .IsRequired()
                    .HasColumnType("image");

                entity.Property(e => e.MaNhanVien).HasMaxLength(10);

                entity.Property(e => e.TenMonAn)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.ThucDon)
                    .HasForeignKey(d => d.MaNhanVien)
                    .HasConstraintName("FK__ThucDon__MaNhanV__5441852A");
            });

            modelBuilder.Entity<VaiTro>(entity =>
            {
                entity.HasKey(e => e.MaVaiTro)
                    .HasName("PK__VaiTro__C24C41CFCF39F296");

                entity.Property(e => e.MaVaiTro).HasMaxLength(10);

                entity.Property(e => e.TenVaiTro)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
