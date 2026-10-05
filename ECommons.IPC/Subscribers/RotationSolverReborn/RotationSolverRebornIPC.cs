using ECommons.EzIpcManager;
using System;
using System.ComponentModel;
using static ECommons.IPC.Subscribers.RotationSolverReborn.RotationSolverRebornIPC.Delegates;

namespace ECommons.IPC.Subscribers.RotationSolverReborn;

public sealed class RotationSolverRebornIPC : IPCBase
{
	public RotationSolverRebornIPC()
	{
	}

	public RotationSolverRebornIPC(SafeWrapper wrapper) : base(wrapper)
	{
	}

	public override string InternalName { get; } = "RotationSolver";

    public override string IPCPrefix { get; } = "RotationSolverReborn";

    /// <summary>
	/// The type of targeting.
	/// </summary>
	public enum TargetingType
	{
		/// <summary>
		/// Find the target whose hit box is biggest.
		/// </summary>
		[Description("Big")]
		Big,

		/// <summary>
		/// Find the target whose hit box is smallest.
		/// </summary>
		[Description("Small")]
		Small,

		/// <summary>
		/// Find the target whose HP is highest.
		/// </summary>
		[Description("High HP")]
		HighHP,

		/// <summary>
		/// Find the target whose HP is lowest.
		/// </summary>
		[Description("Low HP")]
		LowHP,

		/// <summary>
		/// Find the target whose HP percentage is highest.
		/// </summary>
		[Description("High HP%")]
		HighHPPercent,

		/// <summary>
		/// Find the target whose HP percentage is lowest.
		/// </summary>
		[Description("Low HP%")]
		LowHPPercent,

		/// <summary>
		/// Find the target whose max HP is highest.
		/// </summary>
		[Description("High Max HP")]
		HighMaxHP,

		/// <summary>
		/// Find the target whose max HP is lowest.
		/// </summary>
		[Description("Low Max HP")]
		LowMaxHP,

		/// <summary>
		/// Find the target that is nearest.
		/// </summary>
		[Description("Nearest")]
		Nearest,

		/// <summary>
		/// Find the target that is farthest.
		/// </summary>
		[Description("Farthest")]
		Farthest,

		/// <summary>
		/// PVP: Find the nearest Healer.
		/// </summary>
		[Description("Focus Healers in PvP")]
		PvPHealers,

		/// <summary>
		/// PVP: Find the nearest Tank.
		/// </summary>
		[Description("Focus Tanks in PvP")]
		PvPTanks,

		/// <summary>
		/// PVP: Find the nearest DPS.
		/// </summary>
		[Description("Focus DPS in PvP")]
		PvPDPS
	}

	/// <summary>
	/// Hostile target.
	/// </summary>
	public enum TargetHostileType : byte
	{
		/// <summary>
		/// All targets that are in range for any abilities (Tanks/Autoduty).
		/// </summary>
		[Description("All targets that are in range for any abilities (Tanks/Autoduty)")]
		AllTargetsCanAttack,

		/// <summary>
		/// Previously engaged targets (Non-Tanks).
		/// </summary>
		[Description("Previously engaged targets (Non-Tanks)")]
		TargetsHaveTarget,

		/// <summary>
		/// All targets when solo in duty, or previously engaged.
		/// </summary>
		[Description("All targets when solo in duty (includes Occult Crescent), or previously engaged.")]
		AllTargetsWhenSoloInDuty,

		/// <summary>
		/// All targets when solo, or previously engaged.
		/// </summary>
		[Description("All targets when solo, or previously engaged.")]
		AllTargetsWhenSolo,

		/// <summary>
		/// Solo Deep Dungeons: out of combat pull the nearest single enemy; in combat only previously engaged.
		/// </summary>
		[Description("Solo Deep Dungeons: if solo, out of combat pull the nearest single enemy; in combat only previously engaged.")]
		SoloDeepDungeonSmart,
	}

	/// <summary>
	/// How an IPC caller wants a boolean setting handled while the operating mode it requested is active.
	/// </summary>
	public enum SettingOverride : byte
	{
		/// <summary>
		/// Keep the user's own setting.
		/// </summary>
		UseSetting,

