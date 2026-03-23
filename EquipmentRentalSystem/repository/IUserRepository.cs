﻿using EquipmentRentalSystem.models.users;

namespace EquipmentRentalSystem.repository;

public interface IUserRepository
{
    User addUser(User user);
    User? getUser(Guid userId);
    IEnumerable<User> getAllUsers();
    void replaceAllUsers(IEnumerable<User> users);
}