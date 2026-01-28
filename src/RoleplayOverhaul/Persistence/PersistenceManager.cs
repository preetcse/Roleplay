using System;
using System.IO;
using System.Xml.Serialization;
using GTA;
using GTA.Math;

namespace RoleplayOverhaul.Persistence
{
    public class GameState
    {
        public Vector3 PlayerPosition { get; set; }
        public float PlayerHeading { get; set; }
        public int Health { get; set; }
        public int Armor { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public string Weather { get; set; }
        public int Money { get; set; }
    }

    public class PersistenceManager
    {
        private string _saveFile = "RoleplayOverhaul_Save.xml";
        private int _lastAutoSave;

        public void OnTick()
        {
            // Auto-save every 5 mins
            if (GTA.Game.GameTime - _lastAutoSave > 300000)
            {
                SaveGame();
                _lastAutoSave = GTA.Game.GameTime;
            }
        }

        public void SaveGame()
        {
            try
            {
                GameState state = new GameState
                {
                    PlayerPosition = GTA.Game.Player.Character.Position,
                    PlayerHeading = GTA.Game.Player.Character.Heading,
                    Health = GTA.Game.Player.Character.Health,
                    Armor = GTA.Game.Player.Character.Armor,
                    // Hour = World.CurrentTimeOfDay.Hours, // Mock
                    // Weather = World.Weather.ToString(),
                    Money = GTA.Game.Player.Money
                };

                XmlSerializer serializer = new XmlSerializer(typeof(GameState));
                using (TextWriter writer = new StreamWriter(_saveFile))
                {
                    serializer.Serialize(writer, state);
                }
                GTA.UI.Screen.ShowSubtitle("Game State Saved.");
            }
            catch (Exception ex)
            {
                GTA.UI.Screen.ShowSubtitle("Save Failed: " + ex.Message);
            }
        }

        public void LoadGame()
        {
            if (!File.Exists(_saveFile)) return;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GameState));
                using (TextReader reader = new StreamReader(_saveFile))
                {
                    GameState state = (GameState)serializer.Deserialize(reader);

                    GTA.Game.Player.Character.Position = state.PlayerPosition;
                    GTA.Game.Player.Character.Heading = state.PlayerHeading;
                    GTA.Game.Player.Character.Health = state.Health;
                    GTA.Game.Player.Character.Armor = state.Armor;
                    GTA.Game.Player.Money = state.Money;

                    // Restore Time/Weather (Mock Native Calls)
                    // World.CurrentTimeOfDay = new TimeSpan(state.Hour, state.Minute, 0);
                    // World.Weather = (Weather)Enum.Parse(typeof(Weather), state.Weather);

                    GTA.UI.Screen.ShowSubtitle("Game State Loaded.");
                }
            }
            catch (Exception ex)
            {
                GTA.UI.Screen.ShowSubtitle("Load Failed: " + ex.Message);
            }
        }
    }
}
