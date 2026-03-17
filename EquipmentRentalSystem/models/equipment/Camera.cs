using EquipmentRentalSystem.enums;
using EquipmentRentalSystem.models.utils;

namespace EquipmentRentalSystem.models.equipment;

public class Camera : Equipment
{
    Processor processor {get; set;}
    public Resolution resolution {get; set;}
}