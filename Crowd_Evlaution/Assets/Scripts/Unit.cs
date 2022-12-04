using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Unit : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField] private CharacterData characterData;
    [SerializeField] private ObjectCreater objectCreater;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 4; 
    [SerializeField] private float minDistanceToFrontPoint = 0.1f;
    [SerializeField] private UnitType unitType;
    private float shootTimer;
    private float shootTimerMax;
    private Shoot shoot;
    public Action OnDestroyThisObjectInformBullet;
    private bool isOnFrontLine = false;
    private bool isGameOver = false;
    private Transform targetFrontPoint;
    private void Awake()
    {
        shoot = GetComponent<Shoot>();
    }
    private void OnEnable()
    {
        shootTimer = characterData.shootTimer;
        shootTimerMax = characterData.shootTimerMax;
    }
    private void Start()
    {
        if (unitType == UnitType.StickMan)
        {
            animator.SetFloat("BlendSpeed", 0.5f);
        }
        LastEnemyTracker.instance.OnAllEnemiesDied += Ýnstance_OnAllEnemiesDied;
    }

    private void Ýnstance_OnAllEnemiesDied(object sender, EventArgs e)
    {
        isGameOver=true;
    }

    private void Update()
    {
        if (isGameOver)
        {
            return;
        }
        shootTimer -= Time.deltaTime;
        if(shootTimer <= 0)
        {

            shootTimer = shootTimerMax;
            shoot.ShotTheEnemy();
        }
        if (isOnFrontLine)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetFrontPoint.position, moveSpeed * Time.deltaTime);
            Vector3 direction = targetFrontPoint.position - transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
            if(Vector3.Distance(transform.position, targetFrontPoint.position)<minDistanceToFrontPoint)
            {
                animator.SetTrigger("KneelDown");
            }
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
  
    public void InformBulletBeforeDestroyingThisGameObject()
    {
    
        OnDestroyThisObjectInformBullet?.Invoke();
    }
    public void GoToFrontLinePoint(Transform taregtPoint)
    {
        transform.parent = null;
        this.targetFrontPoint = taregtPoint;
        isOnFrontLine = true;
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("FrontLine"))
        {
           
        }
    }
    public enum UnitType
    {
        StickMan,
        Soldier

    }
}
