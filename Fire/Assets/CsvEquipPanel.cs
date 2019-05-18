using System.Text;
using UnityEngine;

public class CsvEquipPanel : CSVdata
{
    public GameObject Equiplist = null;

    public Transform TextPanel;

    private void Awake()
    {
        Equip_Read();
    }

    public void Equip_Read()
    {
        var Read = CSVReader.Read("EquipTextData");

        for (int i = 0; i < Read.Count / 3; i++)
        {
            GameObject Equip = Instantiate(Equiplist, transform);

            for (int j = 0; j < 3; j++)
            {
                ChildReference obj = GameObject.Instantiate<ChildReference>(list, Equip.transform);
                
                StringBuilder strBldr = new StringBuilder(Read[i * 3 + j]["적용능력Text"].ToString());
                strBldr.Append("\n" + Read[i * 3 + j]["적용능력Text2"] + "\n" + Read[i * 3 + j]["적용능력Text3"]);

                obj.Name.text = Read[i * 3 + j]["이름Text"].ToString();
                obj.ability.text = strBldr.ToString();
            }
            var p = Instantiate(TextPanel, Equip.transform);
        }

        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 320.0f * (Read.Count / 3));
    }
}