using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace FileSystemVisitorApp
{
    public class FileSystemInfoCustomCollection<T> : IEnumerable<T>
            where T : FileSystemInfo
    {
        public T this[int index]
        {
            get
            {
                if (IsIndexInRangeOfArray(index))
                    return files[index];

                throw new System.ArgumentOutOfRangeException(index.ToString(), "Index is out of bounds");
            }
        }

        private T[] files;

        // TODO: Add exception hanlder on user access rights - inside of method...

        // TODO: Refactoring: move to Constants.cs
        private const int DefaultSize = 100;

        private int _count;

        public int Count { get { return _count; } }

        public FileSystemInfoCustomCollection()
        {
            files = new T[DefaultSize];
            _count = 0;
        }

        public void Add(T value)
        {
            if (IsArrayFilled())
            {
                files = IncreaseArraySize(files);
            }

            files[_count] = value;
            _count++;
        }

        public void Remove(T value)
        {
            int searchingIndex = -1;

            for (int index = 0; index < Count; index++)
            {
                if (files[index] == value)
                {
                    searchingIndex = index;
                    break;
                }
            }

            RemoveAt(searchingIndex);
        }

        public void RemoveAt(int index)
        {
            if (!IsIndexInRangeOfArray(index))
                throw new System.ArgumentOutOfRangeException(index.ToString(), "Index is out of bounds");

            for (int arrayIndex = index; arrayIndex < _count - 1; arrayIndex++)
            {
                files[arrayIndex] = files[arrayIndex + 1];
            }
            _count--;
        }

        public void Sort()
        {
            for (int i = 0; i < _count - 1; i++)
            {
                for (int j = 0; j < _count - i; j++)
                {
                    int result = string.Compare(files[j].FullName, files[j + 1].FullName);
                    if (result > 0)
                    {
                        var temp = files[j + 1];
                        files[j + 1] = files[j];
                        files[j] = temp;
                        break;
                    }
                }
            }
        }

        private bool IsIndexInRangeOfArray(int index)
        {
            return index >= 0 && index < _count;
        }

        private bool IsArrayFilled()
        {
            return files.Length == Count;
        }

        private T[] IncreaseArraySize(T[] array)
        {
            var copiedArray = new T[array.Length + DefaultSize];
            for (int index = 0; index < array.Length; index++)
            {
                copiedArray[index] = array[index];
            }
            return copiedArray;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int index = 0; index < Count; index++)
            {
                yield return files[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
