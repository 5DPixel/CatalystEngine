using Game_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_1.ScriptsCore
{
    internal interface IScript
    {
        void Start();
        void ExecuteUpdate(GameObject currentInstance);
    }
}
