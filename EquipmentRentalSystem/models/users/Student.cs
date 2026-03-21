namespace EquipmentRentalSystem.models.users;

public class Student : User
{
    private const int MAX_RENTS = 2;
    public Student(string firstName, string lastName) 
        : base(firstName, lastName) {}

    public override int getMaxRentals()
    {
        return MAX_RENTS;
    }
}