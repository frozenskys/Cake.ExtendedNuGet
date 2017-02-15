using System;
using Cake.Core.IO;
using Cake.Core;
using System.Collections.Generic;
using Cake.Core.Tooling;
using Cake.Testing;

namespace Cake.Xamarin.Tests.Fakes
{
    public class FakeCakeContext
    {
        ICakeContext context;
        FakeLog log;
        DirectoryPath testsDir;

        public FakeCakeContext ()
        {
            testsDir = new DirectoryPath (System.IO.Path.GetFullPath (AppDomain.CurrentDomain.BaseDirectory));
            
            var fileSystem = new FileSystem ();
            var platform = new CakePlatform();
            var runtime = new CakeRuntime();
            var environment = new CakeEnvironment (platform, runtime, null);
            var globber = new Globber (fileSystem, environment);
            log = new FakeLog ();
            var args = new FakeCakeArguments ();
            var processRunner = new ProcessRunner (environment, log);
            var registry = new WindowsRegistry ();
            var configuration = new FakeConfiguration();
            var toolLocator = new ToolLocator(environment, new ToolRepository(environment),new ToolResolutionStrategy(fileSystem,environment,globber,configuration));
            context = new CakeContext (fileSystem, environment, globber, log, args, processRunner, registry, toolLocator);
            context.Environment.WorkingDirectory = testsDir;
        }

        public DirectoryPath WorkingDirectory {
            get { return testsDir; }
        }
            
        public ICakeContext CakeContext {
            get { return context; }
        }

        public string GetLogs ()
        {
            return string.Join(Environment.NewLine, log.Messages);
        }

        public void DumpLogs ()
        {
            foreach (var m in log.Messages)
                Console.WriteLine (m);
        }
    }
}

