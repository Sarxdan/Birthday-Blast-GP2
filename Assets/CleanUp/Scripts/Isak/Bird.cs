using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy // fly around
{
    // Start is called before the first frame update
    protected override void CheckForPlayer()
    {
        base.CheckForPlayer();
        if(checkedColliders.Length > 0)
        {
            audio = AudioManager.instance.PlayClipAtPoint("Bird", transform.position);
        }
        else if(checkedColliders.Length == 0)
        {
            if(audio != null)
            {
                audio.Stop();
            }            
        }
    }
}
