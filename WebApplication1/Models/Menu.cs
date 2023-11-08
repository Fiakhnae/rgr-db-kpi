namespace WebApplication1.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public int restaurantId { get; set; }
        public string dishname { get; set; }
        public int price { get; set; }
    }
}
