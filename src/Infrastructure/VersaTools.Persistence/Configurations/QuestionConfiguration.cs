using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VersaTools.Domain.Entitities;

namespace VersaTools.Persistence.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(123);
            builder.Property(x => x.MainText).IsRequired().HasMaxLength(2047);
        }
    }
}
