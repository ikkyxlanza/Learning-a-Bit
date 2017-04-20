using System;

public interface IVariable
{
    string name { get; set; }

    IVariable clone();
}