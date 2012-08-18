using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LoneWolf.Migration.Core;

namespace LoneWolf.Migration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Log("Usage: lwm.exe <path-to-input> <path-to-output>");

                return;
            }

            var input = args.First();
            var output = args.Last();

            var result = Migrate(input, output);

            Log(result.Message);
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
                new PageMigration(input, output),
            };
        }
    }
}
