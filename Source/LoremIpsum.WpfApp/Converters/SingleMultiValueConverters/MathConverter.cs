﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// Value converter that performs arithmetic calculations over its argument(s)
    /// </summary>
    /// <remarks>
    /// MathConverter can act as a value converter, or as a multi value converter (WPF only).
    /// It is also a markup extension (WPF only) which allows to avoid declaring resources,
    /// ConverterParameter must contain an arithmetic expression over converter arguments. Operations supported are +, -, * and /
    /// Single argument of a value converter may referred as x, a, or {0}
    /// Arguments of multi value converter may be referred as x,y,z,t (first-fourth argument), or a,b,c,d, or {0}, {1}, {2}, {3}, {4}, ...
    /// The converter supports arithmetic expressions of arbitrary complexity, including nested subexpressions
    /// </remarks>
    public class MathConverter : BaseSingleMultiValueConverter<MathConverter>
    {

        #region Single Value Implementation
        /// <inheritdoc/>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(new object[] { value }, targetType, parameter, culture);
        }

        /// <inheritdoc/>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Multi Value Implementation
        /// <inheritdoc/>
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            var result = Parse(parameter.ToString()).Eval(values);
            if (targetType == typeof(decimal))
                return result;
            if (targetType == typeof(string))
                return result.ToString();
            if (targetType == typeof(int))
                return (int)result;
            if (targetType == typeof(double)) return (double)result;
            if (targetType == typeof(long)) return (long)result;
            if (targetType == typeof(object)) return result;
            throw new ArgumentException(string.Format("Unsupported target type {0}", targetType.FullName));


        }
        /// <inheritdoc/>
        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Classes for Parser

        readonly Dictionary<string, IExpression> _storedExpressions = new Dictionary<string, IExpression>();
        private IExpression Parse(string s)
        {
            if (!_storedExpressions.TryGetValue(s, out var result))
            {
                result = new Parser().Parse(s);
                _storedExpressions[s] = result;
            }

            return result;
        }

        interface IExpression
        {
            decimal Eval(object[] args);
        }

        class Constant : IExpression
        {
            private readonly decimal _value;

            public Constant(string text)
            {
                if (!decimal.TryParse(text, out _value))
                {
                    throw new ArgumentException(string.Format("'{0}' is not a valid number", text));
                }
            }

            public decimal Eval(object[] args)
            {
                return _value;
            }
        }

        class Variable : IExpression
        {
            private readonly int _index;

            public Variable(string text)
            {
                if (!int.TryParse(text, out _index) || _index < 0)
                {
                    throw new ArgumentException(string.Format("'{0}' is not a valid parameter index", text));
                }
            }

            public Variable(int n)
            {
                _index = n;
            }

            public decimal Eval(object[] args)
            {
                if (_index >= args.Length)
                {
                    throw new ArgumentException(string.Format("MathConverter: parameter index {0} is out of range. {1} parameter(s) supplied", _index, args.Length));
                }

                return System.Convert.ToDecimal(args[_index]);
            }
        }

        class BinaryOperation : IExpression
        {
            private readonly Func<decimal, decimal, decimal> _operation;
            private readonly IExpression _left;
            private readonly IExpression _right;

            public BinaryOperation(char operation, IExpression left, IExpression right)
            {
                _left = left;
                _right = right;
                switch (operation)
                {
                    case '+': _operation = (a, b) => (a + b); break;
                    case '-': _operation = (a, b) => (a - b); break;
                    case '*': _operation = (a, b) => (a * b); break;
                    case '/': _operation = (a, b) => (a / b); break;
                    default: throw new ArgumentException("Invalid operation " + operation);
                }
            }

            public decimal Eval(object[] args)
            {
                return _operation(_left.Eval(args), _right.Eval(args));
            }
        }

        class Negate : IExpression
        {
            private IExpression _param;

            public Negate(IExpression param)
            {
                _param = param;
            }

            public decimal Eval(object[] args)
            {
                return -_param.Eval(args);
            }
        }

        class Parser
        {
            private string text;
            private int pos;

            public IExpression Parse(string text)
            {
                try
                {
                    pos = 0;
                    this.text = text;
                    var result = ParseExpression();
                    RequireEndOfText();
                    return result;
                }
                catch (Exception ex)
                {
                    var msg =
                        string.Format("MathConverter: error parsing expression '{0}'. {1} at position {2}", text, ex.Message, pos);

                    throw new ArgumentException(msg, ex);
                }
            }

            private IExpression ParseExpression()
            {
                var left = ParseTerm();

                while (true)
                {
                    if (pos >= text.Length) return left;

                    var c = text[pos];

                    if (c == '+' || c == '-')
                    {
                        ++pos;
                        var right = ParseTerm();
                        left = new BinaryOperation(c, left, right);
                    }
                    else
                    {
                        return left;
                    }
                }
            }

            private IExpression ParseTerm()
            {
                var left = ParseFactor();

                while (true)
                {
                    if (pos >= text.Length) return left;

                    var c = text[pos];

                    if (c == '*' || c == '/')
                    {
                        ++pos;
                        var right = ParseFactor();
                        left = new BinaryOperation(c, left, right);
                    }
                    else
                    {
                        return left;
                    }
                }
            }

            private IExpression ParseFactor()
            {
                SkipWhiteSpace();
                if (pos >= text.Length) throw new ArgumentException("Unexpected end of text");

                var c = text[pos];

                if (c == '+')
                {
                    ++pos;
                    return ParseFactor();
                }

                if (c == '-')
                {
                    ++pos;
                    return new Negate(ParseFactor());
                }

                if (c == 'x' || c == 'a') return CreateVariable(0);
                if (c == 'y' || c == 'b') return CreateVariable(1);
                if (c == 'z' || c == 'c') return CreateVariable(2);
                if (c == 't' || c == 'd') return CreateVariable(3);

                if (c == '(')
                {
                    ++pos;
                    var expression = ParseExpression();
                    SkipWhiteSpace();
                    Require(')');
                    SkipWhiteSpace();
                    return expression;
                }

                if (c == '{')
                {
                    ++pos;
                    var end = text.IndexOf('}', pos);
                    if (end < 0) { --pos; throw new ArgumentException("Unmatched '{'"); }
                    if (end == pos) { throw new ArgumentException("Missing parameter index after '{'"); }
                    var result = new Variable(text.Substring(pos, end - pos).Trim());
                    pos = end + 1;
                    SkipWhiteSpace();
                    return result;
                }

                const string decimalRegEx = @"(\d+\.?\d*|\d*\.?\d+)";
                var match = Regex.Match(text.Substring(pos), decimalRegEx);
                if (match.Success)
                {
                    pos += match.Length;
                    SkipWhiteSpace();
                    return new Constant(match.Value);
                }
                else
                {
                    throw new ArgumentException(string.Format("Unexpected character '{0}'", c));
                }
            }

            private IExpression CreateVariable(int n)
            {
                ++pos;
                SkipWhiteSpace();
                return new Variable(n);
            }

            private void SkipWhiteSpace()
            {
                while (pos < text.Length && char.IsWhiteSpace((text[pos]))) ++pos;
            }

            private void Require(char c)
            {
                if (pos >= text.Length || text[pos] != c)
                {
                    throw new ArgumentException("Expected '" + c + "'");
                }

                ++pos;
            }

            private void RequireEndOfText()
            {
                if (pos != text.Length)
                {
                    throw new ArgumentException("Unexpected character '" + text[pos] + "'");
                }
            }
        }

        #endregion

    }
}
