using FluentNHibernate.Mapping;
using Poker.Transportation.Entities;

namespace Poker.Transportation.Mapping
{
    public class ProjectMap : ClassMap<Project>
    {
        public ProjectMap()
        {
            Schema("core");

            Table("tProject");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Code).Not.Nullable()
                                .Length(100);

            Map(x => x.YouTrackUrl).Not.Nullable()
                            .Length(100);

            Map(x => x.YouTrackQuery).Not.Nullable()
                            .Length(100);

            Map(x => x.DeletedAt).Nullable();
        }
    }
}