using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oasys.Common.GameObject.Clients.ExtendedInstances.Spells;
using Oasys.Common.Enums.GameEnums;
using SharpDX;

namespace Oasys.SDK.SpellCasting
{
    /// <summary>
    /// Provides spell casting emulation.
    /// </summary>
    public class SpellCastProvider
    {
        /// <summary>
        /// Casts a spell at/towards the position of the mouse.
        /// </summary>
        /// <param name="slot"></param>
        public static void CastSpell(CastSlot slot)
        {
            SpellBook.CastSpell((SpellCastSlot)slot);
        }

        /// <summary>
        /// Casts a spell at/towards the given vector3 position parameter
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="objectPosition"></param>
        public static void CastSpell(CastSlot slot, Vector3 objectPosition)
        {
            SpellBook.CastSpell((SpellCastSlot)slot, objectPosition);
        }

        /// <summary>
        /// Casts a spell at/towards the given vector2 position parameter.
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="screenPosition"></param>
        public static void CastSpell(CastSlot slot, Vector2 screenPosition)
        {
            SpellBook.CastSpell((SpellCastSlot)slot, screenPosition);
        }

        /// <summary>
        /// Casts multiple spells at/towards the position of the mouse.
        /// </summary>
        /// <param name="slotArray"></param>
        public static void CastMultiSpell(CastSlot[] slotArray)
        {
            List<SpellCastSlot> convSCSArray = new List<SpellCastSlot>();

            foreach (var slot in slotArray)
                convSCSArray.Add((SpellCastSlot)slot);

            SpellBook.CastMultiSpell(convSCSArray.ToArray());
        }

        /// <summary>
        /// Casts multiple spells at/towards the given vector3 position parameter.
        /// </summary>
        /// <param name="slotArray"></param>
        /// <param name="objectPosition"></param>
        public static void CastMultiSpell(CastSlot[] slotArray, Vector3 objectPosition)
        {
            List<SpellCastSlot> convSCSArray = new List<SpellCastSlot>();

            foreach (var slot in slotArray)
                convSCSArray.Add((SpellCastSlot)slot);

            SpellBook.CastMultiSpell(convSCSArray.ToArray(), objectPosition);
        }

        /// <summary>
        /// Casts multiple spells at/towards the given vector2 position parameter.
        /// </summary>
        /// <param name="slotArray"></param>
        /// <param name="screenPosition"></param>
        public static void CastMultiSpell(CastSlot[] slotArray, Vector2 screenPosition)
        {
            List<SpellCastSlot> convSCSArray = new List<SpellCastSlot>();

            foreach (var slot in slotArray)
                convSCSArray.Add((SpellCastSlot)slot);

            SpellBook.CastMultiSpell(convSCSArray.ToArray(), screenPosition);
        }

        /// <summary>
        /// Casts multiple spells at/towards the given each vector3 position array in a tuple.
        /// </summary>
        /// <param name="respectiveSpells"></param>
        public static void CastMultiSpell(Tuple<CastSlot, Vector3>[] respectiveSpells)
        {
            List<Tuple<SpellCastSlot, Vector3>> convRespSpellArray = new List<Tuple<SpellCastSlot, Vector3>>();

            foreach (var sp in respectiveSpells)
                convRespSpellArray.Add(new Tuple<SpellCastSlot, Vector3>((SpellCastSlot)sp.Item1, sp.Item2));

            SpellBook.CastMultiSpell(convRespSpellArray.ToArray());
        }

        /// <summary>
        /// Casts multiple spells at/towards the given each vector2 position array in a tuple.
        /// </summary>
        /// <param name="respectiveSpells"></param>
        public static void CastMultiSpell(Tuple<CastSlot, Vector2>[] respectiveSpells)
        {
            List<Tuple<SpellCastSlot, Vector2>> convRespSpellArray = new List<Tuple<SpellCastSlot, Vector2>>();

            foreach (var sp in respectiveSpells)
                convRespSpellArray.Add(new Tuple<SpellCastSlot, Vector2>((SpellCastSlot)sp.Item1, sp.Item2));

            SpellBook.CastMultiSpell(convRespSpellArray.ToArray());
        }
    }

    public enum CastSlot
    {
        Q = SpellCastSlot.Q,
        W = SpellCastSlot.W,
        E = SpellCastSlot.E,
        R = SpellCastSlot.R,
        Summoner1 = SpellCastSlot.Summoner1,
        Summoner2 = SpellCastSlot.Summoner2
    }
}
