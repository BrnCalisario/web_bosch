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

// Regex rgx = new Regex(@"[A-Z]+\s?[A-Z]+\s?[A-Z]+");
// string value = "20 - 36 - (1.4 * 3)";

string value = "50 - (50 * 2) / 5 * (35 - 4)";
var val = SplitExpression(value);

// val.ForEach(p => Console.Write(p + " "));

var tokens = Tokenizer(val);

tokens.ForEach(p => Console.Write(p.Token + " "));


Decompose(tokens);
Console.WriteLine(tokens[0].Calculate());


void Decompose(List<ParseTree> tokens)
{
    while (tokens.Count > 2)
    {
        // Console.WriteLine("\n\n");
        // foreach (var t in tokens)
        //     Console.Write(t.Token + " ");

        for (int i = 0; i < tokens.Count - 2; i++)
        {
            var possibleMatch = new List<Token> { tokens[i].Token, tokens[i + 1].Token, tokens[i + 2].Token};

            if (highExpCondition.SequenceEqual(possibleMatch))
            {
                changeList(tokens, i, Token.highExp);
            } 
            else if (medExpCondition.Any(coll => coll.SequenceEqual(possibleMatch)))
            {
                changeList(tokens, i, Token.medExp);
            } else if (lowExpCondition.Any(coll => coll.SequenceEqual(possibleMatch)))
            {
                changeList(tokens, i, Token.lowExp);
            }
        }

        for (int i = 0; i < tokens.Count; i++)
            if (expCondition.Contains(tokens[i].Token))
            {
                var pt = new ParseTree();
                pt.Token = Token.exp;
                pt.NodeList.Add(tokens[i]);
                tokens[i] = pt;
            }

    }
}

void changeList(List<ParseTree> tokens, int i, Token TOKEN){
    var pt = new ParseTree();
    pt.Token = TOKEN;

    pt.NodeList.Add(tokens[i]);
    pt.NodeList.Add(tokens[i + 1]);
    pt.NodeList.Add(tokens[i + 2]);

    tokens[i] = pt;
    tokens.RemoveAt(i + 1);
    tokens.RemoveAt(i + 1);
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
    if (buffer.Length > 0)
        ret.Add(float.Parse(buffer));

    return ret;
}

List<ParseTree> Tokenizer(List<object> list)
{
    List<ParseTree> tokenList = new List<ParseTree>();

    foreach(var obj in list)
    {
        ParseTree tk = new ParseTree();
        tk.Value = obj; 
        switch(obj) 
        {
            case "+":
                tk.Token = Token.OPSUM;
                break;
            case "-":
                tk.Token = Token.OPSUB;
                break;
            case "*":
                tk.Token = Token.OPMUL;
                break;
            case "/":
                tk.Token = Token.OPDIV;
                break;
            case "(":
                tk.Token = Token.OPENPARENTHESIS;
                break;
            case ")":
                tk.Token = Token.CLOSEPARENTHESIS;
                break;
            default:
                tk.Token =  Token.NUM;
                break;
        }
        tokenList.Add(tk);
    }
    return tokenList;
}


