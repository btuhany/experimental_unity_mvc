using System;
using System.Collections;
using System.Collections.Generic;
namespace Batuhan.GridSystem.Core
{
    public abstract class Grid2D<T> : IEnumerable<T>
    {
        public int Width { get; }
        public int Height { get; }
        protected T[,] _data;
        protected Grid2D(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("Grid dimensions must be greater than zero.");

            Width = width;
            Height = height;

            _data = new T[width, height];
        }
        public virtual bool TrySetElement(int x, int y, T element)
        {
            if (IsInBounds(x, y))
            {
                _data[x, y] = element;
                return true;
            }
            return false;
        }
        public virtual bool TryRemoveElement(int x, int y)
        {
            if (IsOccupied(x,y))
            {
                _data[x, y] = default;
                return true;
            }
            return false;
        }
        public virtual T GetElement(int x, int y)
        {
            if (IsInBounds(x, y))
            {
                return _data[x, y];
            }
            return default(T);
        }

        public bool IsInBounds(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public bool IsOccupied(int x, int y)
        {
            return IsInBounds(x, y) && !EqualityComparer<T>.Default.Equals(_data[x, y], default);
        }
        public void Clear()
        {
            Array.Clear(_data, 0, _data.Length);
        }
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _data)
                yield return item;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
