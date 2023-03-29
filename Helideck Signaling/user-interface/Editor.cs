using GTA;
using GTA.UI;

using Font = GTA.UI.Font;

using System.Drawing;

using Helideck_Signaling.setup_manager;


namespace Helideck_Signaling.user_interface
{
    internal struct StIntermediary
    {
        public static PointF CurrentPosition
        { get; set; }
        public static bool IsTheEditorEnabled
        { get; set; }
    }

    internal sealed class Editor : Script
    {
        public Editor()
        {
            Tick += (o, e) =>
            {
                if (Game.WasCheatStringJustEntered("Editor(){}"))
                {
                    var defaultText =
                        string.Empty;

                    var defaultInputs = new[]
                    {
                        "[on]",
                        "[off]"
                    };

                    if (!StIntermediary.IsTheEditorEnabled)
                        defaultText =
                            defaultInputs[0];
                    else
                        defaultText =
                            defaultInputs[1];

                    var userInput =
                        Game
                            .GetUserInput(windowTitle: WindowTitle.EnterMessage60, 
                                          defaultText: defaultText, 
                                          maxLength  : 60);

                    switch (userInput)
                    {
                        case "[on]":
                            {
                                StIntermediary
                                    .IsTheEditorEnabled = true;
                            }
                            return;
                        case "[Off]":
                            {
                                StIntermediary
                                    .IsTheEditorEnabled = false;
                            }
                            return;
                        case "SaveTheRelativePositionOfLayoutGroup(){}":
                            {
                                SaveTheRelativePositionOfThis(textureName: "alert - instruction", 
                                                              layoutGroup: LayoutGroups.ALERT);
                            }
                            return;
                    }
                }


                if (StIntermediary.IsTheEditorEnabled)
                {
                    Game
                        .DisableAllControlsThisFrame();
                    Game
                        .EnableControlThisFrame(Control
                                                    .EnterCheatCode);
                }
            };
        }
        
        ///
        ///private void SavePositionIn(ScriptSettings[] scriptsSettings)
        ///{
        ///    var displayCompatibility = 
        ///        scriptsSettings[0];
        ///
        ///    var screenCompatibility =
        ///        displayCompatibility
        ///            .GetAllValues<string>(section: "Compatibility",
        ///                                  name   : $"{Screen.AspectRatio}")[0];
        ///
        ///
        ///    displayCompatibility
        ///        .SetValue(section: screenCompatibility,
        ///                  name   : "Screen Center Position",
        ///                  value  : StIntermediary.CurrentPosition.X);
        ///
        ///    displayCompatibility
        ///        .Save();
        ///
        ///    Notification
        ///            .Show(message : $"{StIntermediary.CurrentPosition.X} - ~g~successfully saved~w~!",
        ///                  blinking: true);
        ///}
        ///private void SaveTexturesResolutionsIn(ScriptSettings[] scriptsSettings)
        ///{
        ///    var interfaceSettings =
        ///        scriptsSettings[1];
        ///
        ///    var contentResolution =
        ///        scriptsSettings[2];
        ///
        ///    var texturesName =
        ///        ReturnNameOfTexturesIn(interfaceSettings);
        ///
        ///    var textureDict =
        ///        interfaceSettings
        ///            .GetAllValues<string>(section: "Texture Dictionaries To Be Used",
        ///                                  name   : "_")[0];
        ///
        ///    foreach (var textureName in texturesName)
        ///    {
        ///        contentResolution
        ///            .SetValue(section: textureName, 
        ///                      name   : "_", 
        ///                      value  : ReturnResolutionOfThis(textureName, 
        ///                                                      textureDict).ToString()
        ///                                                                    .Replace("{", "")
        ///                                                                    .Replace("}", "")
        ///                                                                    .Replace(",", "")
        ///                                                                    .Replace("=", ":"));
        ///    }
        ///
        ///    contentResolution
        ///        .Save();
        ///}

        private void SaveTheRelativePositionOfThis(string textureName, LayoutGroups layoutGroup)
        {
            var settings =
                new Settings();

            var displayCompatibility =
                settings
                    .ReturnThePositionOfCenterOfScreen();

            var layoutGroupsFile =
                ScriptSettings
                    .Load($@"{settings.PathToTheHelideckSignalingFolder}\PopUpSettings\LayoutGroups.ini");

            var relativePosition =
                new PointF(StIntermediary.CurrentPosition.X - displayCompatibility.X, 
                           StIntermediary.CurrentPosition.Y - displayCompatibility.Y);

            layoutGroupsFile
                .SetValue(section: $"Layout Group - {layoutGroup}",
                          name   : textureName,
                          value  : relativePosition.ToString()
                                                        .Replace("{", "")
                                                        .Replace("}", "")
                                                        .Replace(",", "")
                                                        .Replace("=", ":"));

            layoutGroupsFile
                .Save();

            Notification
                    .Show(message : $"{textureName} - {StIntermediary.CurrentPosition} - ~g~successfully saved~w~!",
                          blinking: true);
        }
    }
    internal sealed class ScreenCenterIdentifier : Script
    {
        private Sprite _sprite;

