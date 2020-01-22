using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RasterPropMonitor_Kerbalism
{
	static class Kerbalism
	{
		static Func<Vessel, string, List<KeyValuePair<string, double>>> resourceBrokersDel;
		static readonly List<KeyValuePair<string, double>> emptyBrokerList = new List<KeyValuePair<string, double>>();

		static Kerbalism()
		{
			if (AssemblyLoader.loadedAssemblies.Any(a => String.Equals(a.name, "Kerbalism", StringComparison.InvariantCultureIgnoreCase)))
			{
				MethodInfo resourceBrokersInfo = Type.GetType("KERBALISM.API").GetMethod("ResourceBrokers");
				resourceBrokersDel = (Func<Vessel, string, List<KeyValuePair<string, double>>>)
					Delegate.CreateDelegate(typeof(Func<Vessel, string, List<KeyValuePair<string, double>>>), resourceBrokersInfo);
			}
		}

		public static List<KeyValuePair<string, double>> GetResourceBrokers(Vessel vessel)
		{
			return resourceBrokersDel == null ? emptyBrokerList : resourceBrokersDel.Invoke(vessel, "ElectricCharge");
		}
	}
}
