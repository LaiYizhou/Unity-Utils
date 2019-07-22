using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinHeap<T>
{
    private List<T> _collection { get; set; }
    private Comparer<T> _heapComparer = Comparer<T>.Default;

    public MinHeap() : this(0, null) { }
    public MinHeap(int capacity) : this(capacity, null) { }
    public MinHeap(Comparer<T> comparer) : this(0, comparer) { }
    public MinHeap(int capacity, Comparer<T> comparer)
    {
        _collection = new List<T>(capacity);
        _heapComparer = comparer ?? Comparer<T>.Default;
    }

    public int Count
    {
        get { return _collection.Count; }
    }

    public bool IsEmpty
    {
        get { return (_collection.Count == 0); }
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index > this.Count || this.Count == 0)
                throw new IndexOutOfRangeException();

            return _collection[index];
        }
        set
        {
            if (index < 0 || index >= this.Count)
                throw new IndexOutOfRangeException();

            _collection[index] = value;

            if (_heapComparer.Compare(_collection[index], _collection[0]) <= 0) // less than or equal to min
            {
                T temp = _collection[0];
                _collection[0] = _collection[index];
                _collection[index] = temp;

                _buildMinHeap();
            }
        }
    }

    private void _buildMinHeap()
    {
        for (int node = _collection.Count/2 - 1; node >= 0; node--)
        {
            _minHeapify(node, _collection.Count);
        }
    }

    private void _minHeapify(int nodeIndex, int size)
    {
        while (nodeIndex * 2 + 1 < size)
        {
            int childIndex = nodeIndex * 2 + 1;
            while (childIndex + 1 < size && _heapComparer.Compare(_collection[childIndex + 1], _collection[childIndex]) < 0)
                childIndex++;

            if (_heapComparer.Compare(_collection[childIndex], _collection[nodeIndex]) < 0)
            {
                T temp = _collection[childIndex];
                _collection[childIndex] = _collection[nodeIndex];
                _collection[nodeIndex] = temp;
            }

            nodeIndex = childIndex;

        }
    }

    public void Insert(T heapKey)
    {
        if (IsEmpty)
        {
            _collection.Add(heapKey);
        }
        else
        {
            _collection.Add(heapKey);
            _buildMinHeap();
        }
    }

    public T Peek()
    {
        if (IsEmpty)
        {
            throw new Exception("Heap is empty.");
        }

        return _collection[0];
    }

    public void Pop()
    {
        if (IsEmpty)
        {
            throw new Exception("Heap is empty.");
        }

        int min = 0;
        int last = _collection.Count - 1;

        T temp = _collection[min];
        _collection[min] = _collection[last];
        _collection[last] = temp;

        _collection.RemoveAt(last);
        last--;

        _minHeapify(0, last + 1);
    }

    public void Clear()
    {
        if (IsEmpty)
        {
            throw new Exception("Heap is empty.");
        }

        _collection.Clear();
    }

    public List<T> ToList()
    {
        return _collection;
    }


}
