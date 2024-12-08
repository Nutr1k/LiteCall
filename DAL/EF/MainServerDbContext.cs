using DAL.EF.Configurations;
using DAL.Entities;
using DAL.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Text;

namespace DAL.EF
{
	public class MainServerDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Server> Servers { get; set; }
		public DbSet<SequrityQuestion> SecurityQuestions { get; set; }
		public MainServerDbContext()
		{
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite($"Data Source={Path.Combine(AppContext.BaseDirectory, "MainServer.db")}");
		}


		//Причины для изменения OnModelCreating
		//1.Изменение способа генерации пароля и логина для администратора+
		//2.Изменение спопосба логирования, будь то запись в файл или БД
		//3.Изменение серкетных вопрос как по тексту так и по колличеству
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            
			if (!File.Exists(Path.Combine(AppContext.BaseDirectory, "MainServer.db")))
			{
				
				modelBuilder.ApplyConfiguration(new AccountForAdminConfiguration(new PassworSHA()));

				//Запис в БД секретных вопросов
				modelBuilder.ApplyConfiguration(new SequrityQuestionConfiguration(
									"Какое прозвище было у вас в детстве?",
									"Как звали вашего лучшего друга детства?",
									"На какой улице вы жили в третьем классе?",
									"Какую школу вы посещали в шестом классе?",
									"Как звали вашу первую плюшевую игрушку?",
									"В каком месте встретились ваши родители?",
									"Как звали вашего учителя в третьем классе?",
									"В каком городе живет ваш ближайший родственник?"));



			}
		}


	}
}
