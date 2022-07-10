using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Calculator2.Tests")]

namespace Calculator2
{
    internal class Calculator
    {
        public Calculator()
        {
            Op = DefaultOp;
            Num1 = DefaultNum1;
            Num2 = DefaultNum2;
            Numbers2 = DefaultNumbers2;
        }


        public readonly Dictionary<string, string> Operators = new()
        {
            {"add", "+"},
            {"subtract", "-"},
            {"multiply", "*"},
            {"divide", "/"},
            {"remainder", "%"},
        };


        private string _op = string.Empty;
        public string Op
        {
            get
            {
                return _op;
            }
            set
            {
                if (IsValidOperator(value))
                {
                    _op = value;
                }
                else
                {
                    throw new ArgumentException($"Invalid operator: '{value}'");
                }
            }
        }
        public readonly string DefaultOp = "+";


        public double Num1 {get; set;}
        public readonly double DefaultNum1 = double.NaN;


        public double Num2 {get; set;}
        public readonly double DefaultNum2 = double.NaN;


        public double[] Numbers2{get; set;}
        public readonly double[] DefaultNumbers2 = Array.Empty<double>();


        private bool IsValidOperator(string op)
        {
            return Operators.ContainsValue(op);
        }


        public bool Num1AndNum2AreSet()
        {
            return !double.IsNaN(Num1) && !double.IsNaN(Num2);
        }


        public bool Num1AndNumbers2AreSet()
        {
            return !double.IsNaN(Num1) && Numbers2.Length > 0;
        }


        public bool Ready()
        {
            return Num1AndNum2AreSet() || Num1AndNumbers2AreSet();
        }


        public double Calculate()
        {
            double result = Op switch
            {
                string when Op == "+" && Num1AndNum2AreSet() => Add(),
                string when Op == "+" && Num1AndNumbers2AreSet() => Add(Numbers2),

                string when Op == "-" && Num1AndNum2AreSet() => Subtract(),
                string when Op == "-" && Num1AndNumbers2AreSet() => Subtract(Numbers2),

                string when Op == "*" && Num1AndNum2AreSet() => Multiply(),
                string when Op == "/" && Num1AndNum2AreSet() => Divide(),
                string when Op == "%" && Num1AndNum2AreSet() => Remainder(),

                _ => throw new ArgumentException($"Invalid combination of operator and operand!"),
            };

            return result;
        }


        public void Reset()
        {
            Op = DefaultOp;
            Num1 = DefaultNum1;
            Num2 = DefaultNum2;
            Numbers2 = DefaultNumbers2;
        }


        private double Add()
        {
            return Math.Round(Num1 + Num2, 2);
        }


        private double Add(double[] numbers)
        {
            double result = Num1;

            foreach (double number in numbers)
            {
                result += number;
            }

            return result;
        }


        private double Subtract()
        {
            return Math.Round(Num1 - Num2, 2);
        }


        private double Subtract(double[] numbers)
        {
            double result = Num1;

            foreach (double number in numbers)
            {
                result -= number;
            }

            return result;
        }


        private double Multiply()
        {
            return Math.Round(Num1 * Num2, 2);
        }


        private double Divide()
        {
            if (Num2 == 0)
            {
                throw new DivideByZeroException("Division by 0!");
            }

            return Math.Round(Num1 / Num2, 2);
        }


        private double Remainder()
        {
            if (Num2 == 0)
            {
                throw new DivideByZeroException("Division by 0!");
            }

            return Math.Round(Num1 % Num2, 2);
        }
    }


    internal class CalculatorController
    {
        public CalculatorController()
        {
            Menu();

            while (UseCalculator)
            {
                Console.WriteLine("\n- - -\n");

                GetUserInput();

                if (Calc.Ready())
                {
                    CalculateAndPresentResult();
                    Calc.Reset();
                }
            }
        }


        private readonly Calculator Calc = new();


        private bool UseCalculator {get; set;} = true;


        private readonly Dictionary<string, string> Options = new()
        {
            {"h", "Help"},
            {"o", "Options"},
            {"op", "Available operators"},
            {"q", "Quit"},
        };


