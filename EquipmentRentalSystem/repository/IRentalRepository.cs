﻿using EquipmentRentalSystem.models;

namespace EquipmentRentalSystem.repository;

public interface IRentalRepository
{
    Rent addRent(Rent rent);
    Rent? getRentById(Guid rentId);
    IEnumerable<Rent> getAllRents();
    IEnumerable<Rent> getRentsByEquipmentId(Guid equipmentId);
    IEnumerable<Rent> getRentByUserId(Guid userId);
    void replaceAllRents(IEnumerable<Rent> rents);
}