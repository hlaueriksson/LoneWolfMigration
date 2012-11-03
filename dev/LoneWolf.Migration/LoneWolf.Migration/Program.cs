using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LoneWolf.Migration.Code;
using LoneWolf.Migration.Core;

namespace LoneWolf.Migration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Log("Usage:");
                Log("lwm.exe -files <path-to-input> <path-to-output>");
                Log("lwm.exe -code <path-to-input>");

                return;
            }

            var command = args.First();

            Result result;

            switch (command)
            {
                case "-files":
                    result = Files(args); break;
                case "-code":
                    result = Code(args); break;
                default:
                    result = new Result("Fail!"); break;
            }

            Log(result.Message);
        }

        private static Result Files(string[] args)
        {
            var input = args.ElementAt(1);
            var output = args.ElementAt(2);

            return Migrate(input, output);
        }

        private static Result Code(IEnumerable<string> args)
        {
            var input = args.ElementAt(1);

            return Generate(input);
        }

        private static void Log(string message)
        {
            Console.WriteLine(message);
        }

        private static Result Migrate(string input, string output)
        {
            if (!Directory.Exists(input)) return new Result("The path to input does not exist");
            if (!Directory.Exists(output)) return new Result("The path to output does not exist");

            try
            {
                foreach (var migration in GetMigrations(input, output))
                {
                    migration.Execute();
                }

                return new Result("Done");
            }
            catch (Exception ex)
            {
                return new Result(ex.GetType().Name + ": " + ex.Message + "\n" + ex.StackTrace);
            }
        }

        private static IEnumerable<IMigration> GetMigrations(string input, string output)
        {
            return new List<IMigration> {
                new ImageMigration(input, output),
                new SectionMigration(input, output),
                //new PageMigration(input, output),
            };
        }

        private static Result Generate(string input)
        {
            if (!Directory.Exists(input)) return new Result("The path to input does not exist");

            try
            {
                foreach (var migration in GetGenerators(input))
                {
                    migration.Execute();
                }

                return new Result("Done");
            }
            catch (Exception ex)
            {
                return new Result(ex.GetType().Name + ": " + ex.Message + "\n" + ex.StackTrace);
            }
        }

        private static IEnumerable<ICodeGenerator> GetGenerators(string input)
        {
            return new List<ICodeGenerator> {
                new SectionFactoryGenerator(input),
                //new ItemFactoryGenerator(input)
            };
        }
    }
}
