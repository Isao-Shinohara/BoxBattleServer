using System;
using System.IO;
using System.Threading;
using Grpc.Core;
using Grpc.Core.Logging;
using MagicOnion.Redis;
using MagicOnion.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BoxBattle
{
	public class Program
	{
		private static IConfigurationRoot configuration;

		public static void Main(string[] args)
		{
			SetConfiguration();
			ConfigureServices(ServiceLocator.ServiceCollection);
			StartMagicOnion();
			WaitApplication();
		}

		private static void SetConfiguration()
		{
			string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
			Console.WriteLine($"ASPNETCORE_ENVIRONMENT: {environment}");

			configuration = new ConfigurationBuilder()
						.SetBasePath($"{Directory.GetCurrentDirectory()}/appsettings")
						.AddJsonFile($"appsettings.json", optional: true)
						.AddJsonFile($"appsettings.{environment}.json", optional: true)
						.AddEnvironmentVariables()
						.Build();
		}

		private static void ConfigureServices(IServiceCollection serviceCollection)
		{
			if (configuration["ASPNETCORE_ENVIRONMENT"] == "Local" || configuration["ASPNETCORE_ENVIRONMENT"] == "Development")
			{
				// Local or Development.
				// Dependency Injection.
			} else {
				// Staging or Production.
				var redisHost = $"{configuration["Redis:Host"]}:{configuration["Redis:Port"]}";
				Console.WriteLine($"Redis host: {redisHost}");
				ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(redisHost);
				IDatabase db = redis.GetDatabase();

				// Dependency Injection.
				serviceCollection.AddSingleton<IBattleRepository>(new RedisBattleRepository(db));
				serviceCollection.AddSingleton<IPlayerRepository>(new RedisPlayerRepository(db));
			}

			// Common settings.
			serviceCollection.AddSingleton(new BattleService());
		}

		private static void StartMagicOnion()
		{
			// MagicOnion
			var options = new MagicOnionOptions(true);
			if (configuration["ASPNETCORE_ENVIRONMENT"] == "Local")
			{
				// Local.
				// Logger
				GrpcEnvironment.SetLogger(new CompositeLogger(new ConsoleLogger()));
				options.MagicOnionLogger = new MagicOnionLogToGrpcLoggerWithNamedDataDump();
			} else if (configuration["ASPNETCORE_ENVIRONMENT"] != "Development")  {
				// Staging or Production.
				// Redis
				var redisHost = $"{configuration["Redis:Host"]}:{configuration["Redis:Port"]}";
				options.DefaultGroupRepositoryFactory = new RedisGroupRepositoryFactory();
				options.ServiceLocator.Register(ConnectionMultiplexer.Connect(redisHost));
			}
			var service = MagicOnionEngine.BuildServerServiceDefinition(options);

			// gRPC
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

			var exitEvent = new ManualResetEvent(false);

			Console.CancelKeyPress += (sender, eventArgs) => {
				eventArgs.Cancel = true;
				exitEvent.Set();
			};

			exitEvent.WaitOne();

			Console.WriteLine("\nApplication end.");
		}
	}
}
