using System;
using UnityEngine;

public class VariableStatement : INode
{
    private string name { get; set; }
    private INode expr { get; set; }

    public VariableStatement(IteratorTokening it)
    {
        IteratorToken ite = new IteratorToken(it.current().token);
        name = (ite.current().value as KeyWord).word;
        Token token = ite.next();

        if(!Interpreter.variables.checkName(name))
            Interpreter.variables.createVariable(name, null);

        if(token.type == Type.ASSIGN)
        {
            token = ite.next();
            expr = Expression.expr(ite);
        }
        else if(token.type == Type.ASSIGN_PLUS)
        {
            token = ite.next();
            expr = Expression.expr(ite);
            expr = new BinaryOperator(new CheckVariable(name),Type.PLUS,expr);
        }
        else if(token.type == Type.ASSIGN_MINUS)
        {
            token = ite.next();
            expr = Expression.expr(ite);
            expr = new BinaryOperator(new CheckVariable(name), Type.MINUS, expr);
        }
        else if(token.type == Type.ASSIGN_MUL)
        {
            token = ite.next();
            expr = Expression.expr(ite);
            expr = new BinaryOperator(new CheckVariable(name), Type.MUL, expr);
        }
        else if(token.type == Type.ASSIGN_DIV)
        {
            token = ite.next();
            expr = Expression.expr(ite);
            expr = new BinaryOperator(new CheckVariable(name), Type.DIV, expr);
        }
        else if(token.type == Type.ASSIGN_MOD)
        {
            token = ite.next();
            expr = Expression.expr(ite);
            expr = new BinaryOperator(new CheckVariable(name), Type.MOD, expr);
        }
        else
            throw new Error("Erro no código!");
    }

    public INode run()
    {
        IOperator iOperator = expr.run() as IOperator;
        if(iOperator.type == Type.INT)
            Interpreter.variables.setVariable(name, iOperator as Integer);
        else if(iOperator.type == Type.FLOAT)
            Interpreter.variables.setVariable(name, iOperator as Float);
        else if(iOperator.type == Type.BOOL)
            Interpreter.variables.setVariable(name, iOperator as Bool);
        return null;
    }
}