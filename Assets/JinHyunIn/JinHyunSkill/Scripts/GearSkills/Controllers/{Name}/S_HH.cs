public class SkillHH : SkillController
{
    private void Start()
    {
        SoundManager.Instance.PlaySound(SoundType.ThrowBNN);
        StartCoroutine(MoveRoutine(transform));
    }
}
