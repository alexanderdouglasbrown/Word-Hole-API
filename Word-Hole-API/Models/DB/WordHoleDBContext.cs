﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Word_Hole_API.Models.DB
{
    public partial class WordHoleDBContext : DbContext
    {
        public WordHoleDBContext()
        {
        }

        public WordHoleDBContext(DbContextOptions<WordHoleDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Likes> Likes { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.ToTable("comments");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment");

                entity.Property(e => e.Createdon)
                    .HasColumnName("createdon")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Editdate).HasColumnName("editdate");

                entity.Property(e => e.Postid).HasColumnName("postid");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Postid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comments_postid_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comments_userid_fkey");
            });

            modelBuilder.Entity<Likes>(entity =>
            {
                entity.HasKey(e => new { e.Userid, e.Postid })
                    .HasName("likes_pkey");

                entity.ToTable("likes");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Postid).HasColumnName("postid");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.Postid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("likes_postid_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("likes_userid_fkey");
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.ToTable("posts");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Createdon)
                    .HasColumnName("createdon")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Editdate).HasColumnName("editdate");

                entity.Property(e => e.Post)
                    .IsRequired()
                    .HasColumnName("post");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("posts_userid_fkey");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Username)
                    .HasName("users_username_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Access)
                    .IsRequired()
                    .HasColumnName("access")
                    .HasMaxLength(50);

                entity.Property(e => e.Createdon)
                    .HasColumnName("createdon")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasColumnName("hash")
                    .HasMaxLength(255);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50);
            });
        }
    }
}
