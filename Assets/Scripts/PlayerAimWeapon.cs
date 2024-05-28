using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAimWeapon : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }
    private Transform aimTranform;
    private Transform aimGunEndPointTransform;
    private void Awake()
    {
        aimTranform = transform.Find("Aim");
        aimGunEndPointTransform = transform.Find("GunEndPointPos");

    }
    private void HandleAiming()
    {
        Vector3 mousePosition = GetMouseWorldPos();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTranform.eulerAngles = new Vector3(0, 0, angle);
    }
    public static Vector3 GetMouseWorldPos()
    {
        Vector3 vec = GetMouseWorldPosWithZ(Input.mousePosition, Camera.main);
        vec.z = 0;
        return vec;
    }

    public static Vector3 GetMouseWorldPosWithZ(Vector3 mousePosition)
    {
        return GetMouseWorldPosWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPosWithZ(Camera worldCamera)
    {
        return GetMouseWorldPosWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPosWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    private void Update()
    {
        HandleAiming();
        HandleShooting();
    }
    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = GetMouseWorldPos();
            OnShoot?.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mousePosition

            });

        }
    }

}
