using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using LootLocker.Requests;

public class WhiteLabelLogin : MonoBehaviour
{
    // Input fields
    [Header("New User")]
    public TMP_InputField newUserEmailInputField;
    public TMP_InputField newUserPasswordInputField;
    public TMP_InputField nickNameInputField;

    [Header("Existing User")]
    public TMP_InputField existingUserEmailInputField;
    public TMP_InputField existingUserPasswordInputField;

    [Header("Reset password")]
    public TMP_InputField resetPasswordInputField;

    [Header("RememberMe")]
    // Components for enabling auto login
    public Toggle rememberMeToggle;
    public Animator rememberMeAnimator;
    private int rememberMe;

    [Header("Player name")]
    public TextMeshProUGUI playerNameText;
    public Animator playerNameTextAnimator;

}
