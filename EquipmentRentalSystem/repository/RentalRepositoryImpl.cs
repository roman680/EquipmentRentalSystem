﻿using EquipmentRentalSystem.models;

namespace EquipmentRentalSystem.repository;

public class RentalRepositoryImpl : IRentalRepository
{
    private readonly List<Rent> rents = new();

    public Rent addRent(Rent rental)
    {
        rents.Add(rental);
        return rental;
    }

    public Rent? getRentById(Guid rentalId)
    {
        return rents.FirstOrDefault(r => r.id == rentalId);
    }

    public IEnumerable<Rent> getAllRents()
    {
        return rents;
    }

    public IEnumerable<Rent> getRentsByEquipmentId(Guid equipmentId)
    {
        return rents.Where(r => r.equipmentId == equipmentId);
    }

    public IEnumerable<Rent> getRentByUserId(Guid userId)
    {
        return rents.Where(r => r.userId == userId);
    }

    public void replaceAllRents(IEnumerable<Rent> rents)
    {
        this.rents.Clear();
        this.rents.AddRange(rents);
    }
}