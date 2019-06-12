using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseButton : MonoBehaviour
{
    public string targetProductId;

    public void HandleClick()
    {
        if (targetProductId == IAPManager.ProductCharacterSkin ||
            targetProductId == IAPManager.ProductSubscription )
        {
            if (IAPManager.Instance.HadPurchase(targetProductId))
            {
                Debug.Log("이미 구매한 상품");
                return;
            }
        }

        IAPManager.Instance.Purchase(targetProductId);
    }
}


//에디터에서는 즉시처리되는데
//안드로이드나 IOS에서는 버튼을 누르는 순간 팝업이뜨고 
//유니티 동작 정지
//구매처리 완료를 해야 구매 성공 실패 처리가 진행된다.
