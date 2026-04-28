using System;

namespace SpaceGame.Core
{
    public class Angle : IEquatable<Angle>
    {
        // Общий знаменатель для всех углов 
        public static int Denominator { get; set; } = 8;

        public int Numerator { get; }

        public Angle(int numerator)
        {
            // Приводим числитель к диапазону [0, Denominator-1]
            int n = numerator % Denominator;
            if (n < 0) n += Denominator;
            Numerator = n;
        }

        // Оператор сложения (углы складываются по модулю знаменателя)
        public static Angle operator +(Angle a, Angle b)
        {
            return new Angle(a.Numerator + b.Numerator);
        }

        // Статические методы для синуса и косинуса 
        public static double Cos(Angle angle)
        {
            double radians = 2 * Math.PI * angle.Numerator / Denominator;
            return Math.Cos(radians);
        }

        public static double Sin(Angle angle)
        {
            double radians = 2 * Math.PI * angle.Numerator / Denominator;
            return Math.Sin(radians);
        }

        // Переопределение Equals и GetHashCode
        public override bool Equals(object obj) => Equals(obj as Angle);

        public bool Equals(Angle other)
        {
            if (other is null) return false;
            return Numerator == other.Numerator;
        }

        public static bool operator ==(Angle left, Angle right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }

        public static bool operator !=(Angle left, Angle right) => !(left == right);

        public override int GetHashCode() => Numerator.GetHashCode();
    }
}