public class SkillHH : SkillController
{
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }
}
