using System;
using System.Drawing;
using GTA;
using GTA.UI;
using System.Windows.Forms; // For Keys

namespace RoleplayOverhaul.UI
{
    public class InventoryMenu
    {
        private RoleplayOverhaul.Core.Inventory.GridInventory _inventory;
        private bool _isVisible = false;

        // Grid Settings
        private int _cols = 5;
        private int _rows = 6;
        private float _slotSize = 60f;
        private float _padding = 5f;
        private PointF _startPos = new PointF(200f, 200f);

        private int _selectedSlot = 0;

        public InventoryMenu(RoleplayOverhaul.Core.Inventory.GridInventory inventory)
        {
            _inventory = inventory;
        }

        public void Toggle()
        {
            _isVisible = !_isVisible;
            if (_isVisible)
            {
                GTA.Game.Player.CanControlCharacter = false;
                // Game.TimeScale = 0.1f; // Slow motion while in inventory? User preference usually.
            }
            else
            {
                GTA.Game.Player.CanControlCharacter = true;
                // Game.TimeScale = 1.0f;
            }
        }

        public void Draw()
        {
            if (!_isVisible) return;

            // Draw Background
            new ContainerElement(_startPos, new SizeF((_slotSize + _padding) * _cols + _padding, (_slotSize + _padding) * _rows + _padding), Color.FromArgb(200, 20, 20, 20)).Draw();

            // Draw Slots
            for (int i = 0; i < _inventory.Capacity; i++)
            {
                int row = i / _cols;
                int col = i % _cols;
                float x = _startPos.X + _padding + (col * (_slotSize + _padding));
                float y = _startPos.Y + _padding + (row * (_slotSize + _padding));

                // Draw Slot Box
                Color boxColor = (i == _selectedSlot) ? Color.FromArgb(100, 255, 255, 0) : Color.FromArgb(100, 50, 50, 50);
                new ContainerElement(new PointF(x, y), new SizeF(_slotSize, _slotSize), boxColor).Draw();

                // Draw Item
                var item = _inventory.Slots[i];
                if (item != null)
                {
                    // Draw Text fallback (Texture would go here if we had png loading)
                    new TextElement(item.Name, new PointF(x + 2, y + 2), 0.3f, Color.White).Draw();
                    new TextElement($"x{item.Count}", new PointF(x + 40, y + 40), 0.3f, Color.Yellow).Draw();
                }
            }

            // Draw Description of selected
            var selectedItem = _inventory.Slots[_selectedSlot];
            if (selectedItem != null)
            {
                new TextElement(selectedItem.Description, new PointF(_startPos.X, _startPos.Y + ((_slotSize + _padding) * _rows) + 10), 0.4f, Color.White).Draw();
                new TextElement("[Enter] Use | [Del] Drop", new PointF(_startPos.X, _startPos.Y + ((_slotSize + _padding) * _rows) + 40), 0.35f, Color.LightGray).Draw();
            }
        }

        public void HandleInput(KeyEventArgs e)
        {
            if (!_isVisible) return;

            if (e.KeyCode == Keys.Right) _selectedSlot = Math.Min(_selectedSlot + 1, _inventory.Capacity - 1);
            if (e.KeyCode == Keys.Left) _selectedSlot = Math.Max(_selectedSlot - 1, 0);
            if (e.KeyCode == Keys.Down) _selectedSlot = Math.Min(_selectedSlot + _cols, _inventory.Capacity - 1);
            if (e.KeyCode == Keys.Up) _selectedSlot = Math.Max(_selectedSlot - _cols, 0);

            if (e.KeyCode == Keys.Enter)
            {
                var item = _inventory.Slots[_selectedSlot];
                if (item != null)
                {
                    item.OnUse(Game.Player.Character);
                    if (item.Count <= 1) _inventory.RemoveItem(_selectedSlot); // Simple consume logic
                    else item.Count--;
                }
            }
        }
    }
}
