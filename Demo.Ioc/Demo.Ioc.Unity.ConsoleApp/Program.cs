using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Demo.Ioc.Unity.ConsoleApp
{
	/// <summary>
	/// https://msdn.microsoft.com/en-us/library/ff647202.aspx?f=255&MSPPError=-2147217396
	/// </summary>
	public class Program
	{
		public static void Main(string[] args)
		{
			var container = new UnityContainer();
			container.RegisterType<ICreditCard, MasterCard>();
			//container.RegisterType<ICreditCard, VisaCard>("visa");

			var shopper = container.Resolve<Shopper>();
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
