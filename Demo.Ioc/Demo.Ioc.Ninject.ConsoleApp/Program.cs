using System;
using Ninject;
using Ninject.Modules;

namespace Demo.Ioc.Ninject.ConsoleApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var kernel = new StandardKernel(new ShopperModule());

			var shopper = kernel.Get<Shopper>();
			shopper.Charge();

			Console.Read();
		}
	}

	public class ShopperModule : INinjectModule
	{
		public IKernel Kernel { get; } = new StandardKernel();
		public string Name { get; } = "ShopperModule";

		public void OnLoad(IKernel kernel)
		{
			kernel.Bind<ICreditCard>().To<MasterCard>().Named("Master");
		}

		public void OnUnload(IKernel kernel) {}
		public void OnVerifyRequiredModules() {}
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
