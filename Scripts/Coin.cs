
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Player _player;
    [SerializeField]
    private AudioClip _coinAudio;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player") return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            var player = other.GetComponent<Player>();
            if (player != null)
            {
                AudioSource.PlayClipAtPoint(_coinAudio, transform.position, 1f);
                player.isGetCoin = true;
                var uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                if (uIManager != null)
                {
                    uIManager.CollectedCoin();
                }
                Destroy(this.gameObject);
            }
        }
    }
}
