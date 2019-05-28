using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace EnhancedScrollerDemos.SuperSimpleDemo
{
    /// <summary>
    /// Set up our demo script as a delegate for the scroller by inheriting from the IEnhancedScrollerDelegate interface
    ///
    /// EnhancedScroller delegates will handle telling the scroller:
    ///  - How many cells it should allocate room for (GetNumberOfCells)
    ///  - What each cell size is (GetCellSize)
    ///  - What the cell at a given index should be (GetCell)
    /// </summary>
    public class SimpleDemo : MonoBehaviour, IEnhancedScrollerDelegate
    {
        /// <summary>
        /// Internal representation of our data. Note that the scroller will never see
        /// this, so it separates the data from the layout using MVC principles.
        /// </summary>
        private SmallList<Data> _data;

        /// <summary>
        /// This is our scroller we will be a delegate for
        /// </summary>
        public EnhancedScroller scroller;

        /// <summary>
        /// This will be the prefab of each cell in our scroller. Note that you can use more
        /// than one kind of cell, but this example just has the one type.
        /// </summary>
        public EnhancedScrollerCellView cellViewPrefab;
        private List<Dictionary<string, object>> UpgradeRead = new List<Dictionary<string, object>>();
        private List<Dictionary<string, object>> EquipRead = new List<Dictionary<string, object>>();

        private void Awake()
        {
            // tell the scroller that this script will be its delegate
            scroller.Delegate = this;

            LoadUpgradeData();
            UpgradeRead = CSVReader.Read("UpgradeTextData");
            EquipRead = CSVReader.Read("EquipTextData");


        }


        private void LoadUpgradeData()
        {
            // set up some simple data
            _data = new SmallList<Data>();

            for (int i = 0; i < UpgradeRead.Count; i++)
            {
                _data.Add(new Data()
                {
                    Name = UpgradeRead[i]["이름Text"].ToString(),
                    Ability = UpgradeRead[i]["적용능력Text"].ToString(),
                });
            }

            // tell the scroller to reload now that we have the data
            scroller.ReloadData();
        }

        private void LoadEquipData()
        {
            _data = new SmallList<Data>();

            for (int i = 0; i < EquipRead.Count; i++)
            {
                StringBuilder strBldr = new StringBuilder(EquipRead[i]["적용능력Text"].ToString());
                strBldr.Append("\n" + EquipRead[i]["적용능력Text2"] + "\n" + EquipRead[i]["적용능력Text3"]);

                _data.Add(new Data()
                {
                    Name = EquipRead[i]["이름Text"].ToString(),
                    Ability = strBldr.ToString(),
                    //Read[i]["적용능력Text"].ToString(),
                });
            }

          //  scroller.ReloadData();
        }

        #region UI Handlers

        public void LoadUpgradeDataButton_OnClick()
        {
            LoadUpgradeData();
        }

        public void LoadEquipDataButton_OnClick()
        {
            LoadEquipData();
        }

        #endregion UI Handlers

        #region EnhancedScroller Handlers

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _data.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            //return (dataIndex % 2 == 0 ? 80f : 70f);
            return 100f;//
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            CellView cellView = scroller.GetCellView(cellViewPrefab) as CellView;

            cellView.name = dataIndex.ToString();

            cellView.SetData(_data[dataIndex]);

            return cellView;
        }

        #endregion EnhancedScroller Handlers
    }
}