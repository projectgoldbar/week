#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class AppleTangle
    {
        private static byte[] data = System.Convert.FromBase64String("TeI3VgPMlvfgp4gM4IkNqQxLNLr7b1CrEjzvDXKFjxD2VTvdjDw2BH99aHkuKEpoS2p9eC5/cWhxOgsLzkHWj3R1e+lwylptVQ+uR3agGW19eC5mdX9tf29QqxI87w1yhY8Q9gJbGggIDhYeCFsaGBgeCw8aFRgeKR4XEhoVGB5bFBVbDxMSCFsYHgltS299eC5/eGh2OgsLFx5bKRQUDwFL+XoNS3V9eC5mdHp6hH9/eHl6V1sYHgkPEh0SGBoPHlsLFBcSGAL5ent9clH9M/2MGB9+ekv6iUtRfXyXBkL48ChbqEO/ysThNHEQhFCHyksjlyF/SfcTyPRmpR4IhBwlHsci3H5yB2w7LWplD6jM8FhAPNiuFAwMVRoLCxceVRgUFlQaCwsXHhga0NgK6TwoLrrUVDrIg4CYC7ad2DdbFB1bDxMeWw8THhVbGgsLFxIYGqJNBLr8LqLc4sJJOYCjrgrlBdopGRceWwgPGhUfGgkfWw8eCRYIWxoEOtPjgqqxHedfEGqr2MCfYFG4ZEhNIUsZSnBLcn14Ln99aHkuKEpoWzg6S/l6WUt2fXJR/TP9jHZ6enoLFx5bKRQUD1s4OktlbHZLTUtPSX1LdH14LmZoenqEf35LeHp6hEtmUf0z/Yx2enp+fntLGUpwS3J9eC5dS199eC5/cGhmOgsLFx5bOB4JDxz0c89bjLDXV1sUC81Eekv3zDi0Fx5bMhUYVUpdS199eC5/cGhmOgt05kaIUDJTYbOFtc7CdaIlZ62wRgsXHls4HgkPEh0SGBoPEhQVWzoOS2p9eC5/cWhxOgsLFx5bMhUYVUqyYgmOJnWuBCTgiV54wS70NiZ2il+ZkKrMC6R0PppcsYoWA5aczmxsFR9bGBQVHxIPEhQVCFsUHVsOCB7wYvKlgjAXjnzQWUt5k2NFgytyqH57ePl6dHtL+Xpxefl6enuf6tJyTklKT0tITSFsdkhOS0lLQklKT0t2fXJR/TP9jHZ6en5+e3j5enp7J0v5f8BL+XjY23h5enl5enlLdn1yuxhIDIxBfFctkKF0WnWhwQhiNM5zJUv5emp9eC5mW3/5enNL+Xp/SzKjDeRIbx7aDO+yVnl4ent62Pl6c1B9en5+fHl6bWUTDw8LCEFUVAxk6qBlPCuQfpYlAv9WkE3ZLDcul/QI+hu9YCByVOnJgz8zixtD5W6OPgVkNxAr7Tryvw8ZcGv4OvxI8fpbGhUfWxgeCQ8SHRIYGg8SFBVbCx9OWG4wbiJmyO+MjefltCvBuiMrDxIdEhgaDx5bGQJbGhUCWwsaCQ8SHRIYGg8SFBVbOg4PExQJEg8CSu7lAXffPPAgr21MSLC/dDa1bxKqCRoYDxIYHlsIDxoPHhYeFQ8IVUtVO92MPDYEcyVLZH14LmZYf2NLbcxgxug5X2lRvHRmzTbnJRizMPtsZP74/mDiRjxMidLgO/VXr8rraaPTpwVZTrFerqJ0rRCv2V9Yaoza10ZdHFvxSBGMdvm0pZDYVIIoESAfDxMUCRIPAkptS299eC5/eGh2OgvFjwjglakfdLACNE+j2UWCA4QQs1RL+rh9c1B9en5+fHl5S/rNYfrIK9HxrqGfh6tyfEzLDg5a");
        private static int[] order = new int[] { 48,32,2,23,30,40,38,12,28,44,34,51,26,52,36,24,41,58,42,58,28,52,38,29,32,34,52,29,38,30,44,58,53,43,41,36,52,47,48,57,43,44,52,53,55,55,55,48,57,51,52,55,58,59,55,55,56,59,58,59,60 };
        private static int key = 123;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
