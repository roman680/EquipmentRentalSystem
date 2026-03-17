using EquipmentRentalSystem.enums;

namespace EquipmentRentalSystem.models.equipment;

public class Projector : Equipment
{
    public Resolution resolution {get; set;}
    public int numberOfOpticalLenses {get; set;}
}