using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace BeeralopeEvents
{
    
        public class IncidentWorker_BeerbroPasses : IncidentWorker
        {
            protected override bool CanFireNowSub(IncidentParms parms)
            {
                Map map = (Map)parms.target;
                IntVec3 intVec;
                return !map.gameConditionManager.ConditionIsActive(GameConditionDefOf.ToxicFallout) && map.mapTemperature.SeasonAndOutdoorTemperatureAcceptableFor(ThingDef.Named("Beerbro")) && this.TryFindEntryCell(map, out intVec);

            throw new NotImplementedException();
            }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 intVec;
            if (!this.TryFindEntryCell(map, out intVec))
            {
                return false;
            }
            PawnKindDef pawnKindDef = PawnKindDef.Named("Beerbro");
            int num = GenMath.RoundRandom(StorytellerUtility.DefaultThreatPointsNow(map) / pawnKindDef.combatPower);
            int max = Rand.RangeInclusive(3, 6);
            num = Mathf.Clamp(num, 2, max);
            int num2 = Rand.RangeInclusive(90000, 150000);
            IntVec3 invalid = IntVec3.Invalid;
            if (!RCellFinder.TryFindRandomCellOutsideColonyNearTheCenterOfTheMap(intVec, map, 10f, out invalid))
            {
                invalid = IntVec3.Invalid;
            }
            Pawn pawn = null;
            for (int i = 0; i < num; i++)
            {
                IntVec3 loc = CellFinder.RandomClosewalkCellNear(intVec, map, 10, null);
                pawn = PawnGenerator.GeneratePawn(pawnKindDef, null);
                GenSpawn.Spawn(pawn, loc, map, Rot4.Random, WipeMode.Vanish, false);
                pawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + num2;
                if (invalid.IsValid)
                {
                    pawn.mindState.forcedGotoPosition = CellFinder.RandomClosewalkCellNear(invalid, map, 10, null);
                }
                base.SendStandardLetter("LetterLabelBeerbroPasses".Translate(pawnKindDef.label).CapitalizeFirst(), "LetterBeerbroPasses".Translate(pawnKindDef.label), LetterDefOf.PositiveEvent, parms, pawn, Array.Empty<NamedArgument>());
                return true;
            }
            return true;
        }


            private bool TryFindEntryCell(Map map, out IntVec3 cell)
            {
            return RCellFinder.TryFindRandomPawnEntryCell(out cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f, false, null);
            }
        }
    }
