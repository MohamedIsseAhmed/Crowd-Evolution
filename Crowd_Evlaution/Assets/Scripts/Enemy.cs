using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private float coloChngeSpeed = 0.2f;
    [SerializeField] private float moveSpeed = 4;
    [SerializeField] private float diatnceCanBulletDamageEnemy = 15;
    [SerializeField] private EnemyType enemyType;
    private bool goToPlayerOnFrontLine;
    private Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        FrontLinePoints.instance.OnFrontLÝneTakeActionEvent += Ýnstance_OnFrontLÝneTakeActionEvent;
       
    }

    private void Ýnstance_OnFrontLÝneTakeActionEvent(object sender, List<Transform> e)
    {
        goToPlayerOnFrontLine = true;
       
    }
    private void Update()
    {
        if (goToPlayerOnFrontLine && enemyType==EnemyType.Runner)
        {
            animator.SetFloat("BlendSpeed", 0.8f);
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        float distanceToPlayer = Vector3.SqrMagnitude(player.position - transform.position);
        
        if (other.CompareTag("Bullet") && distanceToPlayer <= diatnceCanBulletDamageEnemy * diatnceCanBulletDamageEnemy)
        {
            LastEnemyTracker.instance.RemoveEnemy(gameObject);
            print(distanceToPlayer);
            enabled = false;
            goToPlayerOnFrontLine = false;
            Bullet bullet = other.transform.GetComponent<Bullet>();
            bullet.Release();
            animator.SetTrigger("Death");
            meshRenderer.material.DOColor(Color.white , coloChngeSpeed);
          
        }
        else if (other.CompareTag("Unit"))
        {
            Unit unit = other.gameObject.GetComponent<Unit>();
            if (unit != null)
            {
                UnitController unitController= player.GetComponent<UnitController>();
                unitController.UnitLists.Remove(unit);
                unitController.FormaCharactersOrderly();
                unit.InformBulletBeforeDestroyingThisGameObject();
                enabled = false;
                Destroy(other.gameObject);
            }
            
        }
    }
    //Animation Event
    public void OnEnemyDeath()
    {
        Destroy(gameObject,0.5f);
    }
}
public enum EnemyType
{
    idle,
    Runner
    
}
