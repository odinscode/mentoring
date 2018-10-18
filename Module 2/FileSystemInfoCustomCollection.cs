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
                if (index > 0 && index < _count)
                    return files[index];

                throw new System.ArgumentOutOfRangeException();
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

        // TODO: add remove

        public void RemoveAt(int index)
        {
            for (int arrayIndex = 0; arrayIndex < files.Length - 1; arrayIndex++)
            {
                files[arrayIndex] = files[arrayIndex + 1];
            }
            files[files.Length] = null;
            _count--;
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
