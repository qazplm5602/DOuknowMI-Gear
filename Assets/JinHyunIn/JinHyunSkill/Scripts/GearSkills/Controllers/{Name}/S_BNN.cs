public class BNNSkill : SkillController
{
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }
}
