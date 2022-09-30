using System;

namespace JacobiMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[,] a_matrix = {
            {15.7, 1.2, -2.1, 1.7},
            {0.1, 17.5, 2.3, -3.5},
            {-3.7, 1.4, -16.2, 2.1},
            {0.5, 2.4, 1.9, 19.7 }
            };

            double[] b_matrix = { 14.24, 78.39, -31.60, 49.20 };
            LinearEquationsSystem equationsSystem = new LinearEquationsSystem(a_matrix, b_matrix);

            var result = equationsSystem.Jacobi(0.001);

            Console.WriteLine("RESULT: ");
            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine("x" + i + ": " + result[i]);
            }
            Console.ReadKey();
        }
    }
}
