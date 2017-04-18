using System;

public interface IOperator : INode, IVariable
{
    Type type { get; }
}