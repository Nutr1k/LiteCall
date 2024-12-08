using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Configurations
{
	class ServerInfoConfiguration : IEntityTypeConfiguration<Server>
	{
		private EntityTypeBuilder<Server> _builder;
		private IUidGenerator _uidGenerator;
		private string _title;
		private string _description;

		public ServerInfoConfiguration(string title,string description,IUidGenerator uidGenerator)
		{
			_uidGenerator = uidGenerator;
			_title = title;
			_description = description;
		}
		public void Configure(EntityTypeBuilder<Server> builder)
		{
			_builder = builder;
			AddServerInfo();
		}

		private void AddServerInfo()
		{
			_builder.HasData(new Server { id = 1, Title = _title, Description = _description, Ident = _uidGenerator.GenerateUid() });
		}
	}


	interface IUidGenerator
	{
		string GenerateUid();
	}

	class UID : IUidGenerator
	{
		public string GenerateUid()
		{
			StringBuilder builder = new StringBuilder();
			Enumerable
			   .Range(65, 26)
				.Select(e => ((char)e).ToString())
				.Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
				.Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
				.OrderBy(e => Guid.NewGuid())
				.Take(20)
				.ToList().ForEach(e => builder.Append(e));
			string _Ident = builder.ToString();

			return _Ident;
		}
	}
}
