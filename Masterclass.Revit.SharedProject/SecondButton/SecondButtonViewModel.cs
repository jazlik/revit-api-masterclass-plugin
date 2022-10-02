using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Masterclass.Revit.SecondButton
{
    public class SecondButtonViewModel : ViewModelBase
    {
        public SecondButtonModel Model { get; }
        // 2A. This is the method which is called from the Command called Close in View.
        public RelayCommand<Window> Close { get; set; }
        public RelayCommand<Window> Delete { get; set; }
        private ObservableCollection<SpatialObjectWrapper> spatialObjects;
        public ObservableCollection<SpatialObjectWrapper> SpatialObjects
        { 
            get { return spatialObjects; }
            set { spatialObjects = value; RaisePropertyChanged(() => SpatialObjects); }
        }

        public SecondButtonViewModel(SecondButtonModel model)
        {
            Model = model;
            SpatialObjects = Model.CollectSpatialObjects();
            // 3A. When RelayCommand gets triggered, OnClose is executed.
            Close = new RelayCommand<Window>(OnClose);
            Delete = new RelayCommand<Window>(OnDelete);
        }

        // 4A. OnClose execution.
        private void OnClose(Window win)
        {
            win.Close();
        }

        private void OnDelete(Window win)
        {
            // selected is a list of objects from SpatialObjects where IsSelected equal true.
            var selected = SpatialObjects.Where(x => x.IsSelected).ToList();
            Model.Delete(selected);
            win.Close();
        }
    }
}
 