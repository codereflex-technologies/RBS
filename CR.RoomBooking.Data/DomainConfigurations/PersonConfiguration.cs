using CR.RoomBooking.Data.Domain;
using CR.RoomBooking.Data.DomainConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.RoomBooking.Data.DomainConfigurations
{
    internal sealed class PersonConfiguration : BaseEntityConfiguration<Person>
    {
        public override void Configure(EntityTypeBuilder<Person> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.FirstName).IsRequired();
            builder.Property(e => e.LastName).IsRequired();
            builder.Property(e => e.PhoneNumber).IsRequired();
            builder.Property(e => e.Email);
            builder.Property(e => e.DateOfBirth);
        }
    }
}