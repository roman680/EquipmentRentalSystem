using EquipmentRentalSystem.models.equipment;

namespace EquipmentRentalSystem.repository;

public class EqupmentRepositoryImpl : IEquipmentRepository
{
    private Dictionary<long, Equipment> equipments = new Dictionary<long, Equipment>();
    
    public Equipment addEquipment(Equipment equipment)
    {
        equipments.Add(equipment.id, equipment);
        return equipment;
    }

    public IEnumerable<Equipment> getEquipments()
    {
        return equipments.Values;
    }

    public Equipment getEquipmentById(long equipmentId)
    {
        if (equipments.TryGetValue(equipmentId, out var equipment))
        {
            return equipment;
        }
        Console.WriteLine("User not found");
        return null;
    }
}