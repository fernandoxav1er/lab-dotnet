using System.ComponentModel.DataAnnotations.Schema;

namespace FinShark.API.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public string AppUserId { get; set; }
        public int EstoqueId { get; set; }
        public AppUser AppUser { get; set; }
        public Estoque Estoque { get; set; }
    }
}
