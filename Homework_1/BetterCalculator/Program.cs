var continueCalculating = true;


while (continueCalculating)
{
    // Prompt the user to enter the first number
    Console.WriteLine("Enter the first number:");
    var input1 = Console.ReadLine();
    var number1 = Convert.ToDouble(input1);

    // Prompt the user to choose an operation
    Console.WriteLine("Choose an operation: +, -, *, /");
    var operation = Console.ReadLine();

    // Prompt the user to enter the second number
    Console.WriteLine("Enter the second number:");
    var input2 = Console.ReadLine();
    var number2 = Convert.ToDouble(input2);

    // Perform the chosen operation
    double result = 0;
    if (operation == "+")
    {
        result = number1 + number2;
    }
    else if (operation == "-")
    {
        result = number1 - number2;
    }
    else if (operation == "*")
    {
        result = number1 * number2;
    }
    else if (operation == "/")
    {
        if (number1 == 0 || number2 == 0)
        {
            Console.WriteLine("Cannot divide by zero");
            continue;
        }

        result = number1 / number2;
    }

    // Display the result
    Console.WriteLine("Result: " + result);

    // Ask the user if they want to continue
    Console.WriteLine("Do you want to perform another calculation? (yes/no)");
    var continueResponse = Console.ReadLine();
    if (continueResponse?.ToLower() != "yes")
    {
        continueCalculating = false;
    }
}