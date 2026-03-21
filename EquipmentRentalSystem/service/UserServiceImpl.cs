using EquipmentRentalSystem.models.users;
using EquipmentRentalSystem.repository;

namespace EquipmentRentalSystem.service;

public class UserServiceImpl : IUserService
{
    private readonly IUserRepository userRepository;
    
    public UserServiceImpl(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    public User addUser(User user)
    {
        if (user == null)
        {
            Console.WriteLine("User is null");
            return null;
        }

        if (userRepository.getUser(user.id) != null)
        {
            Console.WriteLine("User already exists with given ID: " + user.id);
            return null;
        }
        
        return userRepository.addUser(user);
    }

    public User getUser(Guid userId)
    {
        return userRepository.getUser(userId);
    }

    public IEnumerable<User> getUsers()
    {
        return userRepository.getUsers();
    }
}