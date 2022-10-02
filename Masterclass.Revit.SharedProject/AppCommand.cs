using System;
using System.Linq;
using Autodesk.Revit.UI;
using Masterclass.Revit.FirstButton;
using Masterclass.Revit.SecondButton;

namespace Masterclass.Revit
{
    class AppCommand : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication app)
        {
            try
            {
                // It is put in try catch just in case there is another tab called with the same name
                app.CreateRibbonTab("Masterclass");
            }
            catch (Exception e)
            {
                // Ignored
            }
            // This var is looking to get an existing panel which x.Name equals AEC Tech.
            // If it finds it, ribbonPanel var is assignet to that panel object.
            // If it cannot find it(is null) it creates a panel with that name.
            var ribbonPanel = app.GetRibbonPanels("Masterclass").FirstOrDefault(x => x.Name == "Masterclass") ??
                              app.CreateRibbonPanel("Masterclass", "Masterclass");
            
            // Create buttons
            FirstButtonCommand.CreateButton(ribbonPanel);
            SecondButtonCommand.CreateButton(ribbonPanel);


            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication app)
        {
            return Result.Succeeded;
        }
    }
}
