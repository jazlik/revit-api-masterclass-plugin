using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Interop;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Masterclass.Revit.Utilities;

namespace Masterclass.Revit.SecondButton
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class SecondButtonCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                var uiApp = commandData.Application;
                var m = new SecondButtonModel(uiApp);
                var vm = new SecondButtonViewModel(m);
                // Setting the DataContext of the View, to the ViewModel.
                var v = new SecondButtonView { DataContext = vm };

                // Creating a WindowInteropHelper class for that window dialog that was created - View
                // and tied it to Revit's MainWindow as a child.
                var unused = new WindowInteropHelper(v)
                {
                    Owner = Process.GetCurrentProcess().MainWindowHandle
                };

                v.ShowDialog();

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
            panel.AddItem(new PushButtonData("SecondButtonCommand",
                "Second Button",
                assembly.Location,
                MethodBase.GetCurrentMethod().DeclaringType.FullName)
                {
                    ToolTip = "Second Button Command.",
                    // _32x32.Previous-Button-Transparent.jpg -> there should be a "_" before the path
                    LargeImage = ImageUtils.LoadImage(assembly, "_32x32.secondButton.png")
                });
        }
    }
}
