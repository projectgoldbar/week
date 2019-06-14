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
        goldText.text = moneyBuffer.ToString();
    }

    private float moneyBuffer;

    private void Update()
    {
        if (userData.Money != moneyBuffer)
        {
            moneyBuffer = userData.Money;
            goldText.text = moneyBuffer.ToString();
        }
    }
}