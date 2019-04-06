using UnityEngine;

public class SharkShop : MonoBehaviour
{
    [SerializeField]
    private GameObject _weapon;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var player = other.GetComponent<Player>();
                if (player.isGetCoin == true)
                {
                    player.isGetCoin = false;
                    _weapon.SetActive(true);
                    player.isCanFire = true;
                    var uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                    uIManager.coin.SetActive(false);
                    var audio = GetComponent<AudioSource>();
                    audio.Play();
                }

                if (player.isGetCoin == false)
                {
                    Debug.Log("You have no coin");
                }

            }
        }
    }
}