		/// <summary>
		/// Force the setting off.
		/// </summary>
		ForceOff,

		/// <summary>
		/// Force the setting on.
		/// </summary>
		ForceOn,
	}

	/// <summary>
	/// Represents the positional relationship of an enemy.
	/// </summary>
	public enum EnemyPositional : byte
	{
		/// <summary>
		/// No specific positional relationship.
		/// </summary>
		None,

		/// <summary>
		/// In the rear of the enemy.
		/// </summary>
		Rear,

		/// <summary>
		/// In the flank of the enemy.
		/// </summary>
		Flank,

		/// <summary>
		/// In front of the enemy.
		/// </summary>
		Front,
	}

	/// <summary>
	/// Special State.
	/// </summary>
	public enum SpecialCommandType : byte
	{
		/// <summary>
		/// To end this special duration before the set time.
		/// </summary>
		[Description("To end this special duration before the set time.")]
		EndSpecial,

		/// <summary>
		/// Open a window to use AoE heal.
		/// </summary>
		[Description("Open a window to use AoE heal.")]
		HealArea,

		/// <summary>
		/// Open a window to use single heal.
		/// </summary>
		[Description("Open a window to use single heal.")]
		HealSingle,

		/// <summary>
		/// Open a window to use AoE defense.
		/// </summary>
		[Description("Open a window to use AoE defense.")]
		DefenseArea,

		/// <summary>
		/// Open a window to use single defense.
		/// </summary>
		[Description("Open a window to use single defense.")]
		DefenseSingle,

		/// <summary>
		/// Open a window to use Esuna, tank stance actions or True North.
		/// </summary>
		[Description("Open a window to use Esuna, tank stance actions or True North.")]
		DispelStancePositional,

		/// <summary>
		/// Open a window to use Raise or Shirk.
		/// </summary>
		[Description("Open a window to use Raise or Shirk.")]
		RaiseShirk,

		/// <summary>
		/// Open a window to move forward.
		/// </summary>
		[Description("Open a window to move forward.")]
		MoveForward,

		/// <summary>
		/// Open a window to move back.
		/// </summary>
		[Description("Open a window to move back.")]
		MoveBack,

		/// <summary>
		/// Open a window to use knockback immunity actions.
		/// </summary>
		[Description("Open a window to use knockback immunity actions.")]
		AntiKnockback,

		/// <summary>
		/// Open a window to burst.
		/// </summary>
		[Description("Open a window to burst.")]
		Burst,

		/// <summary>
		/// Open a window to speed up.
		/// </summary>
		[Description("Open a window to speed up.")]
		Speed,

		/// <summary>
		/// Open a window to use limit break.
		/// </summary>
		[Description("Open a window to use limit break.")]
		LimitBreak,

		/// <summary>
		/// Open a window to do not use the casting action.
		/// </summary>
		[Description("Open a window to do not use the casting action.")]
		NoCasting,

		/// <summary>
		/// Intercepting action.
		/// </summary>
		[Description("Indicator for when RSR is intercepting action.")]
		Intercepting,
	}

	/// <summary>
	/// The state of the plugin.
	/// </summary>
	public enum StateCommandType : byte
	{
		/// <summary>
		/// Stop the addon. Always remember to turn it off when it is not in use!
		/// </summary>
		[Description("Stop the addon. Always remember to turn it off when it is not in use!")]
		Off,

		/// <summary>
		/// Start the addon in Auto mode. When out of combat or when combat starts, switches the target according to the set condition.
		/// </summary>
		[Description("Start the addon in Auto mode. When out of combat or when combat starts, switches the target according to the set condition. " +
			"\r\n Optionally: You can add the target type to the end of the command you want RSR to do. For example: /rotation Auto Big")]
		Auto,

		/// <summary>
		/// Start the addon in Target-Only mode. RSR will auto-select targets per normal logic but will not perform any actions.
		/// </summary>
		[Description("Start in Target-Only mode. RSR will auto-select targets per normal logic but will not perform any actions.")]
		TargetOnly,

