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
        if (FindObjectOfType<Level>().levelType == LevelType.Island)
        {
            
            //Island => Jetpack/Island
            
            var player = FindObjectOfType<ThirdPersonController>().gameObject;
            var launchDir = directionOfLaunch.forward;
            var cineCam = FindObjectOfType<CinemachineVirtualCamera>();
            cineCam.LookAt = player.GetComponentInChildren<Rigidbody>().transform;

            foreach (var scripts in player.GetComponentsInChildren<MonoBehaviour>())
            {
                scripts.enabled = false;
            }

            foreach (var animators in player.GetComponentsInChildren<Animator>())
            {
                animators.enabled = false;
            }

            foreach (var rigidbodies in player.GetComponentsInChildren<Rigidbody>())
            {
                rigidbodies.AddForce(launchDir * launchForce);
            }

            foreach (var colliders in player.GetComponentsInChildren<Collider>())
            {
                colliders.enabled = false;
            }
        }
        else
        {
            //Jetpack => jetpack/Island
        }

        PlayerPrefs.SetInt("TransitionLevelID",nextLevel);
        var scene = SceneManager.GetActiveScene();
        StartCoroutine(WaitBeforeLoading(scene));
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
