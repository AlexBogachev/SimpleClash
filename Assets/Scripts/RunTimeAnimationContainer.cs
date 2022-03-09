using UnityEngine;

// Ќесмотр€ на то, что реализаци€ всех RuntimeAnimatorController-ов одинакова€,
// необходимо разделить их, чтобы была возможность измен€ть их параметры, в зависимости от характерстик солдатов
// + дл€ того, что бы  в будущем, дл€ каждого типа солдатов назначить отдельную анимацию 

public class RunTimeAnimationContainer : MonoBehaviour
{
    [SerializeField] RuntimeAnimatorController bazookaController;
    [SerializeField] RuntimeAnimatorController sniperController;
    [SerializeField] RuntimeAnimatorController machinegunnerController;

    public RuntimeAnimatorController GetController(SoldierType type)
    {
        switch (type)
        {
            case SoldierType.Bazooka:
                return bazookaController;
            case SoldierType.Sniper:
                return sniperController;
            case SoldierType.Machinegunner:
                return machinegunnerController;
        }
        return null;
    }

}
