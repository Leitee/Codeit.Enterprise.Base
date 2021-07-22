/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.DataAccess
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class DataAccessTierException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public DataAccessTierException(string msg) : base(msg) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public DataAccessTierException(string msg, Exception ex) : base(msg, ex) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public DataAccessTierException(Exception ex) : base("Error at Data Access Layer. ", ex) { }
    }
}
