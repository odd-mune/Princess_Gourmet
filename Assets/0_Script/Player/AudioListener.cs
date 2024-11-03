using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListener : MonoBehaviour
{
    private HashSet<FootStepComponent> mFootsteps = new HashSet<FootStepComponent>();

    public bool IsListenable(FootStepComponent footstep)
    {
        return mFootsteps.Contains(footstep);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FootStepComponent footStepComponentOrNull = collision.GetComponent<FootStepComponent>();
        if (footStepComponentOrNull != null )
        {
            mFootsteps.Add( footStepComponentOrNull );
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FootStepComponent footStepComponentOrNull = collision.GetComponent<FootStepComponent>();
        if (footStepComponentOrNull != null)
        {
            mFootsteps.Remove( footStepComponentOrNull );
        }
    }
}
