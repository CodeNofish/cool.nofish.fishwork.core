using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Fishwork.Core {

  /// <summary>
  /// Unsafe相关工具
  /// </summary>
  public static unsafe class UnsafeUtil {
    /// <summary>
    /// 分配内存。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void* Malloc(long size, int alignment = 8, Allocator allocator = Allocator.Persistent) {
      return UnsafeUtility.Malloc(size, alignment, allocator);
    }

    /// <summary>
    /// 分配内存。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T* Malloc<T>() where T : unmanaged {
      return (T*)Malloc(sizeof(T), GetAlignment<T>());
    }

    /// <summary>
    /// 释放内存。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Free(void* memory, Allocator allocator = Allocator.Persistent) {
      UnsafeUtility.Free(memory, allocator);
    }

    /// <summary>
    /// 获取非托管对象的对齐长度
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetAlignment<T>() where T : unmanaged {
      return GetAlignment(sizeof(T));
    }
    
    /// <summary>
    /// 初始化内存
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ZeroMem(void* ptr, long size) {
      UnsafeUtility.MemClear(ptr, size);
    }
    
    /// <summary>
    /// 分配并初始化内存
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void* MallocAndZero(int size, int alignment = 8) {
      return MallocAndZero((long)size, alignment);
    }

    /// <summary>
    /// 分配并初始化内存
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void* MallocAndZero(long size, int alignment = 8) {
      var memory = Malloc(size, alignment);
      ZeroMem(memory, size);
      return memory;
    }
    
    /// <summary>
    /// 分配并初始化内存
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T* MallocAndZero<T>() where T : unmanaged {
      var memory = Malloc(sizeof(T), GetAlignment<T>());
      ZeroMem(memory, sizeof(T));
      return (T*)memory;
    }

    /// <summary>
    /// 分配并初始化内存
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T* MallocAndZeroArray<T>(int length) where T : unmanaged {
      var ptr = Malloc(sizeof(T) * length, GetAlignment<T>());
      ZeroMem(ptr, sizeof(T) * length);
      return (T*)ptr;
    }
    
    /// <summary>
    /// 内存拷贝。
    /// 当源数组和目标数组重叠时，不能保证结果正确。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void MemCpy(void* destination, void* source, int size) {
      UnsafeUtility.MemCpy(destination, source, size);
    }

    /// <summary>
    /// 内存拷贝。
    /// 当源数组和目标数组重叠时，MemMove 会正确复制数据。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void MemMove(void* destination, void* source, int size) {
      UnsafeUtility.MemMove(destination, source, size);
    }
    
    /// <summary>
    /// 数组拷贝
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ArrayCopy<T>(void* source, int sourceIndex, void* destination, int destinationIndex, int count) where T : unmanaged {
      ArrayCopy(source, sourceIndex, destination, destinationIndex, count, sizeof(T));
    }

    /// <summary>
    /// 数组拷贝
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ArrayCopy(void* source, int sourceIndex, void* destination, int destinationIndex, int count, int elementStride) {
      MemCpy((byte*)destination + destinationIndex * elementStride, (byte*)source + sourceIndex * elementStride, count * elementStride);
    }
    
    /// <summary>
    /// 初始化数组
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ZeroArray<T>(T* ptr, int size) where T : unmanaged {
      ZeroMem(ptr, sizeof(T) * size);
    }
    
    /// <summary>
    /// 扩展内存并初始化
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void* ExpandZeroed(void* buffer, int currentSize, int newSize) {
      AssertUtil.Assert(newSize > currentSize, "扩展内存时，新内存大小必须大于原始大小");
      var newBuffer = MallocAndZero(newSize);
      MemCpy(newBuffer, buffer, currentSize);
      Free(buffer);
      return newBuffer;
    }

    /// <summary>
    /// 获取对齐长度，只支持 1 2 4 8 字节的
    /// </summary>
    public static int GetAlignment(int stride) {
      if ((stride & 7) == 0) return 8;
      if ((stride & 3) == 0) return 4;
      return (stride & 1) == 0 ? 2 : 1;
    }
    
    /// <summary>
    /// 获取对齐长度，支持更大范围
    /// </summary>
    public static int RoundUpToPowerOf2(int i) {
      --i;
      i |= i >> 1;
      i |= i >> 2;
      i |= i >> 4;
      i |= i >> 8;
      i |= i >> 16;
      return i + 1;
    }
  }

}
