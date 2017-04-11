using System;

public enum TypeNumber
{
    INTEGER,
    FLOAT
}

public class Number : IToken
{
    public TypeNumber typeNumber { get; private set; }

    public int valueI { get; private set; }
    public int valueF { get; private set; }

    private Number() : base()
    {
        typeNumber = TypeNumber.INTEGER;
        valueI = 0;
        valueF = 0;
    }

    private bool add(char value)
    {
        switch (value)
        {
            case '.':
                typeNumber = TypeNumber.FLOAT;
                return true;

            case '0':
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
                if (typeNumber == TypeNumber.INTEGER)
                {
                    valueI = valueI * 10 + (value - 48);
                }
                else
                {
                    valueF = valueF * 10 + (value - 48);
                }
                return true;

            default:
                return false;
        }
    }

    public float getFloat()
    {
        return float.Parse(valueI + "." + valueF);
    }

    public static Token get(Iterator it)
    {
        Number num = new Number();
        if (num.add(it.current()))
        {
            while (it.hasNext())
                if (!num.add(it.next()))
                    break;
            if (num.typeNumber == TypeNumber.INTEGER)
                return new Token(Type.INT, num);
            return new Token(Type.FLOAT, num);
        }
        return null;
    }
}