using System;
using System.Collections.Generic;

namespace Grid
{
    public struct Cell
    {
        public int X;
        public int Y;

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        private sealed class XYEqualityComparer : IEqualityComparer<Cell>
        {
            public bool Equals(Cell x, Cell y)
            {
                return x.X == y.X && x.Y == y.Y;
            }

            public int GetHashCode(Cell obj)
            {
                return HashCode.Combine(obj.X, obj.Y);
            }
        }

        public static IEqualityComparer<Cell> XYComparer { get; } = new XYEqualityComparer();
    }
}
