using EquipmentRentalSystem.models.users;

namespace EquipmentRentalSystem.service;

public interface IUserService
{
    User addUser(User user);
    User getUser(long userId);
    IEnumerable<User> getUsers();
}