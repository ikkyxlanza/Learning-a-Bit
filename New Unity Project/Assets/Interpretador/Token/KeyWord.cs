using System;

public class KeyWord : IToken
{
	public string variable { get; private set;}

	private KeyWord()
	{
		this.variable = "";
	}

	private bool add (char value)
	{
		if((int)value >= 97 && (int)value <= 122) {
			variable += value;
			return true;
        }
        return false;
	}

	public static Token get (Iterator it)
	{
        KeyWord v = new KeyWord();
        if(v.add(it.current())) {
            while (it.hasNext ())
				if (!v.add (it.next ()))
					break;
			return new Token(Type.ID, v);
        }
		return null;
	}
}