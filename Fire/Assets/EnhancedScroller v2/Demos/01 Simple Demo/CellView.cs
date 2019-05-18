using EnhancedUI.EnhancedScroller;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

namespace EnhancedScrollerDemos.SuperSimpleDemo
{
    public class CellView : EnhancedScrollerCellView
    {
        public Text Name = null;
        public Text ability = null;
        public Text ability2 = null;
        public Button GoldButton = null;
        public UnityEvent Evnt;
        public Text GoldButtonText = null;
        public List<Dictionary<string, object>> goldRead = new List<Dictionary<string, object>>();

        public void SetData(Data data)
        {
            Name.text = data.Name;
            ability.text = data.Ability;
            ability2.text = "";
            GoldButton.onClick.AddListener(ButtonOnClick);
            GoldButtonText.text = "0";
        }

        private void Start()
        {
            goldRead = CSVReader.Read("ShopGoldData");
        }

        public void ButtonOnClick()
        {
            Evnt?.Invoke();
        }


        //저장할 고정치
        [System.NonSerialized]
        public int cnt = 0;

        public void DataUpdate()
        {
            var ArrNumber = int.Parse(transform.name);

            if (goldRead.Count - 1 == cnt)
            {
                GoldButtonText.text = "Max";
                ability2.text = "+" + goldRead[cnt][ArrNumber + "번능력치"].ToString();
                return;
            }

            if (goldRead.Count > cnt)
            {
                GoldButtonText.text = goldRead[cnt][ArrNumber + "번금액"].ToString();
                ability2.text = "+" + goldRead[cnt][ArrNumber + "번능력치"].ToString();
                cnt++;
            }
        }
    }
}