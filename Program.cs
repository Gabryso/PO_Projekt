namespace Sklep_Internetowy_PO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WareHouse magazyn = new WareHouse();
            Cart koszyk = new Cart();
            Boolean session = true;
            while (session)
            {
                Console.WriteLine("\nGŁÓWNE MENU\n     1 - Administrator\n     2 - Użytkownik\n     0 - Zakończ program");
                Console.Write("Opcja: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("\nPodaj Hasło pracownicze: ");
                        string? password = Console.ReadLine();
                        if (password == "qwerty") AdminMenu(magazyn);
                        else Console.WriteLine("\nBłędne hasło, powrót do menu głównego.");
                        break;
                    case "2":
                        UserMenu(magazyn, koszyk);
                        break;
                    case "0":
                        session = false;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja.");
                        break;
                }
            }
        }
        private static void AdminMenu(WareHouse magazyn)
        {
            Boolean back = false;
            while (!back)
            {
                Console.WriteLine("\nMENU ADMINISTRATORA\n     1 - Pokaż katalog\n     2 - Dodaj produkt\n     3 - Usuń produkt całkowicie\n     4 - Sprawdź stan produktu\n     0 - Powrót");
                Console.Write("Opcja: ");
                string? opt = Console.ReadLine();
                switch (opt)
                {
                    case "1":
                        magazyn.Catalogue();
                        break;
                    case "2":
                        AddProductInteractive(magazyn);
                        break;
                    case "3":
                        Console.Write("Nazwa produktu do usunięcia: ");
                        string? nameDel = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(nameDel))
                        {
                            magazyn.RemoveProduct(nameDel);
                            Console.WriteLine("Usunięto wszystkie sztuki produktu z magazynu (jeśli istniał).");
                        }
                        else
                        {
                            Console.WriteLine("Nie usunięto żadnego produktu, ponieważ nie podano nazwy.");
                        }
                        break;
                    case "4":
                        Console.Write("Podaj nazwę produktu: ");
                        string? n = Console.ReadLine();
                        Console.WriteLine($"W magazynie jest {magazyn.Stock(n)} sztuk produktu {n}.");
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja.");
                        break;
                }
            }
        }
        private static void AddProductInteractive(WareHouse magazyn)
        {
            Console.WriteLine("\nWybierz typ produktu do dodania:\n     1 - Zabawka\n     2 - Napój\n     3 - Jedzenie\n     4 - Inny");
            Console.Write("Typ: ");
            string? t = Console.ReadLine();

            string? name;
            do
            {
                Console.Write("Nazwa: ");
                name = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(name));
            name = name.Trim();

            double price;
            do
            {
                Console.Write("Cena: ");
            } while (!double.TryParse(Console.ReadLine() ?? "".Replace('.', ','), out price) || price <= 0);


            Console.Write("Marka (Opcjonalnie): ");
            string? brand = Console.ReadLine();
            Console.Write("Opis: (Opcjonalnie)");
            string? desc = Console.ReadLine();

            int qty;
            do
            {
                Console.Write("Ilość: ");
            } while (!int.TryParse(Console.ReadLine(), out qty) || qty <= 0);


            Product prod;
            switch (t)
            {
                case "1":
                    Console.Write("Wiek PEGI: ");
                    int pegi = int.TryParse(Console.ReadLine(), out int pg) ? pg : 3;
                    Console.Write("Czy hazardowe elementy? (t/n): ");
                    Boolean haz = Console.ReadLine().ToLower().StartsWith("t");
                    prod = new ToyProduct(name, price, brand, desc, pegi, haz);
                    break;
                case "2":
                    Console.Write("Czy niegazowany? (t/n): ");
                    Boolean still = Console.ReadLine().ToLower().StartsWith("t");
                    Console.Write("Czy ZERO? (t/n): ");
                    Boolean zero = Console.ReadLine().ToLower().StartsWith("t");
                    prod = new DrinkProduct(name, price, brand, desc, still, zero);
                    break;
                case "3":
                    Console.Write("Czy organiczne? (t/n): ");
                    Boolean org = Console.ReadLine().ToLower().StartsWith("t");
                    Console.Write("Czy wegańskie? (t/n): ");
                    Boolean veg = Console.ReadLine().ToLower().StartsWith("t");
                    prod = new FoodProduct(name, price, brand, desc, org, veg);
                    break;
                case "4":
                    prod = new Product(name, price, brand, desc);
                    break;
                default:
                    Console.WriteLine("Nieznany typ produktu.");
                    return;
            }
            magazyn.AddProduct(prod, qty);
            Console.WriteLine($"Dodano {qty} szt. {name} do magazynu.");
        }
        private static void UserMenu(WareHouse magazyn, Cart koszyk)
        {
            Boolean back = false;
            while (!back)
            {
                Console.WriteLine("\nMENU UŻYTKOWNIKA\n     1 - Pokaż katalog\n     2 - Dodaj do koszyka\n     3 - Zwróć do magazynu\n     4 - Pokaż koszyk\n     5 - Wystaw paragon\n     0 - Powrót");
                Console.Write("Opcja: ");
                string? opt = Console.ReadLine();
                switch (opt)
                {
                    case "1":
                        magazyn.Catalogue();
                        break;
                    case "2":
                        Console.Write("Nazwa produktu: ");
                        string? nAdd = Console.ReadLine();
                        Console.Write("Ilość: ");
                        int qAdd = int.TryParse(Console.ReadLine(), out int qa) ? qa : 0;
                        koszyk.AddProductsFromWareHouse(magazyn, nAdd, qAdd);
                        break;
                    case "3":
                        Console.Write("Nazwa produktu: ");
                        string? nRet = Console.ReadLine();
                        Console.Write("Ilość: ");
                        int qRet = int.TryParse(Console.ReadLine(), out int qr) ? qr : 0;
                        koszyk.ReturnProductsToWareHouse(magazyn, nRet, qRet);
                        break;
                    case "4":
                        koszyk.ShowCart();
                        break;
                    case "5":
                        koszyk.Receipt();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Błędna opcja.");
                        break;
                }
            }
        }
    }
}
public class Product
{
    protected string Name;
    protected double Price;
    protected string Brand;
    protected string Description;
    public Product(string name, double price, string brand = "", string description = "")
    {
        Name = name;
        if (price > 0.0) Price = price;
        Brand = brand;
        Description = description;
    }
    public virtual void DisplayInfo()
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
    public override void DisplayInfo()
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
    public override void DisplayInfo()
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
    public ToyProduct(string name, double price, string brand, string description, int pEGI = 3, Boolean hazard = true) : base(name, price, brand, description)
    {
        PEGI = pEGI;
        Hazard = hazard;
    }
    public override void DisplayInfo()
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
    public Boolean ReturnProductsToWareHouse(WareHouse wareHouse, string name, int quantity)
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
        Console.WriteLine("\nZAWARTOŚĆ KOSZYKA\n");
        List<string> seen = new List<string>();
        double lacznaCena = 0.0;
        foreach (Product p in products)
        {
            if (!seen.Contains(p.NamePublic))
            {
                p.DisplayInfo();
                int ilosc = 0;
                foreach (Product x in products)
                {
                    if (x.NamePublic == p.NamePublic)
                    {
                        ilosc++;
                        lacznaCena += x.PricePublic;
                    }
                }

                Console.WriteLine($"Ilość w koszyku: {ilosc}\n");
                seen.Add(p.NamePublic);
            }
        }
        Console.WriteLine($"Łączna wartość: {lacznaCena:0.00} zł\n");
    }
    public void Receipt()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("\nKoszyk jest pusty — brak paragonu.\n");
            return;
        }
        Console.WriteLine("\nPARAGON\n");
        List<string> seen = new List<string>();
        double lacznaCena = 0.0;
        foreach (Product p in products)
        {
            string nazwa = p.NamePublic;
            if (!seen.Contains(nazwa))
            {
                int ilosc = 0;
                double cenaJednostkowa = p.PricePublic;
                double suma = 0.0;
                foreach (Product x in products)
                {
                    if (x.NamePublic == nazwa)
                    {
                        ilosc++;
                        suma += x.PricePublic;
                    }
                }
                Console.WriteLine($"     {nazwa} {cenaJednostkowa:0.00} zł × {ilosc} --- {suma:0.00} zł");
                lacznaCena += suma;
                seen.Add(nazwa);
            }
        }
        Console.WriteLine($"\nSuma do zapłaty:{lacznaCena:0.00} zł\n");
    }
}