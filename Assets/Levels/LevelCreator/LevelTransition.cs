using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelTransition : MonoBehaviour
{
    public static Events.TransitionEvent onLevelTransit;
    [Header("Insert LVL ID of the level you want to transition TO")]
    public int nextLevel;
    public float transitionDuration = 1.5f;

    [Header("If going from Island => new level")]
    public Transform directionOfLaunch;
    public float launchForce = 100;

    public void LoadScene()
    {
        PlayerPrefs.SetInt("TransitionLevelID",nextLevel);
        var scene = SceneManager.GetActiveScene();
        
        if (FindObjectOfType<Level>().levelType == LevelType.Island)
        {
            StartCoroutine(WaitBeforeLoading(scene));
            //Island => Jetpack/Island
            
            var player = FindObjectOfType<ThirdPersonController>().gameObject;
            var launchDir = directionOfLaunch.forward;
            var cineCam = FindObjectOfType<CinemachineVirtualCamera>();
            cineCam.LookAt = player.GetComponentInChildren<Rigidbody>().transform;

            PlayerPrefs.SetInt("LandJetpack", 1);
            Invoke(nameof(LaunchPlayer),0.2f);
            
        }
        else
        {
            //Jetpack => jetpack/Island
            SceneManager.LoadScene(scene.buildIndex);
        }

        
    }

    private void LaunchPlayer()
    {
        var player = FindObjectOfType<ThirdPersonController>();
        player.ToggleRagdoll(true);
        
        foreach (var playerRagdollBody in player.ragdollBodies)
        {
            playerRagdollBody.AddForce(directionOfLaunch.forward * launchForce);
        }
    }

    private IEnumerator WaitBeforeLoading(Scene sceneToLoad)
    {
        yield return new WaitForSeconds(transitionDuration);
        SceneManager.LoadScene(sceneToLoad.buildIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadScene();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(directionOfLaunch.position, directionOfLaunch.forward);
    }
}
