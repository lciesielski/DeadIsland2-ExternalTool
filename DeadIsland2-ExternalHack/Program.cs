using System.Diagnostics;
using Reloaded.Memory;


class Program
{
	const string targetProcessName = "DeadIsland-Win64-Shipping";

	static void Main(string[] args)
	{
#if WINDOWS
			Process targetProcess = Process.GetProcessesByName(targetProcessName)[0];
			ExternalMemory externalMemory = new ExternalMemory(targetProcess);

			//SetMaxBackwardSpeed(LocalPlayerPawn, externalMemory);
			//SetCharacterAttributes(LocalPlayerPawn, externalMemory);

			do
			{
				try
				{
					UIntPtr LocalPlayerPawn = GetLocalPawn(targetProcess, externalMemory);
					//SetGodMode(LocalPlayerPawn, externalMemory);
					SetDamageMultiplier(LocalPlayerPawn, externalMemory);
					SetStamina(LocalPlayerPawn, externalMemory);
					SetCurrentlyEquippedMeeleWeapon(LocalPlayerPawn, externalMemory);
				}
				catch (Exception ex)
				{
					UIntPtr CurrentEquippedOuterCache = UIntPtr.Zero;
					Console.WriteLine("Read error... Retrying...");
				}
				finally 
				{
					Thread.Sleep(1000);
				}
			}
			while (true);
#else
		Console.WriteLine("This code is intended to run on Windows only.");
#endif
	}

	static bool SetDamageMultiplier(UIntPtr LocalPlayerPawn, ExternalMemory externalMemory)
	{
		externalMemory.Read(LocalPlayerPawn, out UIntPtr DamageableComponent);
		DamageableComponent += Offsets.DamageableComponent;
		Console.WriteLine($"DamageableComponent: {DamageableComponent:X2}");

		externalMemory.Read(DamageableComponent, out UIntPtr GlobalDamageMultiplier);
		GlobalDamageMultiplier += Offsets.GlobalDamageMultiplier;
		Console.WriteLine($"GlobalDamageMultiplier: {GlobalDamageMultiplier:X2}");

		externalMemory.Read(GlobalDamageMultiplier, out float GlobalDamageMultiplierValue);
		Console.WriteLine($"GlobalDamageMultiplierValue Before: {GlobalDamageMultiplierValue}");

		externalMemory.Write(GlobalDamageMultiplier, (float)0.1);

		externalMemory.Read(GlobalDamageMultiplier, out GlobalDamageMultiplierValue);
		Console.WriteLine($"GlobalDamageMultiplierValue After: {GlobalDamageMultiplierValue}");

		return true;
	}

	static UIntPtr GetLocalPawn(Process targetProcess, ExternalMemory externalMemory)
	{
		UIntPtr baseAddress = (UIntPtr)targetProcess.MainModule.BaseAddress;
		Console.WriteLine($"baseAddress {baseAddress:X2}");

		UIntPtr GWorld = baseAddress + Offsets.GWorld;
		Console.WriteLine($"GWorld {GWorld:X2}");

		externalMemory.Read(GWorld, out UIntPtr UWorld);
		Console.WriteLine($"UWorld {UWorld:X2}");

		UIntPtr UGameInstance = UWorld + Offsets.UGameInstance;
		Console.WriteLine($"UGameInstance {UGameInstance:X2}");

		externalMemory.Read(UGameInstance, out UIntPtr LocalPlayers);
		LocalPlayers += Offsets.LocalPlayers;
		Console.WriteLine($"LocalPlayers {LocalPlayers:X2}");

		externalMemory.Read(LocalPlayers, out UIntPtr LocalPlayer);
		Console.WriteLine($"LocalPlayer {LocalPlayer:X2}");

		externalMemory.Read(LocalPlayer, out UIntPtr LocalPlayerController);
		LocalPlayerController += Offsets.LocalPlayerController;
		Console.WriteLine($"LocalPlayerController {LocalPlayerController:X2}");

		externalMemory.Read(LocalPlayerController, out UIntPtr LocalPlayerPawn);
		LocalPlayerPawn += Offsets.LocalPlayerPawn;
		Console.WriteLine($"LocalPlayerPawn {LocalPlayerPawn:X2}");

		return LocalPlayerPawn;
	}

