namespace TaskCruds.Models
{
    public class Inventory
    {
        public int Id { get; set; } // Primary key (usually an auto-incremented ID)
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemUnit { get; set; }
        public decimal ItemBalanceInSystem { get; set; }
        public decimal ItemBalanceInStore { get; set; }
        public decimal DamageMaterial { get; set; }
        public decimal ExpiredMaterial { get; set; }
        public decimal NetMaterial { get; set; }
    }
}