		/// <summary>
		/// Start the addon in Manual mode. You need to choose the target manually. This will bypass any engage settings that you have set up and will start attacking immediately once something is targeted.
		/// </summary>
		[Description("Start the addon in Manual mode. You need to choose the target manually. This will bypass any engage settings that you have set up and will start attacking immediately once something is targeted.")]
		Manual,

		/// <summary>
		/// 
		/// </summary>
		[Description("This mode is managed by the Autoduty plugin")]
		AutoDuty,

		/// <summary>
		/// 
		/// </summary>
		[Description("This mode is managed by the Henchman plugin, or any other plugin that requires RSR just do rotation and not targetting.")]
		Henched,

		/// <summary>
		/// 
		/// </summary>
		[Description("Optional mode for PvP specific activities.")]
		PvP,
	}

	/// <summary>
	/// Some Other Commands.
	/// </summary>
	public enum OtherCommandType : byte
	{
		/// <summary>
		/// Open the settings.
		/// </summary>
		[Description("Open the settings.")]
		Settings,

		/// <summary>
		/// Open the rotations.
		/// </summary>
		[Description("Open the rotations.")]
		Rotations,

		/// <summary>
		/// Open the rotations.
		/// </summary>
		[Description("Open the duty rotations.")]
		DutyRotations,

		/// <summary>
		/// Perform the actions.
		/// </summary>
		[Description("Perform the actions.")]
		DoActions,

		/// <summary>
		/// Toggle the actions.
		/// </summary>
		[Description("Toggle the actions.")]
		ToggleActions,

		/// <summary>
		/// Do the next action.
		/// </summary>
		[Description("Do the next action.")]
		NextAction,

		/// <summary>
		/// Cycles between states following settings in Target > Configuration.
		/// </summary>
		[Description("Cycles between states following settings in Target > Configuration.")]
		Cycle,

		/// <summary>
		/// Open the autorotation state window with the Auto, Manual and Off buttons.
		/// </summary>
		[Description("Open the autorotation state window with the Auto, Manual and Off buttons.")]
		Control,

		/// <summary>
		/// Open the update notes.
		/// </summary>
		[Description("Open the update notes.")]
		Changelog,
	}

	/// <summary>
	/// Test hook used to verify IPC connectivity with RotationSolver.
	/// </summary>
	/// <remarks>
	/// Delegate signature: <see cref="RotationSolverRebornIPC.Delegates.TestDelegate"/> (string <c>param</c>).
	/// </remarks>
	[EzIPC] public TestDelegate Test { get; private set; }

    /// <summary>
    /// Adds a target <c>NameID</c> to RotationSolver's priority list.
    /// </summary>
    /// <remarks>
    /// Delegate signature: <see cref="RotationSolverRebornIPC.Delegates.NameIdDelegate"/> (uint <c>nameId</c>).
    /// </remarks>
    [EzIPC] public NameIdDelegate AddPriorityNameID { get; private set; }

    /// <summary>
    /// Removes a target <c>NameID</c> from RotationSolver's priority list.
    /// </summary>
    /// <remarks>
    /// Delegate signature: <see cref="RotationSolverRebornIPC.Delegates.NameIdDelegate"/> (uint <c>nameId</c>).
    /// </remarks>
    [EzIPC] public NameIdDelegate RemovePriorityNameID { get; private set; }

    /// <summary>
    /// Adds a target <c>NameID</c> to RotationSolver's blacklist.
    /// </summary>
    /// <remarks>
    /// Delegate signature: <see cref="RotationSolverRebornIPC.Delegates.NameIdDelegate"/> (uint <c>nameId</c>).
    /// </remarks>
    [EzIPC] public NameIdDelegate AddBlacklistNameID { get; private set; }

    /// <summary>
    /// Removes a target <c>NameID</c> from RotationSolver's blacklist.
    /// </summary>
    /// <remarks>
    /// Delegate signature: <see cref="RotationSolverRebornIPC.Delegates.NameIdDelegate"/> (uint <c>nameId</c>).
    /// </remarks>
    [EzIPC] public NameIdDelegate RemoveBlacklistNameID { get; private set; }

