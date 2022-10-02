using System;
using System.ComponentModel;
using Autodesk.Revit.DB;

namespace Masterclass.Revit.SecondButton
{
    public class SpatialObjectWrapper : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public double Area { get; set; }
        public ElementId Id { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            // Anytime isSelected is being "set", an event is going to be raised.
            set { isSelected = value; RaisePropertyChanged(nameof(IsSelected)); }
        }

        public SpatialObjectWrapper(SpatialElement room)
        {
            Name = room.Name;
            Area = room.Area;
            Id = room.Id;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
