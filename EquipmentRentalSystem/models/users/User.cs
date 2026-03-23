namespace EquipmentRentalSystem.models.users;

public abstract class User
{
    public Guid id { get; set; } = Guid.NewGuid();
    public string name { get; set; }
    public string lastName { get; set; }
    public string userType { get; protected set; } = string.Empty;

    public abstract int getMaxRentals();

    public User(string name, string lastName)
    {
        this.name = name;
        this.lastName = lastName;
    }
}