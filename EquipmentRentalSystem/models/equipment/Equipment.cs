namespace EquipmentRentalSystem.models.equipment;

public class Equipment
{
    public long id { get; set; }
    public string name { get; set; }
    public bool isAvaliable { get; set; }
    public string description { get; set; }
}