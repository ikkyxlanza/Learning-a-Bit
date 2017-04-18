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
    POW,
    SQRT,
    ASSIGN_PLUS,
    ASSIGN_MINUS,
    ASSIGN_DIV,
    ASSIGN_MUL,
    ASSIGN_MOD,
    LPAREN,
    RPAREN,

    GREATER,
    LESS_THAN,
    EQUALS,
    DIFFERENT,
    GREATER_EQUALS,
    LESS_THAN_EQUALS,
    AND,
    OR,
    NOT,

    ASSIGN,

    INT,
    FLOAT,
    BOOL,

    TRUE,
    FALSE,

    IF,
    COLON,
    ELSE
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