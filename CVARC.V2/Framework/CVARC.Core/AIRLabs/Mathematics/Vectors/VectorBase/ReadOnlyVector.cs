using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AIRLab.Mathematics
{
    /// <summary>
    ///   Abstract class for a read-only vector. This vector's components cannot be assign.
    ///   Provides a functionality, which is possible without assigning parameter's components, such as basic arithmetical and comparison operation. All operations are implemented as static methods. In addition, some operators are overriden to provide a usable form to access these methods, if language's compiler allows so.
    /// </summary>
    [Serializable]
    public abstract class ReadOnlyVector : IEquatable<ReadOnlyVector>, IComparable<ReadOnlyVector>,
                                           IList<double>
    {
        #region Basic functionality

        /// <summary>
        ///   Checks if vector's lengths are equal. Throws exception if not.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">Throws exception, if vector's lengthes are not equal</exception>
        public static void CheckLengths(ReadOnlyVector a, ReadOnlyVector b)
        {
            if (a.Count != b.Count)
                throw new IndexOutOfRangeException("Vectors lengthes are not equal");
        }

        /// <summary>
        ///   Creates a real vector, which's components are equal to components of this vector
        /// </summary>
        public RealVector AsRealVector()
        {
            var v = new RealVector(Count);
            for (int i = v.Count - 1; i >= 0; i--)
                v.SetElem(i, Elem(i));
            return v;
        }

        /// <summary>
        ///   Returns a string representation of vector in format:
        ///   (val1; val2;...)
        /// </summary>
        public override string ToString()
        {
            var b = new StringBuilder();
            b.Append('(');
            for (int i = 0; i < Count; i++)
            {
                if (i != 0)
                    b.Append("; ");
                b.Append(this[i].ToString(CultureInfo.InvariantCulture));
            }
            b.Append(')');
            return b.ToString();
        }

        #endregion

        #region Standart arithmetical Operations (Generated by TH)

        ///<summary>
        ///  Returns a new vector which is a sum of vector and argument
        ///</summary>
        ///<exception cref="Exception">Throw exception if vector's lengthes are not equal</exception>
        public Vector Add(ReadOnlyVector arg)
        {
            CheckLengths(this, arg);
            Vector res = new RealVector(Count);
            for (int i = Count - 1; i >= 0; --i)
                res.SetElem(i, Elem(i) + arg.Elem(i));
            return res;
        }

        ///<summary>
        ///  Returns a new vector which is a sum of vector and argument
        ///</summary>
        ///<exception cref="Exception">Throw exception if vector's lengthes are not equal</exception>
        public static Vector operator +(ReadOnlyVector a, ReadOnlyVector b)
        {
            return a.Add(b);
        }

        ///<summary>
        ///  Returns a new vector which is a difference of vector and argument
        ///</summary>
        ///<exception cref="Exception">Throw exception if vector's lengthes are not equal</exception>
        public Vector Subtract(ReadOnlyVector arg)
        {
            CheckLengths(this, arg);
            Vector res = new RealVector(Count);
            for (int i = Count - 1; i >= 0; --i)
                res.SetElem(i, Elem(i) - arg.Elem(i));
            return res;
        }

        ///<summary>
        ///  Returns a new vector which is a difference of vector and argument
        ///</summary>
        ///<exception cref="Exception">Throw exception if vector's lengthes are not equal</exception>
        public static Vector operator -(ReadOnlyVector a, ReadOnlyVector b)
        {
            return a.Subtract(b);
        }

        ///<summary>
        ///  Returns a new vector which is a result of componentwise multiplication of vector and argument
        ///</summary>
        ///<exception cref="Exception">Throw exception if vector's lengthes are not equal</exception>
        public Vector MultiplyComponentwise(ReadOnlyVector arg)
        {
            CheckLengths(this, arg);
            Vector res = new RealVector(Count);
            for (int i = Count - 1; i >= 0; --i)
                res.SetElem(i, Elem(i)*arg.Elem(i));
            return res;
        }

        ///<summary>
        ///  Returns a new vectorwhich is a result of componentwise division of vector and argument
        ///</summary>
        ///<exception cref="Exception">Throw exception if vector's lengthes are not equal</exception>
        ///<exception cref="Exception">Throws exception if divisor is zero</exception>
        public Vector DivideComponentwise(ReadOnlyVector arg)
        {
            CheckLengths(this, arg);
            Vector res = new RealVector(Count);
            for (int i = Count - 1; i >= 0; --i)
                res.SetElem(i, Elem(i)/arg.Elem(i));
            return res;
        }

        ///<summary>
        ///  Returns a new vector which is a result of multiplication of vector to scalar argument
        ///</summary>
        public Vector Multiply(double arg)
        {
            Vector res = new RealVector(Count);
            for (int i = Count - 1; i >= 0; --i)
                res.SetElem(i, Elem(i)*arg);
            return res;
        }

        ///<summary>
        ///  Returns a new vector which is a result of multiplication of vector to scalar argument
        ///</summary>
        public static Vector operator *(ReadOnlyVector a, double b)
        {
            return a.Multiply(b);
        }

        ///<summary>
        ///  Returns a new vector which is a result of division of vector to scalar argument
        ///</summary>
        public Vector Divide(double arg)
        {
            Vector res = new RealVector(Count);
            for (int i = Count - 1; i >= 0; --i)
                res.SetElem(i, Elem(i)/arg);
            return res;
        }

        ///<summary>
        ///  Returns a new vector which is a result of division of vector to scalar argument
        ///</summary>
        public static Vector operator /(ReadOnlyVector a, double b)
        {
            return a.Divide(b);
        }

        #endregion

        #region Some more arithmetical operations

        /// <summary>
        ///   Returns a result of scalar multiplication of vector and argument
        /// </summary>
        /// <exception cref="Exception">Throw exception if vector's lengthes are not equal</exception>
        public double MultiplyScalar(ReadOnlyVector arg)
        {
            CheckLengths(this, arg);
            double res = 0;
            for (var i = Count - 1; i >= 0; --i)
                res += Elem(i)*arg.Elem(i);
            return res;
        }

        /// <summary>
        ///   Returns a result of scalar multiplication of vector and argument
        /// </summary>
        /// <exception cref="Exception">Throw exception if vector's lengthes are not equal</exception>
        public static double operator *(ReadOnlyVector a, ReadOnlyVector b)
        {
            return a.MultiplyScalar(b);
        }

        /// <summary>
        ///   Returns a vector, opposite to given
        /// </summary>
        public Vector Inverse()
        {
            Vector res = new RealVector(Count);
            for (var i = Count - 1; i >= 0; --i)
                res.SetElem(i, -Elem(i));
            return res;
        }

        /// <summary>
        ///   Returns a vector, opposite to given
        /// </summary>
        public static Vector operator -(ReadOnlyVector a)
        {
            return a.Inverse();
        }

        #endregion

        #region Comparison operations

        #region IComparable<ReadOnlyVector> Members

        /// <summary>
        ///   Compare this vector to another one
        /// </summary>
        public int CompareTo(ReadOnlyVector b)
        {
            return this < b ? -1 : this == b ? 0 : 1;
        }

        #endregion

        #region IEquatable<ReadOnlyVector> Members

        ///<summary>
        ///  Returns true, if vectors are equal.
        ///</summary>
        ///<exception cref="IndexOutOfRangeException">Throws exception, if vector's lengthes are not equal</exception>
        public bool Equals(ReadOnlyVector b)
        {
            CheckLengths(this, b);
            for (var i = Count - 1; i >= 0; --i)
                if (Math.Abs(Elem(i) - b.Elem(i)) > Geometry.Epsilon)
                    return false;

            return true;
        }

        #endregion

        /// <summary>
        ///   Overriden. See Object.Equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (!(obj is ReadOnlyVector))
                return false;
            return Equals((ReadOnlyVector) obj);
        }

        /// <summary>
        ///   Gets a hash code
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        ///<summary>
        ///  Returns true, if vectors are equal.
        ///</summary>
        ///<exception cref="IndexOutOfRangeException">Throws exception, if vector's lengthes are not equal</exception>
        public static bool operator ==(ReadOnlyVector a, ReadOnlyVector b)
        {
            if ((object) a == null)
                return ((object) b) == null;
            return (object) b != null && a.Equals(b);
        }

        ///<summary>
        ///  Returns true, if vectors are not equal.
        ///</summary>
        ///<exception cref="IndexOutOfRangeException">Throws exception, if vector's lengthes are not equal</exception>
        public bool NotEqual(ReadOnlyVector b)
        {
            CheckLengths(this, b);
            for (var i = Count - 1; i >= 0; --i)
                if (Math.Abs(Elem(i) - b.Elem(i)) > Geometry.Epsilon)
                    return true;

            return false;
        }

        ///<summary>
        ///  Returns true, if vectors are not equal.
        ///</summary>
        ///<exception cref="IndexOutOfRangeException">Throws exception, if vector's lengthes are not equal</exception>
        public static bool operator !=(ReadOnlyVector a, ReadOnlyVector b)
        {
            if ((object) a == null)
                return ((object) b) != null;

            return (object) b == null || a.NotEqual(b);
        }

        ///<summary>
        ///  Returns true, if vector a is componentwise strictly less that b.
        ///</summary>
        ///<exception cref="IndexOutOfRangeException">Throws exception, if vector's lengthes are not equal</exception>
        public bool Less(ReadOnlyVector b)
        {
            CheckLengths(this, b);
            for (int i = Count - 1; i >= 0; --i)
                if (Elem(i) >= b.Elem(i))
                    return false;

            return true;
        }

        ///<summary>
        ///  Returns true, if vector a is componentwise strictly less that b.
        ///</summary>
        ///<exception cref="IndexOutOfRangeException">Throws exception, if vector's lengthes are not equal</exception>
        public static bool operator <(ReadOnlyVector a, ReadOnlyVector b)
        {
            return a.Less(b);
        }

        ///<summary>
        ///  Returns true, if vector a is componentwise less or equal to b, but a is not equal to b.
        ///</summary>
        ///<exception cref="IndexOutOfRangeException">Throws exception, if vector's lengthes are not equal</exception>
        public bool LessOrEqualStrict(ReadOnlyVector b)
        {
            CheckLengths(this, b);
            int k = 0;
            for (int i = Count - 1; i >= 0; --i)
                if (Elem(i) > b.Elem(i))
                    return false;
                else if (Math.Abs(Elem(i) - b.Elem(i)) < Geometry.Epsilon)
                    k++;

            return k != Count;
        }

        ///<summary>
        ///  Returns true, if vector a is componentwise less or equal to b.
        ///</summary>
        ///<exception cref="IndexOutOfRangeException">Throws exception, if vector's lengthes are not equal</exception>
        public bool LessOrEqual(ReadOnlyVector b)
        {
            CheckLengths(this, b);
            for (int i = Count - 1; i >= 0; --i)
                if (Elem(i) > b.Elem(i))
                    return false;

            return true;
        }

        ///<summary>
        ///  Returns true, if vector a is componentwise less or equal to b.
        ///</summary>
        ///<exception cref="IndexOutOfRangeException">Throws exception, if vector's lengthes are not equal</exception>
        public static bool operator <=(ReadOnlyVector a, ReadOnlyVector b)
        {
            return a.LessOrEqual(b);
        }

        ///<summary>
        ///  Returns true, if vectora is componentwise greter than vector b
        ///</summary>
        ///<exception cref="IndexOutOfRangeException">Throws exception, if vector's lengthes are not equal</exception>
        public bool Greater(ReadOnlyVector b)
        {
            CheckLengths(this, b);
            for (int i = Count - 1; i >= 0; --i)
                if (Elem(i) <= b.Elem(i))
                    return false;

            return true;
        }

        ///<summary>
        ///  Returns true, if vectora is componentwise greter than vector b
        ///</summary>
        ///<exception cref="IndexOutOfRangeException">Throws exception, if vector's lengthes are not equal</exception>
        public static bool operator >(ReadOnlyVector a, ReadOnlyVector b)
        {
            return a.Greater(b);
        }

        ///<summary>
        ///  Returns true, if vector a is componentwise greater or equal to b, but a is not equal to b.
        ///</summary>
        ///<exception cref="IndexOutOfRangeException">Throws exception, if vector's lengthes are not equal</exception>
        public bool GreaterOrEqualStrict(ReadOnlyVector b)
        {
            CheckLengths(this, b);
            int k = 0;
            for (int i = Count - 1; i >= 0; --i)
                if (Elem(i) < b.Elem(i))
                    return false;
                else if (Math.Abs(Elem(i) - b.Elem(i)) < Geometry.Epsilon)
                    k++;

            if (k != Count)
                return true;
            return false;
        }

        ///<summary>
        ///  Returns true, if vector a is componentwise greater or equal to b.
        ///</summary>
        ///<exception cref="IndexOutOfRangeException">Throws exception, if vector's lengthes are not equal</exception>
        public bool GreaterOrEqual(ReadOnlyVector b)
        {
            CheckLengths(this, b);
            for (int i = Count - 1; i >= 0; --i)
                if (Elem(i) < b.Elem(i))
                    return false;

            return true;
        }

        ///<summary>
        ///  Returns true, if vector a is componentwise greater or equal to b.
        ///</summary>
        ///<exception cref="IndexOutOfRangeException">Throws exception, if vector's lengthes are not equal</exception>
        public static bool operator >=(ReadOnlyVector a, ReadOnlyVector b)
        {
            return a.GreaterOrEqual(b);
        }

        #endregion

        /// <summary>
        ///   Called instead of get Item
        /// </summary>
        protected internal Func<int, double> Elem;

        /// <summary>
        ///   Called instead of set Item
        /// </summary>
        protected internal Action<int, double> SetElem =
            delegate { throw new InvalidOperationException("The vector is readonly"); };

        #region IList<double> Members

        /// <inheritdoc />
        public abstract int Count { get; }

        int IList<double>.IndexOf(double item)
        {
            int i = 0;
            bool b = false;
            foreach (var e in this)
                if (Math.Abs(e - item) < Geometry.Epsilon)
                {
                    b = true;
                    break;
                }
                else
                    ++i;
            return b ? i : -1;
        }

        void IList<double>.Insert(int index, double item)
        {
            throw new NotSupportedException();
        }

        void IList<double>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public double this[int index]
        {
            get { return Elem(index); }
            set { SetElem(index, value); }
        }

        void ICollection<double>.Add(double item)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public virtual void Clear()
        {
            throw new InvalidOperationException("The vector is readonly");
        }

        bool ICollection<double>.Contains(double item)
        {
            return ((IList<double>) this).IndexOf(item) != -1;
        }

        void ICollection<double>.CopyTo(double[] array, int arrayIndex)
        {
            int L = Count;
            for (int i = 0; i < L; ++i)
                array[arrayIndex + i] = Elem(i);
        }

        /// <inheritdoc />
        public virtual bool IsReadOnly
        {
            get { return true; }
        }

        bool ICollection<double>.Remove(double item)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public abstract IEnumerator<double> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        /// <summary>
        ///   Creates a locked wrap of vector
        /// </summary>
        public LockedVector Lock()
        {
            return new LockedVector(this);
        }

        /// <summary>
        ///   Gets an index of vector, which contains a maximum element
        /// </summary>
        /// <returns> </returns>
        public int GetMaxIndex()
        {
            int max = Count - 1;
            for (int i = Count - 2; i >= 0; i--)
                if (Elem(i) > Elem(max)) max = i;
            return max;
        }

        /// <summary>
        ///   Gets an index of vector, which contains a minimum element
        /// </summary>
        /// <returns> </returns>
        public int GetMinIndex()
        {
            int min = Count - 1;
            for (int i = Count - 2; i >= 0; i--)
                if (Elem(i) < Elem(min)) min = i;
            return min;
        }


        public static Vector Concat(params ReadOnlyVector[] operands)
        {
            int len = operands.Sum(t => t.Count);
            var res = new RealVector(len);
            int ptr = 0;
            foreach (var t in operands)
                foreach (var t1 in t)
                {
                    res[ptr] = t1;
                    ptr++;
                }
            return res;
        }
    }
}