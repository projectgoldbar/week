using UnityEngine;
using UnityEngine.UI;

public class SKinInfo : MonoBehaviour
{
    private SpriteData spriteData;
    private UserDataManager userDataManager;
    public string name = "스킨1";
    public string description = "스킨1설명";
    public int skinnedMeshIdx;

    [Header("이미지")]
    public Image skinImage;

    public Image equipButtonImage;

    [Header("스프라이트")]
    public Sprite skinSprite;

    [Header("텍스트")]
    public Text nameText;

    public Text descriptionText;
    public Text buttonText;

    [Header(" ")]
    public int skillIdx;

    public bool isHave = false;
    public bool isEquiped = false;
    public Button button;

    private void Awake()
    {
        userDataManager = FindObjectOfType<UserDataManager>();
        spriteData = FindObjectOfType<SpriteData>();
        Refresh();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Refresh();
        }
    }

    public void Equip()
    {
        for (int i = 0; i < userDataManager.skinInfos.Length; i++)
        {
            userDataManager.skinInfos[i].isEquiped = false;
        }
        isEquiped = true;
        userDataManager.userData.equipedSkinIdx = skinnedMeshIdx;
        userDataManager.RefreshSkin();
    }

    public void Refresh()
    {
        if (!isHave)
        {
            //button.GetComponent<Text>().text = "Unknown";
            nameText.text = "???";
            descriptionText.text = "???";
            buttonText.text = "???";
            button.GetComponent<Button>().interactable = false;
            skinImage.sprite = spriteData.unknownIconSprite;
            equipButtonImage.sprite = spriteData.unequipSprite;
        }
        else
        {
            nameText.text = name;
            descriptionText.text = description;
            buttonText.text = "Equip";
            button.GetComponent<Button>().interactable = true;
            skinImage.sprite = skinSprite;
            equipButtonImage.sprite = spriteData.unequipSprite;
        }

        if (isEquiped)
        {
            button.GetComponent<Button>().interactable = false;
            equipButtonImage.sprite = spriteData.equipedSprite;
        }
    }
}