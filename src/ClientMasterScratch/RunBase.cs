using System;
using System.Diagnostics;
using System.Threading;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Engines;
using Elasticsearch.Net;
using Nest;

namespace ClientMasterScratch
{
	[SimpleJob(RunStrategy.Monitoring, launchCount: 1, warmupCount: 1, targetCount: 5, id: "BenchmarkRun")]
	[MemoryDiagnoser]
	public abstract class RunBase
	{
		public virtual int LoopCount => 100_000;
		public virtual bool IsNotBenchmark { get; set; }

		public ElasticClient Client { get; set; }
		private Action<bool> _run;

		[GlobalSetup]
		public void GlobalSetup()
		{
			var connection = new InMemoryConnection();
			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(connectionPool, connection)
				.DefaultIndex("some-index");
			Client = new ElasticClient(connectionSettings);
			_run = Routine();
		}

		protected abstract Action<bool> Routine();

		[Benchmark]
		public void Run() => _run(false);

		[Benchmark]
		public void RunCreateOnce() => _run(true);

		protected Action<bool> Loop<T>(Func<T> create, Action<IElasticClient, T> act)
		{
			return createOnce =>
			{
				var instantiator = new Func<Func<T>>(() =>
				{
					if (!createOnce) return create;
					var lazy = new Lazy<T>(create, LazyThreadSafetyMode.ExecutionAndPublication);
					return (() => lazy.Value);
				})();
				var sw = Stopwatch.StartNew();
				act(this.Client, instantiator());
				var limit = LoopCount * (IsNotBenchmark ? 10 : 1);
				for (var i = 0; i < limit; i++) act(this.Client, instantiator());
				Console.WriteLine($"done in {sw.Elapsed}");
			};
		}
	}
}
