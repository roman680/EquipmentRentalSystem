using EquipmentRentalSystem.models.utils;

namespace EquipmentRentalSystem.models.equipment;

public class Laptop : Equipment
{
    public Processor processor {get; set;}
    public int Ram {get; set;}
}