namespace EquipmentRentalSystem.models.users;

public abstract class User
{
    public Guid id { get; } = Guid.NewGuid();
    public string name { get; set; }
    public string lastName { get; set; }

    public abstract int getMaxRentals();

    public User(string name, string lastName)
    {
        this.name = name;
        this.lastName = lastName;
    }
}