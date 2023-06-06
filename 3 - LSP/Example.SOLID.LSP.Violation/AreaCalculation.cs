using System;

namespace Example.SOLID.LSP.Violation
{
    public class AreaCalculation
    {
        private static void GetAreaRectangle(Rectangle ret)
        {
            Console.Clear();
            Console.WriteLine("Rectangle area calculation");
            Console.WriteLine();
            Console.WriteLine(ret.Height + " * " + ret.Width);
            Console.WriteLine();
            Console.WriteLine(ret.Area);
            Console.ReadKey();
        }

        public static void Calculate()
        {
            var quad = new Square()
            {
                Height = 10,
                Width = 5
            };

            GetAreaRectangle(quad);
        }
    }
}