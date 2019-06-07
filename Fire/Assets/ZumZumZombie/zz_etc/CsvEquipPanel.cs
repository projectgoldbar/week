using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;



public class CsvEquipPanel : MonoBehaviour
{
    private WaitForSeconds forSecond;

    public SkinnedMeshRenderer ChangeModel = null;
    public SkinnedMeshRenderer[] meshRenderer;

    private int objnum;

    public List<Equipdata> EquipList = new List<Equipdata>();

    private List<Dictionary<string, object>> Read = new List<Dictionary<string, object>>();
    private List<Dictionary<string, object>> Read2 = new List<Dictionary<string, object>>();



    private void Start()
    {
       forSecond = new WaitForSeconds(0.0002f);
    }

    private void Awake()
    {
       Read = CSVReader.Read("EquipTextData");
       Read2 = CSVReader.Read("EquipTextData2");
       StartCoroutine(Equip_Read());
       
    }

    public void UI_TextSetting()
    {
        for (int i = 0; i < LobyDataManager.Instance.reference1.Length; i++)
        {
            LobyDataManager.Instance.reference1[i].equipRef.Name =
            LobyDataManager.Instance.reference1[i].Name.text;
            LobyDataManager.Instance.reference1[i].equipRef.ability =
            LobyDataManager.Instance.reference1[i].ability.text;
            LobyDataManager.Instance.reference1[i].equipRef.ability2 =
            LobyDataManager.Instance.reference1[i].ability2.text;
        }
    }


