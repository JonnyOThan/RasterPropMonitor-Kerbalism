using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RasterPropMonitor_Kerbalism
{
	class RPMKPartModule : PartModule
	{
		public override void OnStart(StartState state)
		{
			UnityEngine.Debug.Log("RPMKPartModule.Start");
		}

		public override void OnFixedUpdate()
		{
			base.OnFixedUpdate();

			var brokers = Kerbalism.GetResourceBrokers(vessel);

			m_outputSolar = 0.0f;
			m_outputGenerator = 0.0f;
			m_outputFuelCell = 0.0f;

			for (int i = 0; i < brokers.Count; ++i)
			{
				if (brokers[i].Key.Contains("solar"))
				{
					m_outputSolar += brokers[i].Value;
				}
				else if (brokers[i].Key.Contains("cell"))
				{
					m_outputFuelCell += brokers[i].Value;
				}
				else if (brokers[i].Key.Contains("generator"))
				{
					m_outputGenerator += brokers[i].Value;
				}
			}
		}

		public object HandleVariable(string variableName)
		{
			switch (variableName)
			{
				case "ELECOUTPUTSOLAR": return m_outputSolar;
				case "ELECOUTPUTGENERATOR": return m_outputGenerator;
				case "ELECOUTPUTFUELCELL": return m_outputFuelCell;
			}

			return null;
		}

		double m_outputSolar;
		double m_outputGenerator;
		double m_outputFuelCell;
	}
}
