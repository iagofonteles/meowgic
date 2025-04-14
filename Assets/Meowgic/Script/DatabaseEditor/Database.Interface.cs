using System;
using System.Collections;
using UnityEngine;

namespace Meowgic
{
    interface IDatabase : IEnumerable
    {
        Type Type { get; }
        IDatabaseItem Get(int index);
        IDatabaseItem Get(string name);
    }

    public interface IDatabaseItem
    {
        string name { get; }
        string DisplayName { get; }
        Sprite Icon { get; }
    }
}