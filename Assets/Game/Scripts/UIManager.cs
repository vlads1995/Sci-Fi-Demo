using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    public GameObject coin;

    public void UpdateAmmo(int count) 
    {
        _ammoText.text = "Ammo: " + count;
    }
 
    public void CollectedCoin()
    {
        coin.SetActive(true);
    }
}
