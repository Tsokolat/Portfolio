using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game
{
	public class ServiceLocator
	{

		private Dictionary<Type, object> serviceList = new Dictionary<Type, object>();
		public void Bind<T>(T serviceInstance)
		{
			serviceList[typeof(T)] = serviceInstance;
		}
		public T Resolve<T>()
		{
			try
			{
				return (T)serviceList[typeof(T)];

			}
			catch (Exception e)
			{
				UnityEngine.Debug.LogException(e);
				throw;
			}
		}
	}
}
