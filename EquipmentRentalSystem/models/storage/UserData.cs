namespace EquipmentRentalSystem.models.storage;

public class UserData
{
    public string type { get; set; } = string.Empty;
    public Guid id { get; set; }
    public string name { get; set; } = string.Empty;
    public string lastName { get; set; } = string.Empty;
}
