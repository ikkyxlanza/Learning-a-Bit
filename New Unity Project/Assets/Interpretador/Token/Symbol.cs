using System;

public class Symbol : IToken
{
    public char ope { get; private set; }

    private Symbol(char ope)
    {
        this.ope = ope;
    }

    public Type type()
    {
        switch (ope)
        {
            case '+':
                return Type.PLUS;
            case '-':
                return Type.MINUS;
            case '*':
                return Type.MUL;
            case '/':
                return Type.DIV;
            case '%':
                return Type.MOD;
            case '(':
                return Type.LPAREN;
            case ')':
                return Type.RPAREN;
            case '=':
                return Type.ASSIGN;
            default:
                return Type.NONE;
        }
    }

    public static Token get(Iterator it)
    {
        char v = it.current();
        switch (v)
        {
            case '+':
            case '-':
            case '*':
            case '/':
            case '%':
            case '(':
            case ')':
            case '=':
                it.next();
                Symbol op = new Symbol(v);
                return new Token(op.type(), op);
            default:
                return null;
        }
    }
}