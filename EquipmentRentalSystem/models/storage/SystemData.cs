namespace EquipmentRentalSystem.models.storage;

public class SystemData
{
    public List<UserData> users { get; set; } = new();
    public List<EquipmentData> equipments { get; set; } = new();
    public List<RentData> rents { get; set; } = new();
}
