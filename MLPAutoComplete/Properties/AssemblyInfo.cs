using System;
using ObjCRuntime;


[assembly: LinkWith ("libMLPAutoCompleteTextFieldSDK.a", LinkTarget.Simulator | LinkTarget.ArmV7 | LinkTarget.ArmV7s, Frameworks = "CoreGraphics", ForceLoad = true)]
