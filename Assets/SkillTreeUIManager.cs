using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public class SkillTreeUIManager : MonoBehaviour
{
    public TextMeshProUGUI skilltreePointsText;

    private void Update()
    {
        skilltreePointsText.text = "Points: " + Inventory.instance.skillTreePoints;
    }
}
