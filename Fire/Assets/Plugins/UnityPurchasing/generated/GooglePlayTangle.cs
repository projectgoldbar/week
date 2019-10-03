#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("/KRD1BvpYBTuKb/sByb9tT/4J3NS0d/Q4FLR2tJS0dHQe0PHkLmgnL5gRAaTBXvVVBPrbv0EjER9Bvd77z56tMriSDnvigl5HsbOHkkk1saVAJ/9SHKQ1US61bJ/t21PzPuaR/3Se5by3xU7abSQcoFLFiIVYeA6sDAGMIn6EcJe+psvPlRKkHWqfa74y2643o7c35spQHsegmCUWCMY+2hskaXXLsQOdOvW02miCp13GA/Y80+ybCEgrUiMGw/BijdrjdoCX7xMBea+qRRvZRpUetejCX/T5pgqROBS0fLg3dbZ+laYVifd0dHR1dDTDs3wttjgoSMu/3AijEKYmT7lDL+onXbyC12S2hzttEdlMWqu6LtL/tEcZ6kHrlVChdLT0dDR");
        private static int[] order = new int[] { 9,1,7,6,10,8,12,8,10,13,10,13,13,13,14 };
        private static int key = 208;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