    /// <summary>
    /// Changes RotationSolver's operating state.
    /// </summary>
    /// <remarks>
    /// Delegate signature: <see cref="RotationSolverRebornIPC.Delegates.ChangeOperatingModeDelegate"/> (<see cref="RotationSolverRebornIPC.StateCommandType"/> <c>stateCommand</c>).
    /// </remarks>
    [EzIPC] public ChangeOperatingModeDelegate ChangeOperatingMode { get; private set; }

    /// <summary>
    /// Changes RotationSolver's operating state and temporarily overrides some settings without changing the user's config.
    /// The overrides are cleared by any later state change, including RSR turning Off (manually, from a timeout, or via IPC).
    /// </summary>
    /// <remarks>
    /// Delegate signature: <see cref="RotationSolverRebornIPC.Delegates.ChangeOperatingModeWithOverridesDelegate"/> (<see cref="RotationSolverRebornIPC.StateCommandType"/> <c>stateCommand</c>, <see cref="RotationSolverRebornIPC.TargetHostileType"/> <c>targetHostileType</c>, <see cref="RotationSolverRebornIPC.SettingOverride"/> <c>targetFreely</c>, <see cref="RotationSolverRebornIPC.SettingOverride"/> <c>autoOffAfterCombat</c>, <see cref="RotationSolverRebornIPC.SettingOverride"/> <c>friendlyPartyNpcHealRaise</c>).
    /// <c>autoOffAfterCombat</c> has no effect in AutoDuty and Henched modes, which never turn off after combat.
    /// </remarks>
    [EzIPC] public ChangeOperatingModeWithOverridesDelegate ChangeOperatingModeWithOverrides { get; private set; }

    /// <summary>
    /// Changes operating state and targeting rule, typically used by Autoduty integrations.
    /// </summary>
    /// <remarks>
    /// Delegate signature: <see cref="RotationSolverRebornIPC.Delegates.AutodutyChangeOperatingModeDelegate"/> (<see cref="RotationSolverRebornIPC.StateCommandType"/> <c>stateCommand</c>, <see cref="RotationSolverRebornIPC.TargetingType"/> <c>targetingType</c>).
    /// </remarks>
    [EzIPC] public AutodutyChangeOperatingModeDelegate AutodutyChangeOperatingMode { get; private set; }

    /// <summary>
    /// Same as <see cref="AutodutyChangeOperatingMode"/>, but also temporarily overrides some settings without changing the user's config.
    /// The overrides are cleared by any later state change, including RSR turning Off (manually, from a timeout, or via IPC).
    /// </summary>
    /// <remarks>
    /// Delegate signature: <see cref="RotationSolverRebornIPC.Delegates.AutodutyChangeOperatingModeWithOverridesDelegate"/> (<see cref="RotationSolverRebornIPC.StateCommandType"/> <c>stateCommand</c>, <see cref="RotationSolverRebornIPC.TargetingType"/> <c>targetingType</c>, <see cref="RotationSolverRebornIPC.TargetHostileType"/> <c>targetHostileType</c>, <see cref="RotationSolverRebornIPC.SettingOverride"/> <c>targetFreely</c>, <see cref="RotationSolverRebornIPC.SettingOverride"/> <c>autoOffAfterCombat</c>, <see cref="RotationSolverRebornIPC.SettingOverride"/> <c>friendlyPartyNpcHealRaise</c>).
    /// <c>autoOffAfterCombat</c> has no effect in AutoDuty and Henched modes, which never turn off after combat.
    /// </remarks>
    [EzIPC] public AutodutyChangeOperatingModeWithOverridesDelegate AutodutyChangeOperatingModeWithOverrides { get; private set; }

