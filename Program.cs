//Klasser som fångar beteenden för andra funktioner vill vi lägga i andra kataloger, ofta döpt till Domain.
using ProductManager.Domain;
using static System.Console;

namespace ProductManager;

class Program
{
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
        bool productInfoConfirmed = false;
        while(!productInfoConfirmed)
        {
          
        //Här sparas användarens input i referensvariabler (string)
        string productName = GetUserInput("Produktens namn: ");
        string sku = GetUserInput("SKU: ");
        string description = GetUserInput("Beskrivning: ");
        string image = GetUserInput("Bild (url): ");
        string price = GetUserInput("Pris: ");

            //variabel product = new Product från Domain
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
              WriteLine("Är detta korrekt?  (J)a  (N)ej");

              var confirmProductInfo = ReadKey(intercept:true);

              if (confirmProductInfo.Key == ConsoleKey.J)
              {
                //Skicka in produkten i metoden SaveProduct
                SaveProduct(product);
                WriteLine("Produkt registrerad");
                Thread.Sleep(2000);
                productInfoConfirmed = true;
              }
                
            }
            catch
            {
                WriteLine("Produkt redan registrerad");
                Thread.Sleep(2000);
                //WriteLine(ex.Message);
            }
        Clear();
        }
    }

    
    //Metod som hanterar .Add och tar product som parameter
    private static void SaveProduct(Product product)
    {
        using var context = new ApplicationDbContext();

        context.Product.Add(product);

        context.SaveChanges();
    }

    private static void DeleteProduct(Product product)
    {
        using var context = new ApplicationDbContext();

        context.Product.Remove(product);

        context.SaveChanges();
    }

    private static void ShowSearchProductView() 
    {
      string sku = GetUserInput("Ange produktens SKU: ");

      Clear();

      var product = GetProductBySKU(sku);

      //om villkoret (product inte(!=) är tomt/null) skriv ut info, annars skriv ut felmeddelande.
      if (product is not null)
      {
          bool removalConfirmed = false;

          while (!removalConfirmed)
          {
            Clear();

            WriteLine($"Produktens namn: {product.ProductName}");
            WriteLine($"SKU: {product.SKU}");
            WriteLine($"Beskrivning: {product.Description}");
            WriteLine($"Bild (url): {product.Image}");
            WriteLine($"Pris: {product.Price}");
            WriteLine(" ");
            WriteLine("(R)adera");
            WriteLine("Tryck ESC för att gå tillbaka till menyn.");


            var keyPressed = ReadKey(intercept: true);
            if(keyPressed.Key == ConsoleKey.R)
            {
              Clear();
              WriteLine("Är du säker på att du vill radera produkten?");
              WriteLine("(J)a   (N)ej");

              var confirmDelete = ReadKey(intercept:true);

              if(confirmDelete.Key == ConsoleKey.J)
              {
                Clear();
                DeleteProduct(product);
                WriteLine("Produkten har raderats");
                Thread.Sleep(2000);
                removalConfirmed = true; //Set boolean to true to exit the loop
              }

            }
            else if (keyPressed.Key == ConsoleKey.Escape)
            {
              return;
            }
          }
      }

      else
      {
        WriteLine("Kunde inte hitta produkten");
        Thread.Sleep(2000);
      }

    }

    // ? declares a nullable type, and means that the type before it may have a null value.
    private static Product? GetProductBySKU(string sku){

      using var context = new ApplicationDbContext();

      var product = context
            .Product
            .FirstOrDefault(product => product.SKU == sku);

      return product;
    }

    private static string GetUserInput(string descriptionTag)
    {
        Write($"{descriptionTag}");

        return ReadLine() ?? "";
    }

}

