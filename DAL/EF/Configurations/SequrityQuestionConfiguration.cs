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
	class SequrityQuestionConfiguration : IEntityTypeConfiguration<SequrityQuestion>
	{
		private EntityTypeBuilder<SequrityQuestion> _builder;
		private List<string> _questions;
		public SequrityQuestionConfiguration(params string[] sequrityQuestions) 
		{
			_questions = sequrityQuestions.ToList();
		}
		public void Configure(EntityTypeBuilder<SequrityQuestion> builder)
		{
			_builder = builder;
			AddSequrityQuestion();
		}

		private void AddSequrityQuestion()
		{
			for (int i = 1; i < _questions.Count; i++)
			{
				_builder.HasData(new SequrityQuestion { id = i, Questions = _questions[i-1] });
			}
		}
	}
}