        public ScreenCenterIdentifier()
        {
            var velocity =
                (byte)1;

            var textsElement = 
                ConfiguredTextElements(4);

            Tick    += (o, e) =>
            {
                if (!StIntermediary.IsTheEditorEnabled)
                {
                    if (Interval != 1000)
                        Interval = 1000;
                    
                    if (_sprite != null)
                        _sprite
                            .Dispose();

                    return;
                }
                else
                {
                    if (_sprite == null)
                        _sprite =
                            ReturnTheSpriteOfTheCenterIdentifier();

                    var currentPosition =
                        StIntermediary
                            .CurrentPosition;

                    var captions = new[]
                    {
                        $"[~b~Editor mode~w~]",
                        $"Velocity - ~r~{velocity}~w~",
                        $"P - [X] : ~y~{currentPosition.X}",
                        $"P - [Y] : ~y~{currentPosition.Y}"
                    };

                    for (var i = (byte)0; i < textsElement.Length; i++)
                    {
                        textsElement[i]
                            .Caption = captions[i];

                        textsElement[i]
                            .ScaledDraw();
                    }

                    _sprite
                        .ScaledDraw();

                    if (Interval != 0)
                        Interval = 0;
                }
            };

            Aborted += (o, e) =>
            {
                if (_sprite != null)
                    _sprite
                        .Dispose();
            };

            KeyUp   += (o, e) =>
            {
                if (!StIntermediary.IsTheEditorEnabled)
                {
                    return;
                }
                else
                {
                    switch (e.KeyCode)
                    {
                        case System.Windows.Forms.Keys.Oemplus:
                            {
                                velocity++;
                                return;
                            }
                        case System.Windows.Forms.Keys.OemMinus:
                            {
                                velocity--;
                                return;
                            }
                    }
                }
            };

            KeyDown += (o, e) =>
            {
                if (!StIntermediary.IsTheEditorEnabled)
                {
                    return;
                }
                else
                {
                    switch (e.KeyCode)
                    {
                        case System.Windows.Forms.Keys.Right:
                            {
                                var newCurrentPosition =
                                    StIntermediary
                                        .CurrentPosition;

                                newCurrentPosition =
                                        new PointF(newCurrentPosition.X + (byte)1 * velocity,
                                                   newCurrentPosition.Y);

                                StIntermediary
                                    .CurrentPosition = newCurrentPosition;

                                return;
                            }
                        case System.Windows.Forms.Keys.Left:
                            {
                                var currentPosition =
                                    StIntermediary
                                        .CurrentPosition;

                                currentPosition =
                                        new PointF(currentPosition.X - (byte)1 * velocity,
                                                   currentPosition.Y);

                                StIntermediary
                                    .CurrentPosition = currentPosition;
                                return;
                            }

                        case System.Windows.Forms.Keys.Up:
                            {
                                var currentPosition =
                                    StIntermediary
                                        .CurrentPosition;

                                currentPosition =
                                        new PointF(currentPosition.X,
                                                   currentPosition.Y - (byte)1 * velocity);

                                StIntermediary
                                    .CurrentPosition = currentPosition;

                                return;
                            }
                        case System.Windows.Forms.Keys.Down:
                            {
                                var currentPosition = 
                                    StIntermediary
                                        .CurrentPosition;
                                
                                currentPosition = 
                                    new PointF(currentPosition.X, 
                                               currentPosition.Y + (byte)1 * velocity);
                                
                                StIntermediary
                                    .CurrentPosition = currentPosition;

                                return;
                            }
                    }
                }
            };
        }

        private Sprite ReturnTheSpriteOfTheCenterIdentifier()
        {
            var textureDict =
                "niziul_popUp_resources";

            var textureName =
                "screen - center - identifier";

            var size =
                new SizeF(400f / 2.75f, 48 / 2.75f);

            var position =
                StIntermediary.CurrentPosition;

            return new Sprite(textureDict, 
                              textureName, 
                              size, 
                              position) { Centered = true};
        }
        private TextElement[] ConfiguredTextElements(byte amount)
        {
            var textsElement = 
                new TextElement[amount];

            for (var i = (byte)0; i < textsElement.Length; i++)
            {
                textsElement[i] = 
                    new TextElement(caption : string.Empty,
                                    position: PointF.Empty,
                                    scale   : 0f);

                var verticalSpacing = 
                    !(i > 0) ? 0f : i * 1.05f;

                var position =
                    new PointF(15f,
                               20f * verticalSpacing);
                
                var scale =
                    0.35f;

                DefaultSettingFor(textsElement[i], 
                                  position, 
                                  scale);
            }

            return textsElement;
        }
        private void DefaultSettingFor(TextElement textElement, PointF position, float scale)
        {
            textElement
                .Caption   = string.Empty;

            textElement
                .Position  = position;

            textElement
                .Scale     = scale;

            textElement
                .Shadow    = true;
            textElement
                .Outline   = true;

            textElement
                .Color     = Color
                                .White;

            textElement
                .Alignment = Alignment
                                .Left;

            textElement
                .Font      = Font
                                .ChaletLondon;
        }
    }
}