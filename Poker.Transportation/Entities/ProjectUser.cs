using System;

namespace Poker.Transportation.Entities
{
    public class ProjectUser
    {
        public virtual int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Project Project { get; set; }
        public virtual int RoleId { get; set; }
        public virtual DateTime? DeletedAt { get; set; }
    }
}