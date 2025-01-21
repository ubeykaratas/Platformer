using UnityEngine;

public class GetSkill : MonoBehaviour
{
    [SerializeField] SkillNames skillName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            System.Type skillType = skillName.GetSkillType();
            if (player.GetComponent(skillType) != null)
            {
                if(player.GetComponent(skillType) is  Behaviour behaviour)
                {
                    behaviour.enabled = false;
                    behaviour.enabled = true;
                }
            }
            else if (player.GetComponent(skillType) == null)
            {
                player.gameObject.AddComponent(skillType);
            }
            else
            {
                Debug.LogWarning($"Skill type not found for: {skillName}");
            }
            Destroy(gameObject);
        }
    }

}
