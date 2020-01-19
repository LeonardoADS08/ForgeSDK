/*
Developer       : Leonardo Arteaga dos Santos
First release   : 03/01/2020
File            : Repositories/IRepository.cs
Revision        : 1.0
Changelog       :   
*/

using Assets.ForgeSDK.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Repositories
{
    /// <summary>
    /// Abstract class for implemnet repositories
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Repository<T> : IRepository<T> where T : ICopyable<T>
    {
        protected abstract IEnumerable<T> _items { get; }
        protected abstract string _fileName { get; }

        protected string _fileLocation => Path.Combine(Application.persistentDataPath, _fileName);

        public IEnumerable<T> GetAllElements() => _items;
        public T GetElement(Func<T, bool> predicate) => _items.Where(predicate).FirstOrDefault();

        public abstract bool Save();
        public abstract bool Load();
        public abstract bool Add(T Element);
        public abstract bool Remove(T element);
        public abstract int Remove(Func<T, bool> condition);
        public abstract int Update(Func<T, bool> predicate, Action<T> action);
        public abstract bool Exists(Func<T, bool> predicate);
    }
}
