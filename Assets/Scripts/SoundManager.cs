using System.Collections;
using System.Collections.Generic;
using static Weapon;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource shootingChannel;

    public AudioClip GlockShot;
    public AudioClip AK47Shot;

    public AudioSource reloadingSoundGlock;
    public AudioSource reloadingSoundAK47;
    public AudioSource emptyMagazineSoundGlock;

    public AudioClip zombieWalking;
    public AudioClip zombieChase;
    public AudioClip zombieAttack;
    public AudioClip zombieHurt;
    public AudioClip zombieDeath;

    public AudioSource zombieChannel;
    public AudioSource zombieChannel2;

    public AudioSource playerChannel;
    public AudioClip playerHurt;
    public AudioClip playerDeath;

    public AudioClip gameOverSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Glock:
                shootingChannel.PlayOneShot(GlockShot);
                break;
            case WeaponModel.AK47:
                shootingChannel.PlayOneShot(AK47Shot);
                break;
            default:
                break;
        }
    }

    public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Glock:
                reloadingSoundGlock.Play();
                break;
            case WeaponModel.AK47:
                reloadingSoundAK47.Play();
                break;
            default:
                break;
        }
    }
}
