using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Skill {

	SkillType getSkillType();

	void setup(BattleManager battleManager, Character owner);
}

public enum SkillType {Passive, Active}


