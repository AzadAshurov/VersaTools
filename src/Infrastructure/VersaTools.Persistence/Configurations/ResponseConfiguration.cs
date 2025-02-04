using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VersaTools.Domain.Entitities;

namespace VersaTools.Persistence.Configurations
{
    internal class ResponseConfiguration : IEntityTypeConfiguration<Response>
    {
        public void Configure(EntityTypeBuilder<Response> builder)
        {
            builder.Property(x => x.ResponseText).IsRequired().HasMaxLength(2047);
         
        }
    }
}
