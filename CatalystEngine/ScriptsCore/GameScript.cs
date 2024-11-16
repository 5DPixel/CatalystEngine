using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalystEngine.ScriptsCore
{
    abstract class GameScript
    {
        public abstract void Update();
        public abstract void Start();
    }
}
