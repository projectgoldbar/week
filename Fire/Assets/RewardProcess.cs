using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardProcess : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerData playerData;

    private void Awake()
    {
        playerData = FindObjectOfType<PlayerData>();
    }

    public void RewardGold()
    {
        //유저 코인 += (인게임에서 얻은 코인 * 2)
    }

    public void Reward()
    {

    }
        




}
