/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
using System;

namespace EquationFinder
{
    /// <summary>
    /// Describes a mathmatical operation
    /// </summary>
    public class TupleOperation : IOperation
    {
        public OperandType Operand { get; set; }
		
        public TupleOperation() : this((OperandType)(int)Math.Pow(2, (double)StaticRandom.Instance.Next(0, 3)))
        {
        }

		public TupleOperation(string operation)
		{
			this.Operand = Parse(operation);
		}

		public static OperandType Parse(string stringOperand)
		{
			switch (stringOperand)
			{
				case "+":
					return OperandType.Add;
				case "-":
					return OperandType.Subtract;
				case "*":
					return OperandType.Multiply;
				case "/":
					return OperandType.Divide;			
				case "^":
					return OperandType.Raise;
				
				default:
					throw new ArgumentException(
						string.Format("Parameter stringOperand cannot parse string \"{0}\" into one of the OperandType enums.  Valid values: \"+-*/^\". If you added a new OperandType, a translation should be added to the method that threw this Exception.", stringOperand),
						"stringOperand");
			}						
		}

        public TupleOperation(OperandType operation)
        {
            this.Operand = operation;
        }

        public TupleOperation(TupleOperation operation)
        {
            this.Operand = operation.Operand;
        }

        public decimal Calculate(decimal Value1, decimal Value2)
        {
			return TupleOperation.Calculate(Value1, Value2, Operand);
        }

		public static decimal Calculate(decimal Value1, decimal Value2, OperandType Operation)
		{
			switch (Operation)
			{
				case OperandType.Add:
					return Value1 + Value2;
				case OperandType.Subtract:
					return Value1 - Value2;
				case OperandType.Multiply:
					return Value1 * Value2;
				case OperandType.Divide:
					return Value1 / Value2;
				case OperandType.Raise:
					return (decimal)Math.Pow((double)Value1, (double)Value2);
				default:
					throw new ArgumentException(
						string.Format("OperandType \"{0}\" does not exist.", Enum.GetName(typeof(OperandType), Operation)),
						"Operation"
					);
			}
		}

        #region Overrides
        public override string ToString()
        {
			switch (Operand)
			{
				case OperandType.Add: return "+";
				case OperandType.Subtract: return "-";
				case OperandType.Multiply: return "*";
				case OperandType.Divide: return "/";
				case OperandType.Raise: return "^";
				case OperandType.Equal: return "=";
				default:
					return "?";
					//throw new ArgumentException(
					//	string.Format("Cannot convert OperandType.{0} to a string.  If you added a new OperandType, a translation should be added to the method that threw this Exception.", Enum.GetName(typeof(OperandType), Operand)),
					//	"Operation"
					//);
			}
        }

        public override bool Equals(object obj)
        {
            TupleOperation other = obj as TupleOperation;
            if (other == null)
            {
                return false;
            }
            return this.Operand.Equals(other.Operand);
        }

        public override int GetHashCode()
        {
			int hashCode = (int)Operand;
            unchecked
            {
                hashCode += hashCode.GetHashCode(); //(int)this.operation;
            }
            return hashCode;
        }

        public static bool operator ==(TupleOperation lhs, TupleOperation rhs)
        {
            if (ReferenceEquals(lhs, rhs))
            {
                return true;
            }
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
            {
                return false;
            }
            return lhs.Operand.Equals(rhs.Operand);

        }

        public static bool operator !=(TupleOperation lhs, TupleOperation rhs)
        {
            return !(lhs == rhs);
        }
        #endregion
    }
}
