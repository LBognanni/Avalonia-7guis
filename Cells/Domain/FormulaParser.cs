using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Cells.Domain
{
    class FormulaParser
    {
        const string TokensRgx = @"([\w\.:]+|.)";
        static readonly char[] operators = new char[] { '+', '-', '/', '*' };

        public static IFormulaPiece Parse(string text)
        {
            if(!text.StartsWith("="))
            {
                return new ValueFormulaPiece(text);
            }

            var matches = CollateMatches(Regex.Match(text, TokensRgx));

            var tokens = matches
                .Select(c => c.Value)
                .Where(s => !string.IsNullOrEmpty(s))
                .Skip(1)
                .ToArray();

            return SplitTokens(tokens);
        }

        private static IEnumerable<Capture> CollateMatches(Match match)
        {
            while (match.Success)
            {
                foreach(Capture capture in match.Captures)
                { 
                    yield return capture;
                }
                match = match.NextMatch();
            }
        }

        public static IFormulaPiece SplitTokens(string[] tokens)
        {
            var index = FirstIndexOf(tokens);
            if (index == -1)
            {
                return FindSingleFormulaPiece(tokens.Single());
            }

            if (tokens[index] == "(")
            {
                // something like (A+B)
                if (index == 0)
                {
                    return HandleBrackets(tokens, index);
                }
                // something like SUM(***
                else if (index == 1)
                {
                    return HandleFunction(tokens);
                }
                // something weird
                else
                {
                    throw new UnrecognizedTokenException(tokens[0]);
                }
            }
            else if (index == 0)
            {
                throw new UnrecognizedTokenException(tokens[0]);
            }

            // something like a+b
            return new GenericFormulaPiece
            {
                Left = SplitTokens(tokens.Take(index).ToArray()),
                Right = SplitTokens(tokens.Skip(index + 1).ToArray()),
                Operator = tokens[index]
            };
        }

        private static IFormulaPiece HandleFunction(string[] tokens)
        {
            throw new NotImplementedException();
        }

        private static IFormulaPiece HandleBrackets(string[] tokens, int index)
        {
            var lastIndex = FindClosingBracket(tokens, index);
            if (lastIndex <= 0)
            {
                throw new ArgumentException("Missing closing bracket");
            }

            var tokensInBrackets = tokens.Skip(1).Take(lastIndex - 1).ToArray();

            if (lastIndex == tokens.Length - 1)
            {
                return SplitTokens(tokensInBrackets);
            }
            else
            {
                if (_operators.Contains(tokens[lastIndex + 1]))
                {
                    var tokensRight = tokens.Skip(lastIndex + 2).ToArray();

                    return new GenericFormulaPiece
                    {
                        Left = SplitTokens(tokensInBrackets),
                        Right = SplitTokens(tokensRight),
                        Operator = tokens[lastIndex + 1]
                    };
                }
                else
                {
                    throw new ArgumentException("Missing operator after closed bracket");
                }
            }
        }

        private static int FindClosingBracket(string[] tokens, int index)
        {
            int nOpens = 0;

            for (var iToken = index; iToken < tokens.Length; ++iToken)
            {
                var token = tokens[iToken];
                
                if (token == "(")
                {
                    nOpens++;
                }

                if (token == ")")
                {
                    nOpens--;

                    if (nOpens == 0)
                    {
                        return iToken;
                    }
                }
            }

            return -1;
        }

        private static IFormulaPiece FindSingleFormulaPiece(string token)
        {
            var match = Regex.Match(token, "([A-Z])([0-9]+)");
            if (match.Success)
            {
                return new ReferenceFormulaPiece(match.Groups[1].Value, int.Parse(match.Groups[2].Value));
            }
            
            if (decimal.TryParse(token, out var result))
            {
                return new ValueFormulaPiece(result);
            }
            
            throw new UnrecognizedTokenException(token);
        }

        static readonly string[] _operators = new[] { "+", "-", ":", "*"  };

        private static int FirstIndexOf(string[]tokens)
        {
            string[] splitters = new string[] { "+", "-", ":", "*", "(", ")" };
            for(int i=0;i<tokens.Length;++i)
            {
                if(splitters.Contains(tokens[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        private static int FirstIndexOf(string find, string[] tokens)
        {
            for (int i = 0; i < tokens.Length; ++i)
            {
                if (find == tokens[i])
                {
                    return i;
                }
            }

            return -1;
        }
    }
}

