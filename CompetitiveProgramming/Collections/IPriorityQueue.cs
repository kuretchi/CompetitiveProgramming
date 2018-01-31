using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Collections
{
    public interface IPriorityQueue<T>
    {
        int Count { get; }
        bool Any();
        void Clear();
        void Enqueue(T item);
        T Dequeue();
        T Peek();
    }
}
