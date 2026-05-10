using Shared;

namespace DoubleList;

public class DoubleLinkedList<T> : ILinkedList<T>
{
    private Node<T>? _head;
    private Node<T>? _tail;

    public DoubleLinkedList()
    {
        _head = null;
        _tail = null;
    }

    public bool Contains(T data)
    {
        var current = _head;
        while (current != null)
        {
            if (current.Data != null && current.Data.Equals(data))
            {
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    public void InsertAtBeginning(T data)
    {
        var newNode = new Node<T>(data);
        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Next = _head;
            _head.Previous = newNode;
            _head = newNode;
        }
    }

    public void InsertAtEnding(T data)
    {
        var newNode = new Node<T>(data);
        if (_tail == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            _tail.Next = newNode;
            newNode.Previous = _tail;
            _tail = newNode;
        }
    }

    public void InsertOrdered(T data)
    {
        var newNode = new Node<T>(data);

        // Case 1: Empty list
        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
            return;
        }

        // Compare using IComparable (works for numbers and strings)
        var comparer = System.Collections.Generic.Comparer<T>.Default;

        // Case 2: New data is smaller than head (insert at beginning)
        if (comparer.Compare(data, _head.Data) <= 0)
        {
            newNode.Next = _head;
            _head.Previous = newNode;
            _head = newNode;
            return;
        }

        // Case 3: New data is larger than tail (insert at ending)
        if (comparer.Compare(data, _tail!.Data) >= 0)
        {
            _tail.Next = newNode;
            newNode.Previous = _tail;
            _tail = newNode;
            return;
        }

        // Case 4: Insert in the middle
        var current = _head;
        while (current != null)
        {
            if (comparer.Compare(data, current.Data) <= 0)
            {
                // Insert before current
                newNode.Previous = current.Previous;
                newNode.Next = current;
                current.Previous!.Next = newNode;
                current.Previous = newNode;
                return;
            }
            current = current.Next;
        }
    }

    public void Remove(T data)
    {
        var current = _head;
        while (current != null)
        {
            if (current.Data!.Equals(data))
            {
                if (current == _head) // Found at the head
                {
                    _head = _head.Next;
                    _head!.Previous = null;
                }
                else if (current == _tail) // Found at the tail
                {
                    _tail = _tail.Previous;
                    _tail!.Next = null;
                }
                else // Found in the middle
                {
                    current.Previous!.Next = current.Next;
                    current.Next!.Previous = current.Previous;
                }
                return;
            }
            current = current.Next;
        }
    }

    public void RemoveAllOccurrences(T data)
    {
        if (_head == null) return;

        var current = _head;
        while (current != null)
        {
            var next = current.Next;

            if (current.Data != null && current.Data.Equals(data))
            {
                // Found a match, remove this node
                if (current == _head) // Removing head
                {
                    _head = _head.Next;
                    if (_head != null)
                        _head.Previous = null;
                    else
                        _tail = null;
                }
                else if (current == _tail) // Removing tail
                {
                    _tail = _tail.Previous;
                    if (_tail != null)
                        _tail.Next = null;
                    else
                        _head = null;
                }
                else // Removing from middle
                {
                    current.Previous!.Next = current.Next;
                    current.Next!.Previous = current.Previous;
                }
            }
            current = next;
        }
    }

    public void Reverse()
    {
        if (_head == null) return;

        var current = _head;
        Node<T>? temp = null;

        while (current != null)
        {
            // Swap Next and Previous
            temp = current.Previous;
            current.Previous = current.Next;
            current.Next = temp;

            // Move to the next node (which is now in Previous)
            current = current.Previous;
        }

        // Swap head and tail
        temp = _head?.Previous;
        _head = _tail;
        _tail = temp;
    }

    public void Sort()
    {
        // If list has 0 or 1 element, nothing to sort
        if (_head == null || _head.Next == null)
            return;

        // Collect all data into a temporary list
        var items = new List<T>();
        var current = _head;
        while (current != null)
        {
            if (current.Data != null)  // ← Check for null
            {
                items.Add(current.Data);
            }
            current = current.Next;
        }

        // Sort the temporary list
        items.Sort();

        // Clear the original list
        _head = null;
        _tail = null;

        // Rebuild the list using InsertOrdered
        foreach (var item in items)
        {
            InsertOrdered(item);
        }
    }

    public void SortDescending()
    {
        Sort();      // First sort ascending (smallest to largest)
        Reverse();   // Then reverse to get largest to smallest
    }

    override public string ToString()
    {
        var current = _head;
        var result = string.Empty;
        while (current != null)
        {
            result += $"{current.Data} -> ";
            current = current.Next;
        }
        result += "null";
        return result;
    }

    public void ShowMode()
    {
        if (_head == null)
        {
            Console.WriteLine("The list is empty.");
            return;
        }

        // Step 1: Count occurrences of each value
        var dictionary = new Dictionary<T, int>();
        var current = _head;

        while (current != null)
        {
            if (current.Data != null)
            {
                if (dictionary.ContainsKey(current.Data))
                {
                    dictionary[current.Data]++;
                }
                else
                {
                    dictionary[current.Data] = 1;
                }
            }
            current = current.Next;
        }

        // Step 2: Find the maximum frequency
        int maxFrequency = 0;
        foreach (var count in dictionary.Values)
        {
            if (count > maxFrequency)
            {
                maxFrequency = count;
            }
        }

        // Step 3: Show all values with max frequency
        Console.WriteLine($"Mode(s) (appears {maxFrequency} time(s)):");
        foreach (var item in dictionary)
        {
            if (item.Value == maxFrequency)
            {
                Console.WriteLine($"  {item.Key}");
            }
        }
    }

    public void ShowGraph()
    {
        if (_head == null)
        {
            Console.WriteLine("The list is empty.");
            return;
        }

        // Step 1: Count occurrences of each value
        var dictionary = new Dictionary<T, int>();
        var current = _head;

        while (current != null)
        {
            if (current.Data != null)
            {
                if (dictionary.ContainsKey(current.Data))
                {
                    dictionary[current.Data]++;
                }
                else
                {
                    dictionary[current.Data] = 1;
                }
            }
            current = current.Next;
        }

        // Step 2: Display graph
        Console.WriteLine("Frequency Graph:");
        foreach (var item in dictionary)
        {
            Console.Write($"{item.Key} ");

            // Print asterisks based on frequency
            for (int i = 0; i < item.Value; i++)
            {
                Console.Write("*");
            }
            Console.WriteLine(); // New line after each value
        }
    }

    public string ToStringReverse()
    {
        var current = _tail;
        var result = string.Empty;
        while (current != null)
        {
            result += $"{current.Data} -> ";
            current = current.Previous;
        }
        result += "null";
        return result;
    }
}