using System.Collections.Generic;
using UnityEngine;

public class Stage1 : Stage
{
    public GameObject cube;
    public GameObject meat;
    public GameObject tresure;

    public List<Vector3> pivots;
    public List<Vector3> randomVector;
    public List<GameObject> mines;

    public override void Setting()
    {
        Vector3 pivot = new Vector3(-239f, 1.7f, 236f);

        var t = Instantiate(tresure, pivot, Quaternion.identity, transform);
        t.GetComponent<Box>().lv = 0;
        for (int y = 0; y < 14; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                pivots.Add(new Vector3(pivot.x + (x * 12f), pivot.y, pivot.z - (y * 12f)));
                if (randomVector.Count <= y)
                {
                    if (Random.Range(0, 2) == 1)
                    {
                        randomVector.Add(new Vector3(pivot.x + (x * 12f), pivot.y, pivot.z - (y * 12f)));
                    }
                }
            }
        }
        for (int i = 0; i < pivots.Count; i++)
        {
            var a = Random.Range(-8, 8);
            var b = Random.Range(-8, 8);
            pivots[i] = new Vector3(pivots[i].x + a, pivots[i].y, pivots[i].z + b);
            var x = Instantiate(cube, pivots[i], Quaternion.identity, transform);

            x.transform.position = pivots[i];
            mines.Add(x);
        }
        for (int j = 0; j < randomVector.Count; j++)
        {
            var x = Instantiate(meat, randomVector[j], Quaternion.identity, transform);
            x.SetActive(true);
            mines.Add(x);
        }
        UITweenEffectManager.Instace.stageOpenPanel.OpenPanel("BFS");
    }
}