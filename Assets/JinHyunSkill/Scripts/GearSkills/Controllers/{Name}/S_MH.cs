public class SkillMH : SkillController
{
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }
}
