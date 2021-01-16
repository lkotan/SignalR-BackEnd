using Chat.DataAccess.Extensions;
using Chat.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.DataAccess
{
    public class ChatContext:DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = modelBuilder.MapConfiguration();
            modelBuilder = modelBuilder.SetDataType();
            //modelBuilder = modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
