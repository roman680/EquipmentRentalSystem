﻿using EquipmentRentalSystem.models;
using EquipmentRentalSystem.models.users;

namespace EquipmentRentalSystem.service;

public interface IRentPolicyService
{ 
    bool CanUserRent(User user, int activeRents);
    decimal CalculatePenalty(Rent rent);
    decimal GetOneOverdueDayFee();
}
