using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    BulletObjectPooling bulletObject;
    private void Awake()
    {
        bulletObject = GetComponent<BulletObjectPooling>();
    }
    public void ShotTheEnemy()
    {
        bulletObject.GetBullet();
    }
}
