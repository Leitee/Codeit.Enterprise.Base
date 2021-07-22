using System;
using System.Collections.Generic;
using System.Text;

namespace Codeit.NetStdLibrary.Base.Common
{
    public static partial class CodeitUtils
    {
        /// <summary>
        /// Method that returns an element from a list as well as removes it from the list.
        /// </summary>
        /// <typeparam name="T">Type of each element</typeparam>
        /// <param name="list">The list to be passed</param>
        /// <param name="index">Index of the element</param>
        /// <returns>An element of type <see cref="T"/> from the list</returns>
        public static T PopAt<T>(List<T> list, int index)
        {
            T r = list[index];
            list.RemoveAt(index);
            return r;
        }
    }

    public static class EnumerableExtension
    {
        /// <summary>
        /// Method that returns an element from a list as well as removes it from the list.
        /// </summary>
        /// <typeparam name="T">Type of each element</typeparam>
        /// <param name="list">The list to be passed</param>
        /// <param name="index">Index of the element</param>
        /// <returns>An element of type <see cref="T"/> from the list</returns>
        public static T PopAt<T>(this List<T> list, int index)
        {
            return CodeitUtils.PopAt<T>(list, index);
        }
    }
}
