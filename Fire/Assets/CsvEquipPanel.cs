using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CsvEquipPanel : MonoBehaviour
{
    public ChildReference1 list;
    public GameObject Equiplist = null;

    public Transform TextPanel;

    private WaitForSeconds forSecond;

    public SkinnedMeshRenderer ChangeModel = null;
    public SkinnedMeshRenderer[] meshRenderer;


    public GameObject rootGameOBJ = null;

    private int objnum;

    private void Start()
    {
       forSecond = new WaitForSeconds(0.0002f);
    }

    private void Awake()
    {
       StartCoroutine( Equip_Read());
        
    }

    public IEnumerator Equip_Read()
    {
        var Read = CSVReader.Read("EquipTextData");
        var Read2 = CSVReader.Read("EquipTextData2");

        for (int i = 0; i < Read.Count / 3; i++)
        {
            GameObject Equip = Instantiate(Equiplist, transform);

            Equip.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 400.0f);
            for (int j = 0; j < 3; j++)
            {
                ChildReference1 obj = GameObject.Instantiate<ChildReference1>(list, Equip.transform);
                objnum = (i * 3 + j);
                obj.name = objnum.ToString();
                obj.Name.text = Read[i * 3 + j]["이름"].ToString();
                obj.ability.text = StringBillder(Read[i * 3 + j]["적용능력1"].ToString(),
                                                "\n" + Read[i * 3 + j]["적용능력2"] +
                                                "\n" + Read[i * 3 + j]["적용능력3"]);

                obj.ability2.text = StringBillder(Read[i * 3 + j]["적용수치1"].ToString(),
                                                "\n+ " + Read[i * 3 + j]["적용수치2"] +
                                                "\n+ " + Read[i * 3 + j]["적용수치3"] );


                SkillData skillData = new SkillData
                {
                    Index = i * 3 + j,
                    Name = obj.Name.text,
                    Ability = obj.ability.text,
                    Ability2 = obj.ability2.text,
                    Get = true
                };




              


                SettingSkill(skillData, obj);

            }
            var p = Instantiate(TextPanel, Equip.transform);
            var texts = p.GetComponentsInChildren<Text>();

            texts[0].text = StringBillder(Read2[i]["세트효율"].ToString(),
                                                "+" + Read2[i]["세트효율수치"].ToString() +
                                                "% 효율");
            int SetItem = 0;
            texts[1].text = SetItem + "/3";

            transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 420.0f * (i));
            yield return forSecond;
        }
        //rootGameOBJ.SetActive(false);
       // transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 400.0f * (Read.Count / 3));
    }

    


    public void SettingSkill(SkillData data , ChildReference1 obj)
    {
        var b_GetSkill = data.Get;
        if (!b_GetSkill)
        {
            obj.Name.text = "???";
            obj.ability.text = "???";
            obj.ability2.text = "???";
        }
        else
        {
            obj.Name.text = data.Name;
            obj.ability.text = data.Ability;
            obj.ability2.text = data.Ability2;
        }
    }


    public string StringBillder(string str1 ,string str2 )
    {
        StringBuilder strB = new StringBuilder(str1);
        strB.Append(str2);

        return strB.ToString();
    }

}