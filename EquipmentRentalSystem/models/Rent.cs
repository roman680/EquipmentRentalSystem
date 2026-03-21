using EquipmentRentalSystem.models.equipment;
using EquipmentRentalSystem.models.users;

namespace EquipmentRentalSystem.models;

public class Rent
{
    public Guid id { get; set; } = Guid.NewGuid();
    public DateTime date {get; set;}
    public Guid userId {get; set;}
    public Guid equipmentId {get; set;}
    public int rentalDays  {get; set;}
    public DateTime? returnDate { get; private set; }

    public Rent(DateTime date, Guid userId, Guid equipmentId, int rentalDays)
    {
        this.date = date;
        this.userId = userId;
        this.equipmentId = equipmentId;
        this.rentalDays = rentalDays;
    }
    
    public void Return(DateTime returnDate)
    {
        this.returnDate = returnDate;
    }
    public DateTime DueDate => date.AddDays(rentalDays);

    public bool IsActive => returnDate == null;

    public bool IsOverdue => returnDate == null && DateTime.Now > DueDate;
}