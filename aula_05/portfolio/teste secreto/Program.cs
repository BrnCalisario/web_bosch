using System.Globalization;
using System.Text.RegularExpressions;

// string value = "20 - 36 - (1.4 * 3)";
string value = "4 * (1 + 2)";
var val = SplitExpression(value);

var tokens = TokenizeExp(val);

foreach(var v in val){
    Console.WriteLine(v);
}


Regex rgx = new Regex(@"[A-Z]+\s?[A-Z]+\s?[A-Z]+");

while(tokens.Count > 0)
{
    Console.WriteLine(rgx.Match(string.Join(' ', tokens)).Value);
    tokens.RemoveAt(0);
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

    foreach(var obj in list)
    {
        switch(obj)
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


public enum Token {
    NUM,
    OPSUM,
    OPSUB,
    OPMUL,
    OPDIV,
    OPENPARENTHESIS,
    CLOSEPARENTHESIS,
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

public class ParseTree 
{
    Token
    Val
    listaFilhos
}
