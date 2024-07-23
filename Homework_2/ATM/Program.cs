//Creating a storage for user credentials and balances
var userCredentials = new Dictionary<string, string>();
var userBalances = new Dictionary<string, double>();


//Flag to exit the main menu loop
var exit = false;

//Main menu loop
while (!exit)
{
    Console.WriteLine("Welcome to the ATM");
    Console.WriteLine("1. Log-in");
    Console.WriteLine("2. Sign-in");
    Console.WriteLine("3. Leave");
    Console.Write("Enter your choice: ");
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            LogIn();
            break;
        case "2":
            SignIn();
            break;
        case "3":
            exit = true;
            Console.WriteLine("Goodbye!");
            break;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
}


void SignIn()
{
    Console.Write("Enter a new username: ");
    var username = Console.ReadLine();

    if (userCredentials.ContainsKey(username))
    {
        Console.WriteLine("Error: User with this name already exists.");
        return;
    }

    Console.Write("Enter a new password: ");
    var password = Console.ReadLine();

    userCredentials[username] = password;
    userBalances[username] = 0.0;

    Console.WriteLine("Account created successfully!");
}

void LogIn()
{
    Console.Write("Enter username: ");
    var username = Console.ReadLine();

    Console.Write("Enter password: ");
    var password = Console.ReadLine();

    if (userCredentials.ContainsKey(username) && userCredentials[username] == password)
    {
        Console.WriteLine("Login successful!");
        LoggedInMenu(username);
    }
    else
    {
        Console.WriteLine("Error: Wrong credentials.");
    }
}

void LoggedInMenu(string username)
{
    var logout = false;

    while (!logout)
    {
        Console.WriteLine("1. Check Balance");
        Console.WriteLine("2. Deposit");
        Console.WriteLine("3. Withdraw");
        Console.WriteLine("4. Log Out");
        Console.Write("Enter your choice: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.WriteLine($"Your balance is: {userBalances[username]:C}");
                break;
            case "2":
                Deposit(username);
                break;
            case "3":
                Withdraw(username);
                break;
            case "4":
                logout = true;
                Console.WriteLine("Logged out successfully!");
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
}

void Deposit(string username)
{
    Console.Write("Enter amount to deposit: ");
    if (double.TryParse(Console.ReadLine(), out var amount) && amount > 0)
    {
        userBalances[username] += amount;
        Console.WriteLine($"Deposit successful! New balance: {userBalances[username]:C}");
    }
    else
    {
        Console.WriteLine("Invalid amount. Please enter a positive number.");
    }
}

void Withdraw(string username)
{
    Console.Write("Enter amount to withdraw: ");
    if (double.TryParse(Console.ReadLine(), out var amount) && amount > 0)
    {
        if (userBalances[username] >= amount)
        {
            userBalances[username] -= amount;
            Console.WriteLine($"Withdrawal successful! New balance: {userBalances[username]:C}");
        }
        else
        {
            Console.WriteLine("Error: Insufficient funds.");
        }
    }
    else
    {
        Console.WriteLine("Invalid amount. Please enter a positive number.");
    }
}