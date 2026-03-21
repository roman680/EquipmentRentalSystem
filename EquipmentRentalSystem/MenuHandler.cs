using EquipmentRentalSystem.models;
using EquipmentRentalSystem.models.equipment;
using EquipmentRentalSystem.models.users;
using EquipmentRentalSystem.models.utils;
using EquipmentRentalSystem.repository;
using EquipmentRentalSystem.service;

namespace EquipmentRentalSystem;

public class MenuHandler
{
    private readonly IUserService _userService;
    private readonly IEquipmentService _equipmentService;
    private readonly IRentService _rentService;

    public MenuHandler(
        IUserService userService,
        IEquipmentService equipmentService,
        IRentService rentService)
    {
        _userService = userService;
        _equipmentService = equipmentService;
        _rentService = rentService;
    }

    public void Handle(string input)
    {
        switch (input)
        {
            case "1": AddUser(); break;
            case "2": AddEquipment(); break;
            case "3": ShowAllEquipment(); break;
            case "4": ShowAvailableEquipment(); break;
            case "5": RentEquipment(); break;
            case "6": ReturnEquipment(); break;
            case "7": MarkUnavailable(); break;
            case "8": ShowUserRentals(); break;
            case "9": ShowOverdue(); break;
            case "10": ShowReport(); break;
            default: Console.WriteLine("Invalid option"); break;
        }
    }

    private void AddUser()
    {
        Console.WriteLine("Enter first name:");
        var first = Console.ReadLine();

        Console.WriteLine("Enter last name:");
        var last = Console.ReadLine();

        Console.WriteLine("1 - Student, 2 - Employee");
        var type = Console.ReadLine();

        User user = type == "1"
            ? new Student(first, last)
            : new Employee(first, last);

        _userService.addUser(user);

        Console.WriteLine($"User added: {user.name} {user.lastName}");
    }

    private Guid SelectUser()
    {
        var users = _userService.getAllUsers().ToList();

        if (!users.Any())
            throw new Exception("No users available");

        for (int i = 0; i < users.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {users[i].name} {users[i].lastName}");
        }

        Console.WriteLine("Select user:");
        int choice = int.Parse(Console.ReadLine());

        return users[choice - 1].id;
    }

    private void AddEquipment()
    {
        Console.WriteLine("Choose type: 1 - Laptop");

        var type = Console.ReadLine();

        Console.WriteLine("Enter name:");
        var name = Console.ReadLine();

        if (type == "1")
        {
            Console.WriteLine("Processor name:");
            var procName = Console.ReadLine();

            Console.WriteLine("Cores:");
            int cores = int.Parse(Console.ReadLine());

            Console.WriteLine("RAM:");
            int ram = int.Parse(Console.ReadLine());

            var processor = new Processor(cores, procName);
            var laptop = new Laptop(processor, ram);

            _equipmentService.addEquipment(laptop);
        }

        Console.WriteLine("Equipment added");
    }

    private Guid SelectEquipment(bool onlyAvailable = true)
    {
        var equipment = (onlyAvailable
                ? _equipmentService.getAvailibleEquipment()
                : _equipmentService.getAllEquipment())
            .ToList();

        if (!equipment.Any())
            throw new Exception("No equipment available");

        for (int i = 0; i < equipment.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {equipment[i].name} (Available: {equipment[i].isAvaliable})");
        }

        Console.WriteLine("Select equipment:");
        int choice = int.Parse(Console.ReadLine());

        return equipment[choice - 1].id;
    }

    private void ShowAllEquipment()
    {
        var items = _equipmentService.getAllEquipment();

        foreach (var e in items)
        {
            Console.WriteLine($"{e.name} | Available: {e.isAvaliable}");
        }
    }

    private void ShowAvailableEquipment()
    {
        var items = _equipmentService.getAvailibleEquipment();

        foreach (var e in items)
        {
            Console.WriteLine(e.name);
        }
    }

    private void MarkUnavailable()
    {
        try
        {
            var id = SelectEquipment(false);
            _equipmentService.markUnAvailibleEquipment(id);
            Console.WriteLine("Marked as unavailable");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void RentEquipment()
    {
        try
        {
            var userId = SelectUser();
            var equipmentId = SelectEquipment(true);

            Console.WriteLine("Enter rental days:");
            int days = int.Parse(Console.ReadLine());

            var rent = _rentService.RentEquipment(userId, equipmentId, days);

            Console.WriteLine($"Rent successful. Rent ID: {rent.id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void ReturnEquipment()
    {
        var rents = _rentService.GetAllRentals().Where(r => r.returnDate == null).ToList();

        if (!rents.Any())
        {
            Console.WriteLine("No active rentals");
            return;
        }

        for (int i = 0; i < rents.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Rent ID: {rents[i].id}");
        }

        Console.WriteLine("Select rent:");
        int choice = int.Parse(Console.ReadLine());

        try
        {
            var penalty = _rentService.ReturnEquipment(rents[choice - 1].id);
            Console.WriteLine($"Returned. Penalty: {penalty}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void ShowUserRentals()
    {
        try
        {
            var userId = SelectUser();

            var rents = _rentService.GetActiveRentalsByUser(userId);

            foreach (var r in rents)
            {
                Console.WriteLine($"Rent ID: {r.id}, Due: {r.DueDate}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void ShowOverdue()
    {
        var rents = _rentService.GetOverdueRentals();

        foreach (var r in rents)
        {
            Console.WriteLine($"Rent ID: {r.id}, Due: {r.DueDate}");
        }
    }

    private void ShowReport()
    {
        var all = _rentService.GetAllRentals();
        var active = all.Count(r => r.returnDate == null);
        var overdue = all.Count(r => r.IsOverdue);

        Console.WriteLine("---- REPORT ----");
        Console.WriteLine($"Total rentals: {all.Count()}");
        Console.WriteLine($"Active rentals: {active}");
        Console.WriteLine($"Overdue rentals: {overdue}");
    }
}