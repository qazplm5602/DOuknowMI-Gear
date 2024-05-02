public class SkillChainMH : SkillController
{
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }
}