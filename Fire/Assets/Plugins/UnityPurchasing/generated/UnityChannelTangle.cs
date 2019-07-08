#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class UnityChannelTangle
    {
        private static byte[] data = System.Convert.FromBase64String("Ar5DndDRXLl96v4we8aafCvzrk0MI4pnAy7kyphFYYNwuufT5JARy0HB98F4C+Azrwtq3s+lu2GEW4xfmZ1gVCbfNf+FGicimFP7bIbp/ikNVbIl6hiR5R/YTh321wxEzgnWghGjIAMRLCcoC6dpp9YsICAgJCEiWWyHA/qsYyvtHEW2lMCbXxlKug+jIC4hEaMgKyOjICAhirI2YUhRbQk6n0kvfy0uatixiu9zkWWp0ukKZPFuDLmDYSS1SyRDjkacvj0Ka7ZPkbX3YvSKJKXiGp8M9X21jPcGiv88AUcpEVDS3w6B032zaWjPFP1OvfQXT1jlnpTrpYsmUviOIhdp27Uez4tFOxO5yB57+IjvNz/vuNUnNyDtllj2X6SzdCMiICEg");
        private static int[] order = new int[] { 13,8,12,4,9,13,9,8,13,10,13,11,12,13,14 };
        private static int key = 33;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
