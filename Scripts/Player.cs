using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _gravity = 9.81f;
    [SerializeField]
    private GameObject _muzzle_flash;
    [SerializeField]
    private GameObject _hitMarkerPrefab;
    [SerializeField]
    private AudioSource weaponaudio;
    [SerializeField]
    private int currentAmmo;
    [SerializeField]
    private int maxAmmo = 50;
    private bool _isreload = false;
    public bool getCoin = false; 
    private UIManager _uiManager;
    public bool canfire = false;
    
    void Start()
    {
        currentAmmo = maxAmmo;   
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && currentAmmo > 0 && canfire == true)
        {         
           
             Shoot();
        }
        else
        {
            _muzzle_flash.SetActive(false);
            weaponaudio.Stop();    // weaponsound off
        }

        
        if (Input.GetKey(KeyCode.Escape))  //enable mouse cursor
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.R) && _isreload == false)
        {
            _isreload = true;
            StartCoroutine(Reload());
        }

        CalculateMovement();
    }

    IEnumerator Reload()
    {
       
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
        _uiManager.UpdateAmmo(currentAmmo);
        _isreload = false;
    }

    void Shoot()
    {
        currentAmmo--;
        _uiManager.UpdateAmmo(currentAmmo);
        _muzzle_flash.SetActive(true);
        if (weaponaudio.isPlaying == false)   // weaponsound on
        {
            weaponaudio.Play();
        }

        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));  //add ray // ViewportPointToRay - coordinates between 0-1
        RaycastHit hitInfo;

        //  if (Physics.Raycast(rayOrigin, Mathf.Infinity))  //if hi smth
        if (Physics.Raycast(rayOrigin, out hitInfo))  //if hit - get info to   out hitinfo
        {
            Debug.Log("HIT" + hitInfo.transform.name);

            GameObject hitmarker = (GameObject)Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(hitmarker, 1f);

            Destructable crate = hitInfo.transform.GetComponent<Destructable>();
            if (crate != null)
            {
                crate.DestroyCrate();
            }
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 diretcion = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = diretcion * _speed;
        velocity.y -= _gravity;
        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }

 
}
