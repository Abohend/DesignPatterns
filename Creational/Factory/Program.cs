namespace Factory
{
	// loosens the coupling of a given code by separating the product's construction code from the code that uses this product
	// When to use:
	// 1. have no idea of the exact objects of your code will work with
	// 2. separate product construction from the rest of the application "introduce new products without breaking existence code"

	internal class Program
	{
		static void Main(string[] args)
		{
			#region Simple Factory Idom
			//var resturant = new SimpleFactoryIdom.Resturant();
			//var beefBurger = resturant.OrderBurger("beef");
			//if (beefBurger != null)
			//{
			//             Console.WriteLine("I got the beef burger");
			//         }

			//var veggieBurger = resturant.OrderBurger("veggie");
			//if (veggieBurger != null)
			//{
			//	Console.WriteLine("I got the veggie burger");
			//}
			#endregion

			#region Factory Method
			FactoryMethodAndAbstractFactory.BeefBurgerRestaurant beefBurgerRestaurant = new();
			var beefBurger = beefBurgerRestaurant.OrderBurger();
			// Abstract
			var beefChips = beefBurgerRestaurant.OrderShips();
			if (beefBurger is BeefBurger && beefChips is BeefShips)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("I got the beef burger with Chips");
				Console.ResetColor();
			}

			FactoryMethodAndAbstractFactory.VeggieBurgerRestaurant veggieBurgerRestaurant = new();
			var veggieBurger = veggieBurgerRestaurant.OrderBurger();
			var veggieChips = veggieBurgerRestaurant.OrderShips();
			if (veggieBurger is VeggieBurger && veggieChips is VeggieShips) 
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("I got the veggie burger with Chips");
				Console.ResetColor();
			}
			#endregion
		}
	}

	namespace SimpleFactoryIdom
	{ 
	// Simple Factory Idom => separating the creation logic
		public class SimpleBurgerFactory
		{
			public Burger? CreateBurger(string request)
			{
				var req = request.ToLower();
				Burger? burger = null;
				if (req == "veggie")
				{
					burger = new VeggieBurger();
				}
				else if (req == "beef")
				{
					burger = new BeefBurger();
				}

				return burger;
			}
		}

		public class Resturant
		{
			public Burger? OrderBurger(string request)
			{
				#region Problem
				/* Note: 
					This piece of code voilate SRP "Order Burger and create burger" and OCP "when you need to add more burger types"
				*/
				//var req = request.ToLower();
				//Burger? burger = null;
				//if (req == "veggie")
				//{
				//	burger = new VeggieBurger();
				//}
				//else if (req == "beef")
				//{
				//	burger = new BeefBurger();
				//}
				//// prepare it
				//if (burger != null)
				//	burger.Prepare();
				//return burger;

				#endregion

				#region Simple Factory Idom
				// SRP is solved but OCP is still unsolved
				var factory = new SimpleBurgerFactory();
				var burger = factory.CreateBurger(request);

				// prepare it
				if (burger != null)
					burger.Prepare();

				return burger;
				#endregion
			

			}
		}


	}
	
	namespace FactoryMethodAndAbstractFactory
	{
		public abstract class Restaurant
		{
			protected abstract Burger createBurger();
			protected abstract Chips createChips();
			public Burger OrderBurger()
			{
				var burger = createBurger();
				burger.Prepare();
				return burger;
			}
			public Chips OrderShips()
			{
				var chips = createChips();
				chips.Prepare();
				return chips;
			}
			
		}

		public class BeefBurgerRestaurant : Restaurant
		{
			protected override Burger createBurger()
			{
				return new BeefBurger();
			}

			protected override Chips createChips()
			{
				return new BeefShips();
			}
		}

		public class VeggieBurgerRestaurant : Restaurant
		{
			protected override Burger createBurger()
			{
				return new VeggieBurger();
			}

			protected override Chips createChips()
			{
				return new VeggieShips();
			}
		}
	
	}
	// If we want to add more dished then the Abstract factory come to play

	public interface Burger
	{
		public void Prepare();
	}

	public class BeefBurger : Burger
	{
		public void Prepare()
		{
			Thread.Sleep(500);
			Console.WriteLine("Preparing Beef Burger ....");
			Thread.Sleep(500);
		}
	}

	public class VeggieBurger : Burger
	{
		public void Prepare()
		{
			Thread.Sleep(500);
			Console.WriteLine("Preparing Veggie Burger ....");
			Thread.Sleep(500);
		}
	}


	// Abstract Factory 
	public interface Chips
	{
		public void Prepare();
	}

	public class BeefShips() : Chips
	{
		public void Prepare()
		{
			Thread.Sleep(500);
			Console.WriteLine("Preparing ships with beef taste ....");
			Thread.Sleep(500);
		}
	}

	public class VeggieShips() : Chips
	{
		public void Prepare()
		{
			Thread.Sleep(500);
			Console.WriteLine("Preparing ships with veggie taste ....");
			Thread.Sleep(500);
		}
	}
}
