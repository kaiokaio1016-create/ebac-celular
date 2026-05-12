using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtPiece : MonoBehaviour
{
    // Esta linha é a que está faltando ou está com erro de digitação:
    public GameObject currentArt;

    public void ChangePiece(GameObject piece)
    {
        if (currentArt != null) Destroy(currentArt);

        currentArt = Instantiate(piece, transform);
        currentArt.transform.localPosition = Vector3.zero;
    }
}
