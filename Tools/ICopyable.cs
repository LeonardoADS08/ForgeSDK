/*
Developer       : Leonardo Arteaga dos Santos
First release   : 03/01/2020
File            : Tools/ICopyable.cs
Revision        : 1.0
Changelog       :   
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ForgeSDK.Tools
{
    /// <summary>
    /// Common interface to copyable objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICopyable<T>
    {
        T Copy();
    }
}
