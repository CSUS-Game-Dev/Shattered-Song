using System.Collections;
using System.Collections.Generic;

public class CharacterStats {

	public static int NUMBER_OF_STATS = 6;
	public static int DEFAULT_STAT_VALUE = 20;
	public static string DEFAULT_NAME = "EMPTY_NAME";


	private string characterName;
	private string screenName;

	public List<Stat> stats;
	public Stat hp, atk, def, mag, wil, spd, tmp;

	public CharacterStats(){
		stats = new List<Stat>();
		initializeStatsDefault();

	}

	public CharacterStats(int[] values, string charName, string scrnName){
		stats = new List<Stat>();
		initializeStatsSpecific(values, charName, scrnName);
	}
	
	public CharacterStats(JSONObject characterFile){
		stats = new List<Stat>();
		if(characterFile.HasField("file_type") && characterFile.GetField("file_type").str == "Character"){
			initializeStatsSpecific(characterFile);
		}
		else{
			initializeStatsDefault();
		}
	}

	private void initializeStatsSpecific(JSONObject obj){
		characterName = obj.GetField("character_name").str;
		screenName = obj.GetField("character_screen_name").str;

		JSONObject statsObject = obj.GetField("character_stats");

		hp = new Stat("hp", int.Parse(statsObject.GetField("hp").str));
		atk = new Stat("atk", int.Parse(statsObject.GetField("atk").str));
		def = new Stat("def", int.Parse(statsObject.GetField("def").str));
		mag = new Stat("mag", int.Parse(statsObject.GetField("mag").str));
		wil = new Stat("wil", int.Parse(statsObject.GetField("wil").str));
		spd = new Stat("spd", int.Parse(statsObject.GetField("spd").str));
		tmp = new Stat("tmp", int.Parse(statsObject.GetField("tmp").str));
		stats.Add(hp);
		stats.Add(atk);
		stats.Add(def);
		stats.Add(mag);
		stats.Add(wil);
		stats.Add(spd);
		stats.Add(tmp);
	}

	private void initializeStatsSpecific(int[] values, string charName, string scrnName){
		if(values.Length == NUMBER_OF_STATS){
			hp = new Stat("hp", values[0]);
			atk = new Stat("atk", values[1]);
			def = new Stat("def", values[2]);
			mag = new Stat("mag", values[3]);
			wil = new Stat("wil", values[4]);
			tmp = new Stat("tmp", values[5]);
			stats.Add(hp);
			stats.Add(atk);
			stats.Add(def);
			stats.Add(mag);
			stats.Add(wil);
			stats.Add(tmp);
		}
		else{
			initializeStatsDefault();
		}

		characterName = charName;
		screenName = scrnName;
	}

	private void initializeStatsDefault(){
		hp = new Stat("hp", DEFAULT_STAT_VALUE);
		atk = new Stat("atk", DEFAULT_STAT_VALUE);
		def = new Stat("def", DEFAULT_STAT_VALUE);
		mag = new Stat("mag", DEFAULT_STAT_VALUE);
		wil = new Stat("wil", DEFAULT_STAT_VALUE);
		tmp = new Stat("tmp", DEFAULT_STAT_VALUE);
		stats.Add(hp);
		stats.Add(atk);
		stats.Add(def);
		stats.Add(mag);
		stats.Add(wil);
		stats.Add(tmp);

		characterName = DEFAULT_NAME;
		screenName = DEFAULT_NAME;
	}

	public override string ToString(){
		string str = "";
		str += "CharName: " + characterName + " ScreenName: " + screenName + "\n";
		str += hp.ToString() + " " + atk.ToString() + " " + def.ToString() + " " + mag.ToString() + " " + wil.ToString() + " " + tmp.ToString();

		return str;
	}

}
