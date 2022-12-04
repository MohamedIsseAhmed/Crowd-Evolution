using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class BulletObjectPooling : MonoBehaviour
{
    IObjectPool<Bullet> bulletPool;

    Unit unit;
    private void Awake()
    {
        unit = GetComponent<Unit>();
        bulletPool = new ObjectPool<Bullet>(CreateBullet, TakeBulletFromPool, ReturnBulletFromPool, OnPoolDestroy,true,20,100);
    }

    private Bullet CreateBullet()
    {
        Bullet newBullet= Instantiate(unit.GetBulletToSpawn(), unit.GetBulletSpawnPosition(), unit.GetBulletToSpawn().transform.rotation);
        newBullet.SetPoolObject(bulletPool, unit);
       
        return newBullet;
    }
    private void TakeBulletFromPool(Bullet bullet)
    {
        bullet.SetTrailRendererTime(true);
        bullet.SetDirection(unit.GetBulletBulletDirection());
        bullet.gameObject.SetActive(true);
        bullet.transform.position = unit.GetBulletSpawnPosition();

    }
    private void ReturnBulletFromPool(Bullet bullet)
    {
        bullet.SetTrailRendererTime(false);
        bullet.gameObject.SetActive(false);
        bullet.transform.position = unit.GetBulletSpawnPosition();

    }
    private void OnPoolDestroy(Bullet bullet)
    {
       
        Destroy(bullet.gameObject);
    }
    public void GetBullet()
    {
        bulletPool.Get();
    }
   
}
