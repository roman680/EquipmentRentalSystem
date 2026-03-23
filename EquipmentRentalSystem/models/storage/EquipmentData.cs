namespace EquipmentRentalSystem.models.storage;

public class EquipmentData
{
    public string type { get; set; } = string.Empty;
    public Guid id { get; set; }
    public string name { get; set; } = string.Empty;
    public bool isAvaliable { get; set; }
    public string description { get; set; } = string.Empty;
    public string manufacturer { get; set; } = string.Empty;
    public string processorName { get; set; } = string.Empty;
    public int processorCores { get; set; }
    public int ram { get; set; }
    public double screenSize { get; set; }
    public int resolution { get; set; }
    public int zoom { get; set; }
    public int numberOfOpticalLenses { get; set; }
    public int brightnessLumens { get; set; }
}
