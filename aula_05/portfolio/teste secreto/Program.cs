using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;

var expCondition =
    new List<Token> { Token.highExp, Token.medExp, Token.lowExp, Token.NUM };

var highExpCondition =
    new List<Token> { Token.OPENPARENTHESIS, Token.exp, Token.CLOSEPARENTHESIS };

var medExpCondition =
    new List<List<Token>> {
        new List<Token> { Token.exp, Token.OPMUL, Token.exp },
        new List<Token> { Token.exp, Token.OPDIV, Token.exp }};

var lowExpCondition =
    new List<List<Token>> {
        new List<Token> { Token.exp, Token.OPSUM, Token.exp },
        new List<Token> { Token.exp, Token.OPSUB, Token.exp }};


// string value = "20 - 36 - (1.4 * 3)";
string value = "4 * (1 + 2)";
var val = SplitExpression(value);

var tokens = TokenizeExp(val);

Decompose(tokens);
 

Regex rgx = new Regex(@"[A-Z]+\s?[A-Z]+\s?[A-Z]+");

void Decompose(List<Token> tokens)
{
    while (tokens.Count > 1)
    {
        Console.WriteLine("\n\n");
        foreach (var t in tokens)
            Console.Write(t + " ");

        for (int i = 0; i < tokens.Count - 2; i++)
        {
            var possibleMatch = new List<Token> { tokens[i], tokens[i + 1], tokens[i + 2] };

            if (highExpCondition.SequenceEqual(possibleMatch))
            {
                tokens[i] = Token.highExp;
                tokens.RemoveAt(i + 1);
                tokens.RemoveAt(i + 1);
            }
            else if (medExpCondition.Any(coll => coll.SequenceEqual(possibleMatch)))
            {
                tokens[i] = Token.medExp;
                tokens.RemoveAt(i + 1);
                tokens.RemoveAt(i + 1);
            }
            else if (lowExpCondition.Any(coll => coll.SequenceEqual(possibleMatch)))
            {
                tokens[i] = Token.lowExp;
                tokens.RemoveAt(i + 1);
                tokens.RemoveAt(i + 1);
            }
        }

        for (int i = 0; i < tokens.Count; i++)
            if (expCondition.Contains(tokens[i]))
                tokens[i] = Token.exp;

        Console.WriteLine();
        foreach (var t in tokens)
            Console.Write(t + " ");

    }
}



List<object> SplitExpression(string expr)
{
    // TODO Add all your delimiters here
    var delimiters = new[] { '(', '+', '-', '*', '/', ')' };
    var buffer = string.Empty;
    var ret = new List<object>();
    expr = expr.Replace(" ", "");
    expr = expr.Replace(".", ",");
    foreach (var c in expr)
    {
        if (delimiters.Contains(c))
        {
            if (buffer.Length > 0)
            {
                ret.Add(float.Parse(buffer));
                buffer = string.Empty;
            }
            ret.Add(c.ToString());
        }
        else
        {
            buffer += c;
        }
    }
    return ret;
}

List<Token> TokenizeExp(List<object> list)
{
    List<Token> tokenList = new List<Token>();

    foreach (var obj in list)
    {
        switch (obj)
        {
            case "+":
                tokenList.Add(Token.OPSUM);
                break;
            case "-":
                tokenList.Add(Token.OPSUB);
                break;
            case "*":
                tokenList.Add(Token.OPMUL);
                break;
            case "/":
                tokenList.Add(Token.OPDIV);
                break;
            case "(":
                tokenList.Add(Token.OPENPARENTHESIS);
                break;
            case ")":
                tokenList.Add(Token.CLOSEPARENTHESIS);
                break;
            default:
                tokenList.Add(Token.NUM);
                break;
        }
    }

    return tokenList;
}


public enum Token
{
    NUM,
    OPSUM,
    OPSUB,
    OPMUL,
    OPDIV,
    OPENPARENTHESIS,
    CLOSEPARENTHESIS,

    exp,
    lowExp,
    medExp,
    highExp,
}

// NUM
// OPSUM
// OPSUB
// OPMUL
// OPDIV
// OPENPARENTHESIS
// CLOSEPARENTHESIS

// highExp = OPENPARENTHESIS exp CLOSEPARENTHESIS
// medExp = exp OPMUL exp | exp OPDIV exp
// lowExp = exp OPSUM exp | exp OPSUB exp
// exp = highExp | medExp | lowExp | NUM

// 4 * (1 + 2)
// NUM OPMUL OPENPARENTHESIS NUM OPSUM NUM CLOSEPARENTHESIS
// exp OPMUL OPENPARENTHESIS exp OPSUM exp CLOSEPARENTHESIS
// exp OPMUL OPENPARENTHESIS lowExp CLOSEPARENTHESIS
// exp OPMUL OPENPARENTHESIS exp CLOSEPARENTHESIS
// exp OPMUL highExp
// exp OPMUL exp
// medExp
// exp

// 1 + 2 * 3
// NUM OPSUM NUM OPMUL NUM
// exp OPSUM exp OPMUL exp
// exp OPSUM medExp
// exp OPSUM exp
// lowExp
// exp

// public class ParseTree 
// {
//     Token
//     Val
//     listaFilhos
// }
