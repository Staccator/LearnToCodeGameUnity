﻿using UnityEngine;
using UnityEngine.EventSystems;

public class DirectionIndicatorScript : MonoBehaviour, IPointerClickHandler
{
    GameObject directionSelectionWindow;
    CanvasGroup canvasGroup;

    public Direction? SelectedDirection { get; set; } = null;

    // Start is called before the first frame update
    void Start()
    {
        directionSelectionWindow = GameObject.Find("DirectionSelectionWindow");
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hide()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    public void Show()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            RaycastManagerScript.SetFocus();

            var selectionPanel = Instantiate(directionSelectionWindow);
            selectionPanel.transform.SetParent(GameObject.Find("LevelCanvas").transform);
            selectionPanel.transform.localScale = new Vector3(1, 1, 1);
            selectionPanel.transform.position = transform.position;

            var selectionPanelScript = selectionPanel.GetComponent<DirectionSelectionWindowScript>();
            bool isInMoveDirection = IsParentMoveDirection();
            selectionPanelScript.Show(isInMoveDirection);
            selectionPanelScript.changedDirectionIndicator = gameObject;
        }
    }

    private bool IsParentMoveDirection()
    {
        return transform.parent.Find("ComparisonIndicator") == null;
    }
}
