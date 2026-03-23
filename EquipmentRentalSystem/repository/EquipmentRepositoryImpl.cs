﻿using EquipmentRentalSystem.models.equipment;

namespace EquipmentRentalSystem.repository;

public class EquipmentRepositoryImpl : IEquipmentRepository
{
    private Dictionary<Guid, Equipment> equipments = new Dictionary<Guid, Equipment>();
    
    public Equipment addEquipment(Equipment equipment)
    {
        equipments.Add(equipment.id, equipment);
        return equipment;
    }

    public IEnumerable<Equipment> getAllEquipment()
    {
        return equipments.Values;
    }

    public Equipment? getEquipmentById(Guid equipmentId)
    {
        if (equipments.TryGetValue(equipmentId, out var equipment))
        {
            return equipment;
        }
        return null;
    }

    public void replaceAllEquipment(IEnumerable<Equipment> equipments)
    {
        this.equipments = equipments.ToDictionary(equipment => equipment.id, equipment => equipment);
    }
}