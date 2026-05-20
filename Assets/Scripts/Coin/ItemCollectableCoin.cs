using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem particleSystem;
    public float timeToHide = 3;
    public GameObject graphicItem;

    [Header("Sounds")]
    public AudioSource audioSource;

    [Header("Coin Lerp (Atração)")]
    public Collider coinCollider;
    public float lerpSpeed = 5f;
    public float minDistance = 1f;

    private bool isCollected = false;
    private Transform playerTransform;

    private void OnTriggerEnter(Collider collision)
    {
        // Só coleta se não tiver sido coletada ainda e se for o Player
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
        if (graphicItem != null) graphicItem.SetActive(false);

        OnCollect();

        // BUSCA ROBUSTA: Procura o BounceHelper em qualquer lugar do Player que colidiu
        if (playerTransform != null)
        {
            // Tenta achar no objeto da colisão, nos filhos ou nos pais dele
            BounceHelper bounce = playerTransform.GetComponent<BounceHelper>();
            if (bounce == null) bounce = playerTransform.GetComponentInChildren<BounceHelper>();
            if (bounce == null) bounce = playerTransform.GetComponentInParent<BounceHelper>();

            // Se ainda assim não achar, tenta buscar pelo Objeto Ativo na cena com o script
            if (bounce == null) bounce = FindObjectOfType<BounceHelper>();

            if (bounce != null)
            {
                // CHAMA A FUNÇÃO PÚBLICA DO BOUNCE HELPER
                bounce.Bounce();
                Debug.Log("<color=green>Sucesso: Bounce acionado pela moeda!</color>");
            }
            else
            {
                Debug.LogError("A moeda não encontrou o componente BounceHelper no player: " + playerTransform.name);
            }
        }

        Destroy(gameObject, timeToHide);
    }

    protected virtual void OnCollect()
    {
        if (particleSystem != null)
        {
            // Evita o erro de modificar a hierarquia do Prefab Asset
            if (particleSystem.gameObject.scene.IsValid())
            {
                particleSystem.transform.SetParent(null);
            }
            particleSystem.Play();
        }

        if (audioSource != null) audioSource.Play();
    }

    private void Update()
    {
        // Efeito da moeda voar/ir até o player
        if (isCollected && playerTransform != null)
        {
            transform.position = Vector3.Lerp(transform.position, playerTransform.position, lerpSpeed * Time.deltaTime);
        }
    }
}