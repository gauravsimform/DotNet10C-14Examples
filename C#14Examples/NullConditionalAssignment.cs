using System;
using System.Collections.Generic;
using System.Text;

namespace C_14Examples
{
    public class NullConditionalAssignment
    {
        public static void NullConditionDemo()
        {
            User user = new User();
            
            // OlD way (C# before 14)
            if (user != null)
            {
                user.Name = "Alice";
                Console.WriteLine($"Your Name is {user.Name}");
            }

            // NEW way (C# 14 Null-Conditional Assignment)
            user?.Name = "Bob";
            Console.WriteLine($"Your Name is {user?.Name}");
        }
    }

    public class User
    {
        public string Name { get; set; }
    }
}
