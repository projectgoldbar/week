using TMPro;
using UnityEngine;

public class TextMeshMoney : MonoBehaviour
{
    private void Awake()
    {
        UserDataMansger.Instance.UserMoney = GetComponent<TextMeshProUGUI>();
    }
}
