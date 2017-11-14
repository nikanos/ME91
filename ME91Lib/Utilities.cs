using System;

namespace ME91Lib
{
    public class Utilities
    {
        public static int IndexOf(byte[] array, byte byteToFind)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (array.Length == 0)
                throw new ArgumentException("length of array must be greater than 0", "array");

            return Array.IndexOf(array, byteToFind);
        }

        public static int LastIndexOf(byte[] array, byte byteToFind)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (array.Length == 0)
                throw new ArgumentException("length of array must be greater than 0", "array");

            return Array.LastIndexOf(array, byteToFind);
        }

        public static int IndexOf(byte[] array, byte[] patternToFind)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (patternToFind == null)
                throw new ArgumentNullException("patternToFind");
            if (array.Length == 0)
                throw new ArgumentException("length of array must be greater than 0", "array");
            if (patternToFind.Length == 0)
                throw new ArgumentException("length of patternToFind must be greater than 0", "patternToFind");
            if (patternToFind.Length > array.Length)
                throw new ArgumentException("pattern length can not be greater than the array length", "patternToFind");

            for (int i = 0; i < array.Length - (patternToFind.Length - 1); i++)
            {
                bool match = true;
                for (int j = 0; j < patternToFind.Length; j++)
                {
                    if (array[i + j] != patternToFind[j])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                    return i;
            }

            return -1;
        }

        public static int LastIndexOf(byte[] array, byte[] patternToFind)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (patternToFind == null)
                throw new ArgumentNullException("patternToFind");
            if (array.Length == 0)
                throw new ArgumentException("length of array must be greater than 0", "array");
            if (patternToFind.Length == 0)
                throw new ArgumentException("length of patternToFind must be greater than 0", "patternToFind");
            if (patternToFind.Length > array.Length)
                throw new ArgumentException("pattern length can not be greater than the array length", "patternToFind");

            for (int i = array.Length - patternToFind.Length; i >= 0; i--)
            {
                bool match = true;
                for (int j = 0; j < patternToFind.Length; j++)
                {
                    if (array[i + j] != patternToFind[j])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                    return i;
            }

            return -1;
        }
    }
}
