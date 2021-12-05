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
using System.Text.RegularExpressions;
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
        List<Rule> actions = new List<Rule>();
        RuleFactory ruleFactory;

        //private static Dictionary<string,int> filesAndfolders = new Dictionary<string, int>();
        private void btnExit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Add multiple files to targets and Show in List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        name = Path.GetFileName(sFileName),
                        extension = Path.GetExtension(sFileName),
                        dir = Path.GetDirectoryName(sFileName) + "\\"
                    };
                    for (int i = 0; i < targets.Count; i++)
                    {
                        if (targets[i].name == target.name)
                        {
                            targets.Remove(targets[i]);
                        }
                    }
                    targets.Add(target);
                    Debug.WriteLine(target.toString());
                    dataListViewCurrent.Items.Add(target);
                }
                //dataListViewCurrent.Items.Clear();
                //foreach (TargetInfor tar in targets)
                //{
                //    dataListViewCurrent.Items.Add(tar);
                //}
                //dataListViewCurrent.ItemsSource = targets;

                //txtGetFile.Text = Path.GetDirectoryName(sFileName);
            }
        }
        /// <summary>
        /// Add multiple folders to targets and Show in List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        name = Path.GetFileName(sFileName),
                        extension = Path.GetExtension(sFileName),
                        dir = Path.GetDirectoryName(sFileName) + "\\"
                    };
                    for (int i = 0; i < targets.Count; i++)
                    {
                        if (targets[i].name == target.name)
                        {
                            targets.Remove(targets[i]);
                        }
                    }
                    targets.Add(target);
                    Debug.WriteLine(target.toString());
                    dataListViewCurrent.Items.Add(target);
                }
                //dataListViewCurrent.Items.Clear();
                //foreach (TargetInfor tar in targets)
                //{
                //    dataListViewCurrent.Items.Add(tar);
                //}
            }
        }

        private void window_loaded(object sender, RoutedEventArgs e)
        {
            ruleFactory = new RuleFactory();
            //dataListViewCurrent.ItemsSource = targets;
        }
        /// <summary>
        /// Add all files in Folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addAllFilesInFolder(object sender, RoutedEventArgs e)
        {
            //add all files in folder
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\";
            dialog.Multiselect = true;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                foreach (string d in dialog.FileNames)
                {
                    foreach (string sFileName in Directory.GetFiles(d))
                    {
                        TargetInfor target = new TargetInfor
                        {
                            newName = "",
                            status = "",
                            name = Path.GetFileName(sFileName),
                            extension = Path.GetExtension(sFileName),
                            dir = Path.GetDirectoryName(sFileName) + "\\"
                        };
                        for (int i = 0; i < targets.Count; i++)
                        {
                            if (targets[i].name == target.name)
                            {
                                targets.Remove(targets[i]);
                            }
                        }
                        targets.Add(target);
                        Debug.WriteLine(target.toString());
                        dataListViewCurrent.Items.Add(target);
                    }
                }
            }
        }
        /// <summary>
        /// refresh all files and folders in CurrentNameList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshAll(object sender, RoutedEventArgs e)
        {
            targets.Clear();
            dataListViewCurrent.Items.Clear();
        }
        /// <summary>
        /// Delete an item in list Current Name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var index = dataListViewCurrent.SelectedIndex;
            targets.RemoveAt(index);
            dataListViewCurrent.Items.RemoveAt(index);
        }
        /// <summary>
        /// handle batch name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BatchNameClick(object sender, RoutedEventArgs e)
        {
            if (actions.Count != 0)
            {
                actions.Clear();
            }
            if (AddCounterBox.IsChecked == true)
            {
                handleAddCounterRule();
            }
            //Thêm hậu tố
            if (AddSuffix.IsChecked == true)
            {
                handleAddSuffixRule();
            }
<<<<<<< HEAD
            //Thêm tiền tố
            if (AddPrefix.IsChecked == true)
            {
                handleAddPrefixRule();
            }
            //replace
            if (AddReplace.IsChecked == true)
            {
                handleAddReplaceRule();
            }
            //extention
            if (AddExtention.IsChecked == true)
            {
                handleAddExtentionRule();
            }
=======
>>>>>>> 60bbf588b206e07c701fe7c2e6b411b96bfef503
            //Low Case & Remove Spaces
            if (LowCaseRemoveSpaces.IsChecked == true)
            {
                handleLowCaseRemoveSpacesRule();
            }
            //Pascal Case
            if (PascalCase.IsChecked == true)
            {
                handlePascalCaseRule();
            }
            for (int i = 0; i < targets.Count; i++)
            {
                string response = processBatchName(targets[i].name);
                if (targets[i].extension == "")
                {
                    try
                    {
                        Directory.Move(targets[i].dir + targets[i].name, targets[i].dir + response);
                        targets[i].newName = response;
                        if (targets[i].newName != targets[i].name)
                        {
                            targets[i].status = "Changed";
                        }
                        else
                        {
                            targets[i].status = "UnChanged";
                        }
                    }
                    catch
                    {
                        targets[i].newName = "error";
                    }
                }
                else
                {
                    try
                    {
                        File.Move(targets[i].dir + targets[i].name, targets[i].dir + response);
                        targets[i].newName = response;
                        if (targets[i].newName != targets[i].name)
                        {
                            targets[i].status = "Changed";
                        }
                        else
                        {
                            targets[i].status = "UnChanged";
                        }
                    }
                    catch
                    {
                        targets[i].newName = "error";
                    }
                }
            }
        }

        private void handleAddCounterRule()
        {
            string numString = NumberDigitCounter.Text;
            string startValueString = StartValueCounter.Text;
            string stepString = StepCounter.Text;
            if (stepString.Length == 0 || stepString == null)
            {
                stepString = "1";
            }
            if (numString == null || startValueString == null || numString.Length == 0 || startValueString.Length == 0)
            {
                MessageBox.Show("Add Counter: Start Value or Number Of Digits empty!!", "Warning");
                return;
            }
            if (!Regex.IsMatch(numString, @"^\d+$") || !Regex.IsMatch(stepString, @"^\d+$") || !Regex.IsMatch(startValueString, @"^\d+$"))
            {
                MessageBox.Show("Add Counter: Start Value or Number Of Digits or Steps is not an integer!!", "Warning");
                return;
            }
            actions.Add(ruleFactory.createRule("counter", new Argument_3 { arg1 = Int32.Parse(startValueString), arg2 = Int32.Parse(stepString), arg3 = Int32.Parse(numString) }));
        }

        private void handleAddSuffixRule()
        {
            string _sufixText = sufixText.Text;

            if (_sufixText == null)
            {
                //MessageBox.Show("Add Counter: Start Value or Number Of Digits empty!!", "Warning");
                //return;
                _sufixText = "";
            }
            actions.Add(ruleFactory.createRule("suffix", new Argument_1 { arg1 = _sufixText }));
        }

