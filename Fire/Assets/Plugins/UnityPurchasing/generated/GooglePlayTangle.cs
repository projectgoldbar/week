#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("lJBtWSvSOPKIFyovlV72YYvk8yQcri0OHCEqJQaqZKrbIS0tLSksLw+zTpDd3FG0cOfzPXbLl3Em/qNAAFi/KOcVnOgS1UMQ+9oBScME249UYYoO96FuJuARSLuZzZZSFEe3AkKcuPpv+YcpqO8XkgH4cLiB+guHAS6Hag4j6ceVSGyOfbfq3umdHMaw+RpCVeiTmeaohitf9YMvGmTWuBPChkg2HrTFE3b1heI6MuK12Co68jEMSiQcXd/SA4zecL5kZcIZ8EOuLSMsHK4tJi6uLS0sh787bEVcYGn8YwG0jmwpuEYpToNLkbMwB2a7TMz6zHUG7T6iBmfTwqi2bIlWgVIEN5JEInIgI2fVvIfifpxopN/kBy3gm1X7Uqm+eS4vLSwt");
        private static int[] order = new int[] { 4,4,13,9,9,7,8,13,8,11,11,11,12,13,14 };
        private static int key = 44;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
