using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace BeeralopeEvents
{
    [DefOf]
    public static class InternalDefOf
    {
        public static PawnKindDef Beerbro;

        static InternalDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(InternalDefOf));
        }
    }
        public class IncidentWorker_BeerbroPasses : IncidentWorker
        {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 cell;
            return !map.gameConditionManager.ConditionIsActive(GameConditionDefOf.ToxicFallout) && map.mapTemperature.SeasonAndOutdoorTemperatureAcceptableFor(InternalDefOf.Beerbro.race) && TryFindEntryCell(map, out cell);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            if (!TryFindEntryCell(map, out var cell))
            {
                return false;
            }
            PawnKindDef beerbro = InternalDefOf.Beerbro;
            int value = GenMath.RoundRandom(StorytellerUtility.DefaultThreatPointsNow(map) / beerbro.combatPower);
            int max = Rand.RangeInclusive(3, 6);
            value = Mathf.Clamp(value, 2, max);
            int num = Rand.RangeInclusive(90000, 150000);
            IntVec3 result = IntVec3.Invalid;
            if (!RCellFinder.TryFindRandomCellOutsideColonyNearTheCenterOfTheMap(cell, map, 10f, out result))
            {
                result = IntVec3.Invalid;
            }
            Pawn pawn = null;
            for (int i = 0; i < value; i++)
            {
                IntVec3 loc = CellFinder.RandomClosewalkCellNear(cell, map, 10);
                pawn = PawnGenerator.GeneratePawn(beerbro);
                GenSpawn.Spawn(pawn, loc, map, Rot4.Random);
                pawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + num;
                if (result.IsValid)
                {
                    pawn.mindState.forcedGotoPosition = CellFinder.RandomClosewalkCellNear(result, map, 10);
                }
            }
            Find.LetterStack.ReceiveLetter("LetterLabelBeerbroPasses".Translate(beerbro.label), "LetterBeerbroPasses".Translate(beerbro.label), LetterDefOf.PositiveEvent, pawn);
            return true;
        }


            private bool TryFindEntryCell(Map map, out IntVec3 cell)
            {
                return RCellFinder.TryFindRandomPawnEntryCell(out cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f);
            }
        }
    }
