using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Unit : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    [SerializeField] Transform bulletSpawnPosition;
    [SerializeField] CharacterData characterData;
    [SerializeField] ObjectCreater objectCreater;
    [SerializeField] Animator animator;
    private float shootTimer;
    private float shootTimerMax;
    private Shoot shoot;
    public Action OnDestroyThisObjectInformBullet;
    private void Awake()
    {
        shoot = GetComponent<Shoot>();
    }
    private void OnEnable()
    {
        shootTimer = characterData.shootTimer;
        shootTimerMax = characterData.shootTimerMax;
    }
    private void Update()
    {
        shootTimer -= Time.deltaTime;
        if(shootTimer <= 0)
        {

            shootTimer = shootTimerMax;
            shoot.ShotTheEnemy();
        }
    }
    public Vector3 GetBulletSpawnPosition()
    {
       return bulletSpawnPosition.position;
    }
    public Vector3 GetBulletBulletDirection()
    {
        return transform.forward;
    }
    public Bullet GetBulletToSpawn()
    {
        return weaponData.bulletPrefab; 
    }
    private void OnDisable()
    {
       
        Debug.Log("Destroying" + transform.name);
    }
    public void InformBulletBeforeDestroyingThisGameObject()
    {
    
        OnDestroyThisObjectInformBullet?.Invoke();
    }
}
