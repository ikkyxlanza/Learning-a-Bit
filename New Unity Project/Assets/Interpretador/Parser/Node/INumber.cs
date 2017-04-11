using System;

public interface INumber : INode
{
    bool isInteger { get; }
    int valueI { get; }
    float valueF { get; }
}