using Microsoft.Win32;
using System;
using System.IO;
using Path = System.IO.Path;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;


namespace ProjectBatchName
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
        }
        BindingList<TargetInfor> targets = new BindingList<TargetInfor>();
        private static Dictionary<string,int> filesAndfolders = new Dictionary<string, int>();
        private void btnExit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void addFiles(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\";
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {

                foreach (string sFileName in dialog.FileNames)
                {
                    TargetInfor target = new TargetInfor
                    {
                        newName = "",
                        status = "",
                        newExtension = "",
                        name = Path.GetFileNameWithoutExtension(sFileName),
                        extension = Path.GetExtension(sFileName),
                        dir = Path.GetDirectoryName(sFileName) + "\\"
                    };
                    for (int i = 0; i < targets.Count; i++)
                    {
                        if (targets[i].fullname==target.fullname)
                        {
                            Debug.WriteLine("TT");
                            targets.Remove(targets[i]);
                        }
                    }
                    targets.Add(target);
                    dataListViewCurrent.Items.Clear();
                    foreach (TargetInfor tar in targets)
                    {
                        dataListViewCurrent.Items.Add(tar);
                        Debug.WriteLine(tar.fullname);
                    }



                }
                //dataListViewCurrent.ItemsSource = targets;
                
                //txtGetFile.Text = Path.GetDirectoryName(sFileName);
            }
        }

        private void addFolders(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\";
            dialog.Multiselect = true;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {

                foreach (string sFileName in dialog.FileNames)
                {
                    TargetInfor target = new TargetInfor
                    {
                        newName = "",
                        status = "",
                        newExtension = "",
                        name = Path.GetFileNameWithoutExtension(sFileName),
                        extension = Path.GetExtension(sFileName),
                        dir = Path.GetDirectoryName(sFileName) + "\\"
                    };
                    for (int i = 0; i < targets.Count; i++)
                    {
                        if (targets[i].fullname == target.fullname)
                        {
                            Debug.WriteLine("TT");
                            targets.Remove(targets[i]);
                        }
                    }
                    targets.Add(target);
                    Debug.WriteLine(target.fullname);
                }
            }
        }

        private void window_loaded(object sender, RoutedEventArgs e)
        {
            //dataListViewCurrent.ItemsSource = targets;
        }
    }
}
