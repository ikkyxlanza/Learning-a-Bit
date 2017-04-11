using System;

public enum Type
{
    NONE,

    ID,

    PLUS,
    MINUS,
    MUL,
    DIV,
    MOD,
    LPAREN,
    RPAREN,
    ASSIGN,

    INT,
    FLOAT
}

public class Token
{
    public Type type { get; private set; }
    public IToken value { get; private set; }
    public Token next { get; set; }

    public Token(Type type, IToken value)
    {
        this.type = type;
        this.value = value;
        this.next = null;
    }
}