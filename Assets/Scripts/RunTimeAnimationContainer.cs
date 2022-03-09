using UnityEngine;

// �������� �� ��, ��� ���������� ���� RuntimeAnimatorController-�� ����������,
// ���������� ��������� ��, ����� ���� ����������� �������� �� ���������, � ����������� �� ������������ ��������
// + ��� ����, ��� ��  � �������, ��� ������� ���� �������� ��������� ��������� �������� 

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