	static bool SetGodMode(UIntPtr LocalPlayerPawn, ExternalMemory externalMemory)
	{
		externalMemory.Read(LocalPlayerPawn, out UIntPtr bCanBeDamaged);
		bCanBeDamaged += Offsets.bCanBeDamaged;
		Console.WriteLine($"bCanBeDamaged {bCanBeDamaged:X2}");

		externalMemory.Read(bCanBeDamaged, out byte bCanBeDamagedBytes);
		bool bCanBeDamagedValue = (bCanBeDamagedBytes & (1 << Offsets.bCanBeDamagedBitIndex)) != 0;
		Console.WriteLine($"bCanBeDamaged before: {bCanBeDamagedValue}");

		byte newByteValue = (byte)(bCanBeDamagedBytes & ~(1 << Offsets.bCanBeDamagedBitIndex));
		externalMemory.Write(bCanBeDamaged, newByteValue);

		externalMemory.Read(bCanBeDamaged, out bCanBeDamagedBytes);
		bCanBeDamagedValue = (bCanBeDamagedBytes & (1 << Offsets.bCanBeDamagedBitIndex)) != 0;
		Console.WriteLine($"bCanBeDamaged after: {bCanBeDamagedValue}");

		return true;
	}

	static bool SetMaxBackwardSpeed(UIntPtr LocalPlayerPawn, ExternalMemory externalMemory)
	{
		externalMemory.Read(LocalPlayerPawn, out UIntPtr MaxBackwardSpeed);
		MaxBackwardSpeed += Offsets.MaxBackwardSpeed;

		externalMemory.Read(MaxBackwardSpeed, out float MaxBackwardSpeedValue);
		Console.WriteLine($"MaxBackwardSpeedValue before: {MaxBackwardSpeedValue}");

		return true;
	}

	static bool SetStamina(UIntPtr LocalPlayerPawn, ExternalMemory externalMemory)
	{
		externalMemory.Read(LocalPlayerPawn, out UIntPtr StaminaComponent);
		StaminaComponent += Offsets.StaminaComponent;
		Console.WriteLine($"StaminaComponent: {StaminaComponent:X2}");

		externalMemory.Read(StaminaComponent, out UIntPtr Stamina);
		Stamina += Offsets.Stamina;
		Console.WriteLine($"Stamina: {Stamina:X2}");

		externalMemory.Read(StaminaComponent, out UIntPtr MaxStamina);
		MaxStamina += Offsets.MaxStamina;
		Console.WriteLine($"MaxStamina: {MaxStamina:X2}");

		externalMemory.Read(Stamina, out float StaminaValue);
		Console.WriteLine($"StaminaValue Before: {StaminaValue}");

		externalMemory.Read(MaxStamina, out float MaxStaminaValue);
		Console.WriteLine($"MaxStaminaValue Before: {MaxStaminaValue}");

		externalMemory.Write(Stamina, (float)5000);
		externalMemory.Write(MaxStamina, (float)5000);

		externalMemory.Read(Stamina, out StaminaValue);
		Console.WriteLine($"StaminaValue After: {StaminaValue}");

		externalMemory.Read(MaxStamina, out MaxStaminaValue);
		Console.WriteLine($"MaxStaminaValue After: {MaxStaminaValue}");

		return true;
	}

	static bool SetCharacterAttributes(UIntPtr LocalPlayerPawn, ExternalMemory externalMemory)
	{
		externalMemory.Read(LocalPlayerPawn, out UIntPtr PlayerState);
		PlayerState += Offsets.PlayerState;
		Console.WriteLine($"PlayerState: {PlayerState:X2}");

		externalMemory.Read(PlayerState, out UIntPtr CharacterAttributes);
		CharacterAttributes += Offsets.CharacterAttributes;
		Console.WriteLine($"CharacterAttributes: {CharacterAttributes:X2}");

		externalMemory.Read(CharacterAttributes, out UIntPtr DamageOutputMultiplierAttribute);
		DamageOutputMultiplierAttribute += Offsets.DamageOutputMultiplierAttribute;
		Console.WriteLine($"DamageOutputMultiplierAttribute: {DamageOutputMultiplierAttribute:X2}");

		SetCharacterAttributeType(DamageOutputMultiplierAttribute, externalMemory);

		return true;
	}

