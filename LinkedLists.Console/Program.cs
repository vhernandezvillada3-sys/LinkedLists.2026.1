using DoubleList;
using SimpleList;

//var list = new SinglyLinkedList<string>();
var list = new DoubleLinkedList<string>();
var option = string.Empty;
var value = string.Empty;
do
{
    option = Menu();
    switch (option)
    {
        case "1":
            Console.Write("Enter a value: ");
            value = Console.ReadLine() ?? string.Empty;
            list.InsertOrdered(value);
            break;

        case "2":
            Console.WriteLine(list.ToString());
            break;

        case "3":
            Console.WriteLine(list.ToStringReverse());
            break;

        case "4":
            list.SortDescending();
            Console.WriteLine("List sorted in descending order.");
            break;

        case "5":
            list.ShowMode();
            break;

        case "6":
            list.ShowGraph();
            break;

        case "7":
            Console.Write("Enter a value: ");
            value = Console.ReadLine() ?? string.Empty;
            var exists = list.Contains(value);
            if (exists)
            {
                Console.WriteLine($"Value '{value}' exists in the list.");
            }
            else
            {
                Console.WriteLine($"Value '{value}' does not exist in the list.");
            }
            break;

        case "8":
            Console.Write("Enter a value: ");
            value = Console.ReadLine() ?? string.Empty;
            list.Remove(value);
            Console.WriteLine($"First occurrence of '{value}' removed (if existed).");
            break;

        case "9":
            Console.Write("Enter a value: ");
            value = Console.ReadLine() ?? string.Empty;
            list.RemoveAllOccurrences(value);
            Console.WriteLine($"All occurrences of '{value}' removed.");
            break;

        case "0":
            Console.WriteLine("Exiting...");
            break;

        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
} while (option != "0");

string Menu()
{
    Console.WriteLine("1. Add (insert in ascending order)");
    Console.WriteLine("2. Show forward");
    Console.WriteLine("3. Show backward");
    Console.WriteLine("4. Sort descending");
    Console.WriteLine("5. Show mode(s)");
    Console.WriteLine("6. Show graph");
    Console.WriteLine("7. Exists");
    Console.WriteLine("8. Remove one occurrence");
    Console.WriteLine("9. Remove all occurrences");
    Console.WriteLine("0. Exit");
    Console.Write("Enter your option: ");
    return Console.ReadLine() ?? string.Empty;
}