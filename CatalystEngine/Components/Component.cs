using CatalystEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalystEngine.Components
{
    internal abstract class Component
    {
        public GameObject gameObject;

        public abstract void Update();

        public virtual void Start()
        {
            //No implementation but not abstract because not all Components will need this method
        }

        public virtual void FixedUpdate()
        {

        }
    }
}
