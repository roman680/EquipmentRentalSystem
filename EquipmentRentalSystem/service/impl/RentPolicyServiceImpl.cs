﻿using EquipmentRentalSystem.models;
using EquipmentRentalSystem.models.users;

namespace EquipmentRentalSystem.service;

public class RentPolicyServiceImpl : IRentPolicyService
{
    private const int ONE_OVERDUE_DAY_FEE = 5;
    
    public bool CanUserRent(User user, int activeRentals)
    {
        return activeRentals < user.getMaxRentals();
    }

    public decimal CalculatePenalty(Rent rental)
    {
        if (rental.returnDate == null || rental.returnDate <= rental.DueDate)
            return 0;

        var daysLate = (rental.returnDate.Value - rental.DueDate).Days;
        return daysLate * ONE_OVERDUE_DAY_FEE; 
    }

    public decimal GetOneOverdueDayFee()
    {
        return ONE_OVERDUE_DAY_FEE;
    }
}
