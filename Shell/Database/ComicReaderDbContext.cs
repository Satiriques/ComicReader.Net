﻿using ComicReader.Net.UI.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicReader.Net.Database
{
    public class ComicReaderDbContext : DbContext
    {
        public ComicReaderDbContext() : base("ComicReaderDb")
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Thumbnail> Thumbnails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Tags)
                .WithRequired(t => t.Book)
                .HasForeignKey(t => t.BookId);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Characters)
                .WithRequired(c => c.Book)
                .HasForeignKey(c => c.BookId);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Thumbnails)
                .WithRequired(t => t.Book)
                .HasForeignKey(t => t.BookId);

            modelBuilder.Entity<Tag>()
                .HasKey(t => new { t.TagId, t.Name });

            modelBuilder.Entity<Thumbnail>()
                .HasKey(t => new { t.ThumbnailId, t.Path });
        }
    }
}