// Prompt the user to enter the first number

Console.WriteLine("Enter the first number:");
string input1 = Console.ReadLine();
double number1 = Convert.ToDouble(input1);

// Prompt the user to choose an operation
Console.WriteLine("Choose an operation: +, -, *, /");
string operation = Console.ReadLine();

// Prompt the user to enter the second number
Console.WriteLine("Enter the second number:");
string input2 = Console.ReadLine();
double number2 = Convert.ToDouble(input2);

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
    result = number1 / number2;
}

// Display the result
Console.WriteLine("Result: " + result);