using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : Singleton<PlayerManager>
{   
    [HideInInspector]
    public int playerHealth;
    
    public List<string> chosenCharacterMeshesNames;

    [Header("Player health settings")]
    [Range(1, 10)]public int playerMaxHealth;

    [Header("Player fuel Settings")]
    [Tooltip("time until fuel recharges")] public float fuelRechargeTime = 1;
    [Tooltip("fuel used when doing stuff that uses fuel")] public int fuelUsage = 1; 
    [Tooltip("How fast fuel recharges")] public int fuelRechargePerTick = 1;
    [Range(1, 10)] public int maxFuel = 10;

    public GameObject chosenCharacterPrefab;
    public RuntimeAnimatorController playerAnimator;
    public RuntimeAnimatorController cutsceneAnimator;

    public float mouseSensitivityMultiplier;
    public bool invertMouse;
    public float horizontalSensitivity;
    public float verticalSensitivity;
    

    GameObject player;
    //settings
    [HideInInspector]
    public float mouseSensitivityMultiplier;
    [HideInInspector]
    public float verticalSensitivity;
    [HideInInspector]
    public float horizontalSensitivity;
    [HideInInspector]
    public bool invertMouse;

    protected override void Awake() {
        base.Awake();
        playerHealth = playerMaxHealth;
        chosenCharacterMeshesNames = new List<string>();
    }

    public void ResetPlayerHealth()
    {
        playerHealth = playerMaxHealth;
    }

    public void PlayerAwake()
    {
        if (chosenCharacterPrefab == null) return;
        var players = FindObjectsOfType<PlayerHealth>();
        foreach (var _player in players)
        {
            foreach (Transform child in _player.transform)
            {
                if (child.name == "MeshBase")
                {
                    foreach (Transform grandchild in child)
                    {

                        if (grandchild.name == "Root") continue;
                        grandchild.gameObject.SetActive(false);
                        if(grandchild.name == chosenCharacterPrefab.name)
                        {
                            grandchild.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }

    }

    public void PutOnModel()
    {
        var players = FindObjectsOfType<PlayerHealth>();
        foreach (var player in players)
        {


            var currPlayerModel = player.transform.Find("MeshBase");
            var spawnedModel = Instantiate(chosenCharacterPrefab, player.transform.position, Quaternion.identity, player.transform);
            var isPlayer = player.CompareTag("Player");
            var animatorToUse = playerAnimator;
            if (isPlayer)
            {
                animatorToUse = playerAnimator;
            }
            else
            {
                animatorToUse = cutsceneAnimator;
            }


            Destroy(currPlayerModel.gameObject);

            spawnedModel.GetComponent<Animator>().runtimeAnimatorController = animatorToUse;

            if (isPlayer)
            {

                //Doesn't work for some reason
                var jetpackParent = GameObject.FindGameObjectWithTag("JetParent").transform;
                var pewpewParent = GameObject.FindGameObjectWithTag("PewParent").transform;

                var jetpackObject = FindObjectOfType<JetpackBase>().transform;
                var pewpewObject = FindObjectOfType<Pewpew>().transform;


                jetpackObject.parent = jetpackParent;
                pewpewObject.parent = pewpewParent;
                


                spawnedModel.transform.position = FindObjectOfType<SpawnPoint>().transform.localPosition;
                spawnedModel.transform.SetParent(player.transform);
            }
            else
            {
                spawnedModel.transform.SetParent(GameObject.Find("CutscenePlayer").transform);
                spawnedModel.transform.localPosition = new Vector3(0, 0, 0);
            }
        }

    }
}
