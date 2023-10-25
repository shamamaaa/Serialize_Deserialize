using Newtonsoft.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace NamesJsonTask;
class Program
{
    private static string jsonpath;

    static void Main(string[] args)
    {

        string currentdirectory = Directory.GetCurrentDirectory();
        jsonpath = Path.Combine(currentdirectory, "names.json");
        if (!File.Exists(jsonpath))
        {
            File.Create(jsonpath).Close();
        }

        //--------------------------------------------------------------------

        List<string> names = new List<string> { "Samama", "Zulfiyya" };
        Serialize(names);
        Add("Sabuhi");
        Add("Xeyal");
        Add("Nigar");

        if (Search(name => name == "Asiman"))
        {
            Console.WriteLine("Asiman adli telebe var");
        }
        else
        {
            Console.WriteLine("Asiman tapilmadi");
        }

        Delete("Xeyal");
        ShowAllNames();
        Console.ReadLine();
    }


    public static void Add(string name)
    {
        List<string> names = Deserialize();
        if (names == null)
        {
            names = new();
        }
        names.Add(name);
        Serialize(names);
        Console.WriteLine($"{name} adli telebe add edildi");
    }

    public static bool Search(Predicate<string> name)
    {
        List<string> names = Deserialize();
        return names.Exists(name);
    }


    public static void Delete(string name)
    {
        List<string> names = Deserialize();

        if (names.Contains(name))
        {
            names.Remove(name);
            Console.WriteLine($"{name} adli telebe silindi");
            Serialize(names);
        }
        else
        {
            Console.WriteLine($"{name} adli telebe tapilmadi");
        }
    }

    public static void Serialize(List<string> names)
    {
        string result = JsonConvert.SerializeObject(names);

        using (StreamWriter sw = new(jsonpath))
        {
            sw.Write(result);
        }
    }

    public static List<string> Deserialize()
    {
        string result;
        using (StreamReader sr = new(jsonpath))
        {
            result = sr.ReadToEnd();
        }
        return JsonConvert.DeserializeObject<List<string>>(result);
    }

    public static void ShowAllNames()
    {
        Console.WriteLine("Adlar:");
        List<string> names = Deserialize();
        names.ForEach(a => Console.WriteLine(a));
    }
}

