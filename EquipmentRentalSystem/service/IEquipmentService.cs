using EquipmentRentalSystem.models.equipment;
using EquipmentRentalSystem.models.users;

namespace EquipmentRentalSystem.repository;

public interface IEquipmentService
{
    Equipment addEquipment(Equipment equipment);
    IEnumerable<Equipment> getEquipments();
    Equipment getEquipmentsById(Guid equipmentId);
}