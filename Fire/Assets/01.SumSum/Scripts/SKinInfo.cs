using UnityEngine;
using UnityEngine.UI;

public class SKinInfo : MonoBehaviour
{
    private SkinSystem skinSystem;

    private SpriteData spriteData;
    private UserDataManager userDataManager;
    public string name = "스킨1";

    public string description = "Hello World. \n I am a boy.";

    public int skinnedMeshIdx;

    [Header("이미지")]
    public Image infoImage;

    public Image selectButtonImage;

    [Header("스프라이트")]
    public Sprite skinSprite;

    [Header("텍스트")]
    public Text nameText;

    public Text descriptionText;

    [Header(" ")]
    public int skillIdx;

    public int skinIdx;
    public bool isHave = false;
    public Button selectButton;

    private void Awake()
    {
        userDataManager = FindObjectOfType<UserDataManager>();
        skinSystem = FindObjectOfType<SkinSystem>();
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

    private void OnEnable()
    {
        Refresh();
    }

    public void Select()
    {
        //selectButton.GetComponent<Button>().interactable = false;
        nameText.text = name;
        descriptionText.text = description;
        infoImage.sprite = skinSprite;
        skinSystem.selectedSkinIdx = skinIdx;
        skinSystem.Refresh();
    }

    public void Refresh()
    {
        if (!isHave)
        {
            //button.GetComponent<Text>().text = "Unknown";
            //nameText.text = "???";
            //descriptionText.text = "???";
            //equipButtonText.text = "???";
            selectButtonImage.sprite = skinSprite;
            selectButtonImage.color = new Color(0, 0, 0, 1);
            selectButton.GetComponent<Button>().interactable = false;
            //equipButton.GetComponent<Button>().interactable = false;
            //infoImage.sprite = spriteData.unknownIconSprite;
            //selectButtonImage.sprite = spriteData.unknownIconSprite;
            //equipButtonImage.sprite = spriteData.unknownIconSprite;
        }
        else
        {
            //nameText.text = name;
            //descriptionText.text = description;
            //equipButtonText.text = "착용하기";
            selectButtonImage.sprite = skinSprite;
            selectButtonImage.color = new Color(1, 1, 1, 1);

            selectButton.GetComponent<Button>().interactable = true;
            //equipButton.GetComponent<Button>().interactable = true;
            //infoImage.sprite = skinSprite;
            //equipButtonImage.sprite = spriteData.unequipSprite;
        }
    }
}