using System;
using UnityEngine;

public class VariableStatement : INode
{
    private string name { get; set; }
    private INode expr { get; set; }
    private INode elementArray { get; set; }

    public VariableStatement(IteratorTokening it)
    {
        IteratorToken ite = new IteratorToken(it.current().token);
        name = (ite.current().value as KeyWord).word;
        Token token = ite.next();
        elementArray = null;

        if (!Interpreter.variables.checkName(name))
            Interpreter.variables.createVariable(name, null);

        INode variable = new CheckVariable(name);
        if (token.type == Type.LBRACKET)
        {
            ite.next();
            elementArray = Expression.expr(ite);
            variable = new CheckElement(name, elementArray);
            if (ite.current().type != Type.RBRACKET)
                throw new Error("Sem colchete direito");
            token = ite.next();
        }

        if (token.type == Type.ASSIGN)
        {
            token = ite.next();
            if (token.type == Type.LBRACKET)
                expr = new Vector(ite);
            else
                expr = Expression.expr(ite);
        }
        else if (token.type == Type.ASSIGN_PLUS)
        {
            token = ite.next();
            expr = Expression.expr(ite);
            expr = new BinaryOperator(variable, Type.PLUS, expr);
        }
        else if (token.type == Type.ASSIGN_MINUS)
        {
            token = ite.next();
            expr = Expression.expr(ite);
            expr = new BinaryOperator(variable, Type.MINUS, expr);
        }
        else if (token.type == Type.ASSIGN_MUL)
        {
            token = ite.next();
            expr = Expression.expr(ite);
            expr = new BinaryOperator(variable, Type.MUL, expr);
        }
        else if (token.type == Type.ASSIGN_DIV)
        {
            token = ite.next();
            expr = Expression.expr(ite);
            expr = new BinaryOperator(variable, Type.DIV, expr);
        }
        else if (token.type == Type.ASSIGN_MOD)
        {
            token = ite.next();
            expr = Expression.expr(ite);
            expr = new BinaryOperator(variable, Type.MOD, expr);
        }
        else
            throw new Error("Erro no código!");
    }

    public INode run()
    {
        IOperator iOperator = expr.run() as IOperator;
        if (Interpreter.debug) Debug.Log("Assign to " + name);
        if (elementArray != null)
        {
            Vector vec = Interpreter.variables.getVariable(name) as Vector;
            vec.value[(elementArray.run() as Integer).value] = iOperator;
            //Interpreter.variables.setVariable(name, vec);
        }
        else if (iOperator.type == Type.INT)
            Interpreter.variables.setVariable(name, iOperator as Integer);
        else if (iOperator.type == Type.FLOAT)
            Interpreter.variables.setVariable(name, iOperator as Float);
        else if (iOperator.type == Type.BOOL)
            Interpreter.variables.setVariable(name, iOperator as Bool);
        else if (iOperator.type == Type.VECTOR)
            Interpreter.variables.setVariable(name, iOperator as Vector);
        return null;
    }
}