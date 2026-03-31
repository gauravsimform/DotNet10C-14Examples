using System;
using System.Collections.Generic;
using System.Text;

namespace C_14Examples
{
    public class ExtensionMembers
    {
        public static void ExtensionDemo()
        {
            string text = "Hello";

            // OLD way (C# before 14)
            //Console.WriteLine("OLD Way:");
            //Console.WriteLine($"{StringExtensionsOld.ToPalindrome(text)}");

            // NEW way (C# 14 Extension Members)
            Console.WriteLine("\nNEW Way (C# 14 Extension Members):");
            Console.WriteLine($"Palindrome: {text.ToPalindrome()}");
        }

    }
    //public static class StringExtensionsOld
    //{
    //    public static string ToPalindrome(this string str)
    //    {
    //        if (string.IsNullOrEmpty(str))
    //            return str;

    //        var reversed = new string(str.Reverse().ToArray());
    //        return str + reversed.Substring(1);
    //    }
    //}

    public static class StringExtensionsNew
    {
       extension(string str)
        {
            public string ToPalindrome()
            {
                if (string.IsNullOrEmpty(str))
                    return str;
                var reversed = new string(str.Reverse().ToArray());
                return str + reversed.Substring(1);
            }
        }
    }
}
