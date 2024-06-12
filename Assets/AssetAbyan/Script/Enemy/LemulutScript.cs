using System;
using System.Collections;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LemulutScript : Enemy
{

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        enemyManager = EnemyManager.instance;
        pointManager = PointManager.instance;
        enemyPoints = SetPlace(EnemyPointNames.Right, EnemyPointNames.Left);
        SetPoint(enemyPoints.pointA);
        SetIdle(enemyManager.lemulutIdleSet);
        movSpeed = enemyManager.lemulutMovSpeed;
        attackSpeed = enemyManager.lemulutAttackSpeed;
        damage = enemyManager.lemulutDamage;

        enemyPoints.enemyCount++;
        enemyManager.lemulutCount++;

        isIdle = false;
        damageAbleDetected = false;
        damageAbleInRange = false;

        enemyStatus = EnemyStatus.Lemulut;
        enemyHealth = this.GetComponent<EnemyHealth>();
    }
    private void Update() {
        SetEnemyAtribut();
        EnemyDirection();
        ChangePoint();
        flip();
        CheckSurrounding();
        RandomIdle();
        setAnimationParameter();
        EnemyAttack();
    }
    private void FixedUpdate() {
        EnemyMovement();
    }
    //========================================================================================================================================================LookAt(Vector3 pos) {
    private void SetEnemyAtribut(){
        SetIdle(enemyManager.lemulutIdleSet);
        rangeDetection = enemyManager.lemulutDetection;
        rangeAttack = enemyManager.lemulutRangeAttack;

        if (enemyHealth != null) {}
        {
            enemyHealth.maxHealth = (int)enemyManager.lemulutHealth;   
        }
    }
    //========================================================================================================================================================
    public override void Idle(Boolean isIdle) {
        if (isIdle){
            this.isIdle = true;
            movSpeed = 0;
        }
        else{
            this.isIdle = false;
            movSpeed = enemyManager.lemulutMovSpeed;
        }
    }
    //========================================================================================================================================================
    public override void EnemyAttack(){
        attackTimer += Time.deltaTime;
        if(damageAbleInRange){
            if (attackTimer > enemyManager.lemulutAttackSpeed)
            {
                animator.Play("LemulutAttack");
                attackTimer = 0;
            }
        }else if(attackTimer > enemyManager.lemulutAttackSpeed){
            attackTimer = enemyManager.lemulutAttackSpeed;
        }
    }
    public override void Attack()
    {
        if(objectInRange != null){
            IDamagable damagable = objectInRange.GetComponent<IDamagable>();
            if (damagable != null)
            {
                if(GameObject.FindGameObjectWithTag("Player").GetComponent<Coin>().coinCount > 0 ){
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Coin>().decreaseCoin(1);
                }
                else{
                    damagable.TakeDamage((int)enemyManager.lemulutDamage);
                }
            }
        }
    }
    //========================================================================================================================================================
    protected override void CheckSurrounding(){
        objectDetected = Physics2D.OverlapCircle(transform.position, rangeDetection, damageAbleLayer);
        objectInRange = Physics2D.OverlapCircle(attackPoint.position, rangeAttack, damageAbleLayer);

        if (objectDetected != null && enemyManager.lelumutDamageAble.Contains(objectDetected.tag)){
            SetPoint(objectDetected.gameObject.transform);
            damageAbleDetected = true;
        } else if(damageAbleDetected){
            SetPoint(enemyPoints.pointA);
            damageAbleDetected =false;
        }
        if (objectInRange != null && enemyManager.lelumutDamageAble.Contains(objectInRange.tag)){
            Idle(true);
            damageAbleInRange = true;
        } else if(damageAbleInRange){
            Idle(false);
            damageAbleInRange =false;
        }
    }
    //========================================================================================================================================================
    public override void Destroy(){
        enemyManager.lemulutCount--;
        enemyPoints.enemyCount--;
        base.Destroy();
    }
    //========================================================================================================================================================
    
    public void OnDrawGizmos(){
        Gizmos.DrawWireSphere(transform.position, rangeDetection);
        Gizmos.DrawWireSphere(attackPoint.position, rangeAttack);
    }
    //========================================================================================================================================================
    private void setAnimationParameter(){
        animator.SetFloat("movSpeed",Mathf.Abs(movSpeed));
    }
}