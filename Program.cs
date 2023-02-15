using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;

var result = new List<string>(); 

Console.WriteLine("Enter a formula:  ");
string formula = Console.ReadLine()!;
Console.Write("result:  ");
Console.ForegroundColor = ConsoleColor.Magenta;
bool isDigit = int.TryParse(formula, out int number);
if (string.IsNullOrEmpty(formula)) { Console.WriteLine(0); }
if(isDigit) { Console.WriteLine(number); }
else
{
    if (formula.Contains(string.Empty)) { formula = formula.Replace(" ", ""); }

    while (formula != "")
    {
        if (Char.IsDigit(formula[0]))
        {
            var num = new String(formula.TakeWhile(c => Char.IsDigit(c)).ToArray());
            result.Add(num);
            formula = formula.Substring(num.Length);
        }
        else
        {
            result.Add(formula.First().ToString());
            formula = formula.Substring(1);
        }
    }

    //Console.Write("Postfix:  ");
    //Console.WriteLine($"{string.Join(",", Postfix(result))}");
    InfixAndCalculation();
}
Console.ResetColor();
Queue<string> Postfix(List<string> formula)
{
    Stack<string> operators = new Stack<string>();
    Queue<string> outputQueue = new Queue<string>();

    string[] ops = { "*", "/", "+", "-" };
    int zähler = 0;
    bool formulaContainsOp = formula[0].Contains("+") || formula[0].Contains("-") || formula[0].Contains("*") || formula[0].Contains("/");


    foreach (var val in formula)
    {

        formulaContainsOp = formula[0].Contains("+") || formula[0].Contains("-") || formula[0].Contains("*") || formula[0].Contains("/");

        if (ops.Contains(val) == false)
        {
            if (zähler == 1 && (formulaContainsOp)) { outputQueue.Enqueue(operators.Pop() + val); }
            else outputQueue.Enqueue(val);

        }
        else
        {
            zähler++;
            if (val == "+" || val == "-")
            {
                if (operators.Count != 0 && operators.Peek() == "*")
                {
                    operators.Pop();
                    outputQueue.Enqueue("*");
                    operators.Push(val);
                }
                else if (operators.Count != 0 && operators.Peek() == "/")
                {
                    operators.Pop();
                    outputQueue.Enqueue("/");
                    operators.Push(val);
                }
                else { operators.Push(val); }
            }
            else if (val == "*" || val == "/")
            {
                if (operators.Count != 0 && operators.Peek() == "*")
                {
                    operators.Pop();
                    outputQueue.Enqueue("*");
                    operators.Push(val);
                }
                else if (operators.Count != 0 && operators.Peek() == "/")
                {
                    operators.Pop();
                    outputQueue.Enqueue("/");
                    operators.Push(val);
                }
                else
                {
                    operators.Push(val);
                }
            }
            else { operators.Push(val); }
        }

    }

    while (operators.Count != 0)
    {
        outputQueue.Enqueue(operators.Pop());
    }

    return outputQueue;
}

void InfixAndCalculation()
{
    Stack<int> numbers = new Stack<int>();
    Queue<string> postFix = Postfix(result);

    bool isDigit = true;
    int number = 0;
    int calculation = 0;
    int counter = 0;

    for (int i = 0; i < postFix.Count; i++)
    {
        isDigit = int.TryParse(postFix.ElementAt(i), out number);

        if (isDigit)
        {
            numbers.Push(number);
            counter++;
        }

        if (!isDigit && counter >= 0)
        {
            if (postFix.ElementAt(i) == "+")
            {
                calculation = numbers.ElementAt(1);
                calculation += numbers.Pop();
                numbers.Pop();
            }
            if (postFix.ElementAt(i) == "-")
            {
                calculation = numbers.ElementAt(1);
                calculation -= numbers.Pop();
                numbers.Pop();
            }
            if (postFix.ElementAt(i) == "*")
            {
                calculation = numbers.ElementAt(1);
                calculation *= numbers.Pop();
                numbers.Pop();
            }
            if (postFix.ElementAt(i) == "/")
            {
                calculation = numbers.ElementAt(1);
                calculation /= numbers.Pop();
                numbers.Pop();
            }

            numbers.Push(calculation);
        }

    }

    Console.WriteLine(numbers.Pop());

}
