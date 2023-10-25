using Newtonsoft.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace NamesJsonTask;
class Program
{
    private static string jsonpath;

    static void Main(string[] args)
    {
        string dirpath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Files");
        Directory.CreateDirectory(dirpath);
        jsonpath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Files", "names.json");
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
        Add("Seid");

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
            Console.WriteLine($"{name} adli telebe tapilmadi, silinmedi");
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

