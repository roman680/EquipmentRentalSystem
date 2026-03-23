﻿using EquipmentRentalSystem.models.equipment;

namespace EquipmentRentalSystem.repository;

public interface IEquipmentRepository
{
    Equipment addEquipment(Equipment equipment);
    IEnumerable<Equipment> getAllEquipment();
    Equipment? getEquipmentById(Guid equipmentId);
    void replaceAllEquipment(IEnumerable<Equipment> equipments);
}