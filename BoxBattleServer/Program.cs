using System;
using System.IO;
using Grpc.Core;
using Grpc.Core.Logging;
using MagicOnion.Redis;
using MagicOnion.Server;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace BoxBattleServer
{
	public class Program
	{
		private static IConfigurationRoot configuration;

		public static void Main(string[] args)
		{
			SetConfiguration();
			StartMagicOnion();
			WaitApplication();
		}

		private static void SetConfiguration()
		{
			string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
			Console.WriteLine($"ASPNETCORE_ENVIRONMENT: {environment}");

			configuration = new ConfigurationBuilder()
						.SetBasePath(Directory.GetCurrentDirectory())
						.AddJsonFile($"appsettings.json", optional: true)
						.AddJsonFile($"appsettings.{environment}.json", optional: true)
						.Build();
		}

		private static void StartMagicOnion()
		{
			// Set console log
			GrpcEnvironment.SetLogger(new CompositeLogger(
				new ConsoleLogger()
			));

			// MagicOnion
			var options = new MagicOnionOptions(true) {
				MagicOnionLogger = new MagicOnionLogToGrpcLoggerWithNamedDataDump(),
				DefaultGroupRepositoryFactory = new RedisGroupRepositoryFactory()
			};

			var redisHost = $"{configuration["Redis:Host"]}:{configuration["Redis:Port"]}";
			Console.WriteLine($"Redis host: {redisHost}");
			options.ServiceLocator.Register(ConnectionMultiplexer.Connect(redisHost));
			var service = MagicOnionEngine.BuildServerServiceDefinition(options);

			var serverHost = configuration["Server:Host"];
			var serverPort = Int32.Parse(configuration["Server:Port"]);
			Console.WriteLine($"Server host: {serverHost}, port {serverPort}");
			var server = new global::Grpc.Core.Server {
				Services = { service },
				Ports = { new ServerPort(serverHost, serverPort, ServerCredentials.Insecure) }
			};

			// Start MagicOnion
			server.Start();
		}

		private static void WaitApplication()
		{
			Console.WriteLine("Application started. Press Ctrl+C to shut down.");
			var keepRunning = true;
			Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e) {
				e.Cancel = true;
				keepRunning = false;
			};

			while (keepRunning) { }
			Console.WriteLine("\nApplication end.");
		}
	}
}
