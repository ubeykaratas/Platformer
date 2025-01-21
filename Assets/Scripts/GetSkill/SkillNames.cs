using System;
using System.Collections.Generic;
using UnityEngine;

public enum SkillNames
{
    PlayerWallSlide,
    PlayerWallJump,
    PlayerDash,
    PlayerDoubleJump,
    SlowMotion,
    Firewall,
}

public static class SkillNamesExtensions
{
    private static readonly Dictionary<SkillNames, Type> skillTypeMap = new Dictionary<SkillNames, Type>
    {
        { SkillNames.PlayerWallSlide, typeof(PlayerWallSlide) },
        { SkillNames.PlayerWallJump, typeof(PlayerWallJump) },
        { SkillNames.PlayerDash, typeof(PlayerDash) },
        { SkillNames.PlayerDoubleJump, typeof(PlayerDoubleJump) },
        { SkillNames.SlowMotion, typeof(SlowMotion) },
        { SkillNames.Firewall, typeof(Firewall) },
    };

    public static Type GetSkillType(this SkillNames skillName)
    {
        skillTypeMap.TryGetValue(skillName, out Type skillType);
        return skillType;
    }
}