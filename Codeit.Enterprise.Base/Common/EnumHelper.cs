using System;
using System.ComponentModel;
using System.Reflection;

namespace Codeit.Enterprise.Base.Common
{
    public partial class CodeitUtils
    {
        public static int GetId(Enum en)
        {
            return Convert.ToInt32(en);
        }

        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }

        public static string GetDescription<T>(T en) where T : Enum
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }
    }
    public static class EnumExtension
    {
        public static int GetId(this Enum en)
        {
            return CodeitUtils.GetId(en);
        }

        public static string GetDescription(this Enum en)
        {
            return CodeitUtils.GetDescription(en);
        }
    }
}
