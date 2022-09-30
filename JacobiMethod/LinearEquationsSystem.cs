using System;

namespace JacobiMethod
{
    internal class LinearEquationsSystem
    {
        double[,] aMatrix;
        double[] bMatrix;

        public LinearEquationsSystem(double[,] a_matrix, double[] b_matrix)
        {
            if (a_matrix.GetLength(0) != b_matrix.Length)
            {
                throw new Exception("b_matrix must have the same number of column as a_matrix");
            }
            this.aMatrix = a_matrix;
            this.bMatrix = b_matrix;
        }

        public double[] Jacobi(double error)
        {
            double maxNorma = FindNorma();
            if (maxNorma>=1)
            {
                throw new Exception("It is not dominant diagonal matrix");
            }
            if (aMatrix.GetLength(0) != aMatrix.GetLength(1))
            {
                throw new Exception("Matrix isn't square");
            }

            int size = aMatrix.GetLength(0);

            double[] xPrev = MakeFirstXMatrix();
            double[] xCurrent = new double[size];

            double current_error = error * 2;

            double maxXdif = 0;

            Console.WriteLine("Iteration 0:");
            for(int i=0; i < size; i++)
            {
                Console.WriteLine("x" + i + ": " + xPrev[i]);
            }
            Console.WriteLine();

            int iteraton = 1;
            while(error < current_error)
            {
                Console.WriteLine("Iteration "+iteraton+": ");
                for (int i = 0; i < size; i++)
                {
                    xCurrent[i] = bMatrix[i];
                    for(int j = 0; j < size; j++)
                    {
                        if (i != j)
                        {
                            xCurrent[i] = xCurrent[i]-(aMatrix[i, j] * xPrev[j]);
                        }
                    }
                    xCurrent[i] /= aMatrix[i, i];
                    Console.WriteLine("x"+i+": "+ xCurrent[i]);
                    if (Math.Abs(xCurrent[i] - xPrev[i]) > maxXdif)
                    {
                        maxXdif = Math.Abs(xCurrent[i] - xPrev[i]);
                    }
                }
                current_error = maxNorma / (1 - maxNorma) * maxXdif;
                maxXdif = 0;
                Console.WriteLine("error: "+ current_error);
                Console.WriteLine();
                xCurrent.CopyTo(xPrev, 0);
                iteraton++;
            }

            return xCurrent;
        }

        public double[] GaussSeidel(double error)
        {
            double maxNorma = FindGaussNorma();
            if (maxNorma >= 1)
            {
                throw new Exception("It is not dominant diagonal matrix");
            }
            if (aMatrix.GetLength(0) != aMatrix.GetLength(1))
            {
                throw new Exception("Matrix isn't square");
            }

            int size = aMatrix.GetLength(0);

            double[] xPrev = MakeFirstXMatrix();
            double[] xCurrent = new double[size];

            double current_error = error * 2;

            double maxXdif = 0;

            Console.WriteLine("Iteration 0:");
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("x" + i + ": " + xPrev[i]);
            }
            Console.WriteLine();

            int iteraton = 1;
            while (error < current_error)
            {
                Console.WriteLine("Iteration " + iteraton + ": ");
                for (int i = 0; i < size; i++)
                {
                    xCurrent[i] = bMatrix[i];
                    for (int j = 0; j < size; j++)
                    {
                        if (i != j)
                        {
                            xCurrent[i] = xCurrent[i] - (aMatrix[i, j] * xPrev[j]);
                        }
                    }
                    xCurrent[i] /= aMatrix[i, i];
                    Console.WriteLine("x" + i + ": " + xCurrent[i]);
                    if (Math.Abs(xCurrent[i] - xPrev[i]) > maxXdif)
                    {
                        maxXdif = Math.Abs(xCurrent[i] - xPrev[i]);
                    }
                }
                current_error = maxNorma / (1 - maxNorma) * maxXdif;
                maxXdif = 0;
                Console.WriteLine("error: " + current_error);
                Console.WriteLine();
                xCurrent.CopyTo(xPrev, 0);
                iteraton++;
            }

            return xCurrent;
        }

        private double FindGaussNorma()
        {
            double s = 0;
            double r = 0;
            double m = 0;
            for(int i=0; i < aMatrix.GetLength(0); i++)
            {
                for(int j=0; j < aMatrix.GetLength(1); j++)
                {
                    if (j < i)
                    {
                        s += Math.Abs(aMatrix[i, j] / aMatrix[i,i]);
                    }
                    if (i > j)
                    {
                        r += Math.Abs(aMatrix[i, j] / aMatrix[i, i]);
                    }
                }
                if (Math.Abs(r / (1 - s)) > m)
                {
                    m = Math.Abs(r / (1 - s));
                }
            }
            return m;
        }

        private double FindNorma()
        {
            double maxNorma = 0;
            double rowSumBuffer = 0;
            for (int i = 0; i < aMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < aMatrix.GetLength(1); j++)
                {
                    if (j != i)
                    {
                        rowSumBuffer += aMatrix[i, j];
                    }
                }
                if (maxNorma < Math.Abs(rowSumBuffer / aMatrix[i, i]))
                {
                    maxNorma = Math.Abs(rowSumBuffer / aMatrix[i, i]);
                }
                rowSumBuffer = 0;
            }
            return maxNorma;
        }

        private double[] MakeFirstXMatrix()
        {
            double[] x0 = new double[bMatrix.Length];
            for (int i = 0; i < bMatrix.Length; i++)
            {
                x0[i] = bMatrix[i] / aMatrix[i, i];
            }
            return x0;
        }
    }
}
