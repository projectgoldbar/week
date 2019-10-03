using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public ShopButtons shopbuttons;
    public int type;
    

    private void Start()
    {
        Block();
    }

    public void Block()
    {
        switch (type)
        {
            case  0 :
                if (UserDataManager.Instance.userData.AdOff)
                {
                    shopbuttons.shopbuttons[0].interactable = false;
                    shopbuttons.texts[0].text = "구매완료";
                }
                break;
            case 1:
                if (UserDataManager.Instance.userData.goldBonus)
                {
                    shopbuttons.shopbuttons[1].interactable = false;
                    shopbuttons.texts[1].text = "구매완료";
                }
                break;
            case 2:
                if (UserDataManager.Instance.userData.pakage)
                {
                    shopbuttons.shopbuttons[2].interactable = false;
                    shopbuttons.texts[2].text = "구매완료";
                    shopbuttons.AllBlock();
                    
                    
                }
                break;

            default:
                break;
        }
    }
}
