using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using MagicOnion.Server;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace BoxBattleServer
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//コンソールにログを表示させる
			GrpcEnvironment.SetLogger(new Grpc.Core.Logging.ConsoleLogger());

			var service = MagicOnionEngine.BuildServerServiceDefinition(isReturnExceptionStackTraceInErrorDetail: true);

			// localhost:12345でListen
			var server = new global::Grpc.Core.Server {
				Services = { service },
				Ports = { new ServerPort("localhost", 12345, ServerCredentials.Insecure) }
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
