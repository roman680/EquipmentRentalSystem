﻿using EquipmentRentalSystem.models.users;

namespace EquipmentRentalSystem.repository;

public class UserRepositoryImpl : IUserRepository
{
    private Dictionary<Guid, User> users = new Dictionary<Guid, User>();
    
    public User addUser(User user)
    {
        users.Add(user.id, user);
        return user;
    }

    public User? getUser(Guid userId)
    {
        if (users.TryGetValue(userId, out var user))
        {
            return user;
        }
        return null;
    }

    public IEnumerable<User> getAllUsers()
    {
        return users.Values;
    }

    public void replaceAllUsers(IEnumerable<User> users)
    {
        this.users = users.ToDictionary(user => user.id, user => user);
    }
}