using EquipmentRentalSystem.models.utils;

namespace EquipmentRentalSystem.models.equipment;

public class Laptop : Equipment
{
    public Processor processor {get; set;}
    public int Ram {get; set;}
    public double screenSize { get; set; }

    public Laptop(Processor processor, int ram, double screenSize)
    {
        this.processor = processor;
        Ram = ram;
        this.screenSize = screenSize;
    }
}