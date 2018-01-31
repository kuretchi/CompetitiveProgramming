using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Collections
{
    public class LeftistHeap<T> : IPriorityQueue<T>
    {
        private class Node
        {
            public Node Left, Right;
            public int MinDepth;
            public T Value;
        }

        public LeftistHeap() : this(Comparer<T>.Default) { }

        public LeftistHeap(IComparer<T> comparer)
        {
            _comparer = comparer;
        }

        private readonly IComparer<T> _comparer;
        private Node _root;

        public int Count { get; private set; }

        public bool Any() => this.Count > 0;

        public void Clear()
        {
            _root = null;
            this.Count = 0;
        }

        public void Enqueue(T item)
        {
            var node = new Node { Value = item };
            _root = Meld(_root, node);
            this.Count++;
        }

        public T Dequeue()
        {
            if (this.Count == 0) throw new InvalidOperationException();
            var ret = _root.Value;
            _root = Meld(_root.Left, _root.Right);
            this.Count--;
            return ret;
        }

        public T Peek()
        {
            if (this.Count == 0) throw new InvalidOperationException();
            return _root.Value;
        }

        public void Merge(LeftistHeap<T> other)
        {
            if (_comparer != other._comparer) throw new InvalidOperationException();
            _root = Meld(_root, other._root);
            this.Count += other.Count;
            other.Clear();
        }

        private Node Meld(Node a, Node b)
        {
            if (a == null) return b;
            if (b == null) return a;
            if (_comparer.Compare(a.Value, b.Value) > 0)
                Swap(ref a, ref b);
            a.Right = Meld(a.Right, b);
            if (a.Left == null || a.Left.MinDepth < a.Right.MinDepth)
                Swap(ref a.Left, ref a.Right);
            a.MinDepth = (a.Right?.MinDepth ?? 0) + 1;
            return a;
        }

        private void Swap(ref Node a, ref Node b)
        {
            var t = a; a = b; b = t;
        }
    }
}
