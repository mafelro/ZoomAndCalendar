using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Menus;

namespace ZoomAndCalendar
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }

        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            if (e.Button.ToString().Equals("PageUp"))
            {
                if (Game1.options.desiredBaseZoomLevel < 3f)
                    Game1.options.desiredBaseZoomLevel += 0.05f;
            }
            if (e.Button.ToString().Equals("PageDown"))
            {
                if (Game1.options.desiredBaseZoomLevel > 0.2f)
                    Game1.options.desiredBaseZoomLevel -= 0.05f;
            }

            if (e.Button.ToString().Equals("G") && Game1.activeClickableMenu != null) //G is for gifts
            {
                if (Game1.activeClickableMenu is Billboard menu)
                {
                    foreach (ClickableTextureComponent day in menu.calendarDays)
                    {
                        if (day.bounds.Contains(Game1.getMouseXRaw(), Game1.getMouseYRaw()))
                        {
                            NPC bdayNPC = Utility.getTodaysBirthdayNPC(Game1.currentSeason, day.myID);
                            if (bdayNPC != null)
                            {
                                Game1.activeClickableMenu = new ProfileMenu(bdayNPC);
                            }
                        }
                    }                    
                }
            }
        }
    }
}