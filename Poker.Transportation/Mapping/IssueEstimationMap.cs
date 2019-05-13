using FluentNHibernate.Mapping;
using Poker.Transportation.Entities;

namespace Poker.Transportation.Mapping
{
    public class IssueEstimationMap : ClassMap<IssueEstimation>
    {
        public IssueEstimationMap()
        {
            Schema("core");

            Table("tIssueEstimation");

            Id(x => x.Id).GeneratedBy.Identity();

            References(x => x.ProjectUser).Not.Nullable()
                                          .Column("Fk_ProjectUser_Id");

            Map(x => x.Issue).Not.Nullable()
                             .Length(100);

            Map(x => x.Estimation).Not.Nullable()
                                .Length(100);

            Map(x => x.CreatedAt).Not.Nullable();

            Map(x => x.IsFinal).Not.Nullable();
        }
    }
}