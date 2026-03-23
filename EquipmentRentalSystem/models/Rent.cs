namespace EquipmentRentalSystem.models;

public class Rent
{
    public Guid id { get; set; } = Guid.NewGuid();
    public DateTime date { get; set; }
    public Guid userId { get; set; }
    public Guid equipmentId { get; set; }
    public int rentalDays { get; set; }
    public DateTime? returnDate { get; private set; }
    public decimal penaltyFee { get; private set; }
    public bool returnedOnTime { get; private set; }

    public Rent(DateTime date, Guid userId, Guid equipmentId, int rentalDays)
    {
        this.date = date;
        this.userId = userId;
        this.equipmentId = equipmentId;
        this.rentalDays = rentalDays;
        returnedOnTime = false;
    }

    public void Return(DateTime returnDate, decimal penaltyFee)
    {
        this.returnDate = returnDate;
        this.penaltyFee = penaltyFee;
        returnedOnTime = returnDate <= DueDate;
    }

    public DateTime DueDate => date.AddDays(rentalDays);

    public bool IsActive => returnDate == null;

    public bool IsOverdue => returnDate == null && DateTime.Now > DueDate;
}