using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    // Initial monetary cost.
    public int moneyCost = 3000;
    // Initial ecological cost.
    public float ecoCost = 1.0f;
    // Tile's resource drain per tick.
    public int tickResourceDrain = 500;
    // Monetary gain per tick.
    public int tickProfit = 1000;
    // Ecological damage per tick.
    public float tickEcoDamage = 0.3f;
    // Monetary gain when removed.
    public int sellValue = 1500;
    public AudioClip[] onPickup;
    public AudioClip[] onDropping;
    public AudioClip[] onSelling;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(onPickup[UnityEngine.Random.Range(0, onPickup.Length - 1)]);
    }

    public float PlaySellSound()
    {
        AudioClip sound = onSelling[UnityEngine.Random.Range(0, onDropping.Length - 1)];

        audioSource.PlayOneShot(sound);
        return sound.length;
    }

    public void PlayDropSound()
    {
        audioSource.PlayOneShot(onDropping[UnityEngine.Random.Range(0, onDropping.Length - 1)]);
    }

    // To be implemented in child class if needed.
    public void TickSpecial()
    {

    }
}
