// Prompt the user to enter the first number
Console.WriteLine("Enter the first number:");
string input1 = Console.ReadLine();
double number1 = Convert.ToDouble(input1);

// Prompt the user to enter the second number
Console.WriteLine("Enter the second number:");
string input2 = Console.ReadLine();
double number2 = Convert.ToDouble(input2);

// Perform the operations
double addition = number1 + number2;
double subtraction = number1 - number2;
double multiplication = number1 * number2;
double division = number1 / number2;

// Display the results
Console.WriteLine("Addition: " + addition);
Console.WriteLine("Subtraction: " + subtraction);
Console.WriteLine("Multiplication: " + multiplication);
Console.WriteLine("Division: " + division);