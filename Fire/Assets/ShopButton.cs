using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public Button button;
    public int type;
    public Text text;

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
                    button.interactable = false;
                    text.text = "구매완료";
                }
                break;
            case 1:
                if (UserDataManager.Instance.userData.goldBonus)
                {
                    button.interactable = false;
                    text.text = "구매완료";
                }
                break;
            case 2:
                if (UserDataManager.Instance.userData.pakage)
                {
                    button.interactable = false;
                    text.text = "구매완료";
                }
                break;

            default:
                break;
        }
    }
}
