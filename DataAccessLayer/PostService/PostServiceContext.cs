using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccessLayer.PostService
{
    public partial class PostServiceContext : DbContext
    {
        public PostServiceContext()
        {
        }

        public PostServiceContext(DbContextOptions<PostServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MessagePlaceHolder> MessagePlaceHolders { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersMessagesMapped> UsersMessagesMappeds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_To_Users");
            });

            modelBuilder.Entity<MessagePlaceHolder>(entity =>
            {
                entity.HasKey(e => e.PlaceHolderId)
                    .HasName("PK_PlaceHolderId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserLogin)
                    .HasName("UQ__Users__7F8E8D5EB731D4C3")
                    .IsUnique();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_To_Roles");
            });

            modelBuilder.Entity<UsersMessagesMapped>(entity =>
            {
                entity.HasOne(d => d.Message)
                    .WithMany(p => p.UsersMessagesMappeds)
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Messages_Mappe_To_Messages");

                entity.HasOne(d => d.PlaceHolder)
                    .WithMany(p => p.UsersMessagesMappeds)
                    .HasForeignKey(d => d.PlaceHolderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Messages_Mappe_To_MessagePlaceHolders");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersMessagesMappeds)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Messages_Mappe_To_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
