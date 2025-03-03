using DynamicArray;
using LinkedList;

namespace Cafeteria_Management;

public class MenuItem
{
    public string Name { get; set; }
    public double Price { get; set; }
    public MenuItem(string name, double price)
    {
        Name = name;
        Price = price;
    }
}

public class MenuCategory
{
    public string CategoryName { get; set; }
    public DArray<MenuItem> MenuItems { get; set; }

    public MenuCategory(string categoryName)
    {
        CategoryName = categoryName;
        MenuItems = new DArray<MenuItem>();
    }

    public void AddItem(MenuItem item)
    {
        MenuItems.Add(item);
    }

    public void RemoveItem(string itemName)
    {
        for (int i = 0; i < MenuItems.Count; i++)
        {
            var item = MenuItems.Get(i);
            if (item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
            {
                MenuItems.RemoveAt(i);
                Console.WriteLine($"{item.Name} has been removed from {CategoryName}.");
                return;
            }
        }
        Console.WriteLine($"Item {itemName} not found in {CategoryName}.");
    }

    public void SortMenuItems(string sortBy, string algorithm)
    {
        var array = MenuItems.ToArray();

        switch (algorithm.ToLower())
        {
            case "b":
                BubbleSort.Sort(array);
                break;
            case "q":
                QuickSort.Sort(array, 0, array.Length - 1);
                break;
            case "m":
                MergeSort.Sort(array, 0, array.Length - 1);
                break;
            default:
                Console.WriteLine("Invalid sorting algorithm. Please choose bubble/b, quick/q, or merge/m.");                
                break;
        }

        MenuItems.Clear();
        foreach (var item in array)
        {
            MenuItems.Add(item);
        }        
    }

    public bool SearchMenuItems(string searchTerm)
    {
        DArray<MenuItem> matchingItems = new DArray<MenuItem>();

        for (int i = 0; i < MenuItems.Count; i++)
        {
            var item = MenuItems.Get(i);
            if (item.Name.ToLower().Contains(searchTerm.ToLower()))
            {
                matchingItems.Add(item); 
            }
        }

        if (matchingItems.Count == 0)
        {
            return false;  
        }

        Console.WriteLine("\nSearch Results:");
        for (int i = 0; i < matchingItems.Count; i++)
        {
            var item = matchingItems.Get(i);
            Console.WriteLine($"- {item.Name} - Rs.{item.Price}");
        }
        Console.WriteLine();

        return true;
    }

    public void DisplayMenu()
    {
        Console.WriteLine($"--- {CategoryName} Menu ---");
        for (int i = 0; i < MenuItems.Count; i++)
        {
            var item = MenuItems.Get(i);
            Console.WriteLine($"{i + 1}. {item.Name} - Rs.{item.Price}");
        }
        Console.WriteLine();
    }
}

public class Menu
{
    public MenuCategory FoodCategory { get; set; }
    public MenuCategory DrinkCategory { get; set; }

    public Menu()
    {
        FoodCategory = new MenuCategory("Food");
        DrinkCategory = new MenuCategory("Drinks");

        FoodCategory.AddItem(new MenuItem("Burger", 850));
        FoodCategory.AddItem(new MenuItem("Pizza", 1200));
        FoodCategory.AddItem(new MenuItem("Pasta", 450));
        FoodCategory.AddItem(new MenuItem("Fries", 250));
        FoodCategory.AddItem(new MenuItem("Sandwich", 120));
       
        DrinkCategory.AddItem(new MenuItem("Soda", 150));
        DrinkCategory.AddItem(new MenuItem("Juice", 120));
        DrinkCategory.AddItem(new MenuItem("Coffee", 70));
        DrinkCategory.AddItem(new MenuItem("Tea", 40));
        DrinkCategory.AddItem(new MenuItem("Milkshake", 290));
    }

}

public class MenuItemWithQuantity
{
    public MenuItem Item { get; set; }
    public int Quantity { get; set; }

    static public DArray<MenuItemWithQuantity> GroupItemsByName(SLinkedList<MenuItem> order)
    {
        DArray<MenuItemWithQuantity> groupedItems = new DArray<MenuItemWithQuantity>();

        Node<MenuItem> currentItem = order.Head;
        while (currentItem != null)
        {
            var item = currentItem.Data;
            bool itemExists = false;

            for (int j = 0; j < groupedItems.Count; j++)
            {
                if (groupedItems.Get(j).Item.Name == item.Name)
                {
                    groupedItems.Get(j).Quantity++;
                    itemExists = true;
                    break;
                }
            }

            if (!itemExists)
            {
                groupedItems.Add(new MenuItemWithQuantity
                {
                    Item = item,
                    Quantity = 1
                });
            }

            currentItem = currentItem.Next;
        }

        return groupedItems;
    }
}

