﻿using System;
using System.Linq;

namespace LoneWolf.Migration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log(Execute(args));
        }

        private static Result Execute(string[] args)
        {
            if (args.Length < 1) return GetFail();

            switch (args.First())
            {
                case "-files":
                    return FileMigration.Execute(args);
                case "-code":
                    return CodeMigration.Execute(args);
                default:
                    return GetFail();
            }
        }

        private static Result GetFail()
        {
            return new Result(
                "Usage:\n" +
                "lwm.exe -files <path-to-input> <path-to-output>\n" +
                "lwm.exe -code <path-to-input>");
        }

        private static void Log(Result result)
        {
            Console.WriteLine(result.Message);
        }
    }
}
