using CR.RoomBooking.Data.Domain;
using CR.RoomBooking.Data.DomainConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.RoomBooking.Data.DomainConfigurations
{
    internal sealed class RoomConfiguration : BaseEntityConfiguration<Room>
    {
        public override void Configure(EntityTypeBuilder<Room> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Name).IsRequired();
        }
    }
}
