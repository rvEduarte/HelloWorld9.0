using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class addingimage : MonoBehaviour
{
    #region DYNAMIC LEADERBOARD Variables
    public TMP_FontAsset customFontLevel;
    public TMP_FontAsset customFontPlayerName;

    public Sprite myImageSprite; // Assign this in the Inspector
    public Sprite imageTrophy; // Assign this in the Inspector
    public Sprite imageTimer;
    public Sprite imageExercise;
    public Sprite imageQuiz;
    public Sprite imageButton;
    public Sprite imageFirstMedal;
    public Sprite imageSecondMedal;
    public Sprite imageThirdMedal;

    public Button myButton; // Assign this in the Inspector
    public GameObject targetPanel; // Assign this in the Inspector

    GameObject newImage;
    GameObject levelName;
    GameObject playerName;
    GameObject scrollView;  
    GameObject viewPort;
    GameObject content;
    GameObject scoreText;
    GameObject imageTrophyObject;
    GameObject textTimer;
    GameObject imageTimerObject;
    GameObject textExercise;
    GameObject imageExerciseObject;
    GameObject textQuiz;
    GameObject imageQuizObject;
    GameObject imageButtonObject;

    ScrollRect scrollRect;

    #endregion

    int count;

    void Start()
    {
        count = 0;
        // Add listener to the button
        myButton.onClick.AddListener(OnButtonClick);
    }

    public void BackGroundImage()
    {
        // BACKGROUND IMAGE 

        // Create a new GameObject
        newImage = new GameObject("BackGround");

        newImage.layer = LayerMask.NameToLayer("UI");
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
    }
    public void LevelFirstMedal()
    {
        // LEVEL MEDAL
        levelName = new GameObject("LevelTextImage");

        levelName.layer = LayerMask.NameToLayer("UI");

        // Set the parent to the new image
        levelName.transform.SetParent(newImage.transform);

        // Add an Image component
        Image imageComponent = levelName.AddComponent<Image>();

        // Set the sprite
        imageComponent.sprite = imageFirstMedal;

        // Set the size of the image
        RectTransform ImagerectTransform = levelName.GetComponent<RectTransform>();

        ImagerectTransform.pivot = Vector2.zero;
        ImagerectTransform.sizeDelta = new Vector2(124, 108); // Adjust size as needed

        ImagerectTransform.anchoredPosition = new Vector2(0.9000244f, -52.3f);

        SetAnchor(ImagerectTransform, AnchorPresets.MiddleLeft);
    }

    public void LevelSecondMedal()
    {
        // LEVEL MEDAL
        levelName = new GameObject("LevelTextImage");

        levelName.layer = LayerMask.NameToLayer("UI");

        // Set the parent to the new image
        levelName.transform.SetParent(newImage.transform);

        // Add an Image component
        Image imageComponent = levelName.AddComponent<Image>();

        // Set the sprite
        imageComponent.sprite = imageSecondMedal;

        // Set the size of the image
        RectTransform ImagerectTransform = levelName.GetComponent<RectTransform>();

        ImagerectTransform.pivot = Vector2.zero;
        ImagerectTransform.sizeDelta = new Vector2(111, 108); // Adjust size as needed

        ImagerectTransform.anchoredPosition = new Vector2(7, -52.3f);

        SetAnchor(ImagerectTransform, AnchorPresets.MiddleLeft);
    }

    public void LevelThirdMedal()
    {
        // LEVEL MEDAL
        levelName = new GameObject("LevelTextImage");

        levelName.layer = LayerMask.NameToLayer("UI");

        // Set the parent to the new image
        levelName.transform.SetParent(newImage.transform);

        // Add an Image component
        Image imageComponent = levelName.AddComponent<Image>();

        // Set the sprite
        imageComponent.sprite = imageThirdMedal;

        // Set the size of the image
        RectTransform ImagerectTransform = levelName.GetComponent<RectTransform>();

        ImagerectTransform.pivot = Vector2.zero;
        ImagerectTransform.sizeDelta = new Vector2(82, 103); // Adjust size as needed

        ImagerectTransform.anchoredPosition = new Vector2(22, -52.3f);

        SetAnchor(ImagerectTransform, AnchorPresets.MiddleLeft);
    }
    public void LevelNumber()
    {
        // LEVEL NUMBER 

        // Create a new GameObject for the text
        levelName = new GameObject("LevelText");

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
    }
    public void PlayerName()
    {
        // PLAYER NAME 

        // Create a new GameObject for the text
        playerName = new GameObject("PlayerText");

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
    }
    public void ScrollRectTransform()
    {
        // SCROLL RECT
        scrollView = new GameObject("ScrollView");

        scrollView.layer = LayerMask.NameToLayer("UI");
        // Set the parent to the new image
        scrollView.transform.SetParent(newImage.transform);

        scrollRect = scrollView.AddComponent<ScrollRect>();

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
        scrollViewRectTransform.sizeDelta = new Vector2(282.0381f, 152); // Adjust size as needed
        scrollViewRectTransform.anchoredPosition = new Vector2(-282.0381f, -76); // Adjust position as needed


        // Example of setting the anchor preset to "stretch (right)"
        SetAnchor(scrollViewRectTransform, AnchorPresets.MiddleRight);

        // BUTTON IMAGE

        // Create a new GameObject
        imageButtonObject = new GameObject("icon_Exercise");

        imageButtonObject.layer = LayerMask.NameToLayer("UI");
        // Set the parent to the target panel
        imageButtonObject.transform.SetParent(scrollView.transform);

        // Add an Image component
        Image imageComponent = imageButtonObject.AddComponent<Image>();

        // Set the sprite
        imageComponent.sprite = imageButton;

        // Set the image type to Sliced
        imageComponent.type = Image.Type.Sliced;

        // Set image raycast
        imageComponent.raycastPadding = new Vector4(-40, -40, -40, -40);

        // Set the size of the image
        RectTransform ImagerectTransform = imageButtonObject.GetComponent<RectTransform>();

        ImagerectTransform.pivot = Vector2.zero;
        ImagerectTransform.sizeDelta = new Vector2(32.7591f, 49.7356f); // Adjust size as needed

        ImagerectTransform.anchoredPosition = new Vector2(-32.759f, -103.4f); ;

        SetAnchor(ImagerectTransform, AnchorPresets.TopLeft);

        //add button
        Button toggleButton = imageButtonObject.AddComponent<Button>();

        PanelController panelController = imageButtonObject.AddComponent<PanelController>();

        panelController.SetScrollViewRectTransform(scrollViewRectTransform);

        toggleButton.onClick.AddListener(() => panelController.TogglePanel());



    }
    public void ViewPortObject()
    {
        // VIEW PORT
        viewPort = new GameObject("ViewPort");

        viewPort.layer = LayerMask.NameToLayer("UI");
        // Set the parent 
        viewPort.transform.SetParent(scrollView.transform);

        CanvasRenderer canvasRenderer = viewPort.AddComponent<CanvasRenderer>();

        RectMask2D viewPortMask2d = viewPort.AddComponent<RectMask2D>();



        RectTransform viewPortRectTransform = viewPort.GetComponent<RectTransform>();
        SetAnchor(viewPortRectTransform, AnchorPresets.StretchAll);

        // Set the pivot to the center
        viewPortRectTransform.pivot = new Vector2(0.5f, 0.5f);
        viewPortRectTransform.offsetMin = Vector3.zero;
        viewPortRectTransform.offsetMax = Vector3.zero;
    }
    public void ContentObject()
    {
        // CONTENT

        content = new GameObject("Content");

        content.layer = LayerMask.NameToLayer("UI");
        // Set the parent 
        content.transform.SetParent(viewPort.transform);

        HorizontalLayoutGroup contentLayoutGroup = content.AddComponent<HorizontalLayoutGroup>();
        contentLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
        contentLayoutGroup.childForceExpandHeight = true;
        contentLayoutGroup.childForceExpandWidth = true;
        contentLayoutGroup.childControlHeight = false;
        contentLayoutGroup.childControlWidth = false;

        contentLayoutGroup.spacing = 80;

        ContentSizeFitter contentSizeFitter = content.AddComponent<ContentSizeFitter>();
        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;

        RectTransform contentRectTransform = content.GetComponent<RectTransform>();
        // Set the pivot to the center
        contentRectTransform.pivot = new Vector2(0, 0.5f);

        // Set the sizeDelta to manage its height, width will be controlled by the ContentSizeFitter
        contentRectTransform.sizeDelta = new Vector2(0, 160); // Adjust the height as needed

        // Set the anchoredPosition to ensure X remains at 0
        contentRectTransform.anchoredPosition = new Vector2(0, 0);

        SetAnchor(contentRectTransform, AnchorPresets.StretchHorizontal);

        scrollRect.content = content.GetComponent<RectTransform>();
        scrollRect.viewport = viewPort.GetComponent<RectTransform>();

    }
    public void TextScoreImage()
    {

        // SCORE TEXT 

        // Create a new GameObject for the text
        scoreText = new GameObject("scoreText");

        // Set the parent to the new image
        scoreText.transform.SetParent(content.transform);

        // Add a TextMeshProUGUI component
        TextMeshProUGUI scoreTextComponent = scoreText.AddComponent<TextMeshProUGUI>();

        // Set the text
        scoreTextComponent.text = "1000"; // Set your desired text

        // Set text properties (e.g., font size, color)
        scoreTextComponent.fontSize = 50; // Adjust font size as needed
        scoreTextComponent.color = Color.white; // Adjust color as needed
        scoreTextComponent.font = customFontLevel;

        scoreTextComponent.alignment = TextAlignmentOptions.Center;
        //textComponent.alignment = TextAlignmentOptions.Midline;

        // Set the size of the text
        RectTransform scoreTextRectTransform = scoreText.GetComponent<RectTransform>();

        scoreTextRectTransform.pivot = Vector2.zero;
        scoreTextRectTransform.sizeDelta = new Vector2(194.119f, 164.4507f); // Adjust size as needed
        SetAnchor(scoreTextRectTransform, AnchorPresets.MiddleLeft);

        // TROPHY IMAGE 

        // Create a new GameObject
        imageTrophyObject = new GameObject("icon_Trophy");

        imageTrophyObject.layer = LayerMask.NameToLayer("UI");
        // Set the parent to the target panel
        imageTrophyObject.transform.SetParent(scoreText.transform);

        // Add an Image component
        Image imageComponent = imageTrophyObject.AddComponent<Image>();

        // Set the sprite
        imageComponent.sprite = imageTrophy;

        // Set the image type to Sliced
        imageComponent.type = Image.Type.Sliced;

        // Set the size of the image
        RectTransform ImagerectTransform = imageTrophyObject.GetComponent<RectTransform>();

        ImagerectTransform.pivot = Vector2.zero;
        ImagerectTransform.sizeDelta = new Vector2(61.7015f, 49.7804f); // Adjust size as needed

        ImagerectTransform.anchoredPosition = new Vector2(-18, -28); ;

        SetAnchor(ImagerectTransform, AnchorPresets.MiddleRight);

    }
    public void TextTimerImage(string name)
    {
        //TEXT EXERCISE

        // Create a new GameObject for the text
        textTimer = new GameObject("textTimer");

        // Set the parent to the new image
        textTimer.transform.SetParent(content.transform);

        // Add a TextMeshProUGUI component
        TextMeshProUGUI scoreTextComponent = textTimer.AddComponent<TextMeshProUGUI>();

        // Set the text
        scoreTextComponent.text = name; // Set your desired text

        // Set text properties (e.g., font size, color)
        scoreTextComponent.fontSize = 50; // Adjust font size as needed
        scoreTextComponent.color = Color.white; // Adjust color as needed
        scoreTextComponent.font = customFontLevel;

        scoreTextComponent.alignment = TextAlignmentOptions.Center;
        //textComponent.alignment = TextAlignmentOptions.Midline;

        // Set the size of the text
        RectTransform scoreTextRectTransform = textTimer.GetComponent<RectTransform>();

        scoreTextRectTransform.pivot = Vector2.zero;
        scoreTextRectTransform.sizeDelta = new Vector2(229.2188f, 164.4507f); // Adjust size as needed
        SetAnchor(scoreTextRectTransform, AnchorPresets.MiddleLeft);

        //TIMER IMAGE 

        // Create a new GameObject
        imageTimerObject = new GameObject("icon_Timer");

        imageTimerObject.layer = LayerMask.NameToLayer("UI");
        // Set the parent to the target panel
        imageTimerObject.transform.SetParent(textTimer.transform);

        // Add an Image component
        Image imageComponent = imageTimerObject.AddComponent<Image>();

        // Set the sprite
        imageComponent.sprite = imageTimer;

        // Set the image type to Sliced
        imageComponent.type = Image.Type.Sliced;

        // Set the size of the image
        RectTransform ImagerectTransform = imageTimerObject.GetComponent<RectTransform>();

        ImagerectTransform.pivot = Vector2.zero;
        ImagerectTransform.sizeDelta = new Vector2(67.2983f, 63.3395f); // Adjust size as needed

        ImagerectTransform.anchoredPosition = new Vector2(0, -34.1032f); ;

        SetAnchor(ImagerectTransform, AnchorPresets.MiddleRight);
    }
    public void TextExerciseImage(string name)
    {
        //TEXT EXERCISE

        // Create a new GameObject for the text
        textExercise = new GameObject("textExercise");

        // Set the parent to the new image
        textExercise.transform.SetParent(content.transform);

        // Add a TextMeshProUGUI component
        TextMeshProUGUI textComponent = textExercise.AddComponent<TextMeshProUGUI>();

        // Set the text
        textComponent.text = name; // Set your desired text

        // Set text properties (e.g., font size, color)
        textComponent.fontSize = 50; // Adjust font size as needed
        textComponent.color = Color.white; // Adjust color as needed
        textComponent.font = customFontLevel;

        textComponent.alignment = TextAlignmentOptions.MidlineRight;
        //textComponent.alignment = TextAlignmentOptions.Midline;

        // Set the size of the text
        RectTransform textRectTransform = textExercise.GetComponent<RectTransform>();

        textRectTransform.pivot = Vector2.zero;
        textRectTransform.sizeDelta = new Vector2(229.2188f, 164.4507f); // Adjust size as needed
        SetAnchor(textRectTransform, AnchorPresets.MiddleLeft);

        //Exercise IMAGE 

        // Create a new GameObject
        imageExerciseObject = new GameObject("icon_Exercise");

        imageExerciseObject.layer = LayerMask.NameToLayer("UI");
        // Set the parent to the target panel
        imageExerciseObject.transform.SetParent(textExercise.transform);

        // Add an Image component
        Image imageComponent = imageExerciseObject.AddComponent<Image>();

        // Set the sprite
        imageComponent.sprite = imageExercise;

        // Set the image type to Sliced
        imageComponent.type = Image.Type.Sliced;

        // Set the size of the image
        RectTransform ImagerectTransform = imageExerciseObject.GetComponent<RectTransform>();

        ImagerectTransform.pivot = Vector2.zero;
        ImagerectTransform.sizeDelta = new Vector2(67.2983f, 63.3395f); // Adjust size as needed

        ImagerectTransform.anchoredPosition = new Vector2(8.7612f, -34.1032f); ;

        SetAnchor(ImagerectTransform, AnchorPresets.MiddleRight);
    }

    public void TextQuizImage(string name)
    {
        //TEXT EXERCISE

        // Create a new GameObject for the text
        textQuiz = new GameObject("textQuiz");

        // Set the parent to the new image
        textQuiz.transform.SetParent(content.transform);

        // Add a TextMeshProUGUI component
        TextMeshProUGUI textComponent = textQuiz.AddComponent<TextMeshProUGUI>();

        // Set the text
        textComponent.text = name; // Set your desired text

        // Set text properties (e.g., font size, color)
        textComponent.fontSize = 50; // Adjust font size as needed
        textComponent.color = Color.white; // Adjust color as needed
        textComponent.font = customFontLevel;

        textComponent.alignment = TextAlignmentOptions.MidlineLeft;
        //textComponent.alignment = TextAlignmentOptions.Midline;

        // Set the size of the text
        RectTransform textRectTransform = textQuiz.GetComponent<RectTransform>();

        textRectTransform.pivot = Vector2.zero;
        textRectTransform.sizeDelta = new Vector2(330.5814f, 164.4507f); // Adjust size as needed
        SetAnchor(textRectTransform, AnchorPresets.MiddleLeft);

        //Exercise IMAGE 

        // Create a new GameObject
        imageQuizObject = new GameObject("icon_Quiz");

        imageQuizObject.layer = LayerMask.NameToLayer("UI");
        // Set the parent to the target panel
        imageQuizObject.transform.SetParent(textQuiz.transform);

        // Add an Image component
        Image imageComponent = imageQuizObject.AddComponent<Image>();

        // Set the sprite
        imageComponent.sprite = imageQuiz;

        // Set the image type to Sliced
        imageComponent.type = Image.Type.Sliced;

        // Set the size of the image
        RectTransform ImagerectTransform = imageQuizObject.GetComponent<RectTransform>();

        ImagerectTransform.pivot = Vector2.zero;
        ImagerectTransform.sizeDelta = new Vector2(67.2983f, 63.3395f); // Adjust size as needed

        ImagerectTransform.anchoredPosition = new Vector2(-94, -31.66975F);

        SetAnchor(ImagerectTransform, AnchorPresets.MiddleRight);
    }
    void OnButtonClick()
    {
        count++;
        if(count == 1)
        {
            // BACKGROUND IMAGE
            BackGroundImage();

            // LEVEL NUMBER 
            LevelFirstMedal();

            // PLAYER NAME 
            PlayerName();

            // SCROLL RECT
            ScrollRectTransform();

            // VIEW PORT
            ViewPortObject();

            // CONTENT
            ContentObject();

            // SCORE TEXT 
            TextScoreImage();

            // TIMER TEXT
            TextTimerImage("10:00 P1");

            TextTimerImage("05:00 P2");

            TextTimerImage("10:00 P3");

            // EXERCISE TEXT
            TextExerciseImage("90% P2");

            TextExerciseImage("100% P3");

            // QUIZ TEXT 4 SPACES
            TextQuizImage("    100% P3");
        }
        else if(count == 2)
        {
            // BACKGROUND IMAGE
            BackGroundImage();

            // LEVEL NUMBER 
            LevelSecondMedal();

            // PLAYER NAME 
            PlayerName();

            // SCROLL RECT
            ScrollRectTransform();

            // VIEW PORT
            ViewPortObject();

            // CONTENT
            ContentObject();

            // SCORE TEXT 
            TextScoreImage();

            // TIMER TEXT
            TextTimerImage("10:00 P1");

            TextTimerImage("05:00 P2");

            TextTimerImage("10:00 P3");

            // EXERCISE TEXT
            TextExerciseImage("90% P2");

            TextExerciseImage("100% P3");

            // QUIZ TEXT 4 SPACES
            TextQuizImage("    100% P3");
        }
        else if (count == 3)
        {
            // BACKGROUND IMAGE
            BackGroundImage();

            // LEVEL NUMBER 
            LevelThirdMedal();

            // PLAYER NAME 
            PlayerName();

            // SCROLL RECT
            ScrollRectTransform();

            // VIEW PORT
            ViewPortObject();

            // CONTENT
            ContentObject();

            // SCORE TEXT 
            TextScoreImage();

            // TIMER TEXT
            TextTimerImage("10:00 P1");

            TextTimerImage("05:00 P2");

            TextTimerImage("10:00 P3");

            // EXERCISE TEXT
            TextExerciseImage("90% P2");

            TextExerciseImage("100% P3");

            // QUIZ TEXT 4 SPACES
            TextQuizImage("    100% P3");
        }
        else
        {
            // BACKGROUND IMAGE
            BackGroundImage();

            // LEVEL NUMBER 
            LevelNumber();

            // PLAYER NAME 
            PlayerName();

            // SCROLL RECT
            ScrollRectTransform();

            // VIEW PORT
            ViewPortObject();

            // CONTENT
            ContentObject();

            // SCORE TEXT 
            TextScoreImage();

            // TIMER TEXT
            TextTimerImage("10:00 P1");

            TextTimerImage("05:00 P2");

            TextTimerImage("10:00 P3");

            // EXERCISE TEXT
            TextExerciseImage("90% P2");

            TextExerciseImage("100% P3");

            // QUIZ TEXT 4 SPACES
            TextQuizImage("    100% P3");
        }


        //=========================================================================//

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

