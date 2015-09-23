using System;
using Ninject;

namespace Demo.Ioc.Ninject.ConsoleApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var kernel = new StandardKernel();
			kernel.Bind<ICreditCard>().To<MasterCard>();
			kernel.Bind<ICreditCard>().To<VisaCard>();

			var shopper = kernel.Get<Shopper>();
			shopper.Charge();

			Console.Read();
		}
	}

	/// <summary>
	/// http://www.pluralsight.com/courses/inversion-of-control
	/// </summary>
	public class Shopper
	{
		public ICreditCard CreditCard { get; set; }

		public Shopper(ICreditCard creditCard)
		{
			CreditCard = creditCard;
		}

		public int ChargesForCurrentCard()
		{
			return CreditCard.ChargeCount;
		}

		public void Charge()
		{
			Console.WriteLine(CreditCard.Charge());
		}
	}

	public interface ICreditCard
	{
		string Charge();
		int ChargeCount { get; }
	}

	public class MasterCard : ICreditCard
	{
		public string Charge()
		{
			ChargeCount++;
			return "Master Card";
		}

		public int ChargeCount { get; private set; }
	}

	public class VisaCard : ICreditCard
	{
		public string Charge()
		{
			ChargeCount++;
			return "Visa Card";
		}

		public int ChargeCount { get; private set; }
	}
}
