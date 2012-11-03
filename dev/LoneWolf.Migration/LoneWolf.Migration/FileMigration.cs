using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LoneWolf.Migration.Common;
using LoneWolf.Migration.Files;

namespace LoneWolf.Migration
{
    public static class FileMigration
    {
        public static Result Execute(string[] args)
        {
            var input = args.ElementAt(1);
            var output = args.ElementAt(2);

            return Migrate(input, output);
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
            return new List<IMigration>
            {
                new ImageMigration(input, output),
                new SectionMigration(input, output),
                //new PageMigration(input, output),
            };
        }
    }
}
