using FluentNHibernate.Mapping;
using Poker.Transportation.Entities;

namespace Poker.Transportation.Mapping
{
    public class ProjectUserMap : ClassMap<ProjectUser>
    {
        public ProjectUserMap()
        {
            Schema("core");

            Table("tProjectUser");

            Id(x => x.Id).GeneratedBy.Identity();

            References(x => x.User).Not.Nullable()
                                          .Column("Fk_User_Id");

            References(x => x.Project).Not.Nullable()
                                   .Column("Fk_Project_Id");

            Map(x => x.RoleId).Not.Nullable()
                              .Column("Fk_Role_Id");

            Map(x => x.DeletedAt).Nullable();
        }
    }
}