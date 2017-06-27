using System;

public interface INode
{
    int lineNumber { get; set; }
    INode run();
}