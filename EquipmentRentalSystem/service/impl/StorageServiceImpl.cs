using System.Text.Json;
using EquipmentRentalSystem.enums;
using EquipmentRentalSystem.models;
using EquipmentRentalSystem.models.equipment;
using EquipmentRentalSystem.models.storage;
using EquipmentRentalSystem.models.users;
using EquipmentRentalSystem.models.utils;
using EquipmentRentalSystem.repository;

namespace EquipmentRentalSystem.service;

public class StorageServiceImpl : IStorageService
{
    private readonly IUserRepository userRepository;
    private readonly IEquipmentRepository equipmentRepository;
    private readonly IRentalRepository rentRepository;

    public StorageServiceImpl(
        IUserRepository userRepository,
        IEquipmentRepository equipmentRepository,
        IRentalRepository rentRepository)
    {
        this.userRepository = userRepository;
        this.equipmentRepository = equipmentRepository;
        this.rentRepository = rentRepository;
    }

    public void SaveToJson(string path)
    {
        var data = new SystemData
        {
            users = userRepository.getAllUsers().Select(MapUser).ToList(),
            equipments = equipmentRepository.getAllEquipment().Select(MapEquipment).ToList(),
            rents = rentRepository.getAllRents().Select(MapRent).ToList()
        };

        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, json);
    }

    public void LoadFromJson(string path)
    {
        if (!File.Exists(path))
        {
            throw new Exception("JSON file not found");
        }

        var json = File.ReadAllText(path);
        var data = JsonSerializer.Deserialize<SystemData>(json);

        if (data == null)
        {
            throw new Exception("Invalid JSON data");
        }

        userRepository.replaceAllUsers(data.users.Select(MapUser));
        equipmentRepository.replaceAllEquipment(data.equipments.Select(MapEquipment));
        rentRepository.replaceAllRents(data.rents.Select(MapRent));
    }

    private UserData MapUser(User user)
    {
        return new UserData
        {
            type = user.userType,
            id = user.id,
            name = user.name,
            lastName = user.lastName
        };
    }

    private User MapUser(UserData user)
    {
        User mappedUser = user.type == "Employee"
            ? new Employee(user.name, user.lastName)
            : new Student(user.name, user.lastName);

        mappedUser.id = user.id;
        return mappedUser;
    }

    private EquipmentData MapEquipment(Equipment equipment)
    {
        var data = new EquipmentData
        {
            id = equipment.id,
            name = equipment.name,
            isAvaliable = equipment.isAvaliable,
            description = equipment.description,
            manufacturer = equipment.manufacturer
        };

        if (equipment is Laptop laptop)
        {
            data.type = "Laptop";
            data.processorName = laptop.processor.name;
            data.processorCores = laptop.processor.numberOfCores;
            data.ram = laptop.Ram;
            data.screenSize = laptop.screenSize;
        }
        else if (equipment is Camera camera)
        {
            data.type = "Camera";
            data.processorName = camera.processor.name;
            data.processorCores = camera.processor.numberOfCores;
            data.resolution = (int)camera.resolution;
            data.zoom = camera.zoom;
        }
        else if (equipment is Projector projector)
        {
            data.type = "Projector";
            data.resolution = (int)projector.resolution;
            data.numberOfOpticalLenses = projector.numberOfOpticalLenses;
            data.brightnessLumens = projector.brightnessLumens;
        }

        return data;
    }

    private Equipment MapEquipment(EquipmentData data)
    {
        Equipment equipment = data.type switch
        {
            "Laptop" => new Laptop(new Processor(data.processorCores, data.processorName), data.ram, data.screenSize),
            "Camera" => new Camera(new Processor(data.processorCores, data.processorName), (Resolution)data.resolution, data.zoom),
            "Projector" => new Projector((Resolution)data.resolution, data.numberOfOpticalLenses, data.brightnessLumens),
            _ => throw new Exception("Unknown equipment type")
        };

        equipment.id = data.id;
        equipment.name = data.name;
        equipment.isAvaliable = data.isAvaliable;
        equipment.description = data.description;
        equipment.manufacturer = data.manufacturer;

        return equipment;
    }

    private RentData MapRent(Rent rent)
    {
        return new RentData
        {
            id = rent.id,
            date = rent.date,
            userId = rent.userId,
            equipmentId = rent.equipmentId,
            rentalDays = rent.rentalDays,
            returnDate = rent.returnDate,
            penaltyFee = rent.penaltyFee
        };
    }

    private Rent MapRent(RentData rent)
    {
        var mappedRent = new Rent(rent.date, rent.userId, rent.equipmentId, rent.rentalDays)
        {
            id = rent.id
        };

        if (rent.returnDate != null)
        {
            mappedRent.Return(rent.returnDate.Value, rent.penaltyFee);
        }

        return mappedRent;
    }
}
