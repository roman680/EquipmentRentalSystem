using EquipmentRentalSystem.repository;
using EquipmentRentalSystem.service;

namespace EquipmentRentalSystem;

public class Program
{
    public static void Main(string[]? args)
    {
        var userRepo = new UserRepositoryImpl();
        var equipmentRepo = new EquipmentRepositoryImpl();
        var rentRepo = new RentalRepositoryImpl();

        var policyService = new RentPolicyServiceImpl();
        var userService = new UserServiceImpl(userRepo);
        var equipmentService = new EquipmentServiceImpl(equipmentRepo);
        var rentService = new RentServiceImpl(userService, equipmentService, rentRepo, policyService);
        var storageService = new StorageServiceImpl(userRepo, equipmentRepo, rentRepo);

        var menu = new MenuHandler(userService, equipmentService, rentService, storageService);

        if (args != null && args.Any(arg => arg.Equals("demo", StringComparison.OrdinalIgnoreCase)))
        {
            menu.Demonstrate();
            return;
        }

        var cliStatus = true;

        while (cliStatus)
        {
            PrintMenu();
            Console.WriteLine("Choose an option:");
            var input = Console.ReadLine();

            if (input == "13")
            {
                cliStatus = false;
                Console.WriteLine("Exiting...");
                continue;
            }

            menu.Handle(input);
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine("\n---- Equipment Rental System ----");
        Console.WriteLine("1. Add new user to the system");
        Console.WriteLine("2. Add new Equipment item of selected type");
        Console.WriteLine("3. Display equipment list with current status");
        Console.WriteLine("4. Display available for rental equipment");
        Console.WriteLine("5. Rent Equipment");
        Console.WriteLine("6. Return equipment (possible penalty)");
        Console.WriteLine("7. Mark equipment as unavailable");
        Console.WriteLine("8. Display active rentals for a selected user");
        Console.WriteLine("9. Display list of overdue rentals");
        Console.WriteLine("10. Generate report of rental service state");
        Console.WriteLine("11. Save to JSON / Load from JSON");
        Console.WriteLine("12. Demonstrate");
        Console.WriteLine("13. Exit");
        Console.WriteLine("---------------------------------\n");
    }
}