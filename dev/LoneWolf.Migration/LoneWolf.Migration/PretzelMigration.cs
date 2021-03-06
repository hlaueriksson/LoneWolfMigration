using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LoneWolf.Migration.Common;
using LoneWolf.Migration.Pretzel;

namespace LoneWolf.Migration
{
    public static class PretzelMigration
    {
        public static Result Execute(IEnumerable<string> args)
        {
            var input = args.ElementAt(1);

            return Migrate(input);
        }

        private static Result Migrate(string input)
        {
            if (!Directory.Exists(input)) return new Result("The path to input does not exist");

            try
            {
                foreach (var migration in GetMigrations(input))
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

        private static IEnumerable<IMigration> GetMigrations(string input)
        {
            return new List<IMigration>
            {
                new SectionMigration(input),
                new PageMigration(input)
            };
        }
    }
}
