using Verse;
using RimWorld;
using Verse.AI.Group;

namespace BeeralopeActions
{
    public class DeathActionWorker_BeerExtremeExplosion : DeathActionWorker

    {
        public override RulePackDef DeathRules
        {
            get
            {
                return RulePackDefOf.Transition_DiedExplosive;
            }
        }
        public override bool DangerousInMelee
        {
            get
            {
                return true;
            }
        }
        public override void PawnDied(Corpse corpse, Lord prevLord)
        {
            float radius;
            if (corpse.InnerPawn.ageTracker.CurLifeStageIndex == 0)
            {
                radius = 1.9f;
            }
            else if (corpse.InnerPawn.ageTracker.CurLifeStageIndex == 1)
            {
                radius = 3.9f;
            }
            else
            {
                radius = 8.9f;
            }
            GenExplosion.DoExplosion(corpse.Position, corpse.Map, radius, DamageDefOf.Flame, corpse.InnerPawn, -1, -1f, null, null, null, null, null, 0f, 1, null, false, null, 0f, 1, 0f, false, null, null, null, true, 1f, 0f, true, null, 1f, null, null);
        }
    }
}
