using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Net.Http;
using System.Net.Mime;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.EF.Configurations;
using DAL.Security;

namespace DAL.EF
{
    public class ServerAuthDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<SequrityQuestion> SecurityQuestions { get; set; }

        public ServerAuthDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={Path.Combine(AppContext.BaseDirectory, "LTdb_sqlite.db")}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            if (!File.Exists(Path.Combine(AppContext.BaseDirectory, "LTdb_sqlite.db")))
            {
                new KeyPairSetter(new KeyPairGenerator()).SetPublicPrivateKeys();

				string password = new PassworSHA().GeneratePassword();

				modelBuilder.Entity<User>().HasData(new User { id = 1, Login = "Admin", Password = password, Role = "Admin", AnswerSecurityQ = string.Empty });
				Console.WriteLine("Login:Admin\nPassword:" + password);


				modelBuilder.Entity<Server>().HasData(new Server { id = 1, Title = "LiteCall", Description = "Community Server", Ident = new UID().GenerateUid() });
				
                modelBuilder.ApplyConfiguration(new SequrityQuestionConfiguration(
									"Какое прозвище было у вас в детстве?" ,
									"Как звали вашего лучшего друга детства?",
								    "На какой улице вы жили в третьем классе?" ,
								    "Какую школу вы посещали в шестом классе?" ,
									"Как звали вашу первую плюшевую игрушку?" ,
									"В каком месте встретились ваши родители?" ,
									"Как звали вашего учителя в третьем классе?" ,
									"В каком городе живет ваш ближайший родственник?"));
			}
        }
    }
}
