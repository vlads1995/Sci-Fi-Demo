using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    
    private Player player;
    [SerializeField]
    private AudioClip coinAudio;


    private void Start()
    {
         
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
               
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    AudioSource.PlayClipAtPoint(coinAudio, transform.position, 1f);
                    player.getCoin = true;
                    UIManager uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                    if (uIManager != null)
                    {
                        uIManager.CollectedCoin();
                    }
                    Destroy(this.gameObject);
                }
                 
            }
        }
    }

   
}
