using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System;
public class Bullet : MonoBehaviour
{
    IObjectPool<Bullet> pool;
    [SerializeField] private float timer;
    [SerializeField] private float timerMax;
    [SerializeField] private float bulletSpeed=5;
    [SerializeField] private float angel=10;
    [SerializeField] private float angelSpeed=10;
    private Vector3 direction;
    private Vector3 originalPosition;
    private Action OnUnitDestroy;
    private Unit unit;
    private TrailRenderer trailRenderer;
    private Transform trailHolder;
    private void Awake()
    {
        trailHolder = transform.GetChild(0);
        trailRenderer = trailHolder.GetComponent<TrailRenderer>();
    }
    private void Start()
    {
        unit.OnDestroyThisObjectInformBullet += Unit_OnDestroyThisObjectInformBullet;
        timer = timerMax;
    }
    public void SetPoolObject(IObjectPool<Bullet> objectPool, Unit _unit)
    {
        //trailRenderer.enabled = true;
        this.unit = _unit;
      
        this.pool = objectPool;
    }
    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
    private void OnEnable()
    {
        OnUnitDestroy += Unit_OnDestroyThisObjectInformBullet;
    }
    private void Update()
    {
        transform.Translate(direction * bulletSpeed * Time.deltaTime, Space.World);
        transform.rotation *= Quaternion.AngleAxis(angel*angelSpeed*Time.deltaTime, -Vector3.forward);
        timer -= Time.deltaTime;
        if (timer <= 0.5)
        {
            SetTrailRendererTime(false);
        }
        if (timer <= 0)
        {

            Release();
            timer = timerMax;
        }
    }
    public void Release()
    {
    
        pool.Release(this);
    }
 
    private void Unit_OnDestroyThisObjectInformBullet()
    {       
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        OnUnitDestroy -= Unit_OnDestroyThisObjectInformBullet;
    }
    private void OnDisable()
    {
        OnUnitDestroy -= Unit_OnDestroyThisObjectInformBullet;
    }
    public void SetTrailRendererTime(bool enable)
    {
        trailHolder.gameObject.SetActive(enable);
     
    }
}
