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
        [SerializeField] private GameObject _bulletPrefab;
        
        private Vector3 _playerDirection = Vector3.zero;
        private Transform _gun;
        
        void Start()
        {
            _gun = transform.GetChild(0);
        }

        private void FixedUpdate()
        {
            _playerDirection.x = Input.GetAxis("Horizontal") < 0f ? -1f : Input.GetAxis("Horizontal") == 0f ? 0f : 1f;
            _playerDirection.z = Input.GetAxis("Vertical") < 0f ? -1f : Input.GetAxis("Vertical") == 0f ? 0f : 1f;
            Move();
        }

        private void Move()
        {
            RaycastHit hitRight;
            RaycastHit hitLeft;
            Ray rayRight = new Ray();
            Ray rayLeft = new Ray();
            var rayRightPosition = new Vector3();
            var rayLeftPosition = new Vector3();
            switch (_playerDirection.x)
            {
                case 1:
                    _gun.rotation = Quaternion.Euler(0f, 90f, 0f);
                    rayRightPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.95f);
                    rayRight = new Ray(rayRightPosition, _playerDirection);
                    rayLeftPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.95f);
                    rayLeft = new Ray( rayLeftPosition, _playerDirection);
                    break;
                case -1:
                    _gun.rotation = Quaternion.Euler(0f, -90f, 0f);
                    rayRightPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.95f);
                    rayRight = new Ray(rayRightPosition, _playerDirection);
                    rayLeftPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.95f);
                    rayLeft = new Ray(rayLeftPosition, _playerDirection);
                    break;
            }
            switch (_playerDirection.z)
            {
                case 1:
                    _gun.rotation = Quaternion.Euler(0f, 0f, 0f);
                    rayRightPosition = new Vector3(transform.position.x + 1.95f, transform.position.y, transform.position.z);
                    rayRight = new Ray(rayRightPosition, _playerDirection);
                    rayLeftPosition = new Vector3(transform.position.x - 1.95f, transform.position.y, transform.position.z);
                    rayLeft = new Ray(rayLeftPosition, _playerDirection);
                    break;
                case -1:
                    _gun.rotation = Quaternion.Euler(0f, 180f, 0f);
                    rayRightPosition = new Vector3(transform.position.x + 1.95f, transform.position.y, transform.position.z);
                    rayRight = new Ray(rayRightPosition, _playerDirection);
                    rayLeftPosition = new Vector3(transform.position.x - 1.95f, transform.position.y, transform.position.z);
                    rayLeft = new Ray(rayLeftPosition, _playerDirection);
                    break;
            }

            if (_playerDirection != Vector3.zero)
            {
                Physics.Raycast(rayRight, out hitRight);
                Physics.Raycast(rayLeft, out hitLeft);
                var tmpDistanceRayRight = Vector3.Distance(rayRightPosition, hitRight.point);
                var tmpDistanceRayLeft = Vector3.Distance(rayLeftPosition, hitLeft.point);
                if (tmpDistanceRayRight > 2.02f && tmpDistanceRayLeft > 2.02f)
                {
                    transform.Translate(_playerDirection);
                }
            }
        }
    }
}
