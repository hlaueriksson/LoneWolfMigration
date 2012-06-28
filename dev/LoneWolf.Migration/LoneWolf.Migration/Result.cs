using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoneWolf.Migration
{
    public class Result
    {
        public string Message { get; set; }

        public Result(string message)
        {
            Message = message;
        }
    }
}
