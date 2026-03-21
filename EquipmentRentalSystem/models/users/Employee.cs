namespace EquipmentRentalSystem.models.users;

public class Employee : User
{
    private const int MAX_RENTS = 5;
    public Employee(string firstName, string lastName) 
        : base(firstName, lastName) {}

    public override int getMaxRentals()
    {
        return MAX_RENTS;
    }
}