using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ActiveSkill : Skill {

	void target();	

	void untarget();

	void activate();
}
