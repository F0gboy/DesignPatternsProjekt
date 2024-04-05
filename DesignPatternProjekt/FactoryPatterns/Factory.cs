using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternProjekt.FactoryPatterns
{
    internal abstract class Factory
    {
        public abstract GameObject Create();
    }
}
