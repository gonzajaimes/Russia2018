namespace Russia2018.Models
{
    using System.Collections.Generic;

    public class StatusMatch
    {
       
        public int StatusMatchId { get; set; }

        public string Name { get; set; }

        public virtual List<Match> Matches { get; set; }
    }
}