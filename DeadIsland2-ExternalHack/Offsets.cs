
class Offsets
{
	public const byte bCanBeDamagedBitIndex = 2;

	public const UIntPtr GWorld = 0x6EAD848;                // Grabbed via tool called GSpots.exe - https://github.com/Do0ks/GSpots
	public const UIntPtr UGameInstance = 0x0198;            // UEDumper - Engine Package - OwningGameInstance
	public const UIntPtr LocalPlayers = 0x038;              // UEDumper - Engine Package - UGameInstance - LocalPlayers
	public const UIntPtr LocalPlayerController = 0x030;     // UEDumper - Engine Package - ULocalPlayer - UPlayer - PlayerController
	public const UIntPtr LocalPlayerPawn = 0x02D8;          // UEDumper - Engine Package - APlayerController - AcknowledgedPawn

	public const UIntPtr bCanBeDamaged = 0x0073;            // UEDumper - Engine Package - APawn - AActor - bCanBeDamaged
	public const UIntPtr MaxBackwardSpeed = 0x0D60;         // UEDumper - Dead Island Package - ADIPlayerCharacter - MaxBackwardSpeed
	public const UIntPtr MainHandHeldObject = 0x15A8;       // UEDumper - Dead Island Package - ADIPlayerCharacter - MainHandHeldObject

	public const UIntPtr CurrentEquipped = 0x0070;                      // UEDumper - Dead Island Package - UPhysicalObject - CurrentEquipped
	public const UIntPtr CurrentDurability = 0x0C20;                    // UEDumper - Dead Island Package - AMeleeWeaponItemActor - CurrentDurability
	public const UIntPtr ProceduralComponent = 0x0C38;                  // UEDumper - Dead Island Package - AMeleeWeaponItemActor - ProceduralComponent
	public const UIntPtr AttributesComponent = 0x0C50;                  // UEDumper - Dead Island Package - AMeleeWeaponItemActor - AttributesComponent
	public const UIntPtr MeleeArchetypeDataStructOffset = 0x0538;       // UEDumper - Dead Island Package - AMeleeWeaponItemActor - MeleeArchetypeData

	public const UIntPtr DurabilityLossPerHit = 0x018C;                 // UEDumper - Dead Island Package - FMeleeWeaponArchetypeData - DurabilityLossPerHit
	public const UIntPtr ImpactConfigurationStructOffset = 0x0328;      // UEDumper - Dead Island Package - FMeleeWeaponArchetypeData - ImpactConfiguration
	public const UIntPtr ImpactSettingsStructOffset = 0x0010;           // UEDumper - Dead Island Package - FImpactConfiguration - Settings
	public const UIntPtr LaunchSettings = 0x0098;                       // UEDumper - Dead Island Package - FImpactSettings - LaunchSettings
	public const UIntPtr RootImpulseSettingsStructOffset = 0x003C;      // UEDumper - Dead Island Package - ULaunchSettings - RootImpulseSettings
	public const UIntPtr CharacterRootImpulseVelocityMagnitude = 0x0004;            // UEDumper - Dead Island Package - FRootImpulseApplicationSettings - CharacterRootImpulseVelocityMagnitude
	public const UIntPtr CharacterRootAdditionalVerticalImpulseScale = 0x001C;      // UEDumper - Dead Island Package - FRootImpulseApplicationSettings - CharacterRootAdditionalVerticalImpulseScale

	public const UIntPtr AttributeValueSetStructOffset = 0x02B0;        // UEDumper - Dead Island Package - UMeleeWeaponAttributesComponent - AttributeValueSet
	public const UIntPtr DamageAttributeStructOffset = 0x0070;          // UEDumper - Dead Island Package - FMeleeWeaponAttributes - DamageAttribute
	public const UIntPtr CriticalChanceAttribute = 0x01C0;              // UEDumper - Dead Island Package - FMeleeWeaponAttributes - CriticalChanceAttribute

	public const UIntPtr ProceduralItemInstanceData = 0x02C8;           // UEDumper - Dead Island Package - UProceduralItemComponent - ProceduralItemInstanceData
	public const UIntPtr ProceduralItemProperties = 0x0060;             // UEDumper - Dead Island Package - UProceduralItemInstanceData - ProceduralItemProperties
	public const UIntPtr Rarity = 0x0028;                               // UEDumper - Dead Island Package - UProceduralItemProperties - Rarity
	public const UIntPtr ItemLevel = 0x00A8;                            // UEDumper - Dead Island Package - UProceduralItemProperties - ItemLevel

	public const UIntPtr HealthComponent = 0x09B8;          // UEDumper - Dead Island Package - ADICharacter - HealthComponent
	public const UIntPtr bInvincible = 0x00E8;              // UEDumper - Dead Island Package - USimpleHealthComponent - bInvincible

	public const UIntPtr DamageableComponent = 0x09C8;          // UEDumper - Dead Island Package - ADICharacter - DamageComponent
	public const UIntPtr GlobalDamageMultiplier = 0x00F0;       // UEDumper - Dead Island Package - UDamagableComponent - GlobalDamageMultiplier

	public const UIntPtr StaminaComponent = 0x09C0;         // UEDumper - Dead Island Package - ADICharacter - StaminaComponent
	public const UIntPtr Stamina = 0x0114;                  // UEDumper - Dead Island Package - UStaminaComponent - Stamina
	public const UIntPtr MaxStamina = 0x0120;               // UEDumper - Dead Island Package - UStaminaComponent - MaxStamina

	public const UIntPtr PlayerState = 0x0278;                          // UEDumper - Dead Island Package - APawn - PlayerState
	public const UIntPtr CharacterAttributes = 0x03D0;                  // UEDumper - Dead Island Package - ADIPlayerState - CharacterAttributes
	public const UIntPtr DamageOutputMultiplierAttribute = 0x02E8;      // UEDumper - Dead Island Package - UPlayerCharacterAttributesComponent - DamageOutputMultiplierAttribute

	//UEDumper - Dead Island Package - UAttributeType
	public struct UAttributeType
	{
		public const UIntPtr DefaultValue = 0x0060;
		public const UIntPtr MinValueLimit = 0x0068;
		public const UIntPtr MaxValueLimit = 0x006C;
		public const UIntPtr bClampToLimits = 0x0064;
		public const UIntPtr bAffectedByUserModifier = 0x0078;
	}

	//UEDumper - CoreUObject Package - UObject
	public struct UObject
	{
		public const UIntPtr OuterPrivate = 0x0020;
	}

	//UEDumper - Dead Island Package FImpactData
	public struct FImpactData
	{
		public const UIntPtr bCanCauseBreak = 0x0000;
		public const UIntPtr bCanCauseDismember = 0x0001;
		public const UIntPtr bSpawnLimbsOnDismember = 0x0002;
		public const UIntPtr bCanCut = 0x0003;
		public const UIntPtr bCanCauseBisection = 0x0004;
		public const UIntPtr BisectionChance = 0x0008;
	}
}

