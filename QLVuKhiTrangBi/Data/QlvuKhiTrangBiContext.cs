using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QLVuKhiTrangBi.Models;

namespace QLVuKhiTrangBi.Data;

public partial class QlvuKhiTrangBiContext : DbContext
{
    public QlvuKhiTrangBiContext()
    {
    }

    public QlvuKhiTrangBiContext(DbContextOptions<QlvuKhiTrangBiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BanGiaoQkSung> BanGiaoQkSungs { get; set; }

    public virtual DbSet<BanGiaoQkTrangBi> BanGiaoQkTrangBis { get; set; }

    public virtual DbSet<BbbanGiaoKho> BbbanGiaoKhos { get; set; }

    public virtual DbSet<BbbanGiaoQk> BbbanGiaoQks { get; set; }

    public virtual DbSet<BbmuonTraSung> BbmuonTraSungs { get; set; }

    public virtual DbSet<BbmuonTraTb> BbmuonTraTbs { get; set; }

    public virtual DbSet<BbmuonTraVktb> BbmuonTraVktbs { get; set; }

    public virtual DbSet<BienCheSung> BienCheSungs { get; set; }

    public virtual DbSet<BienCheTb> BienCheTbs { get; set; }

    public virtual DbSet<CanBoDaiDoi> CanBoDaiDois { get; set; }

    public virtual DbSet<CanBoPhuTrach> CanBoPhuTraches { get; set; }

    public virtual DbSet<CanBoTieuDoan> CanBoTieuDoans { get; set; }

    public virtual DbSet<DaiDoi> DaiDois { get; set; }

    public virtual DbSet<HocVien> HocViens { get; set; }

    public virtual DbSet<LichGac> LichGacs { get; set; }

    public virtual DbSet<LoaiSung> LoaiSungs { get; set; }

    public virtual DbSet<LoaiTrangBi> LoaiTrangBis { get; set; }

    public virtual DbSet<LuotBienChe> LuotBienChes { get; set; }

    public virtual DbSet<NhomNguoiDung> NhomNguoiDungs { get; set; }

    public virtual DbSet<NhomQuyen> NhomQuyens { get; set; }

    public virtual DbSet<Quyen> Quyens { get; set; }

    public virtual DbSet<QuyetDinh> QuyetDinhs { get; set; }

    public virtual DbSet<Sung> Sungs { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<TrangBi> TrangBis { get; set; }

    public virtual DbSet<YeucauLoaisung> YeucauLoaisungs { get; set; }

    public virtual DbSet<YeucauLoaitb> YeucauLoaitbs { get; set; }

    public virtual DbSet<Yeucaumuonvktb> Yeucaumuonvktbs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=NGUYEN-THUY-QUY\\QUYNH;Initial Catalog=QLVuKhiTrangBi;Encrypt=false;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BanGiaoQkSung>(entity =>
        {
            entity.HasKey(e => new { e.MaBienBan, e.SoHieuSung });

            entity.ToTable("BanGiaoQK_Sung");

            entity.Property(e => e.MaBienBan)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.SoHieuSung)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.GhiChu).HasColumnType("ntext");
            entity.Property(e => e.HanhDong).HasMaxLength(50);

            entity.HasOne(d => d.MaBienBanNavigation).WithMany(p => p.BanGiaoQkSungs)
                .HasForeignKey(d => d.MaBienBan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BanGiaoQK_Sung_BBBanGiaoQK");

            entity.HasOne(d => d.SoHieuSungNavigation).WithMany(p => p.BanGiaoQkSungs)
                .HasForeignKey(d => d.SoHieuSung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BanGiaoQK_Sung_Sung");
        });

        modelBuilder.Entity<BanGiaoQkTrangBi>(entity =>
        {
            entity.HasKey(e => new { e.MaBienBan, e.MaTrangBi, e.PhanCap }).HasName("PK_BanGiaoQK_TrangBi_1");

            entity.ToTable("BanGiaoQK_TrangBi");

            entity.Property(e => e.MaBienBan)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.MaTrangBi)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.GhiChu).HasColumnType("ntext");
            entity.Property(e => e.HanhDong).HasMaxLength(50);

            entity.HasOne(d => d.MaBienBanNavigation).WithMany(p => p.BanGiaoQkTrangBis)
                .HasForeignKey(d => d.MaBienBan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BanGiaoQK_TrangBi_BBBanGiaoQK");

            entity.HasOne(d => d.MaTrangBiNavigation).WithMany(p => p.BanGiaoQkTrangBis)
                .HasForeignKey(d => d.MaTrangBi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BanGiaoQK_TrangBi_TrangBi");
        });

        modelBuilder.Entity<BbbanGiaoKho>(entity =>
        {
            entity.HasKey(e => e.MaBanGiaoKho).HasName("PK__BBBanGia__AE98DBFF76277C8A");

            entity.ToTable("BBBanGiaoKho");

            entity.Property(e => e.MaBanGiaoKho)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.MaNguoiGiao)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.MaNguoiNhan)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.ThoiGian).HasColumnType("datetime");
            entity.Property(e => e.TinhTrangKho).HasMaxLength(100);

            entity.HasOne(d => d.MaNguoiGiaoNavigation).WithMany(p => p.BbbanGiaoKhoMaNguoiGiaoNavigations)
                .HasForeignKey(d => d.MaNguoiGiao)
                .HasConstraintName("FK__BBBanGiao__MaNgu__71D1E811");

            entity.HasOne(d => d.MaNguoiNhanNavigation).WithMany(p => p.BbbanGiaoKhoMaNguoiNhanNavigations)
                .HasForeignKey(d => d.MaNguoiNhan)
                .HasConstraintName("FK__BBBanGiao__MaNgu__72C60C4A");
        });

        modelBuilder.Entity<BbbanGiaoQk>(entity =>
        {
            entity.HasKey(e => e.MaBienBan);

            entity.ToTable("BBBanGiaoQK");

            entity.Property(e => e.MaBienBan)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.GhiChu).HasColumnType("ntext");
            entity.Property(e => e.NguoiTao)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.TenBienBan).HasMaxLength(100);
            entity.Property(e => e.ThoiGian).HasColumnType("datetime");

            entity.HasOne(d => d.NguoiTaoNavigation).WithMany(p => p.BbbanGiaoQks)
                .HasForeignKey(d => d.NguoiTao)
                .HasConstraintName("FK_BBBanGiaoQK_CanBoPhuTrach");
        });

        modelBuilder.Entity<BbmuonTraSung>(entity =>
        {
            entity.HasKey(e => new { e.MaBienBan, e.SoHieuSung, e.MuonTra });

            entity.ToTable("BBMuonTraSung");

            entity.Property(e => e.SoHieuSung)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.GhiChu).HasColumnType("ntext");
            entity.Property(e => e.TinhTrang).HasMaxLength(100);

            entity.HasOne(d => d.MaBienBanNavigation).WithMany(p => p.BbmuonTraSungs)
                .HasForeignKey(d => d.MaBienBan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BBMuonTraSung_BBMuonTraVKTB");
        });

        modelBuilder.Entity<BbmuonTraTb>(entity =>
        {
            entity.HasKey(e => new { e.MaBienBan, e.MaTrangBi, e.MuonTra });

            entity.ToTable("BBMuonTraTB");

            entity.Property(e => e.MaTrangBi)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.GhiChu)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Slthieu).HasColumnName("SLThieu");
            entity.Property(e => e.TinhTrang).HasMaxLength(100);

            entity.HasOne(d => d.MaBienBanNavigation).WithMany(p => p.BbmuonTraTbs)
                .HasForeignKey(d => d.MaBienBan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BBMuonTraTB_BBMuonTraVKTB");
        });

        modelBuilder.Entity<BbmuonTraVktb>(entity =>
        {
            entity.HasKey(e => e.MaBienBan);

            entity.ToTable("BBMuonTraVKTB");

            entity.Property(e => e.CbdaidoiGiao)
                .HasMaxLength(50)
                .HasColumnName("cbdaidoiGiao");
            entity.Property(e => e.CbdaidoiNhan)
                .HasMaxLength(50)
                .HasColumnName("cbdaidoiNhan");
            entity.Property(e => e.CbkhoGiao)
                .HasMaxLength(50)
                .HasColumnName("cbkhoGiao");
            entity.Property(e => e.CbkhoNhan)
                .HasMaxLength(50)
                .HasColumnName("cbkhoNhan");
            entity.Property(e => e.DaiDoiGac)
                .HasMaxLength(5)
                .IsFixedLength();
            entity.Property(e => e.GhiChu)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.NgayGac).HasColumnType("date");
            entity.Property(e => e.TenBienBan).HasMaxLength(100);
            entity.Property(e => e.ThoiGianMuon)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ThoiGianTra)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<BienCheSung>(entity =>
        {
            entity.HasKey(e => e.MaBienCheSung).HasName("PK__BienCheS__C8A8A447F592EE45");

            entity.ToTable("BienCheSung");

            entity.Property(e => e.MaBienCheSung)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.MaHocVien)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.MaLuotBienChe)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.SoHieuSung)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.MaHocVienNavigation).WithMany(p => p.BienCheSungs)
                .HasForeignKey(d => d.MaHocVien)
                .HasConstraintName("FK__BienCheSu__MaHoc__5224328E");

            entity.HasOne(d => d.MaLuotBienCheNavigation).WithMany(p => p.BienCheSungs)
                .HasForeignKey(d => d.MaLuotBienChe)
                .HasConstraintName("FK__BienCheSu__MaLuo__531856C7");

            entity.HasOne(d => d.SoHieuSungNavigation).WithMany(p => p.BienCheSungs)
                .HasForeignKey(d => d.SoHieuSung)
                .HasConstraintName("FK__BienCheSu__SoHie__51300E55");
        });

        modelBuilder.Entity<BienCheTb>(entity =>
        {
            entity.HasKey(e => e.MaBienCheTb).HasName("PK__BienCheT__B56E0D9712FC23FB");

            entity.ToTable("BienCheTB");

            entity.Property(e => e.MaBienCheTb)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("MaBienCheTB");
            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.MaHocVien)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.MaLuotBienChe)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.MaTrangBi)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.MaHocVienNavigation).WithMany(p => p.BienCheTbs)
                .HasForeignKey(d => d.MaHocVien)
                .HasConstraintName("FK__BienCheTB__MaHoc__540C7B00");

            entity.HasOne(d => d.MaLuotBienCheNavigation).WithMany(p => p.BienCheTbs)
                .HasForeignKey(d => d.MaLuotBienChe)
                .HasConstraintName("FK__BienCheTB__MaLuo__55009F39");

            entity.HasOne(d => d.MaTrangBiNavigation).WithMany(p => p.BienCheTbs)
                .HasForeignKey(d => d.MaTrangBi)
                .HasConstraintName("FK_BienCheTB_TrangBi");
        });

        modelBuilder.Entity<CanBoDaiDoi>(entity =>
        {
            entity.HasKey(e => e.MaQn);

            entity.ToTable("CanBoDaiDoi");

            entity.Property(e => e.MaQn)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("MaQN");
            entity.Property(e => e.CapBac).HasMaxLength(20);
            entity.Property(e => e.ChucVu).HasMaxLength(20);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.MaDaiDoi)
                .HasMaxLength(5)
                .IsFixedLength();
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SDT");

            entity.HasOne(d => d.MaDaiDoiNavigation).WithMany(p => p.CanBoDaiDois)
                .HasForeignKey(d => d.MaDaiDoi)
                .HasConstraintName("FK_CanBoDaiDoi_DaiDoi");
        });

        modelBuilder.Entity<CanBoPhuTrach>(entity =>
        {
            entity.HasKey(e => e.MaQn);

            entity.ToTable("CanBoPhuTrach");

            entity.Property(e => e.MaQn)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("MaQN");
            entity.Property(e => e.CapBac).HasMaxLength(20);
            entity.Property(e => e.ChucVu).HasMaxLength(20);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SDT");
        });

        modelBuilder.Entity<CanBoTieuDoan>(entity =>
        {
            entity.HasKey(e => e.MaQn);

            entity.ToTable("CanBoTieuDoan");

            entity.Property(e => e.MaQn)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("MaQN");
            entity.Property(e => e.CapBac).HasMaxLength(20);
            entity.Property(e => e.ChucVu).HasMaxLength(20);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SDT");
        });

        modelBuilder.Entity<DaiDoi>(entity =>
        {
            entity.HasKey(e => e.MaDaiDoi);

            entity.ToTable("DaiDoi");

            entity.Property(e => e.MaDaiDoi)
                .HasMaxLength(5)
                .IsFixedLength();
            entity.Property(e => e.TenDaiDoi).HasMaxLength(50);
        });

        modelBuilder.Entity<HocVien>(entity =>
        {
            entity.HasKey(e => e.MaHocVien).HasName("PK__HocVien__685B0E6A4A7757D3");

            entity.ToTable("HocVien");

            entity.Property(e => e.MaHocVien)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.MaDaiDoi)
                .HasMaxLength(5)
                .IsFixedLength();
            entity.Property(e => e.TenHocVien).HasMaxLength(50);

            entity.HasOne(d => d.MaDaiDoiNavigation).WithMany(p => p.HocViens)
                .HasForeignKey(d => d.MaDaiDoi)
                .HasConstraintName("FK__HocVien__MaDaiDo__56E8E7AB");
        });

        modelBuilder.Entity<LichGac>(entity =>
        {
            entity.HasKey(e => e.Stt).HasName("PK_LichGac_1");

            entity.ToTable("LichGac");

            entity.Property(e => e.Stt)
                .ValueGeneratedNever()
                .HasColumnName("STT");
            entity.Property(e => e.DaiDoiGac)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.GhiChu).HasColumnType("ntext");
            entity.Property(e => e.Ngay).HasColumnType("date");
            entity.Property(e => e.TrangThai).HasMaxLength(50);
        });

        modelBuilder.Entity<LoaiSung>(entity =>
        {
            entity.HasKey(e => e.MaLoaiSung);

            entity.ToTable("LoaiSung");

            entity.Property(e => e.MaLoaiSung)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.TenLoaiSung).HasMaxLength(50);
        });

        modelBuilder.Entity<LoaiTrangBi>(entity =>
        {
            entity.HasKey(e => e.MaLoaiTb);

            entity.ToTable("LoaiTrangBi");

            entity.Property(e => e.MaLoaiTb)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("MaLoaiTB");
            entity.Property(e => e.TenLoaiTb)
                .HasMaxLength(50)
                .HasColumnName("TenLoaiTB");
        });

        modelBuilder.Entity<LuotBienChe>(entity =>
        {
            entity.HasKey(e => e.MaLuotBienChe).HasName("PK__LuotBien__187D56202FF722A5");

            entity.ToTable("LuotBienChe");

            entity.Property(e => e.MaLuotBienChe)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.ThoiGianBd)
                .HasColumnType("date")
                .HasColumnName("ThoiGianBD");
            entity.Property(e => e.ThoiGianKt)
                .HasColumnType("date")
                .HasColumnName("ThoiGianKT");
        });

        modelBuilder.Entity<NhomNguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNhom);

            entity.ToTable("NhomNguoiDung");

            entity.Property(e => e.MaNhom)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.TenNhom).HasMaxLength(50);
        });

        modelBuilder.Entity<NhomQuyen>(entity =>
        {
            entity.HasKey(e => new { e.MaNhom, e.MaQuyen });

            entity.ToTable("Nhom_Quyen");

            entity.Property(e => e.MaNhom)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.MaQuyen)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.GhiChu).HasColumnType("text");

            entity.HasOne(d => d.MaNhomNavigation).WithMany(p => p.NhomQuyens)
                .HasForeignKey(d => d.MaNhom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Nhom_Quyen_NhomNguoiDung");

            entity.HasOne(d => d.MaQuyenNavigation).WithMany(p => p.NhomQuyens)
                .HasForeignKey(d => d.MaQuyen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Nhom_Quyen_Quyen");
        });

        modelBuilder.Entity<Quyen>(entity =>
        {
            entity.HasKey(e => e.MaQuyen);

            entity.ToTable("Quyen");

            entity.Property(e => e.MaQuyen)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.TenQuyen).HasMaxLength(50);
        });

        modelBuilder.Entity<QuyetDinh>(entity =>
        {
            entity.HasKey(e => e.SoQd);

            entity.ToTable("QuyetDinh");

            entity.Property(e => e.SoQd)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SoQD");
            entity.Property(e => e.ChiTiet).HasMaxLength(100);
            entity.Property(e => e.NguoiGui)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.ThoiGian).HasColumnType("datetime");

            entity.HasOne(d => d.NguoiGuiNavigation).WithMany(p => p.QuyetDinhs)
                .HasForeignKey(d => d.NguoiGui)
                .HasConstraintName("FK_QuyetDinh_CanBoTieuDoan");
        });

        modelBuilder.Entity<Sung>(entity =>
        {
            entity.HasKey(e => e.SoHieuSung);

            entity.ToTable("Sung");

            entity.Property(e => e.SoHieuSung)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.DonViTinh)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.GhiChu).HasColumnType("text");
            entity.Property(e => e.MaLoaiSung)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.SoQd)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SoQD");

            entity.HasOne(d => d.MaLoaiSungNavigation).WithMany(p => p.Sungs)
                .HasForeignKey(d => d.MaLoaiSung)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Sung_LoaiSung");

            entity.HasOne(d => d.SoQdNavigation).WithMany(p => p.Sungs)
                .HasForeignKey(d => d.SoQd)
                .HasConstraintName("FK_Sung_QuyetDinh");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaQn);

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.MaQn)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("MaQN");
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenDn)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("TenDN");
        });

        modelBuilder.Entity<TrangBi>(entity =>
        {
            entity.HasKey(e => e.MaTrangBi).HasName("PK_TrangBi_1");

            entity.ToTable("TrangBi");

            entity.Property(e => e.MaTrangBi)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.DonViTinh)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.GhiChu).HasColumnType("text");
            entity.Property(e => e.MaLoaiTb)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("MaLoaiTB");
            entity.Property(e => e.SoQd)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SoQD");
            entity.Property(e => e.TenTrangBi).HasMaxLength(50);

            entity.HasOne(d => d.MaLoaiTbNavigation).WithMany(p => p.TrangBis)
                .HasForeignKey(d => d.MaLoaiTb)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_TrangBi_LoaiTrangBi");

            entity.HasOne(d => d.SoQdNavigation).WithMany(p => p.TrangBis)
                .HasForeignKey(d => d.SoQd)
                .HasConstraintName("FK_TrangBi_QuyetDinh");
        });

        modelBuilder.Entity<YeucauLoaisung>(entity =>
        {
            entity.HasKey(e => new { e.MaYc, e.MaLoaiSung });

            entity.ToTable("YEUCAU_LOAISUNG");

            entity.Property(e => e.MaYc).HasColumnName("MaYC");
            entity.Property(e => e.MaLoaiSung)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.GhiChu).HasColumnType("ntext");

            entity.HasOne(d => d.MaYcNavigation).WithMany(p => p.YeucauLoaisungs)
                .HasForeignKey(d => d.MaYc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_YEUCAU_LOAISUNG_YEUCAUMUONVKTB");
        });

        modelBuilder.Entity<YeucauLoaitb>(entity =>
        {
            entity.HasKey(e => new { e.MaYc, e.MaTrangBi });

            entity.ToTable("YEUCAU_LOAITB");

            entity.Property(e => e.MaYc).HasColumnName("MaYC");
            entity.Property(e => e.MaTrangBi)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.GhiChu).HasColumnType("ntext");

            entity.HasOne(d => d.MaYcNavigation).WithMany(p => p.YeucauLoaitbs)
                .HasForeignKey(d => d.MaYc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_YEUCAU_LOAITB_YEUCAUMUONVKTB");
        });

        modelBuilder.Entity<Yeucaumuonvktb>(entity =>
        {
            entity.HasKey(e => e.MaYc);

            entity.ToTable("YEUCAUMUONVKTB");

            entity.Property(e => e.MaYc).HasColumnName("MaYC");
            entity.Property(e => e.GhiChu).HasColumnType("ntext");
            entity.Property(e => e.MaCanBoDaiDoi)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.MaDaiDoi)
                .HasMaxLength(5)
                .IsFixedLength();
            entity.Property(e => e.ThoiGianDuKienMuon).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianDuKienTra).HasColumnType("datetime");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaCanBoDaiDoiNavigation).WithMany(p => p.Yeucaumuonvktbs)
                .HasForeignKey(d => d.MaCanBoDaiDoi)
                .HasConstraintName("FK_YEUCAUMUONVKTB_CanBoDaiDoi");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
