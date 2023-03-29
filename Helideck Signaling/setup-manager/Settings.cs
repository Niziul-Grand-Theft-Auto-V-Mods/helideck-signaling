using GTA;
using GTA.Math;

using System.IO;

using System.Drawing;

using System.Collections.Generic;


namespace Helideck_Signaling.setup_manager
{
    enum LayoutGroups
    {
        TOP,
        BODY,
        BOTTOM,
        TIMER,
        ALERT
    }

    internal sealed class Settings
    {
        internal string PathToTheHelideckSignalingFolder 
        { 
            get 
            { 
                return $@"{Directory
                                .GetCurrentDirectory()}\scripts\HelideckSignaling"; 
            } 
        }

        internal byte ReturnColorForTheBlip()
        {
            var scriptSettings =
                ScriptSettings
                    .Load($@"{PathToTheHelideckSignalingFolder}\BlipColor.ini");

            var color =
                scriptSettings
                    .GetAllValues<byte>(section: "Blip Color",
                                        name   : "Id")[0];
            return color;
        }
        
        internal List<Vector3> ReturnLocationOfAllNativeHelipads()
        {
            var scriptSettings =
                ScriptSettings
                    .Load($@"{PathToTheHelideckSignalingFolder}\LocationOfHelipads\NativeHelipads.ini");

            var allValuesOfNativeHelipadsFile =
                scriptSettings
                    .GetAllValues<string>(section: "Locations",
                                          name   : "_");

            var allNativeHelipads =
                new List<Vector3>();

            foreach (var valueOfNativeHelipadFile in allValuesOfNativeHelipadsFile)
            {
                var valuesForVectorInString =
                    valueOfNativeHelipadFile
                        .Replace("X", "")
                        .Replace("Y", "")
                        .Replace("Z", "")
                        .Replace(":", "")
                            .Split(' ');

                allNativeHelipads
                    .Add(new Vector3(System.Convert.ToSingle(valuesForVectorInString[0]),
                                     System.Convert.ToSingle(valuesForVectorInString[1]),
                                     System.Convert.ToSingle(valuesForVectorInString[2])));
            }

            return allNativeHelipads;
        }
        
        internal List<PointF> ReturnAllRelativePositionsIn(LayoutGroups layoutGroup)
        {
            var layoutGroupsFile =
                ScriptSettings
                    .Load($@"{PathToTheHelideckSignalingFolder}\UserInterfaceResources\PopUpSettings\LayoutGroups.ini");

            var allTextureNames =
                ReturnAllTextureNamesIn(layoutGroup);

            var allPositionsInString =
                new List<string>();

            foreach (var textureName in allTextureNames)
            {
                allPositionsInString
                    .Add(layoutGroupsFile
                            .GetAllValues<string>(section: $"Layout Group - {layoutGroup}", 
                                                  name   : textureName)[0].Replace("X:", "")
                                                                          .Replace("Y:", ""));
            }

            var positionOfCenterOfScreen =
                ReturnThePositionOfCenterOfScreen();

            var allPositions = 
                new List<PointF>();

            foreach (var positionsInString in allPositionsInString)
            {
                var x =
                    positionsInString
                        .Split(' ')[0];
                var y =
                    positionsInString
                        .Split(' ')[1];

                allPositions
                    .Add(new PointF(System.Single.Parse(x) + positionOfCenterOfScreen.X,
                                    System.Single.Parse(y) + positionOfCenterOfScreen.Y));
            }

            return allPositions;
        }
        
        internal List<string> ReturnAllTextureNamesIn(LayoutGroups layoutGroup)
        {
            var interfaceSettings =
                ScriptSettings
                    .Load($@"{PathToTheHelideckSignalingFolder}\UserInterfaceResources\InterfaceSettings.ini");

            var textureDict =
                interfaceSettings
                    .GetAllValues<string>(section: "Texture Dictionaries To Be Used",
                                          name   : "_")[0];

            var section =
                $"Content Of The {textureDict} To Be Used";

            var amountOfContentGroup =
                interfaceSettings
                    .GetAllValues<byte>(section: section,
                                        name   : $"{layoutGroup} - Indexer")[0];

            var texturesName =
                new List<string>();

            for (var i = (byte)0; i < amountOfContentGroup; i++)
            {
                texturesName
                    .Add(interfaceSettings
                            .GetAllValues<string>(section: section,
                                                  name   : $"{layoutGroup} - {i}")[0]);
            }

            return texturesName;
        }
        
        internal SizeF ReturnSizeOfThis(string textureName)
        {
            var contentResolutionFile =
                ScriptSettings
                    .Load($@"{PathToTheHelideckSignalingFolder}\UserInterfaceResources\PopUpSettings\ContentSizes.ini");

            var resolutionInString =
                contentResolutionFile
                    .GetAllValues<string>(section: $"{textureName}",
                                          name   : "_")[0].Replace("Width:", "")
                                                          .Replace("Height:", "");
            var width =
                resolutionInString
                    .Split(' ')[0];
            var height =
                resolutionInString
                    .Split(' ')[1];

            return new SizeF(System.Single.Parse(width), 
                             System.Single.Parse(height));
        }
        
        internal SizeF ReturnResolutionOfThis(string textureName, string textureDict)
        {
            var hash =
                GTA.Native.Hash.GET_TEXTURE_RESOLUTION;

            var argument0 =
                textureDict;

            var argument1 =
                textureName;

            var resolutionInVector3 =
                GTA.Native.Function
                    .Call<GTA.Math.Vector3>(hash,
                                            argument0,
                                            argument1);

            return new SizeF(resolutionInVector3.X,
                             resolutionInVector3.Y);
        }
        
        internal PointF ReturnThePositionOfCenterOfScreen()
        {
            var displayCompatibility =
                ScriptSettings
                    .Load($@"{PathToTheHelideckSignalingFolder}\UserInterfaceResources\DisplayCompatibility.ini");

            var screenCompatibility =
                displayCompatibility
                    .GetAllValues<string>(section: "Compatibility",
                                          name   : $"{GTA.UI.Screen.AspectRatio}")[0];

            var screenCenterPosition =
                displayCompatibility
                    .GetAllValues<string>(section: screenCompatibility,
                                          name   : "Screen Center Position")[0];

            return new PointF(x: System.Single.Parse(screenCenterPosition), 
                              y: 0f);
        }
    }
}