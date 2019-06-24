namespace Poker.Model.Project
{
    public class ProjectModel
    {
        public int? Id { get; set; }

        public string Code { get; set; }

        public string YouTrackUrl { get; set; }

        public string YouTrackQuery { get; set; }

        public bool IsCurrentUserTeamLead { get; set; }
    }
}
