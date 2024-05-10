using System;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmActionDialog : MonoBehaviour
{
    [SerializeField] private Text mQuestion;
    [SerializeField] private Button mConfirmButton; 
    [SerializeField] private Button mCancelButton; 

    public string Question
    {
        get => mQuestion.text;
        set => mQuestion.text = value;
    }

    public Action OnConfirm;
    public Action OnCancel;

    private void Awake()
    {
        if (mConfirmButton != null)
        {
            mConfirmButton.onClick.AddListener(() => OnConfirm?.Invoke());
        }

        if (mCancelButton != null)
        {
            mCancelButton.onClick.AddListener(() => OnCancel?.Invoke());
        }
    }
}
