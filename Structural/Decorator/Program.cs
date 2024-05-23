using System.ComponentModel;

namespace Decorator
{
	/*
	 Decorator Pattern (Wrapper):  let you attach new behaviors to objects
	by placing these objects inside special wrapper objects that contain 
	the behavoirs.

	Wrapping objects happens at runtime.
	
	Replaces multiple inhertance and solve it's problems by applying association & composition instead.
	 */
	internal class Program
	{
		static void Main(string[] args)
		{
			// No Decoration
			var simple = new ConcreteComponent();
            Console.WriteLine($"Non Decorated Component: \n{simple.Operation()}");

			// All possible Decorations
			var decorator1 = new ConcreateDecoratorA(simple);
			var decorator2 = new ConcreteDecoratorB(decorator1);
            Console.WriteLine($"\nDecorated Component: \n{decorator2.Operation()}");
        }
	}

	/*
	   The base Component interface defines operations that can be altered by
	   decorators.
	*/
	public interface IComponent
	{
		public string Operation();
	}

	/* 
	 * Concrete Components provide default implementations of the operations.
	 * There might be several variations of these classes.
	*/
	class ConcreteComponent : IComponent
	{
		public string Operation()
		{
			return "ConcreteComponent";
		}
	}

	/*
	// The base Decorator class follows the same interface as the other
	// components. The primary purpose of this class is to define the wrapping
	// interface for all concrete decorators. The default implementation of the
	// wrapping code might include a field for storing a wrapped component and
	// the means to initialize it.
	*/
	abstract class Decorator : IComponent
	{
		protected IComponent _wrappee;

		protected Decorator(IComponent wrappee)
		{
			_wrappee = wrappee;
		}
		// The Decorator delegates all work to the wrapped component.
		public string Operation()
		{
			if (this._wrappee != null)
			{
				return this._wrappee.Operation();
			}
			else
			{
				return string.Empty;
			}
		}

		public void SetWrappee(IComponent wrappee)
		{
			this._wrappee = wrappee;
		}
	}

	// Concrete Decorators call the wrapped object and alter its result in some way.
	class ConcreateDecoratorA : Decorator
	{
        public ConcreateDecoratorA(IComponent wrappee): base(wrappee) {}
		public new string Operation()
		{
			return $"ConcreteDecoratorA({base.Operation()})";
		}
	}

	class ConcreteDecoratorB : Decorator
	{
		public ConcreteDecoratorB(IComponent comp) : base(comp)
		{
		}

		public new string Operation()
		{
			return $"ConcreteDecoratorB({base.Operation()})";
		}
	}
	
}
