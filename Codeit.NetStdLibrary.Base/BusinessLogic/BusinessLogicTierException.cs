/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.BusinessLogic
{
    using System;
    public class BusinessLogicTierException : Exception
    {
        public BusinessLogicTierException(string msg) : base(msg) { }
        public BusinessLogicTierException(string msg, Exception ex) : base(msg, ex) { }
        public BusinessLogicTierException(Exception ex) : base("Error at Business Layer. ", ex) { }
    }
}
