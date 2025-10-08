using System.Text.Json.Serialization;

namespace Livraria.Entities
{
    public class BookSubscription
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public DateTime SubscribedAt { get; set; } = DateTime.Now;
        
        [JsonIgnore]
        public Book Book { get; set; } = null!;
        [JsonIgnore]
        public Customer Customer { get; set; } = null!;
    }
}
