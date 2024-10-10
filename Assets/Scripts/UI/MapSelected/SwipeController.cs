using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] private int maxPage;
    [SerializeField] private Vector3 pageStep;
    [SerializeField] private RectTransform levelPagesRect;
    [SerializeField] private float tweenTime;
    [SerializeField] private LeanTweenType tweenType;
    [SerializeField] private Image[] barImage;
    [SerializeField] private Button previouusBtn, nextBtn;

    private int currentPage;
    private Vector3 targetPos;
    private float dragThreshould;

    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
        dragThreshould = Screen.width / 15;
        UpdateBar();
        UpdateArrowBtton();
    }

    public void Next()
    {
       if(currentPage < maxPage)
       {
            currentPage++;
            targetPos += pageStep;
            MovePage();
       }
    }

    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }

    private void MovePage()
    {
         levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
         UpdateBar();
         UpdateArrowBtton();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshould)
        {
            if (eventData.position.x > eventData.pressPosition.x) Previous();
            else Next();
        }
        else
        {
            MovePage();
        }
    }

    private void UpdateBar()
    {
        foreach (var item in barImage)
        {
            item.color = Color.white;
        }

        barImage[currentPage - 1].color = Color.red;
    }

    private void UpdateArrowBtton()
    {
        nextBtn.interactable = true;
        previouusBtn.interactable = true;
        if (currentPage == 1) previouusBtn.interactable = false;
        else if (currentPage == maxPage) nextBtn.interactable = false; 
    }
}
