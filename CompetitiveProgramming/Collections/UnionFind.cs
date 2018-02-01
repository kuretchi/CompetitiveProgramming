using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Collections
{
    public class UnionFind
    {
        private readonly int[] _tree;

        public UnionFind(int length)
        {
            _tree = Enumerable.Repeat(-1, length).ToArray();
        }

        public bool Unite(int i, int j)
        {
            i = Find(i);
            j = Find(j);
            if (i == j) return false;
            if (_tree[i] > _tree[j]) { var t = i; i = j; j = t; }
            _tree[i] += _tree[j];
            _tree[j] = i;
            return true;
        }

        public bool AreUnited(int i, int j) => Find(i) == Find(j);

        public int Count(int i) => -_tree[Find(i)];

        public int CountGroups() => _tree.Count(x => x < 0);

        public int Find(int i) => _tree[i] < 0 ? i : _tree[i] = Find(_tree[i]);
    }
}
