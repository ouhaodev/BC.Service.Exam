using BC.Service.Exam.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BC.Service.Exam.DataAccess.Configurations
{
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.ToTable("bc_candidate");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(200);
            builder.Property(e => e.Age).HasColumnName("age").HasColumnType("int");

            builder.Property(e => e.CreatedBy).HasColumnName("createdBy");
            builder.Property(e => e.CreatedDataTime).HasColumnName("createdDataTime");
            builder.Property(e => e.LastModifiedBy).HasColumnName("lastModifiedBy");
            builder.Property(e => e.LastModifiedDataTime).HasColumnName("lastModifiedDataTime");
        }
    }
}
