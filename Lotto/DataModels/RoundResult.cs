﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainModels
{
    public class RoundResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoundId { get; set; }
        [StringLength(20)]
        public string WinningCombination { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
