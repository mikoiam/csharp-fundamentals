using System;
using System.Collections.Generic;

namespace CSharpFundamentals
{
    public static class YieldTest
    {
        public static void Run()
        {
            var numbers = GetNumbers(5);
            
            Console.WriteLine("----------iteration 1----------");
            foreach (var number in numbers)
            {
                if (number == 3) break;
                
                Console.WriteLine(number);
            }
            
            Console.WriteLine("----------iteration 2----------");
            foreach (var number in numbers)
            {
                if (number == 2) break;

                Console.WriteLine(number);
            }
        }

        private static IEnumerable<int> GetNumbers(int count)
        {
            for (var i = 0; i < count; i++)
            {
                Console.WriteLine($"yielding {i}");
                yield return i;
            }
        }
    }
}