    /// <summary>
    /// Triggers a special state window (e.g., healing, movement, burst).
    /// </summary>
    /// <remarks>
    /// Delegate signature: <see cref="RotationSolverRebornIPC.Delegates.TriggerSpecialStateDelegate"/> (<see cref="RotationSolverRebornIPC.SpecialCommandType"/> <c>specialCommand</c>).
    /// </remarks>
    [EzIPC] public TriggerSpecialStateDelegate TriggerSpecialState { get; private set; }

    /// <summary>
    /// Triggers a special state window with a specific duration, overriding the configured default.
    /// </summary>
    /// <remarks>
    /// Delegate signature: <see cref="RotationSolverRebornIPC.Delegates.TriggerSpecialStateWithDurationDelegate"/> (<see cref="RotationSolverRebornIPC.SpecialCommandType"/> <c>specialCommand</c>, float <c>duration</c> in seconds).
    /// </remarks>
    [EzIPC] public TriggerSpecialStateWithDurationDelegate TriggerSpecialStateWithDuration { get; private set; }

    /// <summary>
    /// Executes an auxiliary command or opens a RotationSolver UI panel.
    /// </summary>
    /// <remarks>
    /// Delegate signature: <see cref="RotationSolverRebornIPC.Delegates.OtherCommandDelegate"/> (<see cref="RotationSolverRebornIPC.OtherCommandType"/> <c>otherType</c>, string <c>str</c>).
    /// </remarks>
    [EzIPC] public OtherCommandDelegate OtherCommand { get; private set; }

    /// <summary>
    /// Requests execution of a specific action for a time window.
    /// </summary>
    /// <remarks>
    /// Delegate signature: <see cref="RotationSolverRebornIPC.Delegates.ActionCommandDelegate"/> (string <c>action</c>, float <c>time</c> in seconds).
    /// </remarks>
    [EzIPC] public ActionCommandDelegate ActionCommand { get; private set; }

    /// <summary>
    /// Temporarily enables the TargetFreely behaviour without changing the user's config.
    /// Call <see cref="DisableTargetFreelyOverride"/> to revert.
    /// </summary>
    [EzIPC] public Action EnableTargetFreelyOverride { get; private set; }

    /// <summary>
    /// Disables the IPC-driven TargetFreely override, restoring normal config-based behaviour.
    /// </summary>
    [EzIPC] public Action DisableTargetFreelyOverride { get; private set; }

    /// <summary>
    /// Gets the enemy positional that RotationSolver currently intends to use for its next GCD action.
    /// </summary>
    /// <remarks>
    /// Returns the underlying byte value of <see cref="RotationSolverRebornIPC.EnemyPositional"/>:
    /// 0 = None, 1 = Rear, 2 = Flank, 3 = Front.
    /// </remarks>
    [EzIPC] public Func<byte> GetDesiredPositional { get; private set; }

    /// <summary>
    /// Returns whether RotationSolver's autorotation is currently active.
    /// </summary>
    [EzIPC] public Func<bool> AutorotationActive { get; private set; }

	public static class Delegates
	{
		public delegate void TestDelegate(string param);

		public delegate void NameIdDelegate(uint nameId);

		public delegate void ChangeOperatingModeDelegate(StateCommandType stateCommand);

		public delegate void ChangeOperatingModeWithOverridesDelegate(StateCommandType stateCommand, TargetHostileType targetHostileType, SettingOverride targetFreely, SettingOverride autoOffAfterCombat, SettingOverride friendlyPartyNpcHealRaise);

		public delegate void AutodutyChangeOperatingModeDelegate(StateCommandType stateCommand, TargetingType targetingType);

		public delegate void AutodutyChangeOperatingModeWithOverridesDelegate(StateCommandType stateCommand, TargetingType targetingType, TargetHostileType targetHostileType, SettingOverride targetFreely, SettingOverride autoOffAfterCombat, SettingOverride friendlyPartyNpcHealRaise);

		public delegate void TriggerSpecialStateDelegate(SpecialCommandType specialCommand);

		public delegate void TriggerSpecialStateWithDurationDelegate(SpecialCommandType specialCommand, float duration);

		public delegate void OtherCommandDelegate(OtherCommandType otherType, string str);

		public delegate void ActionCommandDelegate(string action, float time);
	}
}