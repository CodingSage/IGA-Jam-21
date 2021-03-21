using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    public AudioManager audioManager;

    private void OnCollisionEnter(Collision collision)
    {
        int random = Random.Range(1, 4);

        switch (random)
        {
            case 1:
                audioManager.PlaySound("Bonk1");

                break;

            case 2:
                audioManager.PlaySound("Bonk2");

                break;

            case 3:
                audioManager.PlaySound("Bonk3");

                break;

            default:
                break;
        }
    }
}
