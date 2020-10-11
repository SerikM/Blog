using Data.Aurora.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Data.Aurora.Entities
{
    public class BlogPost : IEntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
