using EquipmentRentalSystem.models.users;

namespace EquipmentRentalSystem.repository;

public class UserRepositoryImpl : IUserRepository
{
    Dictionary<Guid, User> users = new Dictionary<Guid, User>();
    
    public User addUser(User user)
    {
        users.Add(user.id, user);
        return user;
    }

    public User getUser(Guid userId)
    {
        if (users.TryGetValue(userId, out var user))
        {
            return user;
        }
        Console.WriteLine("User not found");
        return null;
    }

    public IEnumerable<User> getAllUsers()
    {
        return users.Values;
    }
}