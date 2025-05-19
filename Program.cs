namespace TestTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* ----------  konfiguracja magazynu  ---------- */
            WareHouse magazyn = new WareHouse();

            magazyn.AddProduct(new ToyProduct("Puzzle Pirackie", 6.33, "Arrrs", "Puzle z piratami", 10, true), 2);
            magazyn.AddProduct(new DrinkProduct("Cola", 12, "Pepsi", "Orzeźwiający napój", false, true), 3);
            magazyn.AddProduct(new FoodProduct("Krowa", 47.00, "Wołochy", "Duża krowa mleczna", true, true), 1);
            magazyn.AddProduct(new Product("Gun", 120.50, "Australian", "A big firearm from John Wick!!!"), 1);

            Console.WriteLine("\n=== MAGAZYN – stan początkowy ===");
            magazyn.Catalogue();

            /* ----------  konfiguracja koszyka  ---------- */
            Cart koszyk = new Cart();

            // 1. poprawne dodanie 2 sztuk Coli
            koszyk.AddProductsFromWareHouse(magazyn, "Cola", 2);

            // 2. próba dodania 5 Col (w magazynie zostało 1) – powinno się nie udać
            koszyk.AddProductsFromWareHouse(magazyn, "Cola", 5);

            // 3. próba dodania produktu, którego nie ma
            koszyk.AddProductsFromWareHouse(magazyn, "FantastycznyNapój", 1);

            // 4. próba dodania ujemnej ilości
            koszyk.AddProductsFromWareHouse(magazyn, "Krowa", -3);

            // 5. dodanie 1 sztuki puzzli
            koszyk.AddProductsFromWareHouse(magazyn, "Puzzle Pirackie", 1);

            Console.WriteLine("\n=== KOSZYK – po dodawaniu ===");
            koszyk.ShowCart();

            Console.WriteLine("\n=== MAGAZYN – po przeniesieniu do koszyka ===");
            magazyn.Catalogue();

            /* ----------  zwroty  ---------- */

            // zwrot 1 Coli
            koszyk.ReturnProductsToWareHouse(magazyn, "Cola", 1);

            // próba zwrotu 3 Krow (w koszyku brak) – nie uda się
            koszyk.ReturnProductsToWareHouse(magazyn, "Krowa", 3);

            Console.WriteLine("\n=== KOSZYK – po zwrotach ===");
            koszyk.ShowCart();

            Console.WriteLine("\n=== MAGAZYN – stan końcowy ===");
            magazyn.Catalogue();

            Console.WriteLine("\n*** Test zakończony – naciśnij Enter ***");
            Console.ReadLine();
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
    public double PricePublic
    {
        get { return Price; }
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
        IsStill = isStill;
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
        for (int i = 0; i < quantity; i++) products.Add(product);
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
            if (!(product is ToyProduct) && !(product is DrinkProduct) && !(product is FoodProduct) && !sito.Contains(product))
            {
                sito.Add(product);
                product.DisplayInfo();
                Console.WriteLine($"Ilość sztuk w magazynie:\n     {Stock(product.NamePublic)}\n");
            }
        }
    }
    public List<Product> GetAllProducts()
    {
        return products;
    }
    public void RemoveSingleProduct(Product p)
    {
        products.Remove(p);
    }
}
public class Cart
{
    private List<Product> products = new List<Product>();
    public Boolean AddProductsFromWareHouse(WareHouse wareHouse, string name, int quantity)
    {
        if (quantity <= 0)
        {
            Console.WriteLine("Błędna ilość produktu podana, liczba musi być większa od zera!\nProduktów nie dodano do koszyka.");
            return false;
        }
        int stock = wareHouse.Stock(name);
        if (stock == 0)
        {
            Console.WriteLine($"Produktu {name} nie ma na magazynie!\nProduktów nie dodano do koszyka.");
            return false;
        }
        if (stock < quantity)
        {
            Console.WriteLine($"W magazynie jest tylko {stock} sztuk produktu!\nProduktów nie dodano do koszyka.");
            return false;
        }
        int countCart = 0;
        foreach (Product product in wareHouse.GetAllProducts().ToList())
        {
            if (product.NamePublic == name && countCart < quantity)
            {
                products.Add(product);
                wareHouse.RemoveSingleProduct(product);
                countCart++;
            }
        }
        Console.WriteLine($"Dodano {quantity} sztuk produktu {name} do koszyka.");
        return true;
    }
    public bool ReturnProductsToWareHouse(WareHouse wareHouse, string name, int quantity)
    {
        if (quantity <= 0)
        {
            Console.WriteLine("Błędna ilość produktu podana, liczba musi być większa od zera!\nNic nie zwrócono.");
            return false;
        }
        int inCart = products.Count(p => p.NamePublic == name);
        if (inCart == 0)
        {
            Console.WriteLine($"Produkt {name} nie znajduje się w koszyku.");
            return false;
        }
        if (inCart < quantity)
        {
            Console.WriteLine($"W koszyku jest tylko {inCart} szt. (żądano {quantity})!\nNic nie zwrócono.");
            return false;
        }

        int zwrocone = 0;
        foreach (Product p in products.ToList())
        {
            if (p.NamePublic == name && zwrocone < quantity)
            {
                wareHouse.AddProduct(p, 1);
                products.Remove(p);
                zwrocone++;
            }
        }

        Console.WriteLine($"Zwrócono {zwrocone} szt. {name} do magazynu.");
        return true;
    }
    public void ShowCart()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("\nKoszyk jest pusty.\n");
            return;
        }

        Console.WriteLine("\n=== Zawartość koszyka ===\n");
        List<Product> seen = new List<Product>();
        foreach (Product p in products)
        {
            if (!seen.Contains(p))
            {
                p.DisplayInfo();
                Console.WriteLine($"Ilość w koszyku: {products.Count(x => x.NamePublic == p.NamePublic)}\n");
                seen.Add(p);
            }
        }
        Console.WriteLine($"Łączna wartość: {products.Sum(p => p.PricePublic)} zł\n");
    }
}
