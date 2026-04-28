using System;
using System.Linq;

namespace SpaceGame.Core
{
    public class Vector : IEquatable<Vector>
    {
        private readonly int[] _components;

        public Vector(params int[] components)
        {
            if (components == null)
                throw new ArgumentNullException(nameof(components));
            if (components.Length == 0)
                throw new ArgumentException("Вектор должен иметь хотя бы один компонент", nameof(components));
            _components = components.ToArray();
        }

        public int this[int index] => _components[index];
        public int Dimension => _components.Length;

        public static Vector operator +(Vector a, Vector b)
        {
            if (a.Dimension != b.Dimension)
                throw new ArgumentException("Вектор должен иметь одинаковую размерность");
            var result = new int[a.Dimension];
            for (int i = 0; i < a.Dimension; i++)
                result[i] = a[i] + b[i];
            return new Vector(result);
        }

        public override bool Equals(object obj) => Equals(obj as Vector);

        public bool Equals(Vector other)
        {
            if (other is null) return false;
            if (Dimension != other.Dimension) return false;
            for (int i = 0; i < Dimension; i++)
                if (_components[i] != other[i]) return false;
            return true;
        }

        public static bool operator ==(Vector left, Vector right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }

        public static bool operator !=(Vector left, Vector right) => !(left == right);

        public override int GetHashCode()
        {
            int hash = 17;
            foreach (var comp in _components)
                hash = hash * 31 + comp;
            return hash;
        }
    }
}
