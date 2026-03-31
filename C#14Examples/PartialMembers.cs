using System;
using System.Collections.Generic;
using System.Text;

namespace C_14Examples
{
    public class PartialMembers
    {
        public static void PartialMembersDemo()
        {
            var calc = new Calculator();
            var result = calc.Sum(10,20);

            Console.WriteLine($"Result: {result}");
        }
    }
    public partial class Calculator
    {
        public int Sum(int a, int b)
        {
            Log($"Adding {a} and {b}");
            return a + b;
        }

        // C# 14 allows accessibility + return type
        public partial string Log(string message);
    }

    // Implementation part
    public partial class Calculator
    {
        public partial string Log(string message)
        {
            Console.WriteLine($"Log: {message}");
            return message;
        }
    }

}
