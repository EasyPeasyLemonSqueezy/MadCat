using System.Collections.Generic;

namespace NutEngine
{
    /// <summary>
    /// Compare <see cref="Node"/> by <see cref="Node.ZOrder"/>.
    /// </summary>
    public class NodeComparer : IComparer<Node>
    {
        public int Compare(Node x, Node y)
        {
            var result = x.ZOrder.CompareTo(y.ZOrder);

            /// For stable sorting.
            return result == 0 ? 1 : result;
        }
    }
}
