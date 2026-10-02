using System;
using System.Collections.Generic;
using System.Globalization;

namespace Calculator.Utils
{
    public class ExpressionEvaluator
{
    // Define operator precedence
    private static Dictionary<string, int> precedence = new Dictionary<string, int>
    {
        { "+", 1 },
        { "-", 1 },
        { "*", 2 },
        { "/", 2 },
        { "%", 2 }
    };

    // Convert an infix expression (list of tokens) into a postfix expression.
    public static List<string> InfixToPostfix(List<string> tokens)
    {
        List<string> output = new List<string>();
        Stack<string> opStack = new Stack<string>();

        foreach (string token in tokens)
        {
            if (decimal.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
            {
                // Token is a number.
                output.Add(token);
            }
            else if (precedence.ContainsKey(token))
            {
                // Token is an operator.
                while (opStack.Count > 0 && precedence.ContainsKey(opStack.Peek()) &&
                       precedence[opStack.Peek()] >= precedence[token])
                {
                    output.Add(opStack.Pop());
                }
                opStack.Push(token);
            }
            // Optionally, add support for parentheses if needed.
        }

        while (opStack.Count > 0)
        {
            output.Add(opStack.Pop());
        }

        return output;
    }

    // Evaluate a postfix expression.
    public static decimal EvaluatePostfix(List<string> postfixTokens)
    {
        if (postfixTokens == null) throw new ArgumentNullException(nameof(postfixTokens));

        Stack<decimal> stack = new Stack<decimal>();

        foreach (string token in postfixTokens)
        {
            if (decimal.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal number))
            {
                stack.Push(number);
            }
            else if (precedence.ContainsKey(token))
            {
                if (stack.Count < 2)
                    throw new InvalidOperationException($"Insufficient operands for operator '{token}'");

                decimal b = stack.Pop();
                decimal a = stack.Pop();

                decimal result = token switch
                {
                    "+" => a + b,
                    "-" => a - b,
                    "*" => a * b,
                    "/" => b == 0 ? throw new DivideByZeroException("Division by zero") : a / b,
                    "%" => b == 0 ? throw new DivideByZeroException("Modulo by zero") : a % b,
                    _ => throw new NotSupportedException($"Unsupported operator '{token}'")
                };
                stack.Push(result);
            }
            else
            {
                // Unknown token (could be parentheses or unsupported symbol)
                throw new NotSupportedException($"Unsupported token '{token}' in expression");
            }
        }

        if (stack.Count != 1)
            throw new InvalidOperationException("Malformed expression: unexpected number of values remaining on stack");

        return stack.Pop();
    }

    // This helper method combines the two steps.
    public static decimal EvaluateInfixExpression(List<string> tokens)
    {
        if (tokens == null) throw new ArgumentNullException(nameof(tokens));
        List<string> postfix = InfixToPostfix(tokens);
        return EvaluatePostfix(postfix);
    }
    }
}
