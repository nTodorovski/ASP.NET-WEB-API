using DomainModels.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainModels
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(20)]
        public string Combination { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Round { get; set; }
        public StatusEnum Status { get; set; }
        public int AwardBalance { get; set; }
    }
}
