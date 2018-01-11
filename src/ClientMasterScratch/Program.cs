using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using ClientMasterScratch.Domain;
using Nest;

namespace ClientMasterScratch
{
	public class Program
	{
		static void Main(string[] args)
		{
			//Use Bench<>() to Benchmark using BanchmarkDotNet
			//Use Run<>() to execture the loop (handy for profilers), this will call create for each itteration
			//Use RunCreateOnce<>() to do the same but call create once before the loop.

			Bench<ExpressionCreation>();

			//Run(ResolveSomeIds);
			//RunCreateOnce(ResolveSomePropertyNames);
			//Run(ResolveSomeFieldsx);
			//Run(CreateSomeExpressions);
			//Run(DoNothing);
		}

		static void Bench<TBenchmark>() where TBenchmark : RunBase => BenchmarkRunner.Run<TBenchmark>();

		static void Run<TRun>() where TRun : RunBase, new()
		{
			var runner = new TRun { IsNotBenchmark = true };
			runner.GlobalSetup();
			runner.Run();
		}
		static void RunCreateOnce<TRun>() where TRun : RunBase, new()
		{
			var runner = new TRun { IsNotBenchmark = true };
			runner.GlobalSetup();
			runner.RunCreateOnce();
		}

		public class Index : RunBase
		{
			public override int LoopCount => 10_000;

			protected override Action<bool> Routine() => Loop(() => new MyProject {Id = 200}, (c, d) => c.IndexDocument(d));
		}

		public class FieldInference : RunBase
		{
			public override int LoopCount => 1_000_000;

			protected override Action<bool> Routine() => Loop(() => Infer.Field<MyProject>(p => p.Person.Name), (c, f) => c.Infer.Field(f));
		}

		public class PropertyNameInference : RunBase
		{
            protected override Action<bool> Routine() =>
                Loop(() => Infer.Property<MyProject>(p => p.Person.Name), (c, f) => c.Infer.PropertyName(f));
		}

		public class IdInference : RunBase
		{
			public override int LoopCount => 1_000_000;

			protected override Action<bool> Routine() => Loop(() => Infer.Id(new MyProject {Id = 2}), (c, f) => c.Infer.Id(f));
		}

		public class DoNothing : RunBase
		{
			public override int LoopCount => 100_000_000;

			protected override Action<bool> Routine() => Loop(() => 1, (c, f) => { });
		}

		public class ExpressionCreation : RunBase
		{
			private static Expression<Func<T, object>> Exp<T>(Expression<Func<T, object>> exp) => exp;

			protected override Action<bool> Routine() => Loop(() => Exp<MyProject>(p => p.Person.Name), (c, f) => { });
		}

	}
}
