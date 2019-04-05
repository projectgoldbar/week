using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private LineRenderer line;

    public Transform[] pranets;         //달의 트랜스폼을 담음

    private QuestDateBase questDate;

    public Canvas planetcanvas;
    public Text text;

    //퀘스트창에 전달할 클래스변수
    public static dataInfo info;

    public CanvasGroup PlanetUICanvas;
    // Start is called before the first frame update

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        questDate = GetComponent<QuestDateBase>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Planet")))
            {
                string planetName = hit.transform.name;
                Vector3 pos = PlanetPos(hit);

                info = GetSetDataInfo(questDate, planetName);

                planetcanvas.transform.position = pos;
                text.text = info.QuestContent;
            }
        }
    }

    private dataInfo GetSetDataInfo(QuestDateBase data, string planetName)
    {
        return data.database[planetName];
    }

    private Vector3 PlanetPos(RaycastHit hit)
    {
        return hit.transform.position;
    }
}