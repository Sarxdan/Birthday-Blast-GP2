using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineManager : MonoBehaviour
{
   public PlayableDirector director;
   public GameObject cutsceneFade;
   public GameObject cutsceneBlink;


   [Header("Pop-up message")] 
   public GameObject popUpMessage;

  

   public void PauseCutScene()
   {
      director.Pause();
      cutsceneBlink.GetComponent<Animator>().enabled = false;
      cutsceneFade.GetComponent<Animator>().enabled = false;
      UIManager.instance.ToggleMouse(true);

   }

   public void ResumeCutscene()
   {
      director.Resume();
      cutsceneBlink.GetComponent<Animator>().enabled = true;
      cutsceneFade.GetComponent<Animator>().enabled = true;
      UIManager.instance.ToggleMouse(false);
   }

   public void StopCutScene()
   {
      director.Stop();
      cutsceneBlink.GetComponent<Animator>().enabled = false;
      cutsceneFade.GetComponent<Animator>().enabled = false;
      FindObjectOfType<SpawnPoint>().ManualSpawnPlayer();
   }

   
}
