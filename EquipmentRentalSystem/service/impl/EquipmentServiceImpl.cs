using EquipmentRentalSystem.models.equipment;
using EquipmentRentalSystem.repository;

namespace EquipmentRentalSystem.service;

public class EquipmentServiceImpl : IEquipmentService
{
    
    private readonly IEquipmentRepository equipmentRepository;

    public EquipmentServiceImpl(IEquipmentRepository equipmentRepository)
    {
        this.equipmentRepository = equipmentRepository;
    }


    public Equipment addEquipment(Equipment equipment)
    {
        if (equipment == null)
        {
            Console.WriteLine("User is null");
            return null;
        }

        if (equipmentRepository.getEquipmentById(equipment.id) != null)
        {
            Console.WriteLine("Equipment already exists with given ID: " + equipment.id);
            return null;
        }
        
        return equipmentRepository.addEquipment(equipment);
    }

    public IEnumerable<Equipment> getEquipments()
    {
        return equipmentRepository.getEquipments();
    }

    public Equipment getEquipmentsById(Guid equipmentId)
    {
        return equipmentRepository.getEquipmentById(equipmentId);
    }
}