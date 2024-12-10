using DAL.EF.Configurations;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
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
