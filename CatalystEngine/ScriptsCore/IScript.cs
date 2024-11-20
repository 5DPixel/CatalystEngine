using CatalystEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalystEngine.ScriptsCore
{
    internal abstract class IScript
    {
        public abstract void Start();
        public abstract void Update();

        public GameObject gameObject { get; set; }
    }
}
