﻿using LeagueSandbox.GameServer.GameObjects;
using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using GameServerCore.Enums;

namespace Spells
{
    public class DeathfireGrasp : IGameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            spell.AddProjectileTarget("DeathfireGraspSpell",target);
            var p1 = ApiFunctionManager.AddParticleTarget(owner, "deathFireGrasp_tar.troy", target);
            var p2 = ApiFunctionManager.AddParticleTarget(owner, "obj_DeathfireGrasp_debuff.troy", target);
            ApiFunctionManager.AddBuffHudVisual("DeathfireGraspSpell", 4.0f, 1, BuffType.COMBAT_DEHANCER,
                (ObjAiBase)target, 4.0f);
            ApiFunctionManager.CreateTimer(4.0f, () =>
            {
                ApiFunctionManager.RemoveParticle(p1);
                ApiFunctionManager.RemoveParticle(p2);
            });
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            var damage = target.Stats.HealthPoints.Total * 0.15f;
            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SUMMONER_SPELL, false);
            projectile.SetToRemove();
        }

        public void OnUpdate(double diff)
        {
        }

        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }
    }
}
