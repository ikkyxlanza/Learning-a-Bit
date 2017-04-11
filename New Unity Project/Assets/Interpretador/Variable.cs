using System;

public class Variable
{
    private Unit[] units;
    private int count;

    public Variable()
    {
        this.units = new Unit[20];
        this.count = 0;
    }

    public void createVariable(string name, INumber iNumber)
    {
        if (iNumber.isInteger)
        {
            units[count++] = new Unit(name, iNumber.valueI);
        }
        else
        {
            units[count++] = new Unit(name, iNumber.valueF);
        }
    }

    public void createVariable(INumber iNumber)
    {
        createVariable("var" + count, iNumber);
    }

    public INode getVariable(string name)
    {
        int index = Array.FindIndex(units, unit => unit.name.Equals(name));
        //return (index >= 0 ? (units[index].type == Type.INT) ? new Integer(units[index].valueI) as INode : new Float(units[index].valueF) as INode : null);
        if (index >= 0)
        {
            if (units[index].type == Type.INT)
                return new Integer(units[index].valueI);
            return new Float(units[index].valueF);
        }
        return null;
    }

    public void setVariable(string name, INumber iNumber)
    {
        int index = Array.FindIndex(units, unit => unit.name.Equals(name));
        if (index >= 0)
            if (iNumber.isInteger)
                units[index].valueI = iNumber.valueI;
            else
                units[index].valueF = iNumber.valueF;
    }

    public bool checkName(string name)
    {
        return Array.FindIndex(units, unit => unit.name.Equals(name)) != -1;
    }

    private class Unit
    {
        public int valueI { get; set; }
        private float ValueF { get; set; }
        public float valueF
        {
            get { return this.ValueF; }
            set
            {
                this.type = Type.FLOAT;
                this.ValueF = value;
            }
        }
        public string name { get; set; }
        public Type type { get; set; }


        public Unit(string name, int value)
        {
            this.valueI = value;
            this.name = name;
            this.type = Type.INT;
        }

        public Unit(string name, float value)
        {
            this.valueF = value;
            this.name = name;
            this.type = Type.FLOAT;
        }
    }
}
