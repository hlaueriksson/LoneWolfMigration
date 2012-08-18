using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoneWolf.Migration.Core
{
    public interface IMigration
    {
        void Execute();
    }
}
