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
                string? choice = Console.ReadLine()?.Trim();

                Console.Clear();
                switch (choice)
                {
                    case "1":
                        Console.Write("\nPodaj Hasło pracownicze: ");
                        string? password = Console.ReadLine()?.Trim();
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
                string? opt = Console.ReadLine()?.Trim();

                switch (opt)
                {
                    case "1":
                        Console.Clear();
                        magazyn.Catalogue();
                        break;
                    case "2":
                        AddProductInteractive(magazyn);
                        break;
                    case "3":
                        Console.Write("Nazwa produktu do usunięcia: ");
                        string? nameDel = Console.ReadLine()?.Trim();
                        Console.Clear();
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
                        string? n = Console.ReadLine()?.Trim();
                        Console.Clear();
                        Console.WriteLine($"W magazynie jest {magazyn.Stock(n)} sztuk produktu {n}.");
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Nieprawidłowa opcja.");
                        break;
                }
            }
        }
        private static void AddProductInteractive(WareHouse magazyn)
        {
            Console.WriteLine("\nWybierz typ produktu do dodania:\n     1 - Zabawka\n     2 - Napój\n     3 - Jedzenie\n     4 - Inny");
            Console.Write("Typ: ");
            string? t = Console.ReadLine()?.Trim();

            Console.Clear();
            string? name;
            do
            {
                Console.Write("Nazwa: ");
                name = Console.ReadLine()?.Trim();
                Console.Clear();
            } while (string.IsNullOrWhiteSpace(name));

            double price;
            do
            {
                Console.Clear();
                Console.Write("Cena: ");
            } while (!double.TryParse(Console.ReadLine()?.Trim().Replace('.', ','), out price) || price < 0.01 || (price.ToString().Contains(',') ? price.ToString().Split(',')[1].Length > 2 : false));


            Console.Write("Marka (Opcjonalnie): ");
            string brand = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Opis (Opcjonalnie): ");
            string desc = Console.ReadLine()?.Trim() ?? "";

            int qty;
            do
            {
                Console.Clear();
                Console.Write("Ilość: ");
            } while (!int.TryParse(Console.ReadLine()?.Trim(), out qty) || qty <= 0);


            Product prod;
            string? input;
            switch (t)
            {
                case "1":
                    int[] klasyfikacja_lista = { 3, 6, 10, 12, 16 };
                    int klasyfikacja;

                    do
                    {
                        Console.Clear();
                        Console.Write("Klasyfikacja wiekowa(3, 6, 10, 12, 16): ");
                    } while (!int.TryParse(Console.ReadLine()?.Trim(), out klasyfikacja) || !klasyfikacja_lista.Contains<int>(klasyfikacja));

                    do
                    {
                        Console.Write("Czy zawiera niebezpieczne elementy? (t/n): ");
                        input = Console.ReadLine()?.Trim().ToLower();
                        Console.Clear();
                    } while (input != "t" && input != "n");

                    Boolean haz = input == "t" ? true : false;

                    prod = new ToyProduct(name, price, brand, desc, klasyfikacja, haz);
                    break;
                case "2":

                    do
                    {
                        Console.Write("Czy niegazowany? (t/n): ");
                        input = Console.ReadLine()?.Trim().ToLower();
                        Console.Clear();
                    } while (input != "t" && input != "n");

                    Boolean still = input == "t" ? true : false;

                    do
                    {
                        Console.Write("Czy ZERO? (t/n): ");
                        input = Console.ReadLine()?.Trim().ToLower();
                        Console.Clear();
                    } while (input != "t" && input != "n");

                    Boolean zero = input == "t" ? true : false;
                    prod = new DrinkProduct(name, price, brand, desc, still, zero);
                    break;
                case "3":
                    do
                    {
                        Console.Write("Czy organiczne? (t/n): ");
                        input = Console.ReadLine()?.Trim().ToLower();
                        Console.Clear();
                    } while (input != "t" && input != "n");

                    Boolean org = input == "t" ? true : false;

                    do
                    {
                        Console.Write("Czy wegańskie? (t/n): ");
                        input = Console.ReadLine()?.Trim().ToLower();
                        Console.Clear();
                    } while (input != "t" && input != "n");

                    Boolean veg = input == "t" ? true : false;
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
                string? opt = Console.ReadLine()?.Trim();

                switch (opt)
                {
                    case "1":
                        Console.Clear();
                        magazyn.Catalogue();
                        break;
                    case "2":
                        Console.Write("Nazwa produktu: ");
                        string nAdd = Console.ReadLine() ?? "";
                        Console.Write("Ilość: ");
                        int qAdd = int.TryParse(Console.ReadLine(), out int qa) ? qa : 0;
                        Console.Clear();
                        koszyk.AddProductsFromWareHouse(magazyn, nAdd, qAdd);
                        break;
                    case "3":
                        Console.Write("Nazwa produktu: ");
                        string nRet = Console.ReadLine() ?? "";
                        Console.Write("Ilość: ");
                        int qRet = int.TryParse(Console.ReadLine(), out int qr) ? qr : 0;
                        Console.Clear();
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
                        Console.Clear();
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
    public Product(string name, double price, string brand, string description)
    {
        Name = name;
        Price = price;
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
    public FoodProduct(string name, double price, string brand, string description, Boolean isOrganic, Boolean isVegan) : base(name, price, brand, description)
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
    public DrinkProduct(string name, double price, string brand, string description, Boolean isStill, Boolean isZero) : base(name, price, brand, description)
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
    protected int klasyfikacja;
    protected Boolean Hazard;
    public ToyProduct(string name, double price, string brand, string description, int klasyfikacja, Boolean hazard) : base(name, price, brand, description)
    {
        this.klasyfikacja = klasyfikacja;
        Hazard = hazard;
    }
    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"O zabawce:\n     Od {klasyfikacja} lat.");
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
    public int Stock(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return 0;

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
        Console.Clear();
        if (products.Count == 0)
        {
            Console.WriteLine("\nKoszyk jest pusty — brak paragonu.\n");
            return;
        }
        string wynik = "";
        wynik += "PARAGON\n";
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
                wynik += $"     {nazwa} {cenaJednostkowa:0.00} zł × {ilosc} --- {suma:0.00} zł\n";
                lacznaCena += suma;
                seen.Add(nazwa);
            }
        }
        wynik += $"\nSuma do zapłaty:{lacznaCena:0.00} zł\n\n\n";
        Console.WriteLine(wynik);

        
        string? input;
        do
        {
            Console.Write("Zapisać paragon do pliku? (t/n): ");
            input = Console.ReadLine()?.Trim().ToLower();
        } while (input != "t" && input != "n");

        if (input == "n")
            return;

        do
        {

            try
            {
                File.WriteAllText("paragon.txt", wynik);
                break;
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Wystąpił nieoczekiwany błąd zapisu paragonu do pliku.\n");

                do
                {
                    Console.Write("Spróbować jeszcze raz? (t/n): ");
                    input = Console.ReadLine()?.Trim().ToLower();
                } while (input != "t" && input != "n");

                if (input == "n")
                    break;
            }

        } while (true);
        
    }
}