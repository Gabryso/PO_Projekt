namespace Sklep_Internetowy_PO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WareHouse magazyn = new WareHouse();

            // Dodajemy produkty z ilością
            magazyn.AddProduct(new ToyProduct("Puzzle Pirackie", 6.33, "Arrrs", "Puzle z piratami", 10, true), 2);
            magazyn.AddProduct(new DrinkProduct("Cola", 12, "Pepsi", "Orzeźwiający napój", false, true), 3);
            magazyn.AddProduct(new FoodProduct("Krowa", 47.00, "Wołochy", "Duża krowa mleczna", true, true), 1);
            magazyn.AddProduct(new Product("Gun", 120.50, "Australian", "A big firearm from John Wick!!!"), 1);

            // Wyświetlamy katalog produktów
            magazyn.Catalogue();
        }
    }
}
public class Product
{
    protected string Name;
    protected double Price;
    protected string Brand;
    protected string Description;
    public Product(string name, double price = 0.0, string brand = "", string description = "")
    {
        Name = name;
        if (price > 0.0) Price = price;
        Brand = brand;
        Description = description;
    }
    public void DisplayInfo()
    {
        if (Brand != "") Console.WriteLine($"{Name} marki {Brand}:");
        else Console.WriteLine($"{Name}:");
        Console.WriteLine($"Cena:\n     {Price} zł\nOpis:\n     {Description}");

    }
    public string NamePublic
    {
        get { return Name; }
    }
}
public class FoodProduct : Product
{
    protected Boolean IsOrganic;
    protected Boolean IsVegan;   
    public FoodProduct(string name, double price, string brand, string description, Boolean isOrganic = false, Boolean isVegan = false) : base(name, price, brand, description)
    {
        IsOrganic = isOrganic;
        IsVegan = isVegan;
    }
    public new void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine("Typ żywności:");
        if (IsOrganic) Console.WriteLine("     Żywność organiczna.");
        else Console.WriteLine("     Żywność nieorganiczna.");
        if (IsVegan) Console.WriteLine("     VEGAN");
    }
}
public class DrinkProduct : Product
{
    protected Boolean IsStill;
    protected Boolean IsZero;
    public DrinkProduct(string name, double price, string brand, string description, Boolean isStill = true, Boolean isZero = false) : base(name, price, brand, description)
    {
        IsStill= isStill;
        IsZero = isZero;
    }
    public new void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine("Typ napoju:");
        if (IsStill) Console.WriteLine("     Napój niegazowany.");
        else Console.WriteLine("     Napój gazowany.");
        if (IsZero) Console.WriteLine("     Napój ZERO, nie zawiera cukru.");
        else Console.WriteLine("     Napój zawiera cukier.");
    }
}
public class ToyProduct : Product
{
    protected int PEGI;
    protected Boolean Hazard;
    public ToyProduct(string name, double price, string brand, string description, int pEGI = 3, bool hazard = true) : base(name, price, brand, description)
    {
        PEGI = pEGI;
        Hazard = hazard;
    }
    public new void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"O zabawce:\n     Od {PEGI} lat.");
        if (Hazard) Console.WriteLine("     UWAGA!!! Drobne elementy, trzymać zdala od dzieci!!!");
    }
}
public class WareHouse
{
    private List<Product> products = new List<Product>();
    public void AddProduct(Product product, int quantity)
    {
        for (int i = 0;  i<quantity; i++) products.Add(product);
    }
    public void RemoveProduct(string name)
    {
        products.RemoveAll(delegate (Product product)
        {
            return product.NamePublic == name;
        });
    }
    public int Stock(string name)
    {
        int countOut = 0;
        foreach (Product product in products) if (product.NamePublic == name) countOut++;
        return countOut;
    }
    public void Catalogue()
    {
        List<Product> sito = new List<Product>();
        Console.WriteLine("Katalog produktów:\n");
        Console.WriteLine("Zabawki:\n");
        foreach (Product product in products)
        {
            if (product is ToyProduct && !sito.Contains(product))
            {
                sito.Add(product);
                product.DisplayInfo();
                Console.WriteLine($"Ilość sztuk w magazynie:\n     {Stock(product.NamePublic)}\n");
            }
        }
        Console.WriteLine("Napoje:\n");
        foreach (Product product in products)
        {
            if (product is DrinkProduct && !sito.Contains(product))
            {
                sito.Add(product);
                product.DisplayInfo();
                Console.WriteLine($"Ilość sztuk w magazynie:\n     {Stock(product.NamePublic)}\n");
            }
        }
        Console.WriteLine("Jedzenie:\n");
        foreach (Product product in products)
        {
            if (product is FoodProduct && !sito.Contains(product))
            {
                sito.Add(product);
                product.DisplayInfo();
                Console.WriteLine($"Ilość sztuk w magazynie:\n     {Stock(product.NamePublic)}\n");
            }
        }
        Console.WriteLine("Pozostałe produkty:\n");
        foreach (Product product in products)
        {
            if (!(product is ToyProduct) && !(product is DrinkProduct) && !(product is FoodProduct) &&!sito.Contains(product))
            {
                sito.Add(product);
                product.DisplayInfo();
                Console.WriteLine($"Ilość sztuk w magazynie:\n     {Stock(product.NamePublic)}\n");
            }
        }
    }
}
