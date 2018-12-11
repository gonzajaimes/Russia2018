namespace Russia2018.Models
{
    public class GroupTeam
    {
        public int GroupTeamId { get; set; }

        public int GroupId { get; set; }

        public int TeamId { get; set; }

        public Group Group { get; set; }

        public Team Team { get; set; }
    }
}