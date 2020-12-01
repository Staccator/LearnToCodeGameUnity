﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Commands;
using Models;

public class ComparisonTypeSelectorScript : MonoBehaviour, IPointerClickHandler
{
    public ComparedType comparedType;

    private GameObject DirectionSelector;
    private ComparisonSelectorScript parentScript;

    // Start is called before the first frame update
    void Start()
    {
        DirectionSelector = transform.parent.gameObject;
        parentScript = DirectionSelector.GetComponent<ComparisonSelectorScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(gameObject.name);
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            parentScript.SetSelected(this.comparedType);
        }
    }
}