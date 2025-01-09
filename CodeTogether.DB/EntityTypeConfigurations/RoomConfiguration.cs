using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CodeTogether.DTO;


namespace CodeTogether.DB.EntityTypeConfigurations
{
	internal class RoomConfiguration: IEntityTypeConfiguration<Room>
	{
		public void Configure(EntityTypeBuilder<Room> builder)
		{
			builder.HasKey(room => room.Id);
			builder.HasIndex(room => room.Id).IsUnique();
		}
	}
}
