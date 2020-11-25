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

            if (index == 0)
                throw new UnrecognizedTokenException(tokens[0]);

            if (tokens[index] == "(")
            {
                // something like SUM(***
                if (index != 1)
                {
                    throw new UnrecognizedTokenException(tokens[0]);
                }
                
            }

            // something like a+b
            return new GenericFormulaPiece
            {
                Left = SplitTokens(tokens.Take(index).ToArray()),
                Right = SplitTokens(tokens.Skip(index + 1).ToArray()),
                Operator = tokens[index]
            };
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

    }
}

