using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{

    [SerializeField] Text health;
    [SerializeField] Text npcText;
    [SerializeField] Text npcName;
    [SerializeField] Slider fuelBar;
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
        //fuelBar.text = string.Empty;
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
        fuelBar.value = amount;
    }

    void SetupFuelSlider(float maxValue)
    {
        fuelBar.maxValue = maxValue;
        fuelBar.value = maxValue;
    }

    void OnSceneLoaded() //fix later so it wont speak with gamemanager directly
    {
        switch(Gamemanager.instance.CurrentPlayerState)
        {
            case Gamemanager.PlayerStates.OnJetpack:
            print("jetpack");
            fuelBar.enabled = true;
            break;

            case Gamemanager.PlayerStates.Onland:
            print("onland");
            fuelBar.enabled = false;
            break;
        }
    }
    
    private void OnEnable() {
        UIManager.onNPCDialogue += PrintNPCText;
        UIManager.onPlayerHealthChange += UpdateHealthText;
        UIManager.onFuelUse += UpdateFuelText;
        UIManager.onJetpackAwake += SetupFuelSlider;
        UIManager.onSceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        UIManager.onNPCDialogue -= PrintNPCText;
        UIManager.onPlayerHealthChange -= UpdateHealthText;
        UIManager.onFuelUse -= UpdateFuelText;
        UIManager.onJetpackAwake -= SetupFuelSlider;
        UIManager.onSceneLoaded -= OnSceneLoaded;
    }
}
