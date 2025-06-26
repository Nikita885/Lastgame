using UnityEngine;
using Cinemachine;

public class PortalTrigger : MonoBehaviour
{
    [Header("Игроки")]
    public string playerTag = "Player";

    [Header("Отключаемые объекты")]
    public GameObject[] objectsToDisable;

    [Header("Включаемые объекты")]
    public GameObject[] objectsToEnable;

    [Header("Cinemachine TargetGroup")]
    public CinemachineTargetGroup targetGroup;
    public Transform[] newTargets; // новые цели для камеры

    public float targetWeight = 1f;
    public float targetRadius = 2f;

    [Header("Барьер")]
    public GameObject barrier;

    private bool triggered = false;

    // 🔐 Сохраняем оригинальное состояние группы
    private CinemachineTargetGroup.Target[] originalTargetGroupState;

    private void Start()
    {
        if (targetGroup != null)
        {
            originalTargetGroupState = new CinemachineTargetGroup.Target[targetGroup.m_Targets.Length];
            for (int i = 0; i < targetGroup.m_Targets.Length; i++)
            {
                originalTargetGroupState[i].target = targetGroup.m_Targets[i].target;
                originalTargetGroupState[i].weight = targetGroup.m_Targets[i].weight;
                originalTargetGroupState[i].radius = targetGroup.m_Targets[i].radius;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered || !other.CompareTag(playerTag)) return;

        triggered = true;

        // Выключаем старые
        foreach (GameObject obj in objectsToDisable)
            if (obj != null) obj.SetActive(false);

        // Включаем новые
        foreach (GameObject obj in objectsToEnable)
            if (obj != null) obj.SetActive(true);

        // Заменяем цели камеры
        if (targetGroup != null && newTargets.Length > 0)
        {
            var newGroupTargets = new CinemachineTargetGroup.Target[newTargets.Length];
            for (int i = 0; i < newTargets.Length; i++)
            {
                newGroupTargets[i].target = newTargets[i];
                newGroupTargets[i].weight = targetWeight;
                newGroupTargets[i].radius = targetRadius;
            }
            targetGroup.m_Targets = newGroupTargets;
        }

        Debug.Log("Портал активирован");
    }

    // 📦 Метод сброса состояния при победе
    public void ResetPortalState()
    {
        // Вернём отключённые объекты
        foreach (GameObject obj in objectsToDisable)
            if (obj != null) obj.SetActive(true);

        // Спрячем включённые ранее
        foreach (GameObject obj in objectsToEnable)
            if (obj != null) obj.SetActive(false);

        // Вернём оригинальные цели камеры
        if (targetGroup != null && originalTargetGroupState != null)
        {
            targetGroup.m_Targets = new CinemachineTargetGroup.Target[originalTargetGroupState.Length];
            for (int i = 0; i < originalTargetGroupState.Length; i++)
            {
                targetGroup.m_Targets[i] = originalTargetGroupState[i];
            }
        }

        // Отключаем барьер
        if (barrier != null)
        {
            barrier.SetActive(false);
        }

        Debug.Log("🏁 Победа: всё возвращено на место и барьер снят");
    }
}
