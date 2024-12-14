using System;
using System.Runtime.CompilerServices;

namespace Fishwork.Core {

  public static class ArrayUtil {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] Copy<T>(T[] array, int start, int length) {
      if (start + length > array.Length)
        length = array.Length - start;
      T[] newArray = new T[length];
      Array.Copy(array, start, newArray, 0, length);
      return newArray;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] InsertAt<T>(T[] array, int position, T item) {
      T[] newArray = new T[array.Length + 1];
      if (position > 0)
        Array.Copy(array, newArray, position);
      if (position < array.Length)
        Array.Copy(array, position, newArray, position + 1, array.Length - position);
      newArray[position] = item;
      return newArray;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] InsertAt<T>(T[] array, int position, T[] items) {
      T[] newArray = new T[array.Length + items.Length];
      if (position > 0)
        Array.Copy(array, newArray, position);
      if (position < array.Length)
        Array.Copy(array, position, newArray, position + items.Length, array.Length - position);
      items.CopyTo(newArray, position);
      return newArray;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] Append<T>(T[] array, T item) {
      return InsertAt(array, array.Length, item);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] Append<T>(T[] array, T[] items) {
      return InsertAt(array, array.Length, items);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] RemoveAt<T>(T[] array, int position, int length = 1) {
      if (position + length > array.Length)
        length = array.Length - position;
      T[] newArray = new T[array.Length - length];
      if (position > 0)
        Array.Copy(array, newArray, position);
      if (position < newArray.Length)
        Array.Copy(array, position + length, newArray, position, newArray.Length - position);
      return newArray;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] ReplaceAt<T>(T[] array, int position, T item) {
      T[] newArray = new T[array.Length];
      Array.Copy(array, newArray, array.Length);
      newArray[position] = item;
      return newArray;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] ReplaceAt<T>(T[] array, int position, int length, T[] items) {
      return InsertAt(RemoveAt(array, position, length), position, items);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Reverse<T>(T[] array) {
      Reverse(array, 0, array.Length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Reverse<T>(T[] array, int start, int count) {
      int end = start + count - 1;
      for (int i = start, j = end; i < j; i++, j--)
        (array[i], array[j]) = (array[j], array[i]);
    }

    public static int BinarySearch(int[] array, int value) {
      var low = 0;
      var high = array.Length - 1;
      while (low <= high) {
        var middle = low + ((high - low) >> 1);
        var midValue = array[middle];
        if (midValue == value)
          return middle;
        if (midValue > value)
          high = middle - 1;
        else
          low = middle + 1;
      }
      return ~low;
    }

    public static int BinarySearchUpperBound(int[] array, int value) {
      int low = 0;
      int high = array.Length - 1;
      while (low <= high) {
        int middle = low + ((high - low) >> 1);
        if (array[middle] > value)
          high = middle - 1;
        else
          low = middle + 1;
      }
      return low;
    }
  }

}