	static void SetCharacterAttributeType(UIntPtr AttributeType, ExternalMemory externalMemory)
	{
		// DefaultValue
		externalMemory.Read(AttributeType, out UIntPtr DefaultValue);
		DefaultValue += Offsets.UAttributeType.DefaultValue;
		Console.WriteLine($"DefaultValue: {DefaultValue:X2}");

		externalMemory.Read(DefaultValue, out float DefaultValueValue);
		Console.WriteLine($"DefaultValueValue: {DefaultValueValue}");

		// MinValueLimit
		externalMemory.Read(AttributeType, out UIntPtr MinValueLimit);
		MinValueLimit += Offsets.UAttributeType.MinValueLimit;
		Console.WriteLine($"MinValueLimit: {MinValueLimit:X2}");

		externalMemory.Read(MinValueLimit, out float MinValueLimitValue);
		Console.WriteLine($"MinValueLimitValue: {MinValueLimitValue}");

		// MaxValueLimit
		externalMemory.Read(AttributeType, out UIntPtr MaxValueLimit);
		MaxValueLimit += Offsets.UAttributeType.MaxValueLimit;
		Console.WriteLine($"MaxValueLimit: {MaxValueLimit:X2}");

		externalMemory.Read(MaxValueLimit, out float MaxValueLimitValue);
		Console.WriteLine($"MaxValueLimitValue: {MaxValueLimitValue}");

		// bClampToLimits
		externalMemory.Read(AttributeType, out UIntPtr bClampToLimits);
		bClampToLimits += Offsets.UAttributeType.bClampToLimits;
		Console.WriteLine($"bClampToLimits: {bClampToLimits:X2}");

		externalMemory.Read(bClampToLimits, out int bClampToLimitsValue);
		Console.WriteLine($"bClampToLimitsValue: {bClampToLimitsValue}");

		// bAffectedByUserModifier
		externalMemory.Read(AttributeType, out UIntPtr bAffectedByUserModifier);
		bAffectedByUserModifier += Offsets.UAttributeType.bAffectedByUserModifier;
		Console.WriteLine($"bAffectedByUserModifier: {bAffectedByUserModifier:X2}");

		externalMemory.Read(bAffectedByUserModifier, out long bAffectedByUserModifierValue);
		Console.WriteLine($"bAffectedByUserModifierValue: {bAffectedByUserModifierValue}");
	}

	static void SetCurrentlyEquippedMeeleWeapon(UIntPtr LocalPlayerPawn, ExternalMemory externalMemory)
	{
		externalMemory.Read(LocalPlayerPawn, out UIntPtr MainHandHeldObject);
		MainHandHeldObject += Offsets.MainHandHeldObject;
		Console.WriteLine($"MainHandHeldObject: {MainHandHeldObject:X2}");

		externalMemory.Read(MainHandHeldObject, out UIntPtr CurrentEquipped);
		CurrentEquipped += Offsets.CurrentEquipped;
		Console.WriteLine($"CurrentEquipped: {CurrentEquipped:X2}");

		externalMemory.Read(CurrentEquipped, out UIntPtr CurrentEquippedOuter);
		CurrentEquippedOuter += Offsets.UObject.OuterPrivate;
		Console.WriteLine($"CurrentEquippedOuter: {CurrentEquippedOuter:X2}");

		externalMemory.Read(CurrentEquippedOuter, out UIntPtr CurrentDurability);
		CurrentDurability += Offsets.CurrentDurability;
		Console.WriteLine($"CurrentDurability: {CurrentDurability:X2}");

		externalMemory.Read(CurrentDurability, out float CurrentDurabilityValue);
		Console.WriteLine($"CurrentDurabilityValue: {CurrentDurabilityValue}");

		if (CurrentDurabilityValue >= 0)
		{
			//SetCurrentMeeleProperties(CurrentEquippedOuter, externalMemory);

			externalMemory.Read(CurrentEquippedOuter, out UIntPtr AttributesComponent);
			AttributesComponent += Offsets.AttributesComponent;
			Console.WriteLine($"AttributesComponent: {AttributesComponent:X2}");
			SetCurrentlyMeeleWeaponOther(CurrentEquippedOuter, externalMemory);

			/*
			externalMemory.Read(AttributesComponent, out UIntPtr DamageAttributes);
			DamageAttributes += Offsets.AttributeValueSetStructOffset;
			DamageAttributes += Offsets.DamageAttributeStructOffset;
			Console.WriteLine($"DamageAttributes: {DamageAttributes:X2}");
			SetCurrentMeeleWeaponAttributeType(DamageAttributes, externalMemory);
			*/

			externalMemory.Read(AttributesComponent, out UIntPtr CriticalChanceAttribute);
			CriticalChanceAttribute += Offsets.AttributeValueSetStructOffset;
			CriticalChanceAttribute += Offsets.CriticalChanceAttribute;
			Console.WriteLine($"CriticalChanceAttribute: {CriticalChanceAttribute:X2}");
			SetCurrentMeeleWeaponAttributeType(CriticalChanceAttribute, externalMemory);
		}
	}

