using CatalystEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalystEngine.ScriptsCore
{
    internal interface IScript
    {
        void Start();
        void Update();
    }
}
