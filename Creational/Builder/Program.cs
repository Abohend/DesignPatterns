namespace Builder
{
	internal class Program
	{
		static void Main(string[] args)
		{
			ConcreteBuilder builder = new();
			Director director = new(builder);

            Console.WriteLine("The simple product:");
            director.BuildSimpleProduct();
            Console.WriteLine(builder.GetProduct().ToString());
			
            Console.WriteLine("The complex product:");
            director.BuildComplexProduct();
            Console.WriteLine(builder.GetProduct().ToString());
			
			// without using director
			Console.WriteLine("The custom product:");
			builder.BuildPartC();
			builder.BuildPartA();
            Console.WriteLine(builder.GetProduct().ToString());
        }
	}

	#region Problem
	//public class Product()
	//{
	//	public bool ingredient1;
	//	public bool ingredient2;
	//	public bool ingredient3;

 //       public Product(bool ing1): this()
 //       {
	//		this.ingredient1 = ing1;
 //       }

	//	public Product(bool ingredient1, bool ingredient2) : this()
	//	{
	//		this.ingredient1 = ingredient1;
	//		this.ingredient2 = ingredient2;
	//	}

	//	public Product(bool ingredient1, bool ingredient2, bool ingredient3) : this()
	//	{
	//		this.ingredient1 = ingredient1;
	//		this.ingredient2 = ingredient2;
	//		this.ingredient3 = ingredient3;
	//	}
	//}
	#endregion

	public class Product
	{
		public List<string> _ingredients = new();
		public void Add(string ingredient)
		{
			_ingredients.Add(ingredient);
		}

		public override string ToString()
		{
			string result = string.Empty;
			foreach(var ingredient in _ingredients)
			{
				result += ingredient + ", ";
			}
			return result.Remove(result.Length - 2);
		}
	}

	public interface IBuilder
	{
		public void reset();
		public void BuildPartA();
		public void BuildPartB();
		public void BuildPartC();
	}

	public class ConcreteBuilder : IBuilder
	{
		private Product _product = new(); 
		public void reset()
		{
			_product = new Product();
		}

		public void BuildPartA()
		{
			_product.Add("Part A");
		}

		public void BuildPartB()
		{
			_product.Add("Part B");
		}

		public void BuildPartC()
		{
			_product.Add("Part C");
		}

		public Product GetProduct()
		{
			var result = _product;
			this.reset();
			return result;
		}
	}

	public class Director
	{
		private IBuilder _builder;

		public Director(IBuilder builder)
		{
			_builder = builder;
		}

		public void BuildSimpleProduct()
		{
			this._builder.BuildPartA();
		}

		public void BuildComplexProduct()
		{
			this._builder.BuildPartA();
			this._builder.BuildPartB();
			this._builder.BuildPartC();
		}
	}
}
