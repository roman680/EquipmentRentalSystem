using EquipmentRentalSystem.models.users;

namespace EquipmentRentalSystem.service;

public interface IUserService
{
    User addUser(User user);
    User getUserById(Guid userId);
    IEnumerable<User> getUsers();
}