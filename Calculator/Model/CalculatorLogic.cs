using System;

namespace Calculator.Model
{
    public static class CalculatorLogic
    {
        public static decimal Add(decimal a, decimal b)
        {
            return a + b;
        }
        public static decimal Subtract(decimal a, decimal b)
        {
            return a - b;
        }
        public static decimal Multiply(decimal a, decimal b)
        {
            return a * b;
        }
        public static decimal Divide(decimal a, decimal b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero");
            }
            return a / b;
        }
        public static decimal Sqrt(decimal number)
        {
            if (number < 0)
            {
                throw new ArgumentException("Cannot calculate square root of a negative number");
            }
            return (decimal)Math.Sqrt((double)number);
        }
        public static decimal Square(decimal number)
        {
            return number * number;
        }
        public static decimal Inverse(decimal number)
        {
            return -number;
        }

        /// <summary>
        /// Returns the negation of the provided number. Replaces the older Inverse method name.
        /// </summary>
        public static decimal Negate(decimal number)
        {
            return -number;
        }

        [Obsolete("Use Negate instead.")]
        public static decimal InverseObsolete(decimal number)
        {
            return Negate(number);
        }
        public static decimal OneOver(decimal number)
        {
            if (number == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero");
            }
            return 1 / number;
        }
        public static decimal Modulo(decimal a, decimal b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Cannot perform modulo division by zero");
            }
            return a % b;
        }
    }
}
