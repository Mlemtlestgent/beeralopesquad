using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace Beerlope
{
    [StaticConstructorOnStartup]
    public static class Class1
    {
        static Class1() //our constructor
        {
            Log.Message("Hello World!"); //Outputs "Hello World!" to the dev console.
        }
    }
}