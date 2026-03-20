using EquipmentRentalSystem.models.users;

namespace EquipmentRentalSystem.repository;

public class UserRepositoryImpl : IUserRepository
{
    Dictionary<long, User> users = new Dictionary<long, User>();
    
    public User addUser(User user)
    {
        users.Add(user.id, user);
        return user;
    }

    public User getUser(long userId)
    {
        if (users.TryGetValue(userId, out var user))
        {
            return user;
        }
        Console.WriteLine("User not found");
        return null;
    }

    public IEnumerable<User> getUsers()
    {
        return users.Values;
    }
}