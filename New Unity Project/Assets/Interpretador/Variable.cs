using System;
using UnityEngine;

public class Variable
{
    private IVariable[] variables;
    private int count;

    public Variable(int vars)
    {
        this.variables = new IVariable[vars];
        this.count = 0;
    }

    public void createVariable(string name, IVariable variable)
    {
        if(count >= variables.Length) throw new Error("Quantidade máxima de variáveis atingida!");
        if(variable == null)
            variable = new Null();
        variable.name = name;
        this.variables[count++] = variable;
    }

    public void createVariable(IVariable variable)
    {
        createVariable("var" + count, variable);
    }

    public IVariable getVariable(string name)
    {
        int index = find(name);
        if (index >= 0)
            return variables[index];
        return null;
    }

    public void setVariable(string name, IVariable variable)
    {
        int index = find(name);
        if (index >= 0)
        {
            variable.name = name;
            variables[index] = variable;
        }
    }

    public bool checkName(string name)
    {
        return find(name) != -1;
    }

    private int find(string name)
    {
        for(var i = 0; i < count; i++)
            if(variables[i].name.Equals(name)) return i;
        return -1;
    }

    public string state()
    {
        string s = "Variáveis\n";
        for(var i = 0; i < count; i++)
        {
            IOperator ip = variables[i] as IOperator;
            if(ip == null)
            {
                s += "Nome: " + variables[i].name + ", Valor: Null\n";
                continue;
            }
            Type type = ip.type;
            if(type == Type.BOOL)
                s += "Nome: " + variables[i].name + ", Valor: " + (variables[i] as Bool).value;
            else if(type == Type.INT)
                s += "Nome: " + variables[i].name + ", Valor: " + (variables[i] as Integer).value;
            else
                s += "Nome: " + variables[i].name + ", Valor: " + (variables[i] as Float).value;
            s += "\n";
        }
        return s;
    }

    private class Null : IVariable
    {
        public string name { get; set; }
    }
}
