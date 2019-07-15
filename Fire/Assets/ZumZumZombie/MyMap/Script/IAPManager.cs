using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    //계속 구매되는 (소모품)
    [NonSerialized]
    public string[] ProductID = { "support1000", "support3000", "support5000", "support50000" };

    //개발자센터의 해당상품에 설정한 식별자ID
    private const string _IOS_GoldId = "1000coin";
    [NonSerialized]
    public string[] _Android_ProductId = { "support1000", "support3000", "support5000", "support50000" };

    //한번 구매하면 (소장품)
    [NonSerialized]
    public string[] ProductCharacterSkin = { "01noads", "dia02", "starter03" };

    //개발자센터의 해당상품에 설정한 식별자ID
    private const string _IOS_SkinId = "com.studio.app.skin";

    [NonSerialized]
    public string[] _Android_SkinId = { "01noads", "dia02", "starter03" };

    //구독서비스 (매달 무엇을 내는?)
    private const string ProductSubscription = "premium_subscription";

    //개발자센터의 해당상품에 설정한 식별자ID
    private const string _IOS_PremiumSub = "com.studio.app.sub";

    private const string _ANDROID_PremiumSub = "com.studio.app.sub";

    public static IAPManager Instance;

    //public static IAPManager Instance
    //{
    //    get
    //    {
    //        if (m_instance != null)
    //        {
    //            return m_instance;
    //        }
    //        m_instance = FindObjectOfType<IAPManager>();
    //        if (m_instance == null)

    //            m_instance = new GameObject("IAP Manager").AddComponent<IAPManager>();
    //        return m_instance;
    //    }
    //}

    //구매 과정을 제어하는 함수 제공
    private IStoreController storeController;

    //여러 플랫폼을 위한 확장처리를 제공
    private IExtensionProvider storeExtensionProvider;

    //get만 존재하는 프로퍼티는 람다로 표현
    public bool IsInitialized => storeController != null && storeExtensionProvider != null;

    //  public Text DebugText = null;

    private void Awake()
    {
        //if (m_instance != null && m_instance != this)
        //{
        //    Destroy(this);
        //}
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitUnityIAP();
    }

    private void InitUnityIAP()
    {
        //ConfigurationBuilder
        //인앱결제와 관련된 설정을 빌드할수있는 빌더를 생성하는 클래스
        // StandardPurchasingModule
        //유니티가 제공하는 스토어의 설정
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        //상품 등록

        //상품의 ID , 상품의 타입 , 앱스토어에 사용되는ID

        #region 계속 구매되는 소모품

        for (int i = 0; i < ProductID.Length; i++)
        {
            builder.AddProduct
           (
               ProductID[i], ProductType.Consumable,
               new IDs()
               {
                    //앱스토의 이름
                    {_IOS_GoldId,AppleAppStore.Name },
                    {_Android_ProductId[i],GooglePlay.Name },
               }
           );
        }

        {
           // builder.AddProduct
           // (
           //     ProductID[0], ProductType.Consumable,
           //     new IDs()
           //     {
           //          //앱스토의 이름
           //          {_IOS_GoldId,AppleAppStore.Name },
           //          {ProductID[0],GooglePlay.Name },
           //     }
           // );

           // builder.AddProduct
           //(
           //    ProductID[1], ProductType.Consumable,
           //    new IDs()
           //    {
           //          //앱스토의 이름
           //          {_IOS_GoldId,AppleAppStore.Name },
           //          {ProductID[1],GooglePlay.Name },
           //    }
           //);
           // builder.AddProduct
           //(
           //    ProductID[2], ProductType.Consumable,
           //    new IDs()
           //    {
           //          //앱스토의 이름
           //          {_IOS_GoldId,AppleAppStore.Name },
           //          {ProductID[2],GooglePlay.Name },
           //    }
           //);
           // builder.AddProduct
           //(
           //    ProductID[3], ProductType.Consumable,
           //    new IDs()
           //    {
           //          //앱스토의 이름
           //          {_IOS_GoldId,AppleAppStore.Name },
           //          {ProductID[3],GooglePlay.Name },
           //    }
           //);
        }

        #endregion 계속 구매되는 소모품

        #region 한번만 구매되는 소장서비스

        for (int i = 0; i < ProductCharacterSkin.Length; i++)
        {
            builder.AddProduct
            (
                ProductCharacterSkin[i], ProductType.NonConsumable,
                new IDs()
                {
                    //앱스토의 이름
                    {_IOS_SkinId,AppleAppStore.Name },
                    {_Android_SkinId[i],GooglePlay.Name },
                }
            );
        }

        #endregion 한번만 구매되는 소장서비스

        #region 매달 구매해야되는 구독서비스

        builder.AddProduct
        (
           ProductSubscription, ProductType.Subscription,
           new IDs()
           {
                //앱스토의 이름
                {_IOS_PremiumSub,AppleAppStore.Name },
                {_ANDROID_PremiumSub,GooglePlay.Name },
           }
        );

        #endregion 매달 구매해야되는 구독서비스

        //초기화
        //활성화가 끝났을때 실행,빌더설정
        UnityPurchasing.Initialize(this, builder);
    }

    //UnityPurchasing.Initialize(this, builder);
    //실행후 OnIntialized 자동으로 실행됨.
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        //DebugText.text = "Unity IAP 초기화 성공";
        Debug.Log("Unity IAP 초기화 성공");

        storeController = controller;
        storeExtensionProvider = extensions;
    }

    //실패정보
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"유니티 IAP 초기화 실패{error}");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // DebugText.text = $"구매 성공 - ID : {args.purchasedProduct.definition.id}";
        Debug.Log($"구매 성공 - ID : {args.purchasedProduct.definition.id}");

        



        if (args.purchasedProduct.definition.id == ProductCharacterSkin[0])
        {
            UserDataManager.Instance.userData.AdOff = true;
        }
        if (args.purchasedProduct.definition.id == ProductCharacterSkin[1])
        {
            UserDataManager.Instance.userData.goldBonus = true;

            Debug.Log("500프로");
        }
        if (args.purchasedProduct.definition.id == ProductCharacterSkin[2])
        {
            UserDataManager.Instance.userData.pakage = true;
            Debug.Log("스타터패키지 등록");
        }

        if (args.purchasedProduct.definition.id == ProductID[0])
        {
            Debug.Log("후원1000원");
        }
        if (args.purchasedProduct.definition.id == ProductID[1])
        {
            Debug.Log("후원3000원");
        }
        if (args.purchasedProduct.definition.id == ProductID[2])
        {
            Debug.Log("후원5000원");
        }
        if (args.purchasedProduct.definition.id == ProductID[3])
        {
            Debug.Log("후원50000원");
        }



        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        //상품의 아이디 , 실패정보
        Debug.LogWarning($"구매 실패 - {product.definition.id} , {reason}");
    }

    //구매 시도

    public void Purchase(string productId)
    {
        if (!IsInitialized) return;
        //해당 ID에 대응되는 상품오브젝트를 반환
        var product = storeController.products.WithID(productId);

        if (product != null && product.availableToPurchase) //구매가능
        {
            //var Debug = $"구매시도 - {product.definition.id}";
            //DebugText.text = Debug;

            Debug.Log($"구매시도 - {product.definition.id}");
            storeController.InitiatePurchase(product);
        }
        else
        {
            //var Debug = $"구매시도 불가 - {product.definition.id}";
            //DebugText.text = Debug;
            Debug.Log($"구매시도 불가 - {product.definition.id}");
        }
    }

    //구매복구 IOS만가능
    public void RestorePurchase()
    {
        if (!IsInitialized) return;
        //플랫폼
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("구매 복구 시도");

            var appleExt = storeExtensionProvider.GetExtension<IAppleExtensions>();
            //기존의 구매내역을 복구
            appleExt.RestoreTransactions(
                result => Debug.Log($"구매 복구 시도 결과{result}"));
        }
    }

    //소모품 외에 아이템들만 구매내역을 확인해야한다.. IOS만가능
    public bool HadPurchase(string productId)
    {
        if (!IsInitialized) return false;
        //해당 ID에 대응되는 상품오브젝트를 반환
        var product = storeController.products.WithID(productId);

        if (product != null)
        {
            return product.hasReceipt;  //구매한 정보 (영수증)
        }
        return false;
    }
}