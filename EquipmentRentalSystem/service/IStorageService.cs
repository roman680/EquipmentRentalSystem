namespace EquipmentRentalSystem.service;

public interface IStorageService
{
    void SaveToJson(string path);
    void LoadFromJson(string path);
}
