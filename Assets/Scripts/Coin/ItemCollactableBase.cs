using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public Collider coinCollider;

    [Header("Movement Setup")]
    public float lerpSpeed = 5f;
    public float minDistance = 1f;

    protected bool isCollected = false;
    protected Transform playerTransform;

    protected virtual void OnTriggerEnter(Collider collision)
    {
        if (!isCollected && collision.transform.CompareTag(compareTag))
        {
            playerTransform = collision.transform;
            Collect();
        }
    }

    protected virtual void Collect()
    {
        isCollected = true;
        if (coinCollider != null) coinCollider.enabled = false;
        OnCollect();
    }

    protected virtual void OnCollect()
    {
        // Comportamento genérico (pode ser deixado vazio)
    }

    protected virtual void Update()
    {
        // Move o item até o jogador após a coleta
        if (isCollected && playerTransform != null)
        {
            transform.position = Vector3.Lerp(transform.position, playerTransform.position, lerpSpeed * Time.deltaTime);
        }
    }
}