	private static void SetCurrentlyMeeleWeaponOther(UIntPtr CurrentEquippedOuter, ExternalMemory externalMemory)
	{
		externalMemory.Read(CurrentEquippedOuter, out UIntPtr MeleeArchetypeData);
		MeleeArchetypeData += Offsets.MeleeArchetypeDataStructOffset;
		UIntPtr DurabilityLossPerHit = MeleeArchetypeData + Offsets.DurabilityLossPerHit;

		Console.WriteLine($"CurrentEquippedOuter: {CurrentEquippedOuter:X2}");
		Console.WriteLine($"MeleeArchetypeData: {MeleeArchetypeData:X2}");
		Console.WriteLine($"DurabilityLossPerHit: {DurabilityLossPerHit:X2}");

		externalMemory.Read(DurabilityLossPerHit, out float DurabilityLossPerHitValue);
		Console.WriteLine($"DurabilityLossPerHitValue Before: {DurabilityLossPerHitValue}");

		externalMemory.Write(DurabilityLossPerHit, (float)0.0);

		externalMemory.Read(DurabilityLossPerHit, out DurabilityLossPerHitValue);
		Console.WriteLine($"DurabilityLossPerHitValue After: {DurabilityLossPerHitValue}");

		UIntPtr LaunchSettings =
			MeleeArchetypeData +
			Offsets.ImpactConfigurationStructOffset +
			Offsets.ImpactSettingsStructOffset +
			Offsets.LaunchSettings;
		externalMemory.Read(LaunchSettings, out UIntPtr RootImpulseSettings);

		if (RootImpulseSettings == UIntPtr.Zero)
		{
			return;
		}

		RootImpulseSettings += Offsets.RootImpulseSettingsStructOffset;

		UIntPtr CharacterRootImpulseVelocityMagnitude = RootImpulseSettings + Offsets.CharacterRootImpulseVelocityMagnitude;
		UIntPtr CharacterRootAdditionalVerticalImpulseScale = RootImpulseSettings + Offsets.CharacterRootAdditionalVerticalImpulseScale;

		externalMemory.Read(CharacterRootImpulseVelocityMagnitude, out float CharacterRootImpulseVelocityMagnitudeValue);
		Console.WriteLine($"CharacterRootImpulseVelocityMagnitudeValue Before: {CharacterRootImpulseVelocityMagnitudeValue}");

		externalMemory.Read(CharacterRootAdditionalVerticalImpulseScale, out float CharacterRootAdditionalVerticalImpulseScaleValue);
		Console.WriteLine($"CharacterRootAdditionalVerticalImpulseScaleValue Before: {CharacterRootAdditionalVerticalImpulseScaleValue}");

		externalMemory.Write(CharacterRootImpulseVelocityMagnitude, (float)5000);
		externalMemory.Write(CharacterRootAdditionalVerticalImpulseScale, (float)1);

		externalMemory.Read(CharacterRootImpulseVelocityMagnitude, out CharacterRootImpulseVelocityMagnitudeValue);
		Console.WriteLine($"CharacterRootImpulseVelocityMagnitudeValue After: {CharacterRootImpulseVelocityMagnitudeValue}");

		externalMemory.Read(CharacterRootAdditionalVerticalImpulseScale, out CharacterRootAdditionalVerticalImpulseScaleValue);
		Console.WriteLine($"CharacterRootAdditionalVerticalImpulseScaleValue After: {CharacterRootAdditionalVerticalImpulseScaleValue}");
	}

