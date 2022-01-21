using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private GameObject _boss;
    private PlayerMovement _playerMovement;
    private float _baseMapSpeed;
    //private GameObject _boss;
    //private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        //_boss = GameObject.Find("Boss-A");
        //player = GameObject.Find("PlayerShip");
        _playerMovement = player.GetComponent<PlayerMovement>();
        _baseMapSpeed = _playerMovement.baseMapSpeed;
        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {   
        if (_boss != null && player != null) {
            if (!_boss.GetComponent<EnemyHealth>().dead && !player.GetComponent<PlayerHealth>().dead) {
                float newPosition = transform.position.y + (_baseMapSpeed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, newPosition, transform.position.z);
            }
        } 
    }
}
