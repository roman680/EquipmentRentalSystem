using EquipmentRentalSystem.enums;
using EquipmentRentalSystem.models.utils;

namespace EquipmentRentalSystem.models.equipment;

public class Camera : Equipment
{
    public Processor processor {get; set;}
    public Resolution resolution {get; set;}

    public Camera(Processor processor, Resolution resolution)
    {
        this.processor = processor;
        this.resolution = resolution;
    }
}