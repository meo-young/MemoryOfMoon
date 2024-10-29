using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class WarpSpeedScript : MonoBehaviour
{
    public VisualEffect warpSpeedVFX;
    public MeshRenderer meshRenderer;
    public float rate = 0.02f;
    public float delay = 0.5f;

    private bool warpActive;

    private void Start()
    {
        warpSpeedVFX.Stop();
        warpSpeedVFX.SetFloat("WarpAmount", 0);

        meshRenderer.material.SetFloat("_Active", 0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            warpActive = true;
            StartCoroutine(ActivateParticles());
            StartCoroutine(ActivateShader());
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            warpActive = false;
            StartCoroutine(ActivateParticles());
            StartCoroutine(ActivateShader());
        }
    }

    IEnumerator ActivateParticles()
    {
        if(warpActive)
        {
            warpSpeedVFX.Play();
            float amount = warpSpeedVFX.GetFloat("WarpAmount");
            while(amount < 1 && warpActive)
            {
                amount += rate;
                warpSpeedVFX.SetFloat("WarpAmount", amount);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            float amount = warpSpeedVFX.GetFloat("WarpAmount");
            while (amount > 0 && !warpActive)
            {
                amount -= rate;
                warpSpeedVFX.SetFloat("WarpAmount", amount);
                yield return new WaitForSeconds(0.1f);

                if(amount <= 0+rate)
                {
                    amount = 0;
                    warpSpeedVFX.SetFloat("WarpAmount", amount);
                    warpSpeedVFX.Stop();
                }
            }
        }
    }

    IEnumerator ActivateShader()
    {
        if (warpActive)
        {
            yield return new WaitForSeconds(delay);

            float amount = meshRenderer.material.GetFloat("_Active");
            while (amount < 1 && warpActive)
            {
                amount += rate;
                meshRenderer.material.SetFloat("_Active", amount);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            float amount = meshRenderer.material.GetFloat("_Active");
            while (amount > 0 && !warpActive)
            {
                amount -= rate;
                meshRenderer.material.SetFloat("_Active", amount);
                yield return new WaitForSeconds(0.1f);

                if (amount <= 0 + rate)
                {
                    amount = 0;
                    meshRenderer.material.SetFloat("_Active", amount);
                }
            }
        }
    }
}
