/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.Numerics;
using EquationFinderCore;

namespace EquationFactories
{
	/// <summary>
	/// Describes a mathematical operation
	/// </summary>
	public class TupleOperation : IOperation
	{
		public OperationType Operand { get; private set; }

		public TupleOperation() : this(HelperClass.AlgebraicOperators[StaticRandom.Instance.Next(0, HelperClass.AlgebraicOperators.Length)].ToString())
		{
		}

		public TupleOperation(char operation)
		{
			this.Operand = Parse(operation);
		}

		public TupleOperation(string operation)
		{
			this.Operand = Parse(operation);
		}

		public static OperationType Parse(char charOperand)
		{
			return TupleOperation.Parse(charOperand.ToString());
		}

		public static OperationType Parse(string stringOperand)
		{
			switch (stringOperand)
			{
				case "+":
					return OperationType.Add;
				case "-":
					return OperationType.Subtract;
				case "*":
					return OperationType.Multiply;
				case "/":
					return OperationType.Divide;
				case "^":
					return OperationType.Exponentiation;

				default:
					throw new ArgumentException(
						string.Format("Parameter stringOperand cannot parse string \"{0}\" into one of the OperandType enums.  Valid values: \"+-*/^\". If you added a new OperandType, a translation should be added to the method that threw this Exception.", stringOperand),
						"stringOperand");
			}
		}

		public TupleOperation(OperationType operation)
		{
			this.Operand = operation;
		}

		public BigInteger Calculate(BigInteger Value1, BigInteger Value2)
		{
			return TupleOperation.Calculate(Value1, Value2, this.Operand);
		}

		public static BigInteger Calculate(BigInteger Value1, BigInteger Value2, OperationType Operation)
		{
			switch (Operation)
			{
				case OperationType.Add:
					return Value1 + Value2;
				case OperationType.Subtract:
					return Value1 - Value2;
				case OperationType.Multiply:
					return Value1 * Value2;
				case OperationType.Divide:
					return Value1 / Value2;
				case OperationType.Exponentiation:
					return HelperClass.Pow(Value1, Value2);
				default:
					throw new ArgumentException(
						string.Format("OperandType \"{0}\" does not exist.", Enum.GetName(typeof(OperationType), Operation)),
						"Operation"
					);
			}
		}

		#region Overrides
		public override string ToString()
		{
			switch (Operand)
			{
				case OperationType.Add: return "+";
				case OperationType.Subtract: return "-";
				case OperationType.Multiply: return "*";
				case OperationType.Divide: return "/";
				case OperationType.Exponentiation: return "^";
				case OperationType.Equal: return "=";
				default:
					return " ";
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
				hashCode += hashCode.GetHashCode();
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
