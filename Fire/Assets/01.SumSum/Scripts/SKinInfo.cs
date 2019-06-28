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
    public Image infoImage;

    public Image selectButtonImage;
    public Image equipButtonImage;

    [Header("스프라이트")]
    public Sprite skinSprite;

    [Header("텍스트")]
    public Text nameText;

    public Text descriptionText;
    public Text equipButtonText;

    [Header(" ")]
    public int skillIdx;

    public bool isHave = false;
    public bool isEquiped = false;
    public Button equipButton;
    public Button selectButton;

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
            equipButtonText.text = "???";
            selectButton.GetComponent<Button>().interactable = false;
            equipButton.GetComponent<Button>().interactable = false;
            infoImage.sprite = spriteData.unknownIconSprite;
            selectButtonImage.sprite = spriteData.unknownIconSprite;
            equipButtonImage.sprite = spriteData.unknownIconSprite;
        }
        else
        {
            nameText.text = name;
            descriptionText.text = description;
            equipButtonText.text = "착용하기";
            selectButton.GetComponent<Button>().interactable = true;
            equipButton.GetComponent<Button>().interactable = true;
            infoImage.sprite = skinSprite;
            equipButtonImage.sprite = spriteData.unequipSprite;
        }

        if (isEquiped)
        {
            equipButtonText.text = "착용중";
            selectButton.GetComponent<Button>().interactable = false;
            equipButton.GetComponent<Button>().interactable = false;
            equipButtonImage.sprite = spriteData.equipedSprite;
        }
    }
}