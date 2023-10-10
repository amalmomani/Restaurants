namespace Restaurant.Models
{
    public class JoinTable
    {
        public Product product { get; set; }
        public Category category { get; set; }
        public Customer customer { get; set; }
        public Productcustomer productcustomer { get; set; }
    }
}
