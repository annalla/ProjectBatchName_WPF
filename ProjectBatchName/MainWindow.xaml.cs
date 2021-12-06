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
using System.Collections;
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
        List<List<Rule>> presets = new List<List<Rule>>();
        RuleFactory ruleFactory;

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
                        if (targets[i].toString() == target.toString())
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
                        if (targets[i].toString() == target.toString())
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
                        string[] allfiles = Directory.GetFileSystemEntries(d, "*", SearchOption.AllDirectories);
                        foreach (string sFileName in allfiles)
                        {
                            Debug.WriteLine(sFileName);
                            //files
                            if (Path.GetExtension(sFileName) != "")
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
                                    if (targets[i].toString() == target.toString())
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
                if (!handleAddCounterRule())
                    return ;
            }
            //Thêm hậu tố
            if (AddSuffix.IsChecked == true)
            {
                if (!handleAddSuffixRule())
                    return ;
            }
            //Thêm tiền tố
            if (AddPrefix.IsChecked == true)
            {
                if (!handleAddPrefixRule())
                    return ;
            }
            //replace
            if (AddReplace.IsChecked == true)
            {
                if (!handleAddReplaceRule())
                    return ;
            }
            //extention
            if (AddExtention.IsChecked == true)
            {
                if (!handleAddExtentionRule())
                    return ;
            }
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
                string response = processBatchName(targets[i].name, actions);
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
                        targets[i].newName = "|";
                        targets[i].status = "Error";
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
                        targets[i].newName = "|";
                        targets[i].status = "Error";
                    }
                }
            }
        }

        private bool handleAddCounterRule()
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
                return false;
            }
            if (!Regex.IsMatch(numString, @"^\d+$") || !Regex.IsMatch(stepString, @"^\d+$") || !Regex.IsMatch(startValueString, @"^\d+$"))
            {
                MessageBox.Show("Add Counter: Start Value or Number Of Digits or Steps is not an integer!!", "Warning");
                return false;
            }
            actions.Add(ruleFactory.createRule("counter", new Argument_3 { arg1 = Int32.Parse(startValueString), arg2 = Int32.Parse(stepString), arg3 = Int32.Parse(numString) }));
            return true;
        }

        private bool handleAddSuffixRule()
        {
            string _sufixText = sufixText.Text;
            if (_sufixText == null)
            {
                MessageBox.Show("Suffix empty", "Warning");
                return false;
            }
            if (checkSpecialCharacter(_sufixText))
            {
                MessageBox.Show("Is not contain < > ? * \" \\ : | / ", "Warning");
                return false;
            }
            actions.Add(ruleFactory.createRule("suffix", new Argument_1 { arg1 = _sufixText }));
            return true;
        }

        private bool handleAddPrefixRule()
        {
            string _prefixText = prefixText.Text;

            if (_prefixText == null)
            {
                MessageBox.Show("Suffix empty", "Warning");
                return false;
            }
            if (checkSpecialCharacter(_prefixText))
            {
                MessageBox.Show("Is not contain < > ? * \" \\ : | / ", "Warning");
                return false;
            }
            actions.Add(ruleFactory.createRule("prefix", new Argument_1 { arg1 = _prefixText }));
            return true;
        }

        private bool handleAddReplaceRule()
        {
            string _oldReplaceText = oldReplaceText.Text;
            string _newReplaceText = newReplaceText.Text;

            if (_newReplaceText == null || _oldReplaceText == null)
            {
                _newReplaceText = "";
                _oldReplaceText = "";
            }
            if (checkSpecialCharacter(_newReplaceText))
            {
                MessageBox.Show("Is not contain < > ? * \" \\ : | / ", "Warning");
                return false;
            }
            actions.Add(ruleFactory.createRule("replace", new Argument_2 { arg1 = _oldReplaceText, arg2 = _newReplaceText }));
            return true;
        }

        private bool handleAddExtentionRule()
        {
            string _extentionText = extentionText.Text;

            if (_extentionText == null)
            {
                MessageBox.Show("Extension is empty!!", "Warning");
                return false;
            }
            if (checkSpecialCharacter(_extentionText))
            {
                MessageBox.Show("Is not contain < > ? * \" \\ : | / ", "Warning");
                return false;
            }
            actions.Add(ruleFactory.createRule("ext", new Argument_1 { arg1 = _extentionText }));
            return true;
        }

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
                if (!handleAddCounterRule()) return;
            }
            //Thêm hậu tố
            if (AddSuffix.IsChecked == true)
            {
                if(!handleAddSuffixRule()) return;
            }

            //tiền tố
            if (AddPrefix.IsChecked == true)
            {
                if (!handleAddPrefixRule())
                    return;
            }

            //replace
            if (AddReplace.IsChecked == true)
            {
                if (!handleAddReplaceRule())
                    return;
            }
            //extention
            if (AddExtention.IsChecked == true)
            {
                if (!handleAddExtentionRule())
                    return ;
            }
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
                string response = processBatchName(targets[i].name,actions);
                targets[i].newName = response;
            }

        }

        private string processBatchName(string name,List<Rule> action)
        {
            string res = name;
            for (int j = 0; j < action.Count; j++)
            {
                res = action[j].Rename(res);
                if (res == "|")
                {
                    break;
                }
            }
            return res;
        }
        private bool checkSpecialCharacter(string str)
        {
            string pattern= @"[<>:?*\""\\/|]";
            return Regex.IsMatch(str,pattern);
        }
        /// <summary>
        /// save Preset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void savePreset(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// deleteOneSetOfRule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteOneSetOfRule(object sender, RoutedEventArgs e)
        {

        }
    }
}
