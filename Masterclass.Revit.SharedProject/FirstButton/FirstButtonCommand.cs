using System;
using System.Reflection;
using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Masterclass.Revit.Utilities;

namespace Masterclass.Revit.FirstButton
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class FirstButtonCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                MessageBox.Show("Hello world!", "AEC Tech", MessageBoxButton.OK);

                return Result.Succeeded;
            }
            catch (Exception e)
            {

                return Result.Failed;
            }
        }

        public static void CreateButton(RibbonPanel panel)
        {
            // We need to get a path to your assembly, actual path to drl file on my drive
            var assembly = Assembly.GetExecutingAssembly();
            panel.AddItem(new PushButtonData("FirstButtonCommand",
                "First Button",
                assembly.Location,
                MethodBase.GetCurrentMethod().DeclaringType.FullName)
                {
                    ToolTip = "First Button Command.",
                    // _32x32.Previous-Button-Transparent.jpg -> there should be a "_" before the path
                    LargeImage = ImageUtils.LoadImage(assembly, "_32x32.firstButton.png")
                });
        }
    }
}
