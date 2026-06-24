using Microsoft.EntityFrameworkCore;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Data;

public class SLMSDbContext : DbContext
{
    public SLMSDbContext(DbContextOptions<SLMSDbContext> options)
        : base(options)
    {
    }

    // Master Tables
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Employee> Employees => Set<Employee>();

    // Security
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

    // Library Catalog
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<LibraryResource> LibraryResources => Set<LibraryResource>();
    public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();

    // Custody Management
    public DbSet<CustodyHistory> CustodyHistories => Set<CustodyHistory>();

    // Transactions
    public DbSet<BookIssue> BookIssues => Set<BookIssue>();
    public DbSet<BookReturn> BookReturns => Set<BookReturn>();

    //Shelf
    public DbSet<Shelf> Shelves { get; set; }

    // Requests
    public DbSet<Request> Requests => Set<Request>();

    // Digital Library
    public DbSet<DigitalContent> DigitalContents => Set<DigitalContent>();


    public DbSet<DigitalContentRequest> DigitalContentRequests => Set<DigitalContentRequest>();

    // Notifications
    public DbSet<Notification> Notifications => Set<Notification>();

    // Policies
    public DbSet<Policy> Policies => Set<Policy>();

    // Audit
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Digitalcontent Request
        modelBuilder.Entity<DigitalContentRequest>()
        .HasOne(x => x.Employee)
        .WithMany()
        .HasForeignKey(x => x.EmployeeId);

        modelBuilder.Entity<DigitalContentRequest>()
            .HasOne(x => x.DigitalContent)
            .WithMany(x => x.Requests)
            .HasForeignKey(x => x.DigitalContentId);
        modelBuilder.Entity<DownloadHistory>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey(x => x.EmployeeId);

        modelBuilder.Entity<DownloadHistory>()
            .HasOne(x => x.DigitalContent)
            .WithMany()
            .HasForeignKey(x => x.DigitalContentId);

        // UserRole Composite Key
        modelBuilder.Entity<UserRole>()
            .HasKey(x => new { x.UserId, x.RoleId });

        // RolePermission Composite Key
        modelBuilder.Entity<RolePermission>()
            .HasKey(x => new { x.RoleId, x.PermissionId });

        // Employee -> Department
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId);


        // LibraryResource -> Category
        modelBuilder.Entity<LibraryResource>()
            .HasOne(r => r.Category)
            .WithMany(c => c.Resources)
            .HasForeignKey(r => r.CategoryId);

        //shelg intergration
        modelBuilder.Entity<LibraryResource>()
    .HasOne(r => r.Shelf)
    .WithMany(s => s.Resources)
    .HasForeignKey(r => r.ShelfId)
    .OnDelete(DeleteBehavior.SetNull);

        // InventoryItem -> LibraryResource
        modelBuilder.Entity<InventoryItem>()
            .HasOne(i => i.Resource)
            .WithMany(r => r.InventoryItems)
            .HasForeignKey(i => i.ResourceId);

        // CustodyHistory -> FromDepartment
        modelBuilder.Entity<CustodyHistory>()
            .HasOne(c => c.FromDepartment)
            .WithMany(d => d.CustodyTransfersFrom)
            .HasForeignKey(c => c.FromDepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // CustodyHistory -> ToDepartment
        modelBuilder.Entity<CustodyHistory>()
            .HasOne(c => c.ToDepartment)
            .WithMany(d => d.CustodyTransfersTo)
            .HasForeignKey(c => c.ToDepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Role>().HasData(
    new Role
    {
        Id = 1,
        RoleName = "Admin",
        Description = "System Administrator"
    },
    new Role
    {
        Id = 2,
        RoleName = "Librarian",
        Description = "Library Manager"
    },
    new Role
    {
        Id = 3,
        RoleName = "User",
        Description = "Normal Employee"
    }
);
    }
}