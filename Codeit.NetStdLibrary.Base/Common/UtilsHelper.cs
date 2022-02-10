/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.NetStdLibrary.Base.Common
{
    using System;
    using System.Globalization;

    public partial class CodeitUtils
    {
        /// <summary>
        /// Method that returns a new unique clean id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An string that represents a unique id</returns>
        public static string GetNewUniqueId(Guid id = default)
        {
            if (id == Guid.Empty)
                id = Guid.NewGuid();

            return id.ToString("N", CultureInfo.InvariantCulture);
        }
    }

    public static class UtilsExtension
    {
        /// <summary>
        /// Extension Method that returns a new unique clean id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An string that represents a unique id</returns>
        public static string ToCleanerFormatId(this Guid id)
        {
            return CodeitUtils.GetNewUniqueId(id);
        }
    }
}
