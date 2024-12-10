using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
	class UID
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
