namespace Russia2018.Models
{
    using System.Collections.Generic;

    public class BoardStatus
    {
        
        public int BoardStatusId { get; set; }

        public string Name { get; set; }

        public virtual List<Board> Boards { get; set; }
    }
}