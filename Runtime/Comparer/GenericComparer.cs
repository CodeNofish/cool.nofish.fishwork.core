using System;
using System.Collections.Generic;

namespace Fishwork.Core {

  /// <summary>
  /// A generic comparer class that implements IComparer interface
  /// Allows custom comparison logic to be defined using a delegate function
  /// </summary>
  /// <typeparam name="T">The type of objects to compare</typeparam>
  public class GenericComparer<T> : IComparer<T> {
    private readonly Func<T, T, int> _comparer;

    /// <summary>
    /// Initializes a new instance of the GenericComparer class.
    /// Delegate function that defines the custom comparison logic
    /// Should return:
    /// - Less than 0 if x is less than y
    /// - 0 if x equals y  
    /// - Greater than 0 if x is greater than y
    /// </summary>
    /// <param name="comparer">The comparison function that defines the sorting logic</param>
    /// <exception cref="ArgumentNullException">Thrown when comparer is null</exception>
    public GenericComparer(Func<T, T, int> comparer) {
      _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
    }

    /// <summary>
    /// Compares two objects and returns a value indicating their relative order
    /// </summary>
    /// <param name="x">The first object to compare</param>
    /// <param name="y">The second object to compare</param>
    /// <returns>
    /// Integer that indicates the relative values of x and y:
    /// - Less than 0: x is less than y
    /// - 0: x equals y
    /// - Greater than 0: x is greater than y
    /// </returns>
    public int Compare(T x, T y) {
      return _comparer(x, y);
    }
  }

}
