using UnityEngine;
using UnityEngine.UI;

public class moneyText : MonoBehaviour
{
    private NewUserData userData;

    private Text goldText;

    private void Start()
    {
        goldText = GetComponent<Text>();
        userData = FindObjectOfType<UserDataManager>().userData;
        moneyBuffer = userData.Money;
        var x = UserDataManager.Instance.UnShuffle(moneyBuffer);
        goldText.text = x.ToString();
    }

    public float moneyBuffer;

    private void Update()
    {
        if (userData.Money != moneyBuffer)
        {
            moneyBuffer = userData.Money;
            var x = UserDataManager.Instance.UnShuffle(moneyBuffer);
            Debug.Log(x);
            goldText.text = x.ToString();
        }
    }
}