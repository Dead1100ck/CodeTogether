using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CodeTogether.DTO;


namespace CodeTogether.DB.EntityTypeConfigurations
{
	internal class UserConfiguration: IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(user => user.Id);
			builder.HasIndex(user => user.Id).IsUnique();
		}
	}
}
