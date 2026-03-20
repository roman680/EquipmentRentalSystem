using EquipmentRentalSystem.models.users;

namespace EquipmentRentalSystem.repository;

public interface IUserRepository
{
    User addUser(User user);
    User getUser(long userId);
    IEnumerable<User> getUsers();
}