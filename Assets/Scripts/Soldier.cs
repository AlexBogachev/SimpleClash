using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[SerializeField]
public class SoldierKilled: UnityEvent<Soldier>
{
}

[SerializeField]
public class SoldierClicked : UnityEvent<Soldier>
{
}

public class Soldier : MonoBehaviour, IPointerClickHandler
{
    Animator animator;

    SoldierCharacteristic characteristic;
    HQ soldierHQ;

    Weapon weapon;
    HealthBar healthBar;

    GameObject target;
    bool isTargetKilled;

    [HideInInspector] public SoldierKilled soldierKilled = new SoldierKilled();
    [HideInInspector] public SoldierClicked soldierClicked = new SoldierClicked();

    public void Initialize(SoldierType type, HQ hq)
    {
        gameObject.name = type.ToString();
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = FindObjectOfType<RunTimeAnimationContainer>().GetController(type);
        characteristic = GameManager.Instance.GetSoldiersCharacteristicsContainer().GetCharacteristics(type);
        characteristic = characteristic.Clone();

        weapon = GetComponentInChildren<Weapon>();
        weapon.Initialize(this);

        // Визуализирована полоса жизни (использован скрипт и шейдер третьей стороны)
        healthBar = FindObjectOfType<HealthBar>();
        healthBar.Initialize(this);

        // В данном случае принято решение не создавать FSM, а использовать Animator для этих целей
        
        //Изменяем параметры AnimatorController-а, в сответствии с параметрами солдата
        var states = ((AnimatorController)animator.runtimeAnimatorController).layers[0].stateMachine.states;

        foreach(ChildAnimatorState childAnimatorState in states)
        {
            foreach(AnimatorStateTransition animatorStateTransition in childAnimatorState.state.transitions)
            {
                for(int i = 0; i< animatorStateTransition.conditions.Length; i++)
                {
                    AnimatorCondition animatorCondition = animatorStateTransition.conditions[i];
                    if (animatorCondition.parameter == "DistanceToTarget")
                    {
                        animatorStateTransition.RemoveCondition(animatorCondition);
                        animatorStateTransition.AddCondition(animatorCondition.mode, characteristic.range, animatorCondition.parameter);
                    }
                }
            }
        }

        soldierHQ = hq;
    }

    public void Move(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

    public void Fire(bool isFiring)
    {
        weapon.Fire(isFiring);
    }

    public void Win()
    {
        Fire(false);
        animator.SetTrigger("Winner");
    }

    public void SetTarget(GameObject newTarget)
    {
        if (target != null)
        {
            target.GetComponent<Soldier>().soldierKilled.RemoveListener(RequestNewOrder);
        }
        animator.SetBool("isTargetExists", false);
        target = newTarget;
        isTargetKilled = false;
        target.GetComponent<Soldier>().soldierKilled.AddListener(RequestNewOrder);
    }

    public Soldier GetTarget()
    {
        return target.GetComponent<Soldier>();
    }

    /// <summary>
    ///  Получение урона
    /// </summary>
    public void GetHit(float damage)
    {
        characteristic.health -= damage;
        healthBar.UpdateBar(characteristic.health);

        if (characteristic.health <= 0)
        {
            animator.SetTrigger("Killed");
            GetComponent<Collider2D>().enabled = false;
            soldierKilled.Invoke(this);
        }
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public SoldierCharacteristic GetCharacteristic()
    {
        return characteristic;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        soldierClicked.Invoke(this);
    }

    private void FixedUpdate()
    {

        if (!isTargetKilled)
        {
            animator.SetBool("isTargetExists", true);
            float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
            animator.SetFloat("DistanceToTarget", distanceToTarget);
        }
    }

    /// <summary>
    ///  Цель отсутствует - отдать новый приказ
    /// </summary>
    private void RequestNewOrder(Soldier soldier)
    {
        isTargetKilled = true;
        animator.SetBool("isTargetExists", false);
        Fire(false);
        soldierHQ.SetFocusedSoldier(this);

        //Здесь упрощено принятие решений в случае убийства цели
        // Если солдат не игрока, то ему назначается новая случайная цель
        // Если солдат игрока, то ему назначается новая цель, в соответствии с текущим общим приказом штаба (armyOrder)

        if (soldierHQ.GetBelongingType() == BelongingType.Enemy)
        {
            soldierHQ.SendRandomOrder();
        }
        else if (soldierHQ.GetBelongingType() == BelongingType.Player)
        {
            soldierHQ.SendOrder(soldierHQ.GetArmyOrder());
        }
    }
}
