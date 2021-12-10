namespace AI
{
    public interface IAITrigger
    {
        public void OnEndAction();

        public void OnSpotEnemy();

        public void OnLostTarget();

        public void OnTargetDead();

        public void OnMeetInteractable();

        public void OnDamaged();

        public void OnShieldOut();
    }
}