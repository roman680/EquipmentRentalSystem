namespace EquipmentRentalSystem.models.users;

public class Student : User
{
    public Student(long id, string firstName, string lastName) 
        : base(id, firstName, lastName) {}
}