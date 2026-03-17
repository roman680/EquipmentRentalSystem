namespace EquipmentRentalSystem.models.utils;

public class Processor
{
    public int numberOfCores {get; set;}
    public string name { get; set; }

    public Processor(int numberOfCores, string name)
    {
        this.numberOfCores = numberOfCores;
        this.name = name;
    }
}