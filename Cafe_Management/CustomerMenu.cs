using LinkedList;

namespace Cafeteria_Management;

public static class CustomerMenu
{
    public static SLinkedList<Order> allOrders = new SLinkedList<Order>();
    public static void DisplayMenu(Menu menu)
    {
        Order order = new Order();

        while (true)
        {
            Console.WriteLine("--- Customer Section ---");
            Console.WriteLine("\n1. View Menu");
            Console.WriteLine("2. Sort Menu");
            Console.WriteLine("3. Search Menu");
            Console.WriteLine("4. Place Order");
            Console.WriteLine("5. View Order");
            Console.WriteLine("6. Delete Item from Order");
            Console.WriteLine("7. View Bill");
            Console.WriteLine("8. Exit");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    ViewOrderMenu(menu);
                    break;
                case 2:
                    Console.Clear();
                    SortMenu(menu);
                    break;
                case 3:
                    Console.Clear();
                    SearchMenu(menu);
                    break;
                case 4:
                    Console.Clear();
                    PlaceOrder(menu, order);
                    break;
                case 5:
                    ViewOrder(order);
                    break;
                case 6:
                    Console.Clear();
                    DeleteItemFromOrder(order);
                    break;
                case 7:
                    ViewBill(order);
                    break;
                case 8:
                    Console.Clear();
                    Console.WriteLine("Thank you for visiting!");
                    return;
                default:
                    Console.WriteLine("Invalid choice, try again.\n");
                    break;
            }
        }
    }

    private static void ViewOrderMenu(Menu menu)
    {
        Console.WriteLine("--- Menu ---\n");

        menu.FoodCategory.DisplayMenu();
        menu.DrinkCategory.DisplayMenu();
    }

    private static void PlaceOrder(Menu menu, Order order)
    {
        while (true)
        {
            ViewOrderMenu(menu);

            Console.WriteLine("Enter category (Food - F/Drink - D): ");
            string category = Console.ReadLine();
            MenuCategory selectedCategory;

            if (category != "D" && category != "F" && category != "d" && category != "f")
            {
                Console.WriteLine("Invalid category.\n");
                return;
            }
            else if (category == "F" || category == "f")
            {
                selectedCategory = menu.FoodCategory;
            }
            else
            {
                selectedCategory = menu.DrinkCategory;
            }

            Console.WriteLine("Enter the number of the item you want to order:");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int itemNumber) || itemNumber < 1 || itemNumber > selectedCategory.MenuItems.Count)
            {
                Console.WriteLine("Invalid item number.\n");
                return;
            }

            itemNumber--;

            var item = selectedCategory.MenuItems.Get(itemNumber);

            Console.WriteLine($"Enter the quantity of {item.Name} you want to order:");
            int quantity;

            if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity.\n");
                return;
            }

            for (int i = 0; i < quantity; i++)
            {
                order.AddItem(new MenuItem(item.Name, item.Price));
            }
            Console.WriteLine($"\n{quantity}x {item.Name} have been added to your order.");


            Console.WriteLine("\nWould you like to order something else? (Y/N): ");
            string continueOrder = Console.ReadLine().ToUpper();
            Console.Clear();

            if (continueOrder != "Y")
            {
                Console.WriteLine("Your order has been placed successfully!\n");
                allOrders.AddBack(order);
                return;
            }
        }
    }


    private static void SortMenu(Menu menu)
    {
        while (true)
        {
            ViewOrderMenu(menu);
            Console.WriteLine("Choose category to sort: Food/F or Drinks/D?");
            string category = Console.ReadLine().ToLower();
            if (category != "d" && category != "f")
            {
                Console.WriteLine("Invalid category.\n");
                return;
            }

            /*Console.WriteLine("Choose sorting algorithm: Bubble/B, Quick/Q, or Merge/M?");
            string algorithm = Console.ReadLine().ToLower();
            if (algorithm != "b" && algorithm != "q" && algorithm != "m")
            {
                Console.WriteLine("Invalid category.");
                return;
            }*/

            if (category == "f")
            {
                menu.FoodCategory.SortMenuItems("price", "m");
                Console.WriteLine("Food menu sorted successfully.\n");
                menu.FoodCategory.DisplayMenu();
            }
            else if (category == "d")
            {
                menu.DrinkCategory.SortMenuItems("price", "m");
                Console.WriteLine("Drink menu sorted successfully.\n");
                menu.DrinkCategory.DisplayMenu();
            }

            Console.WriteLine("Do you want to sort another? (Y/N): ");
            string continueSorting = Console.ReadLine().ToUpper();

            if (continueSorting == "Y")
            {
                Console.Clear();
                continue;
            }
            else if (continueSorting == "N")
            {
                Console.Clear();
                break;
            }
            else
            {
                Console.WriteLine("Invalid input, exiting sorting...\n");
                break;
            }
        }
    }

    private static void SearchMenu(Menu menu)
    {
        while (true)
        {
            Console.WriteLine("Choose category to search: Food/F or Drinks/D?");
            string category = Console.ReadLine().ToLower();
            if (category != "d" && category != "f")
            {
                Console.WriteLine("Invalid category.\n");
                return;
            }

            Console.WriteLine("Enter search term (for name or part of name):");
            string searchTerm = Console.ReadLine().ToLower();

            bool searchSuccessful = false;

            if (category == "f")
            {
                searchSuccessful = menu.FoodCategory.SearchMenuItems(searchTerm);
                if (!searchSuccessful)
                {
                    Console.WriteLine("No matching food items found.\n");
                }
            }
            else if (category == "d")
            {
                searchSuccessful = menu.DrinkCategory.SearchMenuItems(searchTerm);
                if (!searchSuccessful)
                {
                    Console.WriteLine("No matching drink items found.\n");
                }
            }

            Console.WriteLine("Do you want to search for another item? (Y/N): ");
            string continueSearching = Console.ReadLine().ToUpper();

            if (continueSearching == "Y")
            {
                Console.Clear();
                continue;
            }
            else if (continueSearching == "N")
            {
                Console.WriteLine("Exiting search...\n");
                Console.Clear();
                break;
            }
            else
            {
                Console.WriteLine("Invalid input, exiting search...\n");
                break;
            }
        }
    }

    private static void ViewOrder(Order order)
    {
        Console.Clear();
        Console.WriteLine("--- Your Order ---");

        if (order.Items.Count == 0)
        {
            Console.WriteLine("No items in your order.\n");
        }
        else
        {
            var groupedItems = MenuItemWithQuantity.GroupItemsByName(order.Items);

            for (int i = 0; i < groupedItems.Count; i++)
            {
                var item = groupedItems.Get(i);
                double totalPrice = item.Item.Price * item.Quantity;

                Console.WriteLine($"{i + 1}. {item.Quantity} x {item.Item.Name} = Rs.{totalPrice}");
            }
            Console.WriteLine();
        }
    }

    private static void DeleteItemFromOrder(Order order)
    {
        ViewOrder(order);

        if (order.Items.Count == 0)
        {
            Console.WriteLine("There are no items in your order to delete.\n");
            return;
        }

        Console.WriteLine("Enter the number of the item you want to delete from the order:");

        int itemNumber;
        if (!int.TryParse(Console.ReadLine(), out itemNumber) || itemNumber < 1 || itemNumber > order.Items.Count)
        {
            Console.WriteLine("Invalid item number.\n");
            return;
        }

        itemNumber -= 1;

        var groupedItems = MenuItemWithQuantity.GroupItemsByName(order.Items);
        if (itemNumber >= 0 && itemNumber < groupedItems.Count)
        {
            var itemToDelete = groupedItems.Get(itemNumber);

            Console.WriteLine($"You are about to delete {itemToDelete.Item.Name}!");
            Console.WriteLine($"How many of '{itemToDelete.Item.Name}' would you like to remove? (Max: {itemToDelete.Quantity}):");

            int quantityToRemove;

            if (!int.TryParse(Console.ReadLine(), out quantityToRemove) || quantityToRemove <= 0 || quantityToRemove > itemToDelete.Quantity)
            {
                Console.WriteLine($"Invalid quantity. You can only remove between 1 and {itemToDelete.Quantity} of {itemToDelete.Item.Name}.\n");
                return;
            }

            int quantityRemoved = 0;
            for (int i = 0; i < order.Items.Count && quantityRemoved < quantityToRemove; i++)
            {
                var currentItem = order.Items.Get(i);
                if (currentItem.Name.Equals(itemToDelete.Item.Name, StringComparison.OrdinalIgnoreCase))
                {
                    order.Items.RemoveAt(i);
                    order.TotalAmount -= currentItem.Price;
                    quantityRemoved++;

                    i--;
                }
            }

            Console.WriteLine($"{quantityRemoved} of '{itemToDelete.Item.Name}' have been removed from your order.\n");
        }
        else
        {
            Console.WriteLine("Invalid item number.\n");
        }
    }

    private static void ViewBill(Order order)
    {
        ViewOrder(order);
        Console.WriteLine($"Total amount: Rs.{order.TotalAmount}\n");
    }

}


