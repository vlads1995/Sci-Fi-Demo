using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    
    [SerializeField]
    private GameObject weapon;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                if (player.getCoin == true)
                {
                   
                    player.getCoin = false;
                    weapon.SetActive(true);
                    player.canfire = true;
                    UIManager uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                    uIManager.coin.SetActive(false);
                    AudioSource audio = GetComponent<AudioSource>();
                    audio.Play();
                }
                if (player.getCoin == false)
                {
                    Debug.Log("U have no coin");
                }

            }
        }
    }
}
