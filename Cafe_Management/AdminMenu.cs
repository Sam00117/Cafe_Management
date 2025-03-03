using DynamicArray;
using LinkedList;

namespace Cafeteria_Management;

public static class AdminMenu
{
    public static Menu DisplayMenu(Menu menu)
    {        
        while (true)
        {
            Console.WriteLine("\n--- Admin Section ---\n");
            Console.WriteLine("1. Add Item to Menu");
            Console.WriteLine("2. Remove Item from Menu");
            Console.WriteLine("3. Update Menu");
            Console.WriteLine("4. View Menu");
            Console.WriteLine("5. View Order List");
            Console.WriteLine("6. Logout");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    AddItemToMenu(menu);
                    break;
                case 2:
                    Console.Clear();
                    RemoveItemFromMenu(menu);
                    break;
                case 3:
                    UpdateMenu(menu);
                    break;
                case 4:
                    Console.Clear();
                    ViewOrderMenu(menu);
                    break;
                case 5:
                    Console.Clear();
                    ViewOrders();
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine("Logging out...");
                    return menu;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }

    private static void AddItemToMenu(Menu menu)
    {
        Console.WriteLine("Enter category (Food - F/Drink - D): ");
        string category = Console.ReadLine();

        if (category != "D" && category != "F" && category != "d" && category != "f")
        {
            Console.WriteLine("Invalid category.");
            return;
        }

        Console.WriteLine("Enter item name: ");
        string name = Console.ReadLine();
        bool itemExists = false;
        string categoryName = "";

        if (category == "F" || category == "f")
        {
            categoryName = "Food";
            for (int i = 0; i < menu.FoodCategory.MenuItems.Count; i++)
            {
                var item = menu.FoodCategory.MenuItems.Get(i);
                if (item.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    itemExists = true;
                    break;
                }
            }
        }
        else
        {
            categoryName = "Drinks";
            for (int i = 0; i < menu.DrinkCategory.MenuItems.Count; i++)
            {
                var item = menu.DrinkCategory.MenuItems.Get(i);
                if (item.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    itemExists = true;
                    break;
                }
            }
        }

        if (itemExists)
        {
            Console.WriteLine($"The item '{name}' already exists in the {categoryName} menu.");
            return;
        }

        Console.WriteLine("Enter item price: ");
        string priceInput = Console.ReadLine();

        if (!double.TryParse(priceInput, out double price))
        {
            Console.WriteLine("Invalid price.");
            return;
        }

        MenuItem newItem = new MenuItem(name, price);

        if (category == "F" || category == "f")
        {
            menu.FoodCategory.AddItem(newItem);
            Console.WriteLine("Item added to Food menu.");
        }
        else
        {
            menu.DrinkCategory.AddItem(newItem);
            Console.WriteLine("Item added to Drinks menu.");
        }
    }

    private static void RemoveItemFromMenu(Menu menu)
    {
        ViewOrderMenu(menu);
        Console.WriteLine("\nEnter category (Food - F/Drink - D): ");
        string category = Console.ReadLine();

        if (category != "D" && category != "F" && category != "d" && category != "f")
        {
            Console.WriteLine("Invalid category.");
            return;
        }

        Console.WriteLine("Enter item name to remove: ");
        string name = Console.ReadLine();

        if (category == "F" || category == "f")
        {
            menu.FoodCategory.RemoveItem(name);
        }
        else
        {
            menu.DrinkCategory.RemoveItem(name);
        }
    }

    private static void UpdateMenu(Menu menu)
    {
        Console.WriteLine("\nEnter category (Food - F/Drink - D): ");
        string category = Console.ReadLine();

        if (category != "D" && category != "F" && category != "d" && category != "f")
        {
            Console.WriteLine("Invalid category.");
            return;
        }

        Console.WriteLine("Enter item name to update: ");
        string name = Console.ReadLine();

        Console.WriteLine("Enter new price: ");
        string newPrice = Console.ReadLine();

        if (!double.TryParse(newPrice, out double price))
        {
            Console.WriteLine("Invalid price.");
            return;
        }


        if (category == "F" || category == "f")
        {
            for (int i = 0; i < menu.FoodCategory.MenuItems.Count; i++)
            {
                var item = menu.FoodCategory.MenuItems.Get(i);
                if (item.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    item.Price = price;
                    Console.WriteLine($"The price of {item.Name} has been updated to Rs.{price}.");
                    return;
                }
            }
            Console.WriteLine($"Item {name} not found in Food menu.");
        }
        else
        {
            for (int i = 0; i < menu.DrinkCategory.MenuItems.Count; i++)
            {
                var item = menu.DrinkCategory.MenuItems.Get(i);
                if (item.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    item.Price = price;
                    Console.WriteLine($"The price of {item.Name} has been updated to Rs.{price}.");
                    return;
                }
            }
            Console.WriteLine($"Item {name} not found in Drinks menu.");
        }
    }

    private static void ViewOrderMenu(Menu menu)
    {
        Console.WriteLine("--- Menu ---\n");

        menu.FoodCategory.DisplayMenu();
        menu.DrinkCategory.DisplayMenu();
    }

    private static void ViewOrders()
    {
        Console.WriteLine("--- Customer Orders ---");

        if (CustomerMenu.allOrders.Count == 0)
        {
            Console.WriteLine("No orders placed yet.");
            return;
        }
        Node<Order> currentOrder = CustomerMenu.allOrders.Head;
        int orderIndex = 1;

        while (currentOrder != null)
        {
            var order = currentOrder.Data; 
            Console.WriteLine($"\nOrder {orderIndex}:");

            DArray<MenuItemWithQuantity> groupedItems = MenuItemWithQuantity.GroupItemsByName(order.Items);

            int index = 1;
            for (int i = 0; i < groupedItems.Count; i++)
            {
                var groupedItem = groupedItems.Get(i);
                Console.WriteLine($"{index}. {groupedItem.Quantity}x {groupedItem.Item.Name} - Rs.{groupedItem.Item.Price * groupedItem.Quantity}");
                index++;
            }

            Console.WriteLine($"Total Amount : Rs.{order.TotalAmount}");

            currentOrder = currentOrder.Next;
            orderIndex++;
        }
    }

}




