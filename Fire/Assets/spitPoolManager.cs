using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spitPoolManager : MonoBehaviour
{
    public GameObject Spit;

    public List<GameObject> SpitList;

    public GameObject SpitCollision;
    public List<GameObject> SpitCollisionList;

    public int SpitCount = 20;

    private static spitPoolManager m_instance;

    public static spitPoolManager Instance
    {
        get
        {
            if (m_instance != null) return m_instance;
            m_instance = FindObjectOfType<spitPoolManager>();
            if (m_instance == null)
                m_instance = new GameObject("IAP Manager").AddComponent<spitPoolManager>();
            return m_instance;
        }
    }

    private void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
        var obParent = new GameObject("SpitManager");
        var obParent2 = new GameObject("SpitCollisionManager");

        for (int i = 0; i < SpitCount; i++)
        {
            var ob = GameObject.Instantiate(Spit, obParent.transform);
            //ob.
            //    ;
            SpitList.Add(ob);

            var ob2 = GameObject.Instantiate(SpitCollision, obParent2.transform);
            ob2.SetActive(false);
            SpitCollisionList.Add(ob2);
        }
    }

    public GameObject GetSpitObj()
    {
        for (int i = 0; i < SpitList.Count; i++)
        {
            if (!SpitList[i].activeSelf)
            {
                return SpitList[i];
            }
        }
        return null;
    }

    public GameObject GetSpitCollisionObj()
    {
        for (int i = 0; i < SpitCollisionList.Count; i++)
        {
            if (!SpitCollisionList[i].activeSelf)
            {
                return SpitCollisionList[i];
            }
        }
        return null;
    }

    public void SetSpitObj(GameObject obj)
    {
        obj.SetActive(false);
    }

    //SpitCollision터0지는이펙트 비활성시키는 로직 (파티클시간? 게임시간? 둘중 하나로 정해서 구현)
    public void NoActive(Vector3 tr, Quaternion rot)
    {
        var particle = GetSpitCollisionObj().GetComponent<ParticleSystem>();
        particle.gameObject.SetActive(true);
        particle.transform.position = tr + Vector3.up * 3.0f;
        particle.transform.rotation = rot;
        particle.Play();

        StartCoroutine(aa(particle.gameObject));
    }

    private IEnumerator aa(GameObject ob)
    {
        yield return new WaitForSeconds(2);
        ob.SetActive(false);
        yield return null;
    }
}