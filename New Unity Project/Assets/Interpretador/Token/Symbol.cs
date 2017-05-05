using System;
using UnityEngine;

public class Symbol : IToken
{
    public string ope { get; private set; }

    private Symbol()
    {
        this.ope = "";
    }

    private bool add(string value)
    {
        if (ope.Length > 1) return false;
        switch (ope)
        {
            case "~":
            case "(":
            case ")":
            case ":":
            case "[":
            case "]":
            case "#":
            case ",":
            case "=":
                return false;
            case "+":
            case "-":
            case "%":
            case ">":
            case "<":
                if (value.Equals("="))
                {
                    ope += value + "";
                    return true;
                }
                else return false;
            default:
                switch (value)
                {
                    case "+":
                    case "-":
                    case "*":
                    case "/":
                    case "%":
                    case "(":
                    case ")":
                    case "=":
                    case ">":
                    case "<":
                    case "!":
                    case "|":
                    case "&":
                    case "~":
                    case ":":
                    case "[":
                    case "]":
                    case ",":
                    case "#":
                        ope += value + "";
                        return true;
                    default:
                        return false;
                }
        }
    }

    private Type type()
    {
        switch (ope)
        {
            case "+":
                return Type.PLUS;
            case "-":
                return Type.MINUS;
            case "*":
                return Type.MUL;
            case "/":
                return Type.DIV;
            case "%":
                return Type.MOD;
            case "**":
                return Type.POW;
            case "//":
                return Type.SQRT;
            case "+=":
                return Type.ASSIGN_PLUS;
            case "-=":
                return Type.ASSIGN_MINUS;
            case "*=":
                return Type.ASSIGN_MUL;
            case "/=":
                return Type.ASSIGN_DIV;
            case "%=":
                return Type.ASSIGN_MOD;
            case ">":
                return Type.GREATER;
            case "<":
                return Type.LESS_THAN;
            case "==":
                return Type.EQUALS;
            case "!=":
                return Type.DIFFERENT;
            case ">=":
                return Type.GREATER_EQUALS;
            case "<=":
                return Type.LESS_THAN_EQUALS;
            case "&&":
                return Type.AND;
            case "||":
                return Type.OR;
            case "~":
                return Type.NOT;
            case "(":
                return Type.LPAREN;
            case ")":
                return Type.RPAREN;
            case "=":
                return Type.ASSIGN;
            case ":":
                return Type.COLON;
            case "[":
                return Type.LBRACKET;
            case "]":
                return Type.RBRACKET;
            case ",":
                return Type.COMMA;
            case "#":
                return Type.HASH;
            case "|":
                return Type.PIPE;
            default:
                return Type.NONE;
        }
    }

    public static Token get(Iterator it)
    {
        Symbol sym = new Symbol();
        if (sym.add(it.current() + ""))
        {
            while (it.hasNext())
                if (!sym.add(it.next() + ""))
                    break;
            return new Token(sym.type(), sym);
        }
        return null;
    }
}