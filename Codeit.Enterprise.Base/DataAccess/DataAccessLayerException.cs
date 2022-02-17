/// <summary>
/// 
/// </summary>
namespace Codeit.Enterprise.Base.DataAccess
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class DataAccessLayerException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public DataAccessLayerException(string msg) : base(msg) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public DataAccessLayerException(string msg, Exception ex) : base(msg, ex) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public DataAccessLayerException(Exception ex) : base("Error at Data Access Layer. ", ex) { }
    }
}
