
namespace Russia2018.Models
{
    using System.Collections.Generic;
    using SQLite.Net.Attributes;

    public class UserType
    {
        [PrimaryKey]
        public int UserTypeId { get; set; }
        
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
