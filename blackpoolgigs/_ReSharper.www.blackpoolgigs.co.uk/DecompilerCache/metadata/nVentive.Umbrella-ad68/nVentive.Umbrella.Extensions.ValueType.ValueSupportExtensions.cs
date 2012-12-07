// Type: nVentive.Umbrella.Extensions.ValueType.ValueSupportExtensions
// Assembly: nVentive.Umbrella, Version=0.9.0.0, Culture=neutral
// Assembly location: C:\business websites\lib\umbrella\nVentive.Umbrella.dll

namespace nVentive.Umbrella.Extensions.ValueType
{
    public static class ValueSupportExtensions
    {
        public static T And<T>(this T lhs, T rhs) where T : struct, new();
        public static T Or<T>(this T lhs, T rhs) where T : struct, new();
        public static T Xor<T>(this T lhs, T rhs) where T : struct, new();
        public static T Add<T>(this T lhs, T rhs) where T : struct, new();
        public static T Substract<T>(this T lhs, T rhs) where T : struct, new();
        public static T Multiply<T>(this T lhs, T rhs) where T : struct, new();
        public static T Divide<T>(this T lhs, T rhs) where T : struct, new();
        public static T Negate<T>(this T instance) where T : struct, new();
        public static T Not<T>(this T instance) where T : struct, new();
        public static bool ContainsAny<T>(this T lhs, T rhs) where T : struct, new();
        public static bool ContainsAll<T>(this T lhs, T rhs) where T : struct, new();
    }
}
