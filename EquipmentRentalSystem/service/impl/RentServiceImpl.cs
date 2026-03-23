using EquipmentRentalSystem.models;
using EquipmentRentalSystem.repository;

namespace EquipmentRentalSystem.service;

public class RentServiceImpl : IRentService
{
    private readonly IUserService userService;
    private readonly IEquipmentService equipmentService;
    private readonly IRentalRepository rentRepository;
    private readonly IRentPolicyService policyService;

    public RentServiceImpl(
        IUserService userService,
        IEquipmentService equipmentService,
        IRentalRepository rentRepository,
        IRentPolicyService policyService)
    {
        this.userService = userService;
        this.equipmentService = equipmentService;
        this.rentRepository = rentRepository;
        this.policyService = policyService;
    }

    public Rent RentEquipment(Guid userId, Guid equipmentId, int days)
    {
        return RentEquipment(userId, equipmentId, days, DateTime.Now);
    }

    public Rent RentEquipment(Guid userId, Guid equipmentId, int days, DateTime rentDate)
    {
        if (days <= 0)
        {
            throw new Exception("Rental days must be greater than zero");
        }

        var user = userService.getUserById(userId)
            ?? throw new Exception("User not found");

        var equipment = equipmentService.getEquipmentsById(equipmentId)
            ?? throw new Exception("Equipment not found");

        if (!equipment.isAvaliable)
        {
            throw new Exception("Equipment is not available");
        }

        var activeRents = rentRepository.getAllRents()
            .Count(r => r.userId == userId && r.returnDate == null);

        if (!policyService.CanUserRent(user, activeRents))
        {
            throw new Exception("User rental limit exceeded");
        }

        var rent = new Rent(rentDate, userId, equipmentId, days);

        rentRepository.addRent(rent);
        equipment.isAvaliable = false;

        return rent;
    }

    public decimal ReturnEquipment(Guid rentId)
    {
        return ReturnEquipment(rentId, DateTime.Now);
    }

    public decimal ReturnEquipment(Guid rentId, DateTime returnDate)
    {
        var rent = rentRepository.getRentById(rentId)
            ?? throw new Exception("Rent not found");

        if (rent.returnDate != null)
        {
            throw new Exception("Already returned");
        }

        var penalty = returnDate <= rent.DueDate
            ? 0
            : (returnDate.Date - rent.DueDate.Date).Days * policyService.GetOneOverdueDayFee();

        rent.Return(returnDate, penalty);

        var equipment = equipmentService.getEquipmentsById(rent.equipmentId)
            ?? throw new Exception("Equipment not found");

        equipment.isAvaliable = true;

        return rent.penaltyFee;
    }

    public IEnumerable<Rent> GetActiveRentalsByUser(Guid userId)
    {
        return rentRepository.getAllRents()
            .Where(r => r.userId == userId && r.returnDate == null);
    }

    public IEnumerable<Rent> GetOverdueRentals()
    {
        return rentRepository.getAllRents()
            .Where(r => r.IsOverdue);
    }

    public IEnumerable<Rent> GetAllRentals()
    {
        return rentRepository.getAllRents();
    }

    public IEnumerable<Rent> GetRentalsByUser(Guid userId)
    {
        return rentRepository.getRentByUserId(userId);
    }
}
