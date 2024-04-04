using System;
using DesignPatternProjekt;

namespace DesignPatternProjekt
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            GameWorld.Instance.Run();
        }
    }
}