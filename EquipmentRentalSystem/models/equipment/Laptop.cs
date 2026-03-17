using EquipmentRentalSystem.models.utils;

namespace EquipmentRentalSystem.models.equipment;

public class Laptop : Equipment
{
    public Processor processor {get; set;}
    public int Ram {get; set;}

    public Laptop(Processor processor, int ram)
    {
        this.processor = processor;
        Ram = ram;
    }
}