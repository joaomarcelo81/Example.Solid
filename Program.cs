// See https://aka.ms/new-console-template for more information
using Example.SOLID.LSP.Violation;
using Example.SOLID.OCP.Solution_Extension_Methods;

Console.WriteLine("Select the operation");
Console.WriteLine("1 - OCP");
Console.WriteLine("2 - LSP");

var option = Console.ReadKey();

switch (option.KeyChar)
{
    case '1':
        ATM.Operations();
        break;
    case '2':
        AreaCalculation.Calculate();
        break;
}