/*
 * 		entity type
 */ 
public enum ENTITY_TYPE
{
	BEGIN = 0,

	AbyssTable,
	AccountLevelTable,
	ADSDealTable,
	AttendanceTable,
	BattlePassTable,
	BoxTable,
	BoxDealTable,
	ChapterTable,
	CharacterTable,
	CharacterLevelTable,
	ContentsStringTable,
	CrystalDealTable,
	DailyDealTable,
	DescTable,
	DiceTable,
	EffectTable,
	EffectGroupTable,
	EpisodeTable,
	FieldObjectTable,
	GearTable,
	GlobalTable,
	HuntTable,
	LeagueTable,
	MaterialTable,
	MissionAdventureTable,
	MissionDefenceTable,
	MissionDefenceGroupTable,
	MoneyDealTable,
	NameTable,
	NPCTable,
	NPCGroupTable,
	NPCStatTable,
	PassDealTable,
	QuestTable,
	RecipeTable,
	RewardTable,
	RewardListTable,
	SkillTable,
	SkillGroupTable,
	UIStringTable,
	VIPDealTable,
	WeaponTable,
	WorkshopLevelTable,

	END,
}

public static partial class Extensions
{
	
	public static string[] fileName = new string[]
	{
		"BEGIN",

		"Abyss",
		"AccountLevel",
		"ADSDeal",
		"Attendance",
		"BattlePass",
		"Box",
		"BoxDeal",
		"Chapter",
		"Character",
		"CharacterLevel",       
		"ContentsString",
		"CrystalDeal",
		"DailyDeal",
		"Desc",
		"Dice",
		"Effect",
		"EffectGroup",
		"Episode",
		"FieldObject",
		"Gear",
		"Global",
		"Hunt",
		"League",
		"Material",
		"MissionAdventure",
		"MissionDefence",
		"MissionDefenceGroup",
		"MoneyDeal",
		"Name",
		"NPC",
		"NPCGroup",
		"NPCStat",
		"PassDeal",
		"Quest",
		"Recipe",
		"Reward",
		"RewardList",
		"Skill",
		"SkillGroup",
		"UIString",
		"VIPDeal",
		"Weapon",
		"WorkshopLevel",

		"END",
	};   

	     
	static string[] types = System.Enum.GetNames(typeof(ENTITY_TYPE));
	static Extensions()
	{
		for (int i = 0; i < types.Length; ++i)
		{
			types[i] = types[i].ToString();
		}
	}
	
	
	public static string FileName(this ENTITY_TYPE type)
	{
		return fileName[(int)type];
	}
	
	public static string ClassName(this ENTITY_TYPE type)
	{
		return types[(int)type];
	}

	public static string TypeName(this ENTITY_TYPE type)
	{
		return types[(int)type];
	}
}