using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    [SerializeField] ParticleSystem pieceParticle;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            pieceParticle.Play();
        }
    }
}
