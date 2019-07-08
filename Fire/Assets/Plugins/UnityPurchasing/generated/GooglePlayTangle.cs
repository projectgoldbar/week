#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("+tV8kfXYEjxus5d1hkwRJRJm5z2SB5j6T3WX0kO90rV4sGpIy/ydQPujRNMc7mcT6S646wAh+rI4/yB0tzcBN479FsVZ/ZwoOVNNl3KteqlLAuG5rhNoYh1TfdCkDnjU4Z8tQ/RItWsmJ6pPixwIxo0wbIrdBVi751XW9efa0d79UZ9RINrW1tbS19Rva5ai0CnDCXPs0dRupQ2acB8I36+acfUMWpXdG+qzQGI2banvvEz5uWdDAZQCfNJTFOxp+gOLQ3oB8HwJyvex3+emJCn4dyWLRZ+eOeILuFXW2NfnVdbd1VXW1td8RMCXvqeb6Dl9s83lTz7ojQ5+GcHJGU4j0cH/zGm/2Ynb2JwuR3wZhWeTXyQf/NYbYK4AqVJFgtXU1tfW");
        private static int[] order = new int[] { 8,10,9,12,9,13,8,9,12,9,11,11,12,13,14 };
        private static int key = 215;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
