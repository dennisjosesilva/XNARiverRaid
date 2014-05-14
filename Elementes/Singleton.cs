using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiverRaid.Elementes
{
    class Singleton
    {
        private static Singleton singleton;
        public string musicaAtual;

        private Singleton()
        {
        }

        private static Singleton getSingleton()
        {
            if (singleton == null)
                singleton = new Singleton();
            return singleton;
        }



    }
}
