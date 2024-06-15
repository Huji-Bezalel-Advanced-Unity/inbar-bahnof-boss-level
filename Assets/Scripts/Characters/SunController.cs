using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Player;
using UnityEngine;

public class SunController : MonoBehaviour
{
    private const float LIVE_DURATION = 4f;

    private void Start()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(LIVE_DURATION);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.AddEnergy();
            StopCoroutine(Die());
            Destroy(gameObject);
        }
    }
}
