using ElitTournament.DAL.Entities.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElitTournament.DAL.Entities
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }

        public BaseEntity()
        {
            CreationDate = DateTime.UtcNow;
        }
    }
}
