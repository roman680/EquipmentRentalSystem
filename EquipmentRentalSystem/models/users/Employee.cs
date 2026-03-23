﻿namespace EquipmentRentalSystem.models.users;

public class Employee : User
{
    private const int MAX_RENTS = 5;
    public Employee(string firstName, string lastName) 
        : base(firstName, lastName)
    {
        userType = "Employee";
    }

    public override int getMaxRentals()
    {
        return MAX_RENTS;
    }
}