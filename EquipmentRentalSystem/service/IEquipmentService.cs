using EquipmentRentalSystem.models.equipment;
using EquipmentRentalSystem.models.users;

namespace EquipmentRentalSystem.repository;

public interface IEquipmentService
{
    Equipment addEquipment(Equipment equipment);
    IEnumerable<Equipment> getAllEquipment();
    Equipment getEquipmentsById(Guid equipmentId);
    IEnumerable<Equipment> getAvailibleEquipment();
    Equipment markUnAvailibleEquipment(Guid equipmentId);
}