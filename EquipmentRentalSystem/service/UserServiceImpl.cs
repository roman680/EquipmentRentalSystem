using EquipmentRentalSystem.models.users;
using EquipmentRentalSystem.repository;

namespace EquipmentRentalSystem.service;

public class UserServiceImpl : IUserService
{
    IUserRepository _userRepository;
    
    public UserServiceImpl(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public User addUser(User user)
    {
        if (user == null)
        {
            Console.WriteLine("User is null");
            return null;
        }

        if (_userRepository.getUser(user.id) != null)
        {
            Console.WriteLine("User already exists with given ID: " + user.id);
            return null;
        }
        
        return _userRepository.addUser(user);
    }

    public User getUser(long userId)
    {
        return _userRepository.getUser(userId);
    }

    public IEnumerable<User> getUsers()
    {
        return _userRepository.getUsers();
    }
}