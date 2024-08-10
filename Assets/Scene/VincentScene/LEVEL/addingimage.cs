using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class addingimage : MonoBehaviour
{
    public TMP_FontAsset customFontLevel;
    public TMP_FontAsset customFontPlayerName;

    public Sprite myImageSprite; // Assign this in the Inspector

    public Sprite imageTrophy; // Assign this in the Inspector
    public Button myButton; // Assign this in the Inspector
    public GameObject targetPanel; // Assign this in the Inspector

    void Start()
    {
        myButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // BACKGROUND IMAGE 

        // Create a new GameObject
        GameObject newImage = new GameObject("BackGround");


        // Set the parent to the target panel
        newImage.transform.SetParent(targetPanel.transform);

        // Add an Image component
        Image imageComponent = newImage.AddComponent<Image>();

        // Set the sprite
        imageComponent.sprite = myImageSprite;

        // Set the image type to Sliced
        imageComponent.type = Image.Type.Sliced;

        // Set the size of the image
        RectTransform rectTransform = newImage.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(1209.212f, 152); // Adjust size as needed
        //rectTransform.localPosition = new Vector3(0, 0, 0); // Adjust position as needed


        //======================================================================================//

        // LEVEL NUMBER 

        // Create a new GameObject for the text
        GameObject levelName = new GameObject("LevelText");

        // Set the parent to the new image
        levelName.transform.SetParent(newImage.transform);

        // Add a TextMeshProUGUI component
        TextMeshProUGUI textComponent = levelName.AddComponent<TextMeshProUGUI>();

        // Set the text
        textComponent.text = "6"; // Set your desired text

        // Set text properties (e.g., font size, color)
        textComponent.fontSize = 50; // Adjust font size as needed
        textComponent.color = Color.white; // Adjust color as needed
        textComponent.font = customFontLevel;

        // Set the size of the text
        RectTransform textRectTransform = levelName.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = new Vector2(108.9009f, 86.61948f); // Adjust size as needed
        textRectTransform.localPosition = new Vector3(-501.8617f, -8.016724f, 0); // Adjust position as needed

        //======================================================================================//

        // PLAYER NAME 

        // Create a new GameObject for the text
        GameObject playerName = new GameObject("PlayerText");

        // Set the parent to the new image
        playerName.transform.SetParent(newImage.transform);

        // Add a TextMeshProUGUI component
        TextMeshProUGUI playerNameComponent = playerName.AddComponent<TextMeshProUGUI>();

        // Set the text
        playerNameComponent.text = "KarlTite"; // Set your desired text

        // Set text properties (e.g., font size, color)
        playerNameComponent.fontSize = 50; // Adjust font size as needed
        playerNameComponent.color = Color.white; // Adjust color as needed
        playerNameComponent.font = customFontLevel;

        // Set the size of the text
        RectTransform playerNameRectTransform = playerName.GetComponent<RectTransform>();
        playerNameRectTransform.sizeDelta = new Vector2(557.9701f, 70.87867f); // Adjust size as needed
        playerNameRectTransform.localPosition = new Vector3(-159.8617f, -3.005615f, 0); // Adjust position as needed


        //======================================================================================//

        // SCROLL RECT
        GameObject scrollView = new GameObject("ScrollRect");

        // Set the parent to the new image
        scrollView.transform.SetParent(newImage.transform);

        ScrollRect scrollRect = scrollView.AddComponent<ScrollRect>();

        scrollRect.horizontal = true;

        scrollRect.vertical = false; 

        scrollRect.movementType = ScrollRect.MovementType.Elastic;

        scrollRect.elasticity = 0.1f;

        scrollRect.scrollSensitivity = 1;

        scrollRect.decelerationRate = 0.135f;

        scrollRect.scrollSensitivity = 1;


        // Set the size of the text
        RectTransform scrollViewRectTransform = scrollView.GetComponent<RectTransform>();
        scrollViewRectTransform.sizeDelta = new Vector2(267.7803f, 152); // Adjust size as needed
        scrollViewRectTransform.localPosition = new Vector3(459.6886f, 0.00051212f, 0); // Adjust position as needed




        //======================================================================================//

        // MASK OBJECT
        GameObject Mask2d = new GameObject("Mask");

        // Set the parent
        Mask2d.transform.SetParent(scrollView.transform);

        // Add an RextMask2d script to the Mask2d object
        RectMask2D rectMask2D = Mask2d.AddComponent<RectMask2D>();

        //viewPortImage

        // Set the size of the text
        RectTransform mask2dRectTransform = Mask2d.GetComponent<RectTransform>();
        mask2dRectTransform.sizeDelta = new Vector2(267.76f, 152); // Adjust size as needed
        mask2dRectTransform.localPosition = new Vector3(-0.010088f, -0.0005111694f, 0); // Adjust position as needed


        // CONTENT
        GameObject content = new GameObject("Content");
        content.transform.SetParent(Mask2d.transform);
        HorizontalLayoutGroup horizontalGroup = content.AddComponent<HorizontalLayoutGroup>();
        //ContentSizeFitter sizeFitter = content.AddComponent<ContentSizeFitter>();
       // sizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        //sizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        horizontalGroup.childAlignment = TextAnchor.MiddleCenter;

        horizontalGroup.childForceExpandHeight = false;
        horizontalGroup.childForceExpandWidth = false;

        scrollRect.content = content.GetComponent<RectTransform>();

        RectTransform contentRectTransform = content.GetComponent<RectTransform>();

        contentRectTransform.pivot = Vector2.zero;
        contentRectTransform.anchorMin = new Vector2(0, 0); // Set anchorMin to bottom-left corner
        contentRectTransform.anchorMax = new Vector2(1, 1); // Set anchorMax to top-right corner
        contentRectTransform.offsetMin = new Vector2(0, 0); // Set offsetMin to zero
        contentRectTransform.offsetMax = new Vector2(-0.0001220703f, 0); // Set offsetMax to zero

        //contentRectTransform.sizeDelta = new Vector2(-1900.687f, 0); // Adjust size as needed
        //mask2dRectTransform.localPosition = new Vector3(-0.010088f, -0.0005111694f, 0); // Adjust position as needed

        //======================================================================================//

        // SCORE

        // Create a new GameObject for the text
        GameObject score = new GameObject("scoreText");

        // Set the parent to the new image
        score.transform.SetParent(content.transform);

        // Add a TextMeshProUGUI component
        TextMeshProUGUI textScoreComponent = score.AddComponent<TextMeshProUGUI>();

        // Set the text
        textScoreComponent.text = "1000"; // Set your desired text

        // Set text properties (e.g., font size, color)
        textScoreComponent.fontSize = 50; // Adjust font size as needed
        textScoreComponent.color = Color.white; // Adjust color as needed
        textScoreComponent.font = customFontLevel;

        // Set the size of the text
        RectTransform textScoreRectTransform = score.GetComponent<RectTransform>();
        textScoreRectTransform.anchorMin = new Vector2(0, 0); // Set anchorMin to bottom-left corner
        textScoreRectTransform.anchorMax = new Vector2(1, 1); // Set anchorMax to top-right corner
        textScoreRectTransform.offsetMin = new Vector2(0, 0); // Set offsetMin to zero
        textScoreRectTransform.offsetMax = new Vector2(0, 0); // Set offsetMax to zero

        // ITEM IMAGE 
        GameObject itemImage = new GameObject("iconImage");
        itemImage.transform.SetParent(score.transform);
        Image itemImageComponent = itemImage.AddComponent<Image>();
        itemImageComponent.sprite = imageTrophy;
        itemImageComponent.type = Image.Type.Sliced;
        RectTransform itemImageRectTransform = itemImage.GetComponent<RectTransform>();
        itemImageRectTransform.sizeDelta = new Vector2(60, 49); // Adjust size as needed
        itemImageRectTransform.localPosition = new Vector3(96.45056f, 0, 0);
        itemImageRectTransform.localScale = Vector3.one;


    }
}
