using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace BattleCity
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Vector3 _startPosition;
        
        private Vector3 _playerDirection = Vector3.zero;
        private Transform _gun;
        private GameObject _player;
        private bool _fire = false;
        private float _bulletSpeed = 700f;
        
        void Start()
        {
            _player = Instantiate(_playerPrefab, _startPosition, Quaternion.identity);
            _gun = _player.transform.GetChild(0);
            _gun.tag = "Up";
        }

        private void Update()
        {
            _playerDirection.x = Input.GetKey(KeyCode.D) ? 1f : Input.GetKey(KeyCode.A) ? -1f : 0;
            _playerDirection.z = Input.GetKey(KeyCode.W) ? 1f : Input.GetKey(KeyCode.S) ? -1f : 0;
            _fire = Input.GetKeyDown(KeyCode.Space) ? true : false;
            if (_fire)
            {
                Fire();
            }
        }

        private void FixedUpdate()
        {
            if (_playerDirection.x != 0f && _playerDirection.z == 0f ||
                _playerDirection.x == 0f && _playerDirection.z != 0f)
            {
                Move();
            }

            
        }

        private void Move()
        {
            RaycastHit hitRight;
            RaycastHit hitLeft;
            Ray rayRight = new Ray();
            Ray rayLeft = new Ray();
            var rayRightPosition = new Vector3();
            var rayLeftPosition = new Vector3();
            if (_playerDirection.x > 0f)
            {
                _gun.rotation = Quaternion.Euler(0f, 90f, 0f);
                _gun.tag = "Right";
                rayRightPosition = new Vector3(_player.transform.position.x, _player.transform.position.y,
                    _player.transform.position.z + 1.95f);
                rayRight = new Ray(rayRightPosition, _playerDirection);
                rayLeftPosition = new Vector3(_player.transform.position.x, _player.transform.position.y,
                    _player.transform.position.z - 1.95f);
                rayLeft = new Ray(rayLeftPosition, _playerDirection);
            }
            else if (_playerDirection.x < 0f)
            {
                _gun.rotation = Quaternion.Euler(0f, -90f, 0f);
                _gun.tag = "Left";
                rayRightPosition = new Vector3(_player.transform.position.x, _player.transform.position.y,
                    _player.transform.position.z + 1.95f);
                rayRight = new Ray(rayRightPosition, _playerDirection);
                rayLeftPosition = new Vector3(_player.transform.position.x, _player.transform.position.y,
                    _player.transform.position.z - 1.95f);
                rayLeft = new Ray(rayLeftPosition, _playerDirection);
            }
            if (_playerDirection.z > 0f)
            {
                _gun.rotation = Quaternion.Euler(0f, 0f, 0f);
                _gun.tag = "Up";
                rayRightPosition = new Vector3(_player.transform.position.x + 1.95f, _player.transform.position.y,
                    _player.transform.position.z);
                rayRight = new Ray(rayRightPosition, _playerDirection);
                rayLeftPosition = new Vector3(_player.transform.position.x - 1.95f, _player.transform.position.y,
                    _player.transform.position.z);
                rayLeft = new Ray(rayLeftPosition, _playerDirection);
            }
            else if(_playerDirection.z < 0f)
            {
                    _gun.rotation = Quaternion.Euler(0f, 180f, 0f);
                    _gun.tag = "Down";
                    rayRightPosition = new Vector3(_player.transform.position.x + 1.95f, _player.transform.position.y, _player.transform.position.z);
                    rayRight = new Ray(rayRightPosition, _playerDirection);
                    rayLeftPosition = new Vector3(_player.transform.position.x - 1.95f, _player.transform.position.y, _player.transform.position.z);
                    rayLeft = new Ray(rayLeftPosition, _playerDirection);
            }
            Physics.Raycast(rayRight, out hitRight);
            Physics.Raycast(rayLeft, out hitLeft);
            var tmpDistanceRayRight = (decimal)Vector3.Distance(rayRightPosition, hitRight.point);
            tmpDistanceRayRight = Decimal.Round(tmpDistanceRayRight, 2);
            var tmpDistanceRayLeft = (decimal)Vector3.Distance(rayLeftPosition, hitLeft.point);
            tmpDistanceRayLeft = Decimal.Round(tmpDistanceRayLeft, 2);
            if (tmpDistanceRayRight > 2 && tmpDistanceRayLeft > 2)
            {
                _player.transform.Translate(_playerDirection);
            }
            if (_player.transform.position.x % 1f != 0f || _player.transform.position.z % 1f != 0f)
            {
                switch (_gun.tag)
                {
                    case "Right":
                    {
                        var tmpPosition = new Vector3(Mathf.Ceil(_player.transform.position.x),
                            _player.transform.position.y,
                            _player.transform.position.z);
                        _player.transform.position = tmpPosition;
                    }
                        break;
                    case "Left":
                    {
                        var tmpPosition = new Vector3(Mathf.Floor(_player.transform.position.x),
                            _player.transform.position.y,
                            _player.transform.position.z);
                        _player.transform.position = tmpPosition;
                    }
                        break;
                    case "Up":
                    {
                        var tmpPosition = new Vector3(_player.transform.position.x,
                            _player.transform.position.y,
                            Mathf.Ceil(_player.transform.position.z));
                        _player.transform.position = tmpPosition;
                    }
                        break;
                    case "Down":
                    {
                        var tmpPosition = new Vector3(_player.transform.position.x,
                            _player.transform.position.y,
                            Mathf.Floor(_player.transform.position.z));
                        _player.transform.position = tmpPosition;
                    }
                        break;
                }
            }
        }

        private void Fire()
        {
            switch (_gun.tag)
            {
                case "Up" : 
                    var bullet = Instantiate(_bulletPrefab, _gun.position + Vector3.forward * 2.5f, Quaternion.identity);
                    bullet.tag = "Up";
                    bullet.AddComponent<Bullet>();
                    bullet.GetComponent<Rigidbody>().AddForce(Vector3.forward * _bulletSpeed, ForceMode.Force);
                    break;
                case "Down" : 
                    bullet = Instantiate(_bulletPrefab, _gun.position + Vector3.back * 2.5f, Quaternion.identity);
                    bullet.tag = "Down";
                    bullet.AddComponent<Bullet>();
                    bullet.GetComponent<Rigidbody>().AddForce(Vector3.back * _bulletSpeed, ForceMode.Force);
                    break;
                case "Left" : 
                    bullet = Instantiate(_bulletPrefab, _gun.position + Vector3.left * 2.5f, Quaternion.identity);
                    bullet.tag = "Left";
                    bullet.AddComponent<Bullet>();
                    bullet.GetComponent<Rigidbody>().AddForce(Vector3.left * _bulletSpeed, ForceMode.Force);
                    break;
                case "Right" : 
                    bullet = Instantiate(_bulletPrefab, _gun.position + Vector3.right * 2.5f, Quaternion.identity);
                    bullet.tag = "Right";
                    bullet.AddComponent<Bullet>();
                    bullet.GetComponent<Rigidbody>().AddForce(Vector3.right * _bulletSpeed, ForceMode.Force);
                    break;
            }
        }
    }
}
