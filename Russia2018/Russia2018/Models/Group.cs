namespace Russia2018.Models
{
    using System.Collections.Generic;

    public class Group
    {
        public int GroupId { get; set; }

        public string Name { get; set; }

        public List<GroupTeam> GroupTeams { get; set; }

        public List<Match> Matches { get; set; }

    }
}