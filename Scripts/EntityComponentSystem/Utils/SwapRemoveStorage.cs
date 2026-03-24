using System;

public class SwapRemoveStorage<T> {
    private int[] _sparseToDense;
    private int[] _denseToSparse;
    private T[] _denseValues;
    private int _count;

    public SwapRemoveStorage(int sparseCapacity, int denseCapacity) {
        _sparseToDense = new int[sparseCapacity];
        _denseToSparse = new int[denseCapacity];
        _denseValues = new T[denseCapacity];
        _count = 0;

        Array.Fill(_sparseToDense, -1);
        Array.Fill(_denseToSparse, -1);
    }

    public bool Contains(int id) {
        if (id < 0 || id >= _sparseToDense.Length) {
            return false;
        }

        int index = _sparseToDense[id];
        return index != -1 && index < _count;
    }

    public ref T Get(int id) {
        return ref _denseValues[_sparseToDense[id]];
    }

    public void Add(int id, T value) {
        EnsureSparseCapacity(id);
        EnsureDenseCapacity();

        if (Contains(id)) {
            _denseValues[_sparseToDense[id]] = value;
            return;
        }

        int index = _count;
        _denseToSparse[index] = id;
        _sparseToDense[id] = index;
        _denseValues[index] = value;
        _count++;
    }

    public bool TryGet(int id, out T value) {
        if (Contains(id)) {
            value = _denseValues[_sparseToDense[id]];
            return true;
        }

        value = default;
        return false;
    }

    public void Remove(int id) {
        if (!Contains(id)) {
            return;
        }

        int index = _sparseToDense[id];
        int lastIndex = _count - 1;
        int lastId = _denseToSparse[lastIndex];

        _denseValues[index] = _denseValues[lastIndex];

        _denseToSparse[index] = lastId;
        _sparseToDense[lastId] = index;

        _sparseToDense[id] = -1;
        _denseToSparse[lastIndex] = -1;
        _count--;
    }

    private void EnsureSparseCapacity(int id) {
        if (id < _sparseToDense.Length) {
            return;
        }

        int newSize = _sparseToDense.Length;
        while (id >= newSize) {
            newSize *= 2;
        }

        int oldSize = _sparseToDense.Length;
        Array.Resize(ref _sparseToDense, newSize);
        for (int i = oldSize; i < newSize; i++) {
            _sparseToDense[i] = -1;
        }
    }

    private void EnsureDenseCapacity() {
        if (_count < _denseValues.Length) {
            return;
        }

        int newSize = _denseValues.Length * 2;
        Array.Resize(ref _denseValues, newSize);
        Array.Resize(ref _denseToSparse, newSize);
    }
}