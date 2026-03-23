﻿using EquipmentRentalSystem.models.users;
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
            throw new Exception("User is null");
        }

        if (userRepository.getUser(user.id) != null)
        {
            throw new Exception("User already exists with given ID: " + user.id);
        }
        
        return userRepository.addUser(user);
    }

    public User? getUserById(Guid userId)
    {
        return userRepository.getUser(userId);
    }

    public IEnumerable<User> getAllUsers()
    {
        return userRepository.getAllUsers();
    }
}
