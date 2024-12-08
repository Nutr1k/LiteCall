using DAL.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.EF.Configurations
{
	public class AccountForAdminConfiguration : IEntityTypeConfiguration<User>
	{
		private EntityTypeBuilder<User> _builder;
		private readonly IPasswordGenerator _passwordGenerator;

		public AccountForAdminConfiguration(IPasswordGenerator passwordGenerator)
		{
			_passwordGenerator = passwordGenerator;
		}

		//Для ApplyConfiguration
		public void Configure(EntityTypeBuilder<User> builder)
		{
			_builder = builder;
			CreateAdminAccount();
		}

		private void CreateAdminAccount()
		{
			string password = _passwordGenerator.GeneratePassword();

			_builder.HasData(new User { id = 1, Login = "Admin", Password = password, Role = "Admin", AnswerSecurityQ = string.Empty });


			//Переделать. Сделать общую систему логирования для всего сервера
			Console.WriteLine("Login:Admin\nPassword:" + password);
			//Запис в файл логирования логина и пароля
			//File.AppendAllText(Path.Combine(AppContext.BaseDirectory, "logger.txt"), $"Login:Admin\nPassword:{password}\n");
			//File.AppendAllText(Path.Combine(AppContext.BaseDirectory, "logger.txt"), "4=============================\n ");
		}
	}

	public interface IPasswordGenerator
	{
		string GeneratePassword();
	}

	class PassworSHA : IPasswordGenerator
	{
		public string GeneratePassword()
		{
			const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
			StringBuilder res = new StringBuilder();
			Random rnd = new Random();
			int length = 8;
			while (0 < length--)
			{
				res.Append(valid[rnd.Next(valid.Length)]);
			}
			return res.ToString().GetSha1().GetSha1();
		}
	}
}
