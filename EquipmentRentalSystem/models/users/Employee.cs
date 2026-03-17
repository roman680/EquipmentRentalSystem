namespace EquipmentRentalSystem.models.users;

public class Employee : User
{
    public Employee(long id, string firstName, string lastName) 
        : base(id, firstName, lastName) {}
}