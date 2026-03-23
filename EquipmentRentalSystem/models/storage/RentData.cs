namespace EquipmentRentalSystem.models.storage;

public class RentData
{
    public Guid id { get; set; }
    public DateTime date { get; set; }
    public Guid userId { get; set; }
    public Guid equipmentId { get; set; }
    public int rentalDays { get; set; }
    public DateTime? returnDate { get; set; }
    public decimal penaltyFee { get; set; }
}
