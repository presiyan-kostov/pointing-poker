using System;

namespace Poker.Transportation.Entities
{
    public class IssueEstimation
    {
        public virtual int Id { get; set; }
        public virtual ProjectUser ProjectUser { get; set; }
        public virtual string Issue { get; set; }
        public virtual int Estimation { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual bool IsFinal { get; set; }
    }
}