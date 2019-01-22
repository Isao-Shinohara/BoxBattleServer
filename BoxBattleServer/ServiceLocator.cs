﻿using Microsoft.Extensions.DependencyInjection;

namespace BoxBattleServer
{
	public static class ServiceLocator
	{
		public static ServiceCollection ServiceCollection { get; }

		static ServiceLocator()
		{
			ServiceCollection = new ServiceCollection();
		}

		public static T Get<T>()
		{
			return ServiceCollection.BuildServiceProvider().GetService<T>();
		}
	}
}
