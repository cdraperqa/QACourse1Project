namespace CodeLouisvilleUnitTestProject
{
    public class CargoItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

        public CargoItem(string Name, string Description, int Quantity)
        {
            this.Name = Name;
            this.Description = Description;
            this.Quantity = Quantity;
        }

    }
}
