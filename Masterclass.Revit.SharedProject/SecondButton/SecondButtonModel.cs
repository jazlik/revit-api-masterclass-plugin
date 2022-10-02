using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;

namespace Masterclass.Revit.SecondButton
{
    public class SecondButtonModel
    {
        public UIApplication UiApp { get; }
        public Document Doc { get; }
        public SecondButtonModel(UIApplication uiApp)
        {
            // Getting the current uiApplication
            UiApp = uiApp;
            // Getting the current document from the uiApplication
            Doc = uiApp.ActiveUIDocument.Document;
        }

        // What is happening here. First we are reaching to Revit, grabbing all spatiacl elements(areas, spaces and rooms).
        // We filter just for rooms. Then we wrap those rooms and then from those rooms we create SpatialObjectWrapper objects.
        // At the end all those SpatialObjectWrapper objects are stored inside ObservableCollection called spatialObjects.
        public ObservableCollection<SpatialObjectWrapper> CollectSpatialObjects()
        {
            var spatialObjects = new FilteredElementCollector(Doc)
                .OfClass(typeof(SpatialElement))
                .WhereElementIsNotElementType() // Only instances of SpatialElements are needed, not types
                .Cast<SpatialElement>()
                .Where(x => x is Room)
                .Select(x => new SpatialObjectWrapper(x));

            return new ObservableCollection<SpatialObjectWrapper>(spatialObjects);
        }      
        
        public void Delete(List<SpatialObjectWrapper> selected)
        {
            // From selected Select the Ids of the objects and convert to list.
            var ids = selected.Select(x => x.Id).ToList();
            // In order to delete something, Revit needs a transaction.
            using (var trans = new Transaction(Doc, "Delete Rooms"))
            {
                trans.Start();
                Doc.Delete(ids);
                trans.Commit();
            }
        }
    }
}
