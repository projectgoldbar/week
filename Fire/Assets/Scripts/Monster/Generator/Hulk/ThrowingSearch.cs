using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingSearch : MonoBehaviour
{
    public int Radius = 5;

    public bool SearchIn = false;
    private WaitForSeconds waitTime = new WaitForSeconds(0.5f);

    private MonsterUnit unit;

    private int m_Mask = 0;
    private float CurrentTime = 0;

    private int SpecialZombieCount = 0;

    private void Awake()
    {
        unit = GetComponent<MonsterUnit>();
        m_Mask = 1 << (LayerMask.NameToLayer("Player"));
        m_Mask |= 1 << LayerMask.NameToLayer("Monster");
    }

    private void Update()
    {
        if (!SearchIn)
        {
            CurrentTime += Time.deltaTime;
            if (CurrentTime >= 0.5f)
            {
                var target = FindTarget(transform.position, m_Mask);
                if (target != null)
                    unit.ChaseTarget = target;
                else
                    unit.ChaseTarget = Utility.Instance.playerTr;

                CurrentTime = 0;
            }

            if (Vector3.Distance(transform.position, unit.ChaseTarget.position) <= 2.5f)
            {
                SearchIn = true;
                var State = unit.ChaseTarget.GetComponent<MonsterState>();
                var Unit = unit.ChaseTarget.GetComponent<MonsterUnit>();

                if (Unit != null)
                {
                    //내 트랜스폼을 박은녀석한테로 전달
                    Unit.SetCatch(transform);
                    State.ChangeState(StateIndex.FlyOutCatch);
                }
            }
        }
        else
        {
            unit.ChaseTarget = Utility.Instance.playerTr;
        }
    }

    private Transform FindTarget(Vector3 center, LayerMask m_Mask)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, Radius, m_Mask);
        SpecialZombieCount = 0;
        GameObject enemyGameObject = null;

        float temp = 0;
        float shortTemp = 10000;

        if (hitColliders.Length == 0)
            enemyGameObject = Utility.Instance.playerTr.gameObject;
        else
        {
            #region
            //for (int i = 0; i < hitColliders.Length; i++)
            //{
            //    if (hitColliders[i].GetComponent<MonsterUnit>().type == ZombieType.Gallery ||
            //        hitColliders[i].GetComponent<MonsterUnit>().type == ZombieType.Hulk)
            //    {
            //        if (hitColliders.Length <= 1) break;
            //        continue;
            //    }

            //    temp = Vector3.Distance(hitColliders[i].transform.position, transform.position);

            //    if (temp < shortTemp)
            //    {
            //        enemyGameObject = hitColliders[i].gameObject;
            //        Debug.Log(enemyGameObject.name);
            //        shortTemp = temp;
            //    }
            //}
            #endregion

            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].GetComponent<MonsterUnit>().type != ZombieType.Hulk &&
                    hitColliders[i].GetComponent<MonsterUnit>().type != ZombieType.Gallery)
                {
                    SpecialZombieCount++;
                }
            }

            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].GetComponent<MonsterUnit>().type != ZombieType.Hulk)
                {
                    if (hitColliders[i].GetComponent<MonsterUnit>().type == ZombieType.Gallery && SpecialZombieCount > 0)
                    {
                        continue;
                    }

                    temp = Vector3.Distance(hitColliders[i].transform.position, transform.position);

                    if (temp < shortTemp)
                    {
                        enemyGameObject = hitColliders[i].gameObject;
                        Debug.Log(enemyGameObject.name);
                        shortTemp = temp;
                    }
                }
            }
        }

        return enemyGameObject.transform;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    SearchIn = true;
    //    // if (other.gameObject.layer == 1 << LayerMask.NameToLayer("Monster"))
    //    {
    //        var State = other.GetComponent<MonsterState>();
    //        var Flycatch = other.GetComponent<FlyOutCatch>();

    //        Debug.Log("뭐지");

    //        //if (Flycatch != null)
    //        {
    //            //내 트랜스폼을 박은녀석한테로 전달
    //            Flycatch.FlyCatch(transform);
    //            State.ChangeState(StateIndex.FlyOutCatch);
    //        }
    //    }
    //}
}