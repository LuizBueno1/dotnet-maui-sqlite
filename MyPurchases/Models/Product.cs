using SQLite;

namespace MyPurchases.Models
{
    public class Product
    {
        string _description;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Description 
        {
            get => _description;
            set
            {
                if (value == null) 
                {
                    throw new Exception("Please, fill in the description.");
                }

                _description = value;
            }
        }
        public double Amount { get; set; }
        public double Price { get; set; }
        public double Total { get => Amount * Price;}
    }
}
