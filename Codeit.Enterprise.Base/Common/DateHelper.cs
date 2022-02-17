/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.Enterprise.Base.Common
{
    using System;

    public partial class CodeitUtils
    {
        /// <summary>
        /// Method that returns the age of something based on its birthday.
        /// </summary>
        /// <param name="birthdate">A Datetime</param>
        /// <returns>An Int32 that represents the age</returns>
        public static int GetAgeFromBirthDate(DateTime birthdate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthdate.Year;
            if (birthdate.Date > today.AddYears(-age)) age--;

            return age;
        }
    }

    public static class DateExtension
    {
        /// <summary>
        /// Extension Method that returns the age of something based on its birthday.
        /// </summary>
        /// <param name="birthdate">A Datetime</param>
        /// <returns>An Int32 that represents the age</returns>
        public static int GetAgeFromBirthDate(this DateTime birthdate)
        {
            return CodeitUtils.GetAgeFromBirthDate(birthdate);
        }
    }
}
