//Klasser som fångar beteenden för andra funktioner vil lägga i andra kataloger, ofta döpt till Domain.
using ProductManager.Domain;
using static System.Console;

namespace ProductManager;

class Program
{

  // List<T>, create a list object of type <Student> called products
  static List<Product> products = new List<Product>();

// Private - funktioner kan endast nås inom egna klassen
// Static - Du behöver inte en instans av ett objekt för att anropa funktionen
// Void - returnerar ingenting
// Public - funktioner kan nås inom andra klasser

//Funktion/Metod ska enbart göra en sak
  public static void Main()
  {

     CursorVisible = false;
        Title = "Product Manager";

        while (true)
        {
            WriteLine("1. Ny produkt");
            WriteLine("2. Sök produkt");
            WriteLine("3. Avsluta");

            // true to not display the pressed key
            var keyPressed = ReadKey(intercept: true);

            Clear();

            switch (keyPressed.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:

                    ShowRegisterProductView();

                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:

                    ShowSearchProductView();

                    break;

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:

                    Environment.Exit(0);

                    return;
            }

            Clear();
        }

  }

  private static void ShowRegisterProductView()
    {

        //Här sparas användarens input i referensvariabler (string)
        Write("Produktens namn: ");

        string productName = ReadLine();

        Write("SKU: ");

        string sku = ReadLine();

        Write("Beskrivning: ");

        string description = ReadLine();

        Write("Bild (url): ");

        string image = ReadLine();

        Write("Pris: ");

        string price = ReadLine();


            //variabel product = new Product från Domain?
            var product = new Product
            {
                ProductName = productName,
                SKU = sku,
                Description = description,
                Image = image,
                Price = price
            };

            try
            {
                //Skicka in produkten i metoden SaveProduct
                SaveProduct(product);
            }
            catch(Exception ex)
            {
                WriteLine(ex.Message);
            }


        Thread.Sleep(2000);
    }

    
    //Metod som hanterar .Add och tar product som parameter
    private static void SaveProduct(Product product)
    {

        if (products.Any(x => x.SKU == product.SKU))
        {
          throw new Exception("Produkt redan registrerad!");
        }

        else 
        {
          //lägg till produkt till listan. Representerar en databas/samling information i det här stadiet.
          products.Add(product);
          WriteLine("Produkt sparad");
        }

    }

    private static void ShowSearchProductView() 
    {
      Write("Ange produktens SKU: ");

      string sku = ReadLine();

      Clear();

      var product = GetProductBySKU(sku);

      //om villkoret (product inte(!=) är tomt/null) skriv ut info, annars skriv ut felmeddelande.
      if (product is not null)
      {
        WriteLine($"Produktens namn: {product.ProductName}");
        WriteLine($"SKU: {product.SKU}");
        WriteLine($"Beskrivning: {product.Description}");
        WriteLine($"Bild (url): {product.Image}");
        WriteLine($"Pris: {product.Price}");

        WriteLine("Tryck ESC för att gå tillbaka till menyn.");
        WaitUntil(ConsoleKey.Escape);
      }

      else
      {
        WriteLine("Kunde inte hitta produkten");

        Thread.Sleep(2000);
      }

    }

    // ? declares a nullable type, and means that the type before it may have a null value.
    private static Product? GetProductBySKU(string sku){

      //products = array
      //"x" is the parameter to the lambda expression. It represents each element in the "products" collection as it iterates over them.
      //"x.SKU == sku" is the condition we're checking.
      return products.Find(x => x.SKU == sku);
    }

    private static void WaitUntil(ConsoleKey key){
      while (ReadKey(true).Key != key);
    }

}