    public IEnumerator Equip_Read()
    {
        

        int count = LobyDataManager.Instance.reference1.Length;

        for (int j = 0; j < count; j++)
        {
            LobyDataManager.Instance.reference1[j].Name.text = Read[j]["hatName"].ToString();

            string[] mutationName = {"", "", "" };
            string[] mutationLV = { "", "", "" };
            string ValueName = "";
            string ValueNum = "";
            int n = int.Parse(Read[j]["mutationsCount"].ToString());

            if (n > 0)
            {
                for (int v = 0; v < n; v++)
                {

                    int MTLV = int.Parse(Read[j][(v + 1) + "_mutationLV"].ToString());

                    mutationName[v] = Read[j][(v + 1) + "_mutatioName"].ToString() + "\n";
                    mutationLV[v]   ="+ "+ MTLV.ToString() + "\n";

                    EquipDataSet dataSet = new EquipDataSet();
                    dataSet.name = j.ToString();
                    dataSet.Key = int.Parse(Read[j][(v + 1) + "_mutationIndex"].ToString());
                    dataSet.Data = MTLV;

                    LobyDataManager.Instance.reference1[j].DataListSet.Add(dataSet);


                    if (mutationName[v] == "none")
                    {
                        continue;
                    }
                    ValueName = string.Concat(mutationName);
                    ValueNum = string.Concat(mutationLV);
                }
            }

            LobyDataManager.Instance.reference1[j].AddHp
                = float.Parse(Read[j]["statValue"].ToString());

            LobyDataManager.Instance.reference1[j].b_Panel 
                = UserDataMansger.Instance.userData.skillEquip[j];
            LobyDataManager.Instance.reference1[j].b_Collection
                = UserDataMansger.Instance.userData.skillCollection[j];


            LobyDataManager.Instance.reference1[j].ability.text 
                = Read[j]["stat"].ToString()+"\n"+ ValueName;
            LobyDataManager.Instance.reference1[j].ability2.text 
                ="+ "+ Read[j]["statValue"].ToString() + "\n" + ValueNum;

        }

        UI_TextSetting();
        Load_Seteffect();
        CollectionPanelOnoff();
        yield return forSecond;
    }

        
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //UI_TextSetting();
            CollectionPanelOnoff();
            Seteffect();
        }
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



    public void CollectionPanelOnoff()
    {
        #region 수집후 수집한 데이터 저장   
        for (int i = 0; i < LobyDataManager.Instance.reference1.Length; i++)
        {
            if (LobyDataManager.Instance.reference1[i].b_Collection) { continue; }

            LobyDataManager.Instance.reference1[i].b_Collection =
            UserDataMansger.Instance.userData.skillCollection[i];
        }
        #endregion

        #region 수집했을때 텍스트변화

        for (int i = 0; i < UserDataMansger.Instance.userData.skillCollection.Length; i++)
        {
            if (!UserDataMansger.Instance.userData.skillCollection[i])
            {
               
                LobyDataManager.Instance.reference1[i].Name.text = "???";
                LobyDataManager.Instance.reference1[i].ability.text = "???";
                LobyDataManager.Instance.reference1[i].ability2.text = "???";
                LobyDataManager.Instance.reference1[i].GoldButton.interactable = false;
                LobyDataManager.Instance.reference1[i].GoldButtonText.text = "";
            }
            else
            {
                LobyDataManager.Instance.reference1[i].Name.text =
                LobyDataManager.Instance.reference1[i].equipRef.Name;
                LobyDataManager.Instance.reference1[i].ability.text =
                LobyDataManager.Instance.reference1[i].equipRef.ability;
                LobyDataManager.Instance.reference1[i].ability2.text =
                LobyDataManager.Instance.reference1[i].equipRef.ability2;
                LobyDataManager.Instance.reference1[i].GoldButton.interactable = true;
                LobyDataManager.Instance.reference1[i].Buttonimage.color = Color.green;

                
                

                if (!UserDataMansger.Instance.userData.skillEquip[i])
                    LobyDataManager.Instance.reference1[i].GoldButtonText.text = "장착";
                else
                {
                    LobyDataManager.Instance.reference1[i].GoldButtonText.text = "장착중";
                    LobyDataManager.Instance.reference1[i].Buttonimage.color = Color.red;
                }

            }
        }
        
        #endregion
    }


    public  void Seteffect()
    {
        for (int i = 0; i < LobyDataManager.Instance.SetText.Length; i++)
        {
            var texts = LobyDataManager.Instance.SetText[i];

            if (texts.SetInt == 3)
            {
                continue;
            }
            int setint = 0;
            for (int j = 0; j < texts.threeRef.Length; j++)
            {
                if (texts.threeRef[j].b_Collection)
                {
                    setint++;
                }
            }

            texts.SetInt = setint;

            EquipDataSet SetValue = new EquipDataSet();
            var EquipSetValueName = Read2[i]["세트효율"].ToString();
            SetValue.name = EquipSetValueName;
            var EquipSetValueData = int.Parse(Read2[i]["세트효율수치"].ToString());
            SetValue.Data = EquipSetValueData;

            texts.GetComponent<Text>().text = StringBillder(SetValue.name,
                                                "+" + SetValue.Data +
                                                "% 효율      " + setint + "/3");

            if (texts.SetInt >= 3)
            {
                //31개의 세트효과들 추가
            }
        }
    }


    public void Load_Seteffect()
    {
        for (int i = 0; i < LobyDataManager.Instance.SetText.Length; i++)
        {
            var texts = LobyDataManager.Instance.SetText[i];


            for (int j = 0; j < texts.threeRef.Length; j++)
            {
                if (texts.threeRef[j].b_Collection && texts.SetInt <= 3) texts.SetInt++;
            }

            EquipDataSet SetValue = new EquipDataSet();
            var EquipSetValueName = Read2[i]["세트효율"].ToString();
            SetValue.name = EquipSetValueName;
            var EquipSetValueData = int.Parse(Read2[i]["세트효율수치"].ToString());
            SetValue.Data = EquipSetValueData;

            texts.GetComponent<Text>().text = StringBillder(SetValue.name,
                                                "+" + SetValue.Data +
                                                "% 효율      " + texts.SetInt + "/3");

            if (texts.SetInt >= 3)
            {
                //31개의 세트효과들 추가
            }
        }
    }




}