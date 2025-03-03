using LinkedList;

namespace Cafeteria_Management;

public class Order
{
    public SLinkedList<MenuItem> Items { get; set; }
    public double TotalAmount { get; set; }

    public Order()
    {
        Items = new SLinkedList<MenuItem>();
        TotalAmount = 0;
    }

    public void AddItem(MenuItem item)
    {
        Items.AddBack(item);
        TotalAmount += item.Price;
    }

    public void RemoveItem(string itemName)
    {
        Node<MenuItem>? current = Items.Head;
        Node<MenuItem>? previous = null;
        while (current != null)
        {
            if (current.Data.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
            {
                if (previous == null)
                    Items.RemoveFront();
                else
                    previous.Next = current.Next;

                TotalAmount -= current.Data.Price;
                Console.WriteLine($"{itemName} removed from order.");
                return;
            }
            previous = current;
            current = current.Next;
        }
        Console.WriteLine($"{itemName} not found in order.");
    }
}