        private void GetUserInput()
        {
            string[] prompts = { "First number", "Operator", "Second number" };

            foreach (string prompt in prompts)
            {
                var input = "";

                while (string.IsNullOrWhiteSpace(input))
                {
                    Console.Write($"{prompt}: ");
                    input = Console.ReadLine() ?? "";
                    input = input.Trim();
                }

                if (IsValidOption(input))
                {
                    ChooseOption(input);
                    Calc.Reset(); //Reset all values
                    break; // start over
                }

                try
                {
                    switch (prompt)
                    {
                        case "First number":
                            Calc.Num1 = double.Parse(input);
                            break;
                        case "Operator":
                            Calc.Op = input;
                            break;
                        case "Second number":
                            if (IsListOfNumbers(input))
                            {
                                Calc.Numbers2 = ExtractNumbers(input);
                            }
                            else
                            {
                                Calc.Num2 = double.Parse(input);
                            }
                            break;
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"\n{e.Message}");
                    break; // start over
                }
                catch (FormatException e)
                {
                    Console.WriteLine($"\n{e.Message}");
                    break; // start over
                }

            }
        }


        private void CalculateAndPresentResult()
        {
            double result = double.NaN;

            try
            {
                result = Calc.Calculate();
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine($"\n{e.Message}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"\n{e.Message}");
            }

            if (!double.IsNaN(result) && Calc.Num1AndNum2AreSet())
            {
                Console.WriteLine($"\n{Calc.Num1} {Calc.Op} {Calc.Num2} = {result}");
            }

            if (!double.IsNaN(result) && Calc.Num1AndNumbers2AreSet())
            {
                Console.WriteLine($"\n{Calc.Num1} {Calc.Op} {string.Join($" {Calc.Op} ", Calc.Numbers2)} = {result}");
            }
        }


        private void Menu()
        {
            Console.WriteLine("\n*-*-*-*-*-*- Welcome to Calculator -*-*-*-*-*-*");
            Console.WriteLine("\nWhen prompted, enter a number, an operator and a second number, then press Enter to calculate the result.\n");

            Console.WriteLine("--Ex 1--                     --Ex 2--");
            Console.WriteLine("First number: 2              First number: 2");
            Console.WriteLine("Operator: +                  Operator: +");
            Console.WriteLine("Second number: 3,5           Second number: 3, 24,5, 4, 1,5");
            Console.WriteLine("(Output: 2 + 3,5 = 5,5)      (Output: 2 + 3 + 24,5 + 4 + 1,5 = 35)\n");

            Console.WriteLine("NOTE: only '+' and '-' operators accept a comma-separated list of numbers as input for their second operand.");

            ListOptions();
        }


        private void ListOptions()
        {
            Console.WriteLine("\n- - - Options- - -");
            foreach (KeyValuePair<string, string> option in Options)
            {
                Console.WriteLine($"'{option.Key}' = {option.Value}");
            }
        }


        private bool IsValidOption(string input)
        {
            return Options.ContainsKey(input);
        }


        private void ChooseOption(string choice)
        {
            switch (choice.ToLower())
            {
                case "h":
                    Menu();
                    break;
                case "o":
                    ListOptions();
                    break;
                case "op":
                    ListOperators();
                    break;
                case "q":
                    UseCalculator = false;
                    Console.WriteLine("\nProgram ended. Bye!");
                    break;
            }
        }


        private void ListOperators()
        {
            Console.WriteLine("\n- - - Possible operations - - -");

            foreach (var op in Calc.Operators)
            {
                Console.WriteLine($"'{op.Value}' = {op.Key}");
            }
        }


        private static bool ContainsMultipleDigits(string input)
        {
            int digits = 0;

            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    digits++;
                }
            }
            
            return digits > 1;
        }


        private static bool IsCommaSeparated(string input)
        {

            return input.Contains(", "); // input list must include whitespace after comma to allow for use of decimal numbers, e.g: [4, 5,5, 6]
        }


        private static bool IsListOfNumbers(string input) 
        {
            return ContainsMultipleDigits(input) && IsCommaSeparated(input);
        }


        private static double[] ExtractNumbers(string input)
        {
            string delimiter = ", ";

            string[] numeric = input.Split(new string[] {delimiter}, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            double[] numbers = new double[numeric.Length];

            for (int i = 0; i < numeric.Length; i++)
            {
                if (numeric[i].EndsWith(','))
                {
                    numeric[i] = numeric[i].Remove(numeric[i].LastIndexOf(',')); //remove any superfluous commas, e.g: "5,5," => "5,5"
                }
                
                _ = double.TryParse(numeric[i], out numbers[i]);
            }

            return numbers;
        }
    }
}