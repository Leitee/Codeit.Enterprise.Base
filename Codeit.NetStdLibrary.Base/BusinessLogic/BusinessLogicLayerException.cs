/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.BusinessLogic
{
    using System;
    public class BusinessLogicLayerException : Exception
    {
        public BusinessLogicLayerException(string msg) : base(msg) { }
        public BusinessLogicLayerException(string msg, Exception ex) : base(msg, ex) { }
        public BusinessLogicLayerException(Exception ex) : base("Error at Business Logic Layer. ", ex) { }
    }
}
