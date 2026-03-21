using EquipmentRentalSystem.models.equipment;
using EquipmentRentalSystem.models.users;

namespace EquipmentRentalSystem.models;

public class Rent
{
    public Guid id { get; set; }
    public DateTime date {get; set;}
    public Guid userId {get; set;}
    public Guid equipmentId {get; set;}
    public int rentalDays  {get; set;}

    public Rent(DateTime date, Guid userId, Guid equipmentId, int rentalDays)
    {
        this.date = date;
        this.userId = userId;
        this.equipmentId = equipmentId;
        this.rentalDays = rentalDays;
    }
}