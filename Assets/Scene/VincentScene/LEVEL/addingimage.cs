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
        RectTransform ImagerectTransform = newImage.GetComponent<RectTransform>();
        ImagerectTransform.sizeDelta = new Vector2(1209.212f, 152); // Adjust size as needed

        SetAnchor(ImagerectTransform, AnchorPresets.StretchAll);

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

        textRectTransform.pivot = Vector2.zero;
        textRectTransform.sizeDelta = new Vector2(34.64f, 50); // Adjust size as needed
        textRectTransform.localPosition = new Vector2(47, -17); // Adjust position as needed

        SetAnchor(textRectTransform, AnchorPresets.MiddleLeft);

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
        playerNameComponent.font = customFontPlayerName;


        // Set the size of the text
        RectTransform playerNameRectTransform = playerName.GetComponent<RectTransform>();

        playerNameRectTransform.pivot = Vector2.zero;
        playerNameRectTransform.localPosition = new Vector2(165, -17); // Adjust position as needed
        SetAnchor(playerNameRectTransform, AnchorPresets.MiddleLeft);

        //======================================================================================//

        // SCROLL RECT
        GameObject scrollView = new GameObject("ScrollView");

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
        scrollViewRectTransform.pivot = Vector2.zero;
        scrollViewRectTransform.sizeDelta = new Vector2(300, 152); // Adjust size as needed
        scrollViewRectTransform.localPosition = new Vector2(-300f, -76); // Adjust position as needed


        // Example of setting the anchor preset to "stretch (right)"
        SetAnchor(scrollViewRectTransform, AnchorPresets.MiddleRight);

        //======================================================================================//

        // VIEW PORT
        GameObject viewPort = new GameObject("ViewPort");

        // Set the parent to the new image
        viewPort.transform.SetParent(scrollRect.transform);

        CanvasRenderer canvasRenderer = viewPort.AddComponent<CanvasRenderer>();

        RectMask2D viewPortMask2d = viewPort.AddComponent<RectMask2D>();

        

        RectTransform viewPortRectTransform = viewPort.GetComponent<RectTransform>();
        SetAnchor(viewPortRectTransform, AnchorPresets.MiddleCenter);
        //viewPortRectTransform.transform.SetParent(scrollViewRectTransform.transform);
        viewPortRectTransform.pivot = new Vector2(0.5f, 0.5f);
        viewPortRectTransform.sizeDelta = new Vector2(300, 152); // Adjust size as needed
        viewPortRectTransform.localPosition = new Vector3(0, 0, 0); // Adjust position as needed
        // Example of setting the anchor preset to "stretch (right)"
       
        
    }
    public void SetAnchor(RectTransform rt, AnchorPresets allign)
    {
        if (rt == null)
        {
            Debug.LogError("RectTransform is null.");
            return;
        }

        switch (allign)
        {
            case AnchorPresets.TopLeft:
                rt.anchorMin = new Vector2(0, 1);
                rt.anchorMax = new Vector2(0, 1);
                break;
            case AnchorPresets.TopCenter:
                rt.anchorMin = new Vector2(0.5f, 1);
                rt.anchorMax = new Vector2(0.5f, 1);
                break;
            case AnchorPresets.TopRight:
                rt.anchorMin = new Vector2(1, 1);
                rt.anchorMax = new Vector2(1, 1);
                break;
            case AnchorPresets.MiddleLeft:
                rt.anchorMin = new Vector2(0, 0.5f);
                rt.anchorMax = new Vector2(0, 0.5f);
                break;
            case AnchorPresets.MiddleCenter:
                rt.anchorMin = new Vector2(0.5f, 0.5f);
                rt.anchorMax = new Vector2(0.5f, 0.5f);
                break;
            case AnchorPresets.MiddleRight:
                rt.anchorMin = new Vector2(1, 0.5f);
                rt.anchorMax = new Vector2(1, 0.5f);
                break;
            case AnchorPresets.BottomLeft:
                rt.anchorMin = new Vector2(0, 0);
                rt.anchorMax = new Vector2(0, 0);
                break;
            case AnchorPresets.BottomCenter:
                rt.anchorMin = new Vector2(0.5f, 0);
                rt.anchorMax = new Vector2(0.5f, 0);
                break;
            case AnchorPresets.BottomRight:
                rt.anchorMin = new Vector2(1, 0);
                rt.anchorMax = new Vector2(1, 0);
                break;
            case AnchorPresets.StretchAll:
                rt.anchorMin = new Vector2(0, 0);
                rt.anchorMax = new Vector2(1, 1);
                break;
            case AnchorPresets.StretchVertical:
                rt.anchorMin = new Vector2(0.5f, 0);
                rt.anchorMax = new Vector2(0.5f, 1);
                break;
            case AnchorPresets.StretchHorizontal:
                rt.anchorMin = new Vector2(0, 0.5f);
                rt.anchorMax = new Vector2(1, 0.5f);
                break;
            case AnchorPresets.StretchRight:
                rt.anchorMin = new Vector2(1, 0);
                rt.anchorMax = new Vector2(1, 1);
                break;
        }
    }

    public enum AnchorPresets
    {
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight,
        StretchAll,
        StretchVertical,
        StretchHorizontal,
        StretchRight
    }
}

































































        /*
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


    }*/

