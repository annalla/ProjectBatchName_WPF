using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ProjectBatchName
{
    class TargetInfor : INotifyPropertyChanged
    {
        public string name { get; set; }
        public string newName { get; set; }
        public string status { get; set; }
        public string dir { get; set; }
        public string extension { get; set; }

        public string newExtension { get; set; }

        public string fullname
        {
            get => extension != "" ? name + extension : name;
            set => fullname= extension != "" ? name + extension : name;
        }
        public string newFullname
        {
            get => newExtension != "" ? newName + newExtension : newName; 
            set => newFullname = newExtension != "" ? newName + newExtension : newName;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
