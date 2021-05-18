using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] Text npcText;
    [SerializeField] Text npcName;
    [SerializeField] Image npcTextBackground;
    [SerializeField] Image npcImage;
    GameObject[] healthbar;
    Slider[] fuelBars;
    private void Awake()
    {       
        UIManager.onNPCDialogue += PrintNPCText;
        UIManager.onPlayerHealthChange += UpdateHealthText;
        UIManager.onFuelUse += UpdateFuelText;
        UIManager.onJetpackAwake += SetupFuel;
        UIManager.onPlayerLeavingConversation += ClearConversation;
        GetHealthBar();
        GetFuelBar();
        ClearScreen();
    }

    void GetFuelBar()
    {
        foreach(Transform child in transform)
        {
            if(child.name == "Fuel")
            {
                fuelBars = new Slider[child.childCount];
                int index = 0;
                foreach(Transform grandchild in child)
                {                   
                    fuelBars[index] = grandchild.GetComponent<Slider>();
                    index++;
                }
            }
        }
    }

    void GetHealthBar()
    {
        foreach(Transform child in transform)
        {
            if(child.name == "Health")
            {
                healthbar = new GameObject[child.childCount];
                int index = 0;
                foreach(Transform grandchild in child)
                {                   
                    healthbar[index] = grandchild.gameObject;
                    index++;
                }
            }
        }
    }

    private void ClearScreen()
    {
        npcImage.gameObject.SetActive(false);
        npcTextBackground.gameObject.SetActive(false);
        npcName.text = string.Empty;
        npcText.text = string.Empty;
    }

    private void Update() { 
        if(Gamemanager.instance.UnlockedItems.jetpack)
        {
            fuelBars[0].transform.parent.gameObject.SetActive(true);
        }
        else
        {
            fuelBars[0].transform.parent.gameObject.SetActive(false);
        }
    }

    void PrintNPCText(string dialogue, string name, Sprite npcSprite)
    {
        npcTextBackground.gameObject.SetActive(true);
        npcImage.gameObject.SetActive(true);
        npcImage.sprite = npcSprite;
        npcText.text = dialogue;
        npcName.text = name;
    }

    public void UpdateHealthText(int amount)
    {
        for(int i = 0; i < healthbar.Length; i++)
        {
            if(i < amount) healthbar[i].gameObject.SetActive(true);
            else healthbar[i].gameObject.SetActive(false);
        }
    }

    void ClearConversation()
    {
        npcText.text = string.Empty;
        npcName.text = string.Empty;
        npcTextBackground.gameObject.SetActive(false);
        npcImage.gameObject.SetActive(false);
    }
    
    void UpdateFuelText(float amount)
    {
        for(int i = 0; i < fuelBars.Length; i++)
        {
            if(i < amount)
            {
                fuelBars[i].value = fuelBars[i].maxValue;
            }
            else
            {
                fuelBars[i].value = fuelBars[i].minValue;
            }
        }
    }

    void SetupFuel(float amount)
    {
        foreach(Slider fuelbar in fuelBars)
        {
            if(amount <= 0)
            {
                fuelbar.gameObject.SetActive(false);
            }
            amount--;
        }
    }
    
    private void OnDestroy() {
        UIManager.onNPCDialogue -= PrintNPCText;
        UIManager.onPlayerHealthChange -= UpdateHealthText;
        UIManager.onFuelUse -= UpdateFuelText;
        UIManager.onJetpackAwake -= SetupFuel;
        UIManager.onPlayerLeavingConversation -= ClearConversation;
    }
}
