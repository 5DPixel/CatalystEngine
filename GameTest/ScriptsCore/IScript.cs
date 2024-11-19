using GameTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest.ScriptsCore
{
    internal interface IScript
    {
        void Start();
        void ExecuteUpdate(GameObject currentInstance);
    }
}