	private static void SetCurrentMeeleWeaponAttributeType(UIntPtr AttributeType, ExternalMemory externalMemory)
	{
		externalMemory.Read(AttributeType, out UIntPtr DefaultValue);
		DefaultValue += Offsets.UAttributeType.DefaultValue;
		Console.WriteLine($"DefaultValue: {DefaultValue:X2}");

		externalMemory.Read(AttributeType, out UIntPtr MinValueLimit);
		MinValueLimit += Offsets.UAttributeType.MinValueLimit;
		Console.WriteLine($"MinValueLimit: {MinValueLimit:X2}");

		externalMemory.Read(AttributeType, out UIntPtr MaxValueLimit);
		MaxValueLimit += Offsets.UAttributeType.MaxValueLimit;
		Console.WriteLine($"MaxValueLimit: {MaxValueLimit:X2}");

		externalMemory.Read(DefaultValue, out float DefaultValueValue);
		Console.WriteLine($"DefaultValueValue Before: {DefaultValueValue}");

		externalMemory.Read(MinValueLimit, out float MinValueLimitValue);
		Console.WriteLine($"MinValueLimitValue Before: {MinValueLimitValue}");

		externalMemory.Read(MaxValueLimit, out float MaxValueLimitValue);
		Console.WriteLine($"MaxValueLimitValue Before: {MaxValueLimitValue}");

		externalMemory.Write(DefaultValue, (float)99999);
		externalMemory.Write(MinValueLimit, (float)99999);
		externalMemory.Write(MaxValueLimit, (float)99999);

		externalMemory.Read(DefaultValue, out DefaultValueValue);
		Console.WriteLine($"DefaultValueValue After: {DefaultValueValue}");

		externalMemory.Read(MinValueLimit, out MinValueLimitValue);
		Console.WriteLine($"MinValueLimitValue After: {MinValueLimitValue}");

		externalMemory.Read(MaxValueLimit, out MaxValueLimitValue);
		Console.WriteLine($"MaxValueLimitValue After: {MaxValueLimitValue}");
	}

	private static void SetCurrentMeeleProperties(UIntPtr CurrentEquippedOuter, ExternalMemory externalMemory)
	{
		externalMemory.Read(CurrentEquippedOuter, out UIntPtr ProceduralComponent);
		ProceduralComponent += Offsets.ProceduralComponent;
		Console.WriteLine($"ProceduralComponent: {ProceduralComponent:X2}");

		externalMemory.Read(ProceduralComponent, out UIntPtr ProceduralItemInstanceData);
		ProceduralItemInstanceData += Offsets.ProceduralItemInstanceData;
		Console.WriteLine($"ProceduralItemInstanceData: {ProceduralItemInstanceData:X2}");

		externalMemory.Read(ProceduralItemInstanceData, out UIntPtr ProceduralItemProperties);
		ProceduralItemProperties += Offsets.ProceduralItemProperties;
		Console.WriteLine($"ProceduralItemProperties: {ProceduralItemProperties:X2}");

		externalMemory.Read(ProceduralItemProperties, out UIntPtr Rarity);
		Rarity += Offsets.Rarity;
		Console.WriteLine($"Rarity: {Rarity:X2}");

		externalMemory.Read(ProceduralItemProperties, out UIntPtr ItemLevel);
		ItemLevel += Offsets.ItemLevel;
		Console.WriteLine($"ItemLevel: {ItemLevel:X2}");

		externalMemory.Read(Rarity, out int RarityValue);
		Console.WriteLine($"RarityValue: {RarityValue}");

		externalMemory.Read(ItemLevel, out UIntPtr ItemLevelValue);
		Console.WriteLine($"ItemLevelValue: {ItemLevelValue}");
	}
}
