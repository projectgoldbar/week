using System.Collections;
using UnityEngine;

public class MonsterUnit : MonoBehaviour
{
    #region 변수들

    public float hp = 10;

    public float atkPoint = 1;

    [SerializeField]
    private float Distance = 0;

    public Transform Righthand;

    // [System.NonSerialized]
    public bool Check = false;

    // [System.NonSerialized]
    public bool AttackCheck = false;

    #endregion 변수들

    private void Start()
    {
        StartCoroutine(DistanceCheck());
    }

    private IEnumerator DistanceCheck()
    {
        while (true)
        {
            Vector3 dir = (Ref.Instance.playerTr.position - transform.position);
            Distance = dir.magnitude;

            if (Distance < 10000)
            {
                Check = true;
            }
            if (Check)
            {
                if (Distance <= 2.5f) AttackCheck = true;
                else AttackCheck = false;
            }

            yield return null;
        }
    }
}