using EquipmentRentalSystem.enums;

namespace EquipmentRentalSystem.models.equipment;

public class Projector : Equipment
{
    public Resolution resolution {get; set;}
    public int numberOfOpticalLenses {get; set;}
    public int brightnessLumens { get; set; }

    public Projector(Resolution resolution, int numberOfOpticalLenses, int brightnessLumens)
    {
        this.resolution = resolution;
        this.numberOfOpticalLenses = numberOfOpticalLenses;
        this.brightnessLumens = brightnessLumens;
    }
}