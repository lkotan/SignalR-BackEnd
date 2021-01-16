using Chat.Core.Enums;
using Chat.Core.Helpers;
using Chat.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Chat.DataAccess.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder MapConfiguration(this ModelBuilder mb)
        {
            return mb.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public static ModelBuilder SetDataType(this ModelBuilder mb)
        {
            foreach (var fk in mb.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()).Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade))
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
            return mb;
        }
        public static ModelBuilder Seed(this ModelBuilder mb)
        {
            HashingHelper.CreatePasswordHash("12345", out var passwordHash, out var passwordSalt);
            mb.Entity<Account>().HasData(
            new Account
            {
                Id = 1,
                Email = "lutfikotann@gmail.com",
                UserName = "lKotan",
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                AccountType = AccountType.SuperAdmin,
                RefreshToken = Helper.CreateToken(),
                RefreshTokenExpiredDate = DateTime.Now.AddDays(-1),
            });
            return mb;
        }
    }
}
