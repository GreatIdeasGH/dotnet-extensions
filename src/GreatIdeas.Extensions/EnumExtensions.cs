using System.ComponentModel;
using System.Reflection;

namespace GreatIdeas.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum genericEnum)
    {
        MemberInfo[] member = genericEnum.GetType().GetMember(genericEnum.ToString());
        if (member != null && member.Length != 0)
        {
            object[] customAttributes = member[0].GetCustomAttributes(typeof (DescriptionAttribute), false);
            if (customAttributes != null && ((IEnumerable<object>) customAttributes).Count<object>() > 0)
                return ((DescriptionAttribute) ((IEnumerable<object>) customAttributes).ElementAt<object>(0)).Description;
        }
        return genericEnum.ToString();
    }
}