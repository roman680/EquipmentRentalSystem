﻿using EquipmentRentalSystem.models.equipment;
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
            throw new Exception("Equipment is null");
        }

        if (equipmentRepository.getEquipmentById(equipment.id) != null)
        {
            throw new Exception("Equipment already exists with given ID: " + equipment.id);
        }
        
        return equipmentRepository.addEquipment(equipment);
    }

    public IEnumerable<Equipment> getAllEquipment()
    {
        return equipmentRepository.getAllEquipment();
    }

    public Equipment? getEquipmentsById(Guid equipmentId)
    {
        return equipmentRepository.getEquipmentById(equipmentId);
    }
    public IEnumerable<Equipment> getAvailibleEquipment()
    {
        var equipments = equipmentRepository.getAllEquipment();
        return equipments.Where(e => e.isAvaliable);
    }

    public Equipment markUnAvailibleEquipment(Guid equipmentId)
    {
        Equipment fromdb = equipmentRepository.getEquipmentById(equipmentId)
                           ?? throw new Exception("Equipment not found");

        if (!fromdb.isAvaliable)
        {
            throw new Exception("Equipment is already unavailable");
        }

        fromdb.isAvaliable = false;
        return fromdb;
    }
}
