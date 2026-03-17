class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("---- Equipment Rental System ----");
            Console.WriteLine("1. Add new user to the system");
            Console.WriteLine("2. Add new Equipment item of selected type");
            Console.WriteLine("3. Display equipment list with current status");
            Console.WriteLine("4. Display available for rental equipment");
            Console.WriteLine("5. Rent Equipment");
            Console.WriteLine("6. Return equipment(possible penalty)");
            Console.WriteLine("7. Mark equpment as anavalible (for example damage)");
            Console.WriteLine("8. Display active rental for a selected user");
            Console.WriteLine("9. Display list of overdue rentals");
            Console.WriteLine("10. Generate Report of rental Service state");  
            Console.WriteLine("11. Exit");
            Console.WriteLine("---------------------------------");
            
            Console.WriteLine("Choose an option:");
            var input =  Console.ReadLine();
        } 
    }

}