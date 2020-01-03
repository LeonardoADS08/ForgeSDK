/*
Developer       : Leonardo Arteaga dos Santos
First release   : 03/01/2020
File            : Repositories/IRepository.cs
Revision        : 1.0
Changelog       :   
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Repositories
{
    /// <summary>
    /// Common interface for repositories
    /// </summary>
    public interface IRepository<T>
    {
        bool Save();
        bool Load();
        IEnumerable<T> GetAllElements();
        T GetElement(Func<T, bool> predicate);
        bool Add(T Element);
        bool Remove(T element);
        int Remove(Func<T, bool> predicate);
        int Update(Func<T, bool> predicate, Action<T> action);
        bool Exists(Func<T, bool> predicate);
    }
}
