﻿using EquipmentRentalSystem.models;

namespace EquipmentRentalSystem.service;

public interface IRentService
{
    Rent RentEquipment(Guid userId, Guid equipmentId, int days);
    decimal ReturnEquipment(Guid rentalId);
    IEnumerable<Rent> GetActiveRentalsByUser(Guid userId);
    IEnumerable<Rent> GetOverdueRentals();
    IEnumerable<Rent> GetAllRentals();
    IEnumerable<Rent> GetRentalsByUser(Guid userId);
    Rent RentEquipment(Guid userId, Guid equipmentId, int days, DateTime rentDate);
    decimal ReturnEquipment(Guid rentalId, DateTime returnDate);
}
