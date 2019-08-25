using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinHeap<T>
{
    private List<T> _collection { get; set; }
    private readonly Comparer<T> _heapComparer = Comparer<T>.Default;

    public MinHeap() : this(Comparer<T>.Default) { }
    public MinHeap(Comparer<T> comparer)
    {
        _collection = new List<T>();
        _heapComparer = comparer ?? Comparer<T>.Default;
    }

    public int Count
    {
        get { return _collection.Count; }
    }

    public void Add(T value)
    {
        if (Count == 0)
        {
            _collection.Add(value);
        }
        else
        {
            _collection.Add(value);
            Swim(_collection.Count - 1);
        }
    }

    public T Peek()
    {
        if (Count == 0)
        {
            throw new Exception("Heap is empty.");
        }

        return _collection[0];
    }

    public T Pop()
    {
        if (Count == 0)
        {
            throw new Exception("Heap is empty.");
        }

        T temp = _collection[0];
        _collection[0] = _collection[Count - 1];
        _collection.RemoveAt(Count - 1);

        Sink(0);

        return temp;
    }

    private void Swim(int index)
    {
        while (index > 0 && _heapComparer.Compare(_collection[index], _collection[Parent(index)]) < 0)
        {
            Swap(index, Parent(index));
            index = Parent(index);
        }
    }

    private void Sink(int index)
    {
        while (LeftChild(index) < Count)
        {
            int childIndex = LeftChild(index);
            if (RightChild(index) < Count && _heapComparer.Compare(_collection[RightChild(index)], _collection[LeftChild(index)]) < 0)
                childIndex = RightChild(index);

            if (_heapComparer.Compare(_collection[index], _collection[childIndex]) < 0)
                break;

            Swap(index, childIndex);
            index = childIndex;
        }
    }

  	private void Swap(int index1, int index2)
    {
        var temp = this._collection[index1];
        this._collection[index1] = this._collection[index2];
        this._collection[index2] = temp;
    }

    private static int Parent(int i)
    {
        return (i - 1) / 2;
    }

    private static int RightChild(int i)
    {
        return 2 * i + 2;
    }

    private static int LeftChild(int i)
    {
        return 2 * i + 1;
    }

}
