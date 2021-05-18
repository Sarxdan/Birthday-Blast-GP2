using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    private Inventory inventory;
    
    public override void OnInspectorGUI() {
        
        base.OnInspectorGUI();

        if (GUILayout.Button("Reset to base stats"))
            inventory.ApplyBaseStats();
        
        if(GUILayout.Button("Apply Upgrades"))
            inventory.ApplySkillTreeUpgrades();
    }

    private void OnEnable()
    {
        inventory = target as Inventory;
    }
}

