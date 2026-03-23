namespace EquipmentRentalSystem.models.equipment;

public abstract class Equipment
{
    public Guid id { get; set; } = Guid.NewGuid();
    public string name { get; set; } = string.Empty;
    public bool isAvaliable { get; set; } = true;
    public string description { get; set; } = string.Empty;
    public string manufacturer { get; set; } = string.Empty;
}