public class SkillSH : SkillController
{
    private void Start()
    {
        SoundManager.Instance.PlaySound(SoundType.ThrowBNN);
        StartCoroutine(MoveRoutine(transform));
    }
}