<<<<<<< HEAD
        private void handleAddPrefixRule()
        {
            string _prefixText = prefixText.Text;

            if (_prefixText == null)
            {
                //MessageBox.Show("Add Counter: Start Value or Number Of Digits empty!!", "Warning");
                //return;
                _prefixText = "";
            }
            actions.Add(ruleFactory.createRule("prefix", new Argument_1 { arg1 = _prefixText }));
        }

        private void handleAddReplaceRule()
        {
            string _oldReplaceText = oldReplaceText.Text;
            string _newReplaceText = newReplaceText.Text;

            if (_newReplaceText == null || _oldReplaceText == null)
            {
                //MessageBox.Show("Add Counter: Start Value or Number Of Digits empty!!", "Warning");
                //return;
                _newReplaceText = "";
                _oldReplaceText = "";
            }
            actions.Add(ruleFactory.createRule("replace", new Argument_2 { arg1 = _oldReplaceText, arg2 = _newReplaceText }));
        }

        private void handleAddExtentionRule()
        {
            string _extentionText = extentionText.Text;

            if (_extentionText == null)
            {
                //MessageBox.Show("Add Counter: Start Value or Number Of Digits empty!!", "Warning");
                //return;
                _extentionText = "";
            }
            actions.Add(ruleFactory.createRule("ext", new Argument_1 { arg1 = _extentionText }));
        }

=======
>>>>>>> 60bbf588b206e07c701fe7c2e6b411b96bfef503
        private void handleLowCaseRemoveSpacesRule()
        {
            actions.Add(ruleFactory.createRule("lowercase", new Arguments { }));
        }

        private void handlePascalCaseRule()
        {
            actions.Add(ruleFactory.createRule("pascalcase", new Arguments { }));
        }

        /// <summary>
        /// Handle preview batch name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandlePreview(object sender, RoutedEventArgs e)
        {
            if (actions.Count != 0)
            {
                actions.Clear();
            }
            //Thêm bộ đếm
            if (AddCounterBox.IsChecked == true)
            {
                handleAddCounterRule();
            }
            //Thêm hậu tố
            if (AddSuffix.IsChecked == true)
            {
                handleAddSuffixRule();
            }
<<<<<<< HEAD

            //tiền tố
            if (AddPrefix.IsChecked == true)
            {
                handleAddPrefixRule();
            }

            //replace
            if (AddReplace.IsChecked == true)
            {
                handleAddReplaceRule();
            }
            //extention
            if (AddExtention.IsChecked == true)
            {
                handleAddExtentionRule();
            }

=======
>>>>>>> 60bbf588b206e07c701fe7c2e6b411b96bfef503
            //Low Case & Remove Spaces
            if (LowCaseRemoveSpaces.IsChecked == true)
            {
                handleLowCaseRemoveSpacesRule();
            }
            //Pascal Case
            if (PascalCase.IsChecked == true)
            {
                handlePascalCaseRule();
            }
            for (int i = 0; i < targets.Count; i++)
            {
                string response = processBatchName(targets[i].name);
                targets[i].newName = response;
            }

        }

        private string processBatchName(string name)
        {
            string res = name;
            for (int j = 0; j < actions.Count; j++)
            {
                res = actions[j].Rename(res);
                if (res == "|")
                {
                    break;
                }
            }
            return res;
        }
    }
}
