using EquipmentRentalSystem.enums;
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
    private readonly IStorageService _storageService;
    private readonly string _jsonPath = Path.Combine(AppContext.BaseDirectory, "rental-data.json");

    public MenuHandler(
        IUserService userService,
        IEquipmentService equipmentService,
        IRentService rentService,
        IStorageService storageService)
    {
        _userService = userService;
        _equipmentService = equipmentService;
        _rentService = rentService;
        _storageService = storageService;
    }

    public void Handle(string? input)
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
            case "11": ManageJson(); break;
            case "12": Demonstrate(); break;
            default: Console.WriteLine("Invalid option"); break;
        }
    }

    public void Demonstrate()
    {
        try
        {
            _storageService.SaveToJson(_jsonPath + ".backup");
        }
        catch
        {
        }

        ResetData();

        Console.WriteLine("11. Adding several equipment items of different types.");
        var laptop = new Laptop(new Processor(8, "Intel i7"), 16, 15.6)
        {
            name = "Dell Latitude",
            manufacturer = "Dell",
            description = "Laptop for classes"
        };

        Console.WriteLine("Creating equipment");
        _equipmentService.addEquipment(laptop);
        Console.WriteLine($"Equipment {laptop.name} successfully created");
        Console.WriteLine($"{laptop.id} | {laptop.name} | {GetEquipmentType(laptop)} | {GetEquipmentDetails(laptop)}");

        var camera = new Camera(new Processor(4, "Sony Bionz"), Resolution.FullHD, 12)
        {
            name = "Sony Camera",
            manufacturer = "Sony",
            description = "Camera for media lab"
        };

        Console.WriteLine("Creating equipment");
        _equipmentService.addEquipment(camera);
        Console.WriteLine($"Equipment {camera.name} successfully created");
        Console.WriteLine($"{camera.id} | {camera.name} | {GetEquipmentType(camera)} | {GetEquipmentDetails(camera)}");

        var projector = new Projector(Resolution.UltraHD, 3, 4000)
        {
            name = "Epson Projector",
            manufacturer = "Epson",
            description = "Projector for lecture halls"
        };

        Console.WriteLine("Creating equipment");
        _equipmentService.addEquipment(projector);
        Console.WriteLine($"Equipment {projector.name} successfully created");
        Console.WriteLine($"{projector.id} | {projector.name} | {GetEquipmentType(projector)} | {GetEquipmentDetails(projector)}");

        var secondLaptop = new Laptop(new Processor(6, "AMD Ryzen 5"), 8, 14.0)
        {
            name = "Lenovo ThinkBook",
            manufacturer = "Lenovo",
            description = "Extra laptop"
        };

        Console.WriteLine("Creating equipment");
        _equipmentService.addEquipment(secondLaptop);
        Console.WriteLine($"Equipment {secondLaptop.name} successfully created");
        Console.WriteLine($"{secondLaptop.id} | {secondLaptop.name} | {GetEquipmentType(secondLaptop)} | {GetEquipmentDetails(secondLaptop)}");

        Console.WriteLine();
        Console.WriteLine("12. Adding several users of different types.");
        var student = new Student("Anna", "Nowak");
        Console.WriteLine("Creating user");
        _userService.addUser(student);
        Console.WriteLine($"User {student.name} {student.lastName} successfully created");
        Console.WriteLine($"{student.id} | {student.name} {student.lastName} | {student.userType}");

        var secondStudent = new Student("Jan", "Kowalski");
        Console.WriteLine("Creating user");
        _userService.addUser(secondStudent);
        Console.WriteLine($"User {secondStudent.name} {secondStudent.lastName} successfully created");
        Console.WriteLine($"{secondStudent.id} | {secondStudent.name} {secondStudent.lastName} | {secondStudent.userType}");

        var employee = new Employee("Maria", "Wisniewska");
        Console.WriteLine("Creating user");
        _userService.addUser(employee);
        Console.WriteLine($"User {employee.name} {employee.lastName} successfully created");
        Console.WriteLine($"{employee.id} | {employee.name} {employee.lastName} | {employee.userType}");

        Console.WriteLine();
        Console.WriteLine("13. A correct rental operation.");
        var correctRent = _rentService.RentEquipment(student.id, laptop.id, 3, DateTime.Now.AddDays(-2));
        Console.WriteLine($"Correct rental created: {correctRent.id}");
        Console.WriteLine($"User: {student.name} {student.lastName}");
        Console.WriteLine($"Equipment: {laptop.name}");
        Console.WriteLine($"Rental date: {correctRent.date:d}");
        Console.WriteLine($"Due date: {correctRent.DueDate:d}");

        Console.WriteLine();
        Console.WriteLine("14. An attempted invalid operation.");
        _rentService.RentEquipment(student.id, camera.id, 2, DateTime.Now.AddDays(-1));
        Console.WriteLine($"{student.name} {student.lastName} rented {camera.name}");

        try
        {
            _rentService.RentEquipment(student.id, projector.id, 2);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Invalid operation blocked: {ex.Message}");
        }

        Console.WriteLine();
        Console.WriteLine("15. A return completed on time.");
        var onTimePenalty = _rentService.ReturnEquipment(correctRent.id, correctRent.DueDate);
        Console.WriteLine($"Returned rental {correctRent.id} on time");
        Console.WriteLine($"Penalty: {onTimePenalty}");

        Console.WriteLine();
        Console.WriteLine("16. A delayed return that leads to a penalty.");
        var lateRent = _rentService.RentEquipment(employee.id, projector.id, 2, DateTime.Now.AddDays(-5));
        var latePenalty = _rentService.ReturnEquipment(lateRent.id, lateRent.DueDate.AddDays(2));
        Console.WriteLine($"Returned rental {lateRent.id} after due date");
        Console.WriteLine($"Penalty: {latePenalty}");

        _equipmentService.markUnAvailibleEquipment(secondLaptop.id);
        Console.WriteLine("One equipment item marked as unavailable");

        _storageService.SaveToJson(_jsonPath);
        _storageService.LoadFromJson(_jsonPath);
        Console.WriteLine("JSON save/load completed");

        Console.WriteLine();
        Console.WriteLine("17. Displaying a final report of the system state.");
        ShowReport();
    }

    private void AddUser()
    {
        try
        {
            Console.WriteLine("Enter first name:");
            var first = ReadRequired();

            Console.WriteLine("Enter last name:");
            var last = ReadRequired();

            Console.WriteLine("1 - Student, 2 - Employee");
            var type = Console.ReadLine();

            User user = type == "1"
                ? new Student(first, last)
                : type == "2"
                    ? new Employee(first, last)
                    : throw new Exception("Invalid user type");

            _userService.addUser(user);

            Console.WriteLine($"User added: {user.name} {user.lastName} ({user.userType})");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private Guid SelectUser()
    {
        var users = _userService.getAllUsers().ToList();

        if (!users.Any())
            throw new Exception("No users available");

        for (int i = 0; i < users.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {users[i].name} {users[i].lastName} ({users[i].userType})");
        }

        Console.WriteLine("Select user:");
        int choice = int.Parse(ReadRequired());

        if (choice < 1 || choice > users.Count)
            throw new Exception("Invalid user choice");

        return users[choice - 1].id;
    }

    private void AddEquipment()
    {
        Console.WriteLine("Choose equipment type:");
        Console.WriteLine("1 - Laptop");
        Console.WriteLine("2 - Camera");
        Console.WriteLine("3 - Projector");

        var type = Console.ReadLine();

        Console.WriteLine("Enter name:");
        var name = ReadRequired();

        Console.WriteLine("Enter manufacturer:");
        var manufacturer = ReadRequired();

        Console.WriteLine("Enter description:");
        var description = ReadRequired();

        try
        {
            Equipment equipment = type switch
            {
                "1" => CreateLaptop(name, manufacturer, description),
                "2" => CreateCamera(name, manufacturer, description),
                "3" => CreateProjector(name, manufacturer, description),
                _ => throw new Exception("Invalid equipment type")
            };

            _equipmentService.addEquipment(equipment);
            Console.WriteLine("Equipment added successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
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
            Console.WriteLine($"{i + 1}. {equipment[i].name} ({GetEquipmentType(equipment[i])}) (Available: {equipment[i].isAvaliable})");
        }

        Console.WriteLine("Select equipment:");
        int choice = int.Parse(ReadRequired());

        if (choice < 1 || choice > equipment.Count)
            throw new Exception("Invalid equipment choice");

        return equipment[choice - 1].id;
    }

    private void ShowAllEquipment()
    {
        var items = _equipmentService.getAllEquipment().ToList();

        if (!items.Any())
        {
            Console.WriteLine("No equipment in the system");
            return;
        }

        foreach (var e in items)
        {
            Console.WriteLine($"{e.id} | {e.name} | {GetEquipmentType(e)} | Available: {e.isAvaliable} | Manufacturer: {e.manufacturer} | {GetEquipmentDetails(e)}");
        }
    }

    private void ShowAvailableEquipment()
    {
        var items = _equipmentService.getAvailibleEquipment().ToList();

        if (!items.Any())
        {
            Console.WriteLine("No available equipment");
            return;
        }

        foreach (var e in items)
        {
            Console.WriteLine($"{e.id} | {e.name} | {GetEquipmentType(e)} | {GetEquipmentDetails(e)}");
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
            int days = int.Parse(ReadRequired());

            var rent = _rentService.RentEquipment(userId, equipmentId, days);

            Console.WriteLine($"Rent successful. Rent ID: {rent.id}. Due date: {rent.DueDate:d}");
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
            Console.WriteLine($"{i + 1}. Rent ID: {rents[i].id}, Due: {rents[i].DueDate:d}");
        }

        Console.WriteLine("Select rent:");
        int choice = int.Parse(ReadRequired());

        if (choice < 1 || choice > rents.Count)
        {
            Console.WriteLine("Invalid rent choice");
            return;
        }

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

            var rents = _rentService.GetActiveRentalsByUser(userId).ToList();

            if (!rents.Any())
            {
                Console.WriteLine("No active rentals for selected user");
                return;
            }

            foreach (var r in rents)
            {
                Console.WriteLine($"Rent ID: {r.id}, Due: {r.DueDate:d}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void ShowOverdue()
    {
        var rents = _rentService.GetOverdueRentals().ToList();

        if (!rents.Any())
        {
            Console.WriteLine("No overdue rentals");
            return;
        }

        foreach (var r in rents)
        {
            Console.WriteLine($"Rent ID: {r.id}, Due: {r.DueDate:d}");
        }
    }

    private void ShowReport()
    {
        var allRents = _rentService.GetAllRentals().ToList();
        var allEquipment = _equipmentService.getAllEquipment().ToList();
        var allUsers = _userService.getAllUsers().ToList();
        var active = allRents.Count(r => r.returnDate == null);
        var overdue = allRents.Count(r => r.IsOverdue);
        var unavailable = allEquipment.Count(e => !e.isAvaliable && !allRents.Any(r => r.equipmentId == e.id && r.returnDate == null));
        var penalties = allRents.Sum(r => r.penaltyFee);

        Console.WriteLine("---- REPORT ----");
        Console.WriteLine($"Users: {allUsers.Count}");
        Console.WriteLine($"Equipment: {allEquipment.Count}");
        Console.WriteLine($"Available equipment: {allEquipment.Count(e => e.isAvaliable)}");
        Console.WriteLine($"Unavailable equipment: {unavailable}");
        Console.WriteLine($"Total rentals: {allRents.Count}");
        Console.WriteLine($"Active rentals: {active}");
        Console.WriteLine($"Overdue rentals: {overdue}");
        Console.WriteLine($"Collected penalties: {penalties}");
    }

    private void ManageJson()
    {
        Console.WriteLine("1 - Save to JSON");
        Console.WriteLine("2 - Load from JSON");
        var choice = Console.ReadLine();

        try
        {
            if (choice == "1")
            {
                _storageService.SaveToJson(_jsonPath);
                Console.WriteLine($"Saved to {_jsonPath}");
            }
            else if (choice == "2")
            {
                _storageService.LoadFromJson(_jsonPath);
                Console.WriteLine($"Loaded from {_jsonPath}");
            }
            else
            {
                Console.WriteLine("Invalid option");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void ResetData()
    {
        _storageService.SaveToJson(_jsonPath + ".temp");
        File.WriteAllText(_jsonPath + ".empty", "{\"users\":[],\"equipments\":[],\"rents\":[]}");
        _storageService.LoadFromJson(_jsonPath + ".empty");
    }

    private Laptop CreateLaptop(string name, string manufacturer, string description)
    {
        Console.WriteLine("Processor name:");
        var procName = ReadRequired();

        Console.WriteLine("Number of cores:");
        int cores = int.Parse(ReadRequired());

        Console.WriteLine("RAM (GB):");
        int ram = int.Parse(ReadRequired());

        Console.WriteLine("Screen size:");
        double screenSize = double.Parse(ReadRequired());

        var processor = new Processor(cores, procName);

        var laptop = new Laptop(processor, ram, screenSize)
        {
            name = name,
            manufacturer = manufacturer,
            description = description
        };

        return laptop;
    }

    private Camera CreateCamera(string name, string manufacturer, string description)
    {
        Console.WriteLine("Processor name:");
        var procName = ReadRequired();

        Console.WriteLine("Number of cores:");
        int cores = int.Parse(ReadRequired());

        var processor = new Processor(cores, procName);

        Console.WriteLine("Resolution (1 - HD, 2 - FullHD, 3 - UltraHD):");
        var resInput = Console.ReadLine();

        var resolution = ParseResolution(resInput);

        Console.WriteLine("Optical zoom:");
        int zoom = int.Parse(ReadRequired());

        var camera = new Camera(processor, resolution, zoom)
        {
            name = name,
            manufacturer = manufacturer,
            description = description
        };

        return camera;
    }

    private Projector CreateProjector(string name, string manufacturer, string description)
    {
        Console.WriteLine("Resolution (1 - HD, 2 - FullHD, 3 - UltraHD):");
        var resInput = Console.ReadLine();

        var resolution = ParseResolution(resInput);

        Console.WriteLine("Number of optical lenses:");
        int lenses = int.Parse(ReadRequired());

        Console.WriteLine("Brightness in lumens:");
        int brightness = int.Parse(ReadRequired());

        var projector = new Projector(resolution, lenses, brightness)
        {
            name = name,
            manufacturer = manufacturer,
            description = description
        };

        return projector;
    }

    private Resolution ParseResolution(string? input)
    {
        return input switch
        {
            "1" => Resolution.HD,
            "2" => Resolution.FullHD,
            "3" => Resolution.UltraHD,
            _ => throw new Exception("Invalid resolution")
        };
    }

    private string GetEquipmentType(Equipment equipment)
    {
        return equipment switch
        {
            Laptop => "Laptop",
            Camera => "Camera",
            Projector => "Projector",
            _ => "Equipment"
        };
    }

    private string GetEquipmentDetails(Equipment equipment)
    {
        return equipment switch
        {
            Laptop laptop => $"Processor: {laptop.processor.name}, RAM: {laptop.Ram}GB, Screen: {laptop.screenSize}",
            Camera camera => $"Processor: {camera.processor.name}, Resolution: {camera.resolution}, Zoom: {camera.zoom}",
            Projector projector => $"Resolution: {projector.resolution}, Lenses: {projector.numberOfOpticalLenses}, Brightness: {projector.brightnessLumens}",
            _ => string.Empty
        };
    }

    private string ReadRequired()
    {
        var value = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new Exception("Value cannot be empty");
        }

        return value;
    }
}