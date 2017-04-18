using System;
using UnityEngine;

public class Interpreter
{
	public static Variable variables { get; private set; }
	private INode iNode { get; set; }

	public Interpreter (string program)
	{
		variables = new Variable(20);
		iNode = Parser.parser(program);
		iNode.run();
		Debug.Log(variables.state());
	}
}