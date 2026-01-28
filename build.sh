#!/bin/bash
# A simple build script to compile the project against SHVDN stubs
# Requires 'mcs' (Mono C# compiler)

# Ensure Dependencies exist (stub for compilation)
mkdir -p src/Dependencies
if [ ! -f src/Dependencies/SHVDN_Stubs.cs ]; then
    echo "Creating SHVDN Stubs..."
    cat > src/Dependencies/SHVDN_Stubs.cs <<EOF
using System;
using System.Collections.Generic;
using System.Drawing;

namespace GTA
{
    public class Script {
        public event EventHandler Tick;
        public event System.Windows.Forms.KeyEventHandler KeyDown;
    }
    public static class Game {
        public static Player Player { get; set; }
        public static int GameTime { get; set; }
        public static void FadeScreenOut(int time) {}
        public static void FadeScreenIn(int time) {}
    }
    public class Player {
        public Ped Character { get; set; }
        public int Money { get; set; }
        public bool CanControlCharacter { get; set; }
        public int WantedLevel { get; set; }
    }
    public class Ped : Entity {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Armor { get; set; }
        public bool IsDead { get; set; }
        public bool IsInVehicle() { return false; }
        public Vehicle CurrentVehicle { get; set; }
        public TaskInvoker Task { get; set; }
        public bool IsReloading { get; set; }
        public bool IsShooting { get; set; }
        public Vector3 Velocity { get; set; }
        public void ClearTasks() {}
        public void Delete() {}
    }
    public class Vehicle : Entity {
        public bool IsEngineRunning { get; set; }
        public float EngineHealth { get; set; }
        public float BodyHealth { get; set; }
        public float FuelLevel { get; set; }
        public float DirtLevel { get; set; }
        public void Repair() {}
        public void Wash() {}
    }
    public class Entity {
        public Vector3 Position { get; set; }
        public float Heading { get; set; }
        public Model Model { get; set; }
        public void ApplyForce(Vector3 force) {}
        public bool Exists() { return true; }
        public void AttachTo(Entity entity, int boneIndex, Vector3 offset, Vector3 rotation) {}
        public void Detach() {}
        public Vector3 ForwardVector { get; set; }
    }
    public struct Vector3 {
        public float X, Y, Z;
        public Vector3(float x, float y, float z) { X = x; Y = y; Z = z; }
        public static float Distance(Vector3 a, Vector3 b) { return 0f; }
        public static Vector3 operator +(Vector3 a, Vector3 b) { return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z); }
        public static Vector3 operator *(Vector3 a, float b) { return new Vector3(a.X * b, a.Y * b, a.Z * b); }
    }
    public class Blip {
        public int Color { get; set; }
        public int Sprite { get; set; }
        public string Name { get; set; }
        public bool ShowRoute { get; set; }
        public void Delete() {}
    }
    public static class World {
        public static Blip CreateBlip(Vector3 pos) { return new Blip(); }
        public static Ped CreatePed(Model model, Vector3 pos) { return new Ped(); }
        public static Vehicle CreateVehicle(Model model, Vector3 pos) { return new Vehicle(); }
        public static Prop CreateProp(Model model, Vector3 pos, bool dynamic, bool placeOnGround) { return new Prop(); }
        public static Prop[] GetNearbyProps(Vector3 pos, float radius) { return new Prop[0]; }
        public static Ped GetClosestPed(Vector3 pos, float radius) { return new Ped(); }
        public static void DrawMarker(int type, Vector3 pos, Vector3 dir, Vector3 rot, Vector3 scale, Color color) {}
    }
    public struct Model {
        public Model(string name) {}
        public Model(int hash) {}
        public static implicit operator Model(int hash) { return new Model(hash); }
        public static implicit operator Model(string name) { return new Model(name); }
        public void Request() {}
        public bool IsLoaded { get { return true; } }
    }
    public enum WeaponHash { Pistol, Unarmed }
    public enum VehicleHash { Adder }
    public enum PedHash { Michael }

    // Stub for Native Hashes used in code
    public enum Hash : ulong {
        ATTACH_ENTITY_TO_ENTITY = 0x12345,
        SET_PED_TO_RAGDOLL = 0x67890,
        SET_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME = 0xABCDE,
        SET_RANDOM_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME = 0xFEDCB,
        SET_PARKED_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME = 0x54321,
        SET_PED_DENSITY_MULTIPLIER_THIS_FRAME = 0x09876,
        SET_SCENARIO_PED_DENSITY_MULTIPLIER_THIS_FRAME = 0x13579
    }

    public static class Function {
        public static void Call(Hash hash, params object[] args) {}
    }

    public class TaskInvoker {
        public void FightAgainst(Ped target) {}
        public void FleeFrom(Ped target) {}
        public void WanderAround() {}
        public void PlayAnimation(string dict, string anim, float speed, int duration, int flags) {}
        public void EnterVehicle(Vehicle vehicle, VehicleSeat seat) {}
    }

    public enum VehicleSeat { Driver, Passenger }

    public class Prop : Entity {}

    namespace UI {
        public class TextElement {
            public TextElement(string caption, PointF position, float scale) {}
            public TextElement(string caption, PointF position, float scale, Color color) {}
            public void Draw() {}
        }
        public class ContainerElement {
            public ContainerElement(PointF pos, SizeF size, Color color) {}
            public void Draw() {}
        }
    }
}
namespace System.Windows.Forms {
    public class KeyEventArgs : EventArgs {
        public Keys KeyCode { get; set; }
    }
    public enum Keys { F5, F10, E, I, M, T, Enter, Up, Down, Left, Right, L, H, B, NumPad1, NumPad2, NumPad3, NumPad4, NumPad5, NumPad6, NumPad7, G, F4, Back, ShiftKey, ControlKey }
}
EOF
fi

# Find all C# files
SOURCES=$(find src -name "*.cs")

# Compile
echo "Compiling..."
mcs -target:library -out:RoleplayOverhaul.dll $SOURCES -r:System.Windows.Forms.dll -r:System.Drawing.dll

if [ $? -eq 0 ]; then
    echo "Build Successful!"
else
    echo "Build Failed!"
    exit 1
fi
