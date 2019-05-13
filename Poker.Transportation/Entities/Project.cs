using System;

namespace Poker.Transportation.Entities
{
    public class Project
    {
        public virtual int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string YouTrackUrl { get; set; }
        public virtual string YouTrackQuery { get; set; }
        public virtual DateTime? DeletedAt { get; set; }
    }
}