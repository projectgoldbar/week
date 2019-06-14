using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spitPoolManager : MonoBehaviour
{

    public GameObject Spit;

    public List<GameObject> SpitList;

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

        for (int i = 0; i < SpitCount; i++)
        {
            var ob = GameObject.Instantiate(Spit, transform);
            ob.SetActive(false);
            SpitList.Add(ob);
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


    public void SetSpitObj(GameObject obj)
    {
        obj.SetActive(false);
    }




}
