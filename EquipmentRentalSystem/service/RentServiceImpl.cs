using EquipmentRentalSystem.models;
using EquipmentRentalSystem.repository;

namespace EquipmentRentalSystem.service;

public class RentServiceImpl : IRentService
{
    private readonly RentalRepositoryImpl rentalRepository;
    
    public Rent addRent(Rent rent)
    {
        return rentalRepository.addRent(rent);
    }

    public Rent RentEquipment(Guid userId, Guid equipmentId, int days)
    {
        throw new NotImplementedException();
    }

    public decimal ReturnEquipment(Guid rentalId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Rent> GetActiveRentalsByUser(Guid userId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Rent> GetOverdueRentals()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Rent> GetAllRentals()
    {
        throw new NotImplementedException();
    }
}