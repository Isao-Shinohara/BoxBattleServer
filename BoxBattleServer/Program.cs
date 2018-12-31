using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Logging;
using MagicOnion.Redis;
using MagicOnion.Server;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using StackExchange.Redis;

namespace BoxBattleServer
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//コンソールにログを表示させる
			GrpcEnvironment.SetLogger(new CompositeLogger(
				new ConsoleLogger()
			));

			var options = new MagicOnionOptions(true) {
				MagicOnionLogger = new MagicOnionLogToGrpcLoggerWithNamedDataDump(),
				DefaultGroupRepositoryFactory = new RedisGroupRepositoryFactory()
			};
			options.ServiceLocator.Register(ConnectionMultiplexer.Connect("localhost"));
			var service = MagicOnionEngine.BuildServerServiceDefinition(options);

			// localhost:12345でListen
			var server = new global::Grpc.Core.Server {
				Services = { service },
				Ports = { new ServerPort("0.0.0.0", Define.Port, ServerCredentials.Insecure) }
			};

			// MagicOnion起動
			server.Start();

			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
