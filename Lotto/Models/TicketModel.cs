using DomainModels.Enum;

namespace Models
{
    public class TicketModel
    {
        public string Combination { get; set; }
        public int Round { get; set; }
        public StatusEnum Status { get; set; }
        public int AwardBalance { get; set; }
    }
}
