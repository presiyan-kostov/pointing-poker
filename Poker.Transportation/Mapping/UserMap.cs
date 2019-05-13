using FluentNHibernate.Mapping;
using Poker.Transportation.Entities;

namespace Poker.Transportation.Mapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Schema("core");

            Table("tUser");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Username).Not.Nullable()
                                .Length(100);
            Map(x => x.Password).Not.Nullable()
                                .Length(100);
            Map(x => x.Firstname).Not.Nullable()
                                 .Length(100);
            Map(x => x.Lastname).Not.Nullable()
                                .Length(100);
            Map(x => x.Email).Not.Nullable()
                             .Length(250);
            Map(x => x.IsAdmin).Not.Nullable();
            Map(x => x.DeletedAt).Nullable();
        }
    }
}