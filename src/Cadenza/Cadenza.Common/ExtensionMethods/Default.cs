
//namespace Cadenza.Common;

//public static class Default
//{
//    public static TEnum For<TEnum>()
//    {
//        return typeof(TEnum).GetDefaultValueAttribute().GetValue<TEnum>();
//    }

//    private static DefaultValueAttribute GetDefaultValueAttribute(this Type type)
//    {
//        return type
//            .GetCustomAttributes(typeof(DefaultValueAttribute), false)
//            .OfType<DefaultValueAttribute>()
//            .FirstOrDefault();
//    }

//    internal static TEnum GetValue<TEnum>(this DefaultValueAttribute attribute)
//    {
//        return attribute == null
//            ? default
//            : (TEnum)attribute.Value;
//    }
//}