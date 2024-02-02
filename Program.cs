using System.Globalization;

internal class Program
{
    static void Main(string[] args)
    {
        Contribuente.InserimentoDati();
        Contribuente.StampaDati();
    }
}

public static class Contribuente
{
    private static string nome;
    private static string cognome;
    private static string dataNascita;
    private static string sesso;
    private static string comuneResidenza;
    private static string codiceFiscale;
    private static decimal redditoAnnuale;

    public static void InserimentoDati()
    {
        Console.WriteLine("Inserisci nome: ");
        nome = UpperFirstLetter(Console.ReadLine());

        Console.WriteLine("Inserisci cognome: ");
        cognome = UpperFirstLetter(Console.ReadLine());

        do
        {
            Console.WriteLine("Inserisci data di nascita (dd/mm/yyyy): ");
            dataNascita = Console.ReadLine();
            if (!IsValidDate(dataNascita) || DateTime.ParseExact(dataNascita, "dd/MM/yyyy", CultureInfo.InvariantCulture) > DateTime.Now.Date)
            {
                Console.WriteLine("Formato data non valido. Inserisci la data nel formato dd/mm/yyyy. (Non puoi mettere una data successiva a quella odierna)");
            }
        } while (!IsValidDate(dataNascita) || DateTime.ParseExact(dataNascita, "dd/MM/yyyy", CultureInfo.InvariantCulture) > DateTime.Now.Date);

        do
        {
            Console.WriteLine("Inserisci sesso (M/F): ");
                sesso = Console.ReadLine().ToUpper();
            if (sesso != "M" && sesso != "F")
            { 
                Console.WriteLine("Errore: inserisci solo 'M' o 'F'!");
            }
        } while (sesso != "M" && sesso != "F");
        

        Console.WriteLine("Inserisci comune di residenza: ");
        comuneResidenza = UpperFirstLetter(Console.ReadLine());

        do
        {
            Console.WriteLine("Inserisci codice fiscale (16 caratteri): ");
                codiceFiscale = Console.ReadLine().ToUpper();
            if (codiceFiscale.Length != 16)
            {
                Console.WriteLine("Errore: il Codice Fiscale deve essere di 16 caratteri");
            }
        } while (codiceFiscale.Length != 16);
        
        Console.WriteLine("Inserisci reddito annuale: ");
        redditoAnnuale = Convert.ToDecimal(Console.ReadLine());
    }

    private static string UpperFirstLetter(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }
        return char.ToUpper(input[0]) +input.Substring(1).ToLower();
    }

    private static bool IsValidDate(string inputDate)
    {
        DateTime parsedDate;
        return DateTime.TryParseExact(inputDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
    }

    public static void StampaDati()
    {
        Console.WriteLine("");
        Console.WriteLine("==================================================");
        Console.WriteLine("");
        Console.WriteLine("CALCOLO DELL'IMPOSTA DA VERSARE:");
        Console.WriteLine($"Contribuente: {nome} {cognome},");
        Console.WriteLine($"Nato il {dataNascita}");
        Console.WriteLine($"Sesso (M/F): {sesso}");
        Console.WriteLine($"Residente in {comuneResidenza},");
        Console.WriteLine($"Codice Fiscale: {codiceFiscale}");
        Console.WriteLine($"Reddito annuale dichiarato: {redditoAnnuale} euro ");
        Console.WriteLine($"Imposta da versare: {CalcoloImposta()} euro");
        Console.WriteLine("");
        Console.WriteLine("==================================================");
        Console.WriteLine("");
    }
    
    public static decimal CalcoloImposta()
    {
        decimal imposta = 0;

        if(redditoAnnuale <= 15000)
        {
            imposta = redditoAnnuale * 0.23m;
        }
        else if (redditoAnnuale <= 28000)
        {
            imposta = 3450 + ((redditoAnnuale - 15000) * 0.27m);
        }
        else if (redditoAnnuale <= 55000)
        {
            imposta = 6960 + ((redditoAnnuale - 28000) * 0.38m);
        }
        else if (redditoAnnuale <= 75000)
        {
            imposta = 17220 + ((redditoAnnuale - 55000) * 0.41m);
        }
        else
        {
            imposta = 25420 + ((redditoAnnuale - 75000) * 0.43m);
        }

        return imposta;
    }
    
}
