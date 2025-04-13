using Drafts.DataView;
using TMPro;
using UnityEngine;

namespace Meowgic.Match.UI
{
    /// <summary>Player cat and enemies</summary>
    public class ActorView : DataView<Actor>
    {
        [SerializeField] private TMP_Text nickname;
        [SerializeField] private IObservableView maxHealth;
        [SerializeField] private IObservableView health;

        protected override void Subscribe()
        {
            nickname.TrySetText(Data.DisplayName);
            maxHealth.TrySetData(Data.MaxHealth);
            health.TrySetData(Data.Health);
        }

        protected override void Unsubscribe()
        {
            nickname.TrySetText(null);
            maxHealth.TrySetData(null);
            health.TrySetData(null);
        }
    }
}