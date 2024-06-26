using System.Data;

namespace ModuleDelegates_Task_2
{
    public class InvalidInputExeption : Exception
    {
        public InvalidInputExeption(string message) : base(message) { }
    }

    public class SortLastname
    { 
       public event Action<List<string>> OnSort;

        public void SortLastNames(List<string> lastNames, int sortOrder)
        {
            try
            {
                if (sortOrder != 1 && sortOrder != 2)
                {
                    throw new InvalidInputExeption("Invalid input. Enter 1 or 2.");
                }
                OnSort?.Invoke(lastNames);
            }
            catch (InvalidInputExeption ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Sorting is over.");
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            List<string> lastNames = new List<string>
            {
                "Sokolov",
                "Smirnov",
                "Belov",
                "Petrov",
                "Kuznetsov",
                "Ivanov"
            };

            SortLastname sortLastname = new SortLastname();
            sortLastname.OnSort += (List<string> list) =>
            {
                Console.WriteLine("Enter 1 to sort A-Z or 2 to sort Z-A:");
                string input = Console.ReadLine();
                int sortOrder;
                if (!int.TryParse(input, out sortOrder))
                {
                    throw new InvalidInputExeption("Invalid input, enter number 1 or 2.");

                }
                if (sortOrder == 1)
                {
                    list.Sort((a, b) => a.CompareTo(b));
                }
                else if (sortOrder == 2)
                {
                    list.Sort((a, b) => b.CompareTo(a));
                }
                Console.WriteLine("Sorted list:");
                foreach (var lastname in list)
                {
                    Console.WriteLine(lastname);
                }
            };

            sortLastname.SortLastNames(lastNames, 1);
        }
    }

   
}
