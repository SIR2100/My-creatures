using System.Collections.Generic;

namespace ProgramTree
{
    public enum AssignType { Assign, AssignPlus, AssignMinus, AssignMult, AssignDivide };

    public class Node // базовый класс для всех узлов    
    {
    }

    public class ExprNode : Node // базовый класс для всех выражений
    {
    }

    public class IdNode : ExprNode
    {
        public string Name { get; set; }
        public IdNode(string name) { Name = name; }
    }

    public class IntNumNode : ExprNode
    {
        public int Num { get; set; }
        public IntNumNode(int num) { Num = num; }
    }

    public class StatementNode : Node // базовый класс для всех операторов
    {
    }

    public class AssignNode : StatementNode
    {
        public IdNode Id { get; set; }
        public ExprNode Expr { get; set; }
        public AssignType AssOp { get; set; }
        public AssignNode(IdNode id, ExprNode expr, AssignType assop = AssignType.Assign)
        {
            Id = id;
            Expr = expr;
            AssOp = assop;
        }
    }

    public class CycleNode : StatementNode
    {
        public ExprNode Expr { get; set; }
        public StatementNode Stat { get; set; }
        public CycleNode(ExprNode expr, StatementNode stat)
        {
            Expr = expr;
            Stat = stat;
        }
    }

    public class WhileNode : StatementNode
    {
        public ExprNode Expr { get; set; }
        public StatementNode Stat { get; set; }
        public WhileNode(ExprNode expr, StatementNode stat)
        {
            Expr = expr;
            Stat = stat;
        }
    }

    public class RepeatNode : StatementNode
    {
        public ExprNode Expr { get; set; }
        public StatementNode Stat { get; set; }

        public RepeatNode(ExprNode expr, StatementNode stat)
        {
            Expr = expr;
            Stat = stat;
        }
    }

    public class ForNode : StatementNode
    {
        public IdNode Id { get; set; }
        public ExprNode ExprFrom { get; set; }
        public ExprNode ExprTo { get; set; }
        public StatementNode Stat { get; set; }

        public ForNode(IdNode id, ExprNode expr1, ExprNode expr2, StatementNode stat)
        {
            Id = id;
            ExprFrom = expr1;
            ExprTo = expr2;
            Stat = stat;
        }
    }

    public class WriteNode : StatementNode
    {
        public ExprNode Expr { get; set; }

        public WriteNode(ExprNode expr)
        {
            Expr = expr;
        }
    }

    public class IfNode : StatementNode
    {
        public ExprNode Expr { get; set; }
        public StatementNode StatIf { get; set; }
        public StatementNode StatThen { get; set; }

        public IfNode(ExprNode expr, StatementNode stat1)
        {
            Expr = expr;
            StatIf = stat1;
            StatThen = null;
        }

        public IfNode(ExprNode expr, StatementNode stat1, StatementNode stat2)
        {
            Expr = expr;
            StatIf = stat1;
            StatThen = stat2;
        }
    }

    public class VarsNode : StatementNode
    {
        public List<IdNode> IdList = new List<IdNode>();

        public VarsNode(IdNode id) { IdList.Add(id); }
        public void Add(IdNode id) { IdList.Add(id); }
    }

    public class VarDefNode : StatementNode
    {
        public VarsNode IdList { get; set; }

        public VarDefNode(VarsNode idlist) { IdList = idlist; }
    }

    public class BinaryNode : StatementNode
    {
        public ExprNode Expr1 { get; set; }
        public ExprNode Expr2 { get; set; }
        public char Op { get; set; }

        public BinaryNode(ExprNode expr1, ExprNode expr2, char op)
        {
            Expr1 = expr1;
            Expr2 = expr2;
            Op = op;
        }
    }

    public class BlockNode : StatementNode
    {
        public List<StatementNode> StList = new List<StatementNode>();
        public BlockNode(StatementNode stat)
        {
            Add(stat);
        }
        public void Add(StatementNode stat)
        {
            StList.Add(stat);
        }
    }

}