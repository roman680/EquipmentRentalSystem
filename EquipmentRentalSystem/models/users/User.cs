namespace EquipmentRentalSystem.models.users;

public abstract class User
{
    public long id { get; set; }
    public string name { get; set; }
    public string lastName { get; set; }

    public User(long id, string name, string lastName)
    {
        this.id = id;
        this.name = name;
        this.lastName = lastName;
    }
}