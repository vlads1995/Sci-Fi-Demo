using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float Speed = 3.5f;
    private const float Gravity = 9.81f;
    private const int MaxAmmo = 50;

    public bool isGetCoin = false;
    public bool isCanFire = false;

    private CharacterController _controller;
    [SerializeField]
    private GameObject _muzzleFlash;
    [SerializeField]
    private GameObject _hitMarkerPrefab;
    [SerializeField]
    private AudioSource _weaponaudio;
    [SerializeField]
    private int _currentAmmo;
    [SerializeField]
    private bool _isReload = false;
    private UIManager _uiManager;

    private void Start()
    {
        _currentAmmo = MaxAmmo;   
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _currentAmmo > 0 && isCanFire == true)
        {                
            Shoot();
        }
        else
        {
            _muzzleFlash.SetActive(false);
            _weaponaudio.Stop();    
        }
        
        if (Input.GetKey(KeyCode.Escape)) 
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.R) && _isReload == false)
        {
            _isReload = true;
            StartCoroutine(Reload());
        }

        CalculateMovement();
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        _currentAmmo = MaxAmmo;
        _uiManager.UpdateAmmo(_currentAmmo);
        _isReload = false;
    }

    private void Shoot()
    {
        _currentAmmo--;
        _uiManager.UpdateAmmo(_currentAmmo);
        _muzzleFlash.SetActive(true);
        if (_weaponaudio.isPlaying == false)   
        {
            _weaponaudio.Play();
        }

        var rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(rayOrigin, out var hitInfo))  
        {
            Debug.Log("HIT" + hitInfo.transform.name);
            var hitmarker = (GameObject)Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(hitmarker, 1f);
            var crate = hitInfo.transform.GetComponent<Destructable>();

            if (crate != null)
            {
                crate.DestroyCrate();
            }
        }
    }

    private void CalculateMovement()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        var direction = new Vector3(horizontalInput, 0, verticalInput);
        var velocity = direction * Speed;
        velocity.y -= Gravity;
        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }
}
