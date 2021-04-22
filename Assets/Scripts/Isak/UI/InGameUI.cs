using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{

    [SerializeField] Text health;
    [SerializeField] Text npcText;
    [SerializeField] Text npcName;
    [SerializeField] Text fuelText;
    [SerializeField] float textExistTimer = 1;
    // Start is called before the first frame update
    private void Awake()
    {
        ClearScreen();
    }

    private void ClearScreen()
    {
        npcName.text = string.Empty;
        npcText.text = string.Empty;
        health.text = string.Empty;
        fuelText.text = string.Empty;
    }

    void PrintNPCText(string dialogue, string name)
    {
        StopAllCoroutines();
        npcText.text = dialogue;
        npcName.text = name;
        StartCoroutine(RemoveText());
    }

    void UpdateHealthText(int amount)
    {
        health.text = amount.ToString();
    }

    IEnumerator RemoveText()
    {
        yield return new WaitForSeconds(textExistTimer);
        npcText.text = string.Empty;
        npcName.text = string.Empty;
    }
    // Update is called once per frame
    void UpdateFuelText(float amount)
    {
        fuelText.text = amount.ToString();
    }
    
    private void OnEnable() {
        UIManager.onNPCDialogue += PrintNPCText;
        UIManager.onPlayerHealthChange += UpdateHealthText;
        UIManager.onFuelUse += UpdateFuelText;
    }

    private void OnDisable() {
        UIManager.onNPCDialogue -= PrintNPCText;
        UIManager.onPlayerHealthChange -= UpdateHealthText;
        UIManager.onFuelUse -= UpdateFuelText;
    }
}