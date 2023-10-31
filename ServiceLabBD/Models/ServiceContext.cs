using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ServiceLabBD;

public partial class ServiceContext : DbContext
{
    public ServiceContext()
    {
    }

    public ServiceContext(DbContextOptions<ServiceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auto> Autos { get; set; }

    public virtual DbSet<CarPart> CarParts { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<DiscountCard> DiscountCards { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<Master> Masters { get; set; }

    public virtual DbSet<Mechanic> Mechanics { get; set; }

    public virtual DbSet<Produser> Produsers { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    //public virtual DbSet<ServiceCarPart> ServiceCarParts { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<WorkTeam> WorkTeams { get; set; }

    public virtual DbSet<WorkTime> WorkTimes { get; set; }

    public virtual DbSet<WorkType> WorkTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=VimeRSPC\\SQLEXPRESS; Database=Service; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auto>(entity =>
        {
            entity.ToTable("Auto");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Model).IsRequired();
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<CarPart>(entity =>
        {
            entity.ToTable("CarPart");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.ProduserId).HasColumnName("ProduserID");
            entity.Property(e => e.StocksId).HasColumnName("StocksID");

            entity.HasOne(d => d.Produser).WithMany(p => p.CarParts)
                .HasForeignKey(d => d.ProduserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CarPart_Produser");

            entity.HasOne(d => d.Stocks).WithMany(p => p.CarParts)
                .HasForeignKey(d => d.StocksId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CarPart_Stocks");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DiscountCardId).HasColumnName("DiscountCardID");
            entity.Property(e => e.FullName).IsRequired();

            entity.HasOne(d => d.DiscountCard).WithMany(p => p.Clients)
                .HasForeignKey(d => d.DiscountCardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Client_DiscountCard");
        });

        modelBuilder.Entity<DiscountCard>(entity =>
        {
            entity.ToTable("DiscountCard");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.ToTable("Manager");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FullName).IsRequired();
        });

        modelBuilder.Entity<Master>(entity =>
        {
            entity.ToTable("Master");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FullName).IsRequired();
            entity.Property(e => e.QualificationLevel).IsRequired();
        });

        modelBuilder.Entity<Mechanic>(entity =>
        {
            entity.ToTable("Mechanic");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FullName).IsRequired();
        });

        modelBuilder.Entity<Produser>(entity =>
        {
            entity.ToTable("Produser");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Adress).IsRequired();
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Service");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AutoId).HasColumnName("AutoID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");
            entity.Property(e => e.ManagerId).HasColumnName("ManagerID");
            entity.Property(e => e.WorkTeamId).HasColumnName("WorkTeamID");
            entity.Property(e => e.WorkTimeId).HasColumnName("WorkTimeID");
            entity.Property(e => e.WorkTypeId).HasColumnName("WorkTypeID");

            entity.HasOne(d => d.Auto).WithMany(p => p.Services)
                .HasForeignKey(d => d.AutoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Service_Auto");

            entity.HasOne(d => d.Client).WithMany(p => p.Services)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Service_Client");

            entity.HasOne(d => d.Equipment).WithMany(p => p.Services)
                .HasForeignKey(d => d.EquipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Service_Equipment");

            entity.HasOne(d => d.Manager).WithMany(p => p.Services)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Service_Manager");

            entity.HasOne(d => d.WorkTeam).WithMany(p => p.Services)
                .HasForeignKey(d => d.WorkTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Service_WorkTeam");

            entity.HasOne(d => d.WorkTime).WithMany(p => p.Services)
                .HasForeignKey(d => d.WorkTimeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Service_WorkTime");

            entity.HasOne(d => d.WorkType).WithMany(p => p.Services)
                .HasForeignKey(d => d.WorkTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Service_WorkType");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<WorkTeam>(entity =>
        {
            entity.ToTable("WorkTeam");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MasterId).HasColumnName("MasterID");
            entity.Property(e => e.MechanicId).HasColumnName("MechanicID");

            entity.HasOne(d => d.Master).WithMany(p => p.WorkTeams)
                .HasForeignKey(d => d.MasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkTeam_Master");

            entity.HasOne(d => d.Mechanic).WithMany(p => p.WorkTeams)
                .HasForeignKey(d => d.MechanicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkTeam_Mechanic");
        });

        modelBuilder.Entity<WorkTime>(entity =>
        {
            entity.ToTable("WorkTime");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<WorkType>(entity =>
        {
            entity.ToTable("WorkType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).IsRequired();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
