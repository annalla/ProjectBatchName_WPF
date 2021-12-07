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
using System.Collections.ObjectModel;
//using System.Windows.Shapes;



namespace ProjectBatchName
{
    public class Item
    {
        public string Name { get; set; }
        public Item(string name)
        {
            this.Name = name;
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //dùng cho drop & drag list 
        private Point _dragStartPoint;

        private T FindVisualParent<T>(DependencyObject child)
            where T : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
                return null;
            T parent = parentObject as T;
            if (parent != null)
                return parent;
            return FindVisualParent<T>(parentObject);
        }

        private IList<Item> _items = new ObservableCollection<Item>();

        public MainWindow()
        {
            InitializeComponent();

            _items.Add(new Item("AddCounter"));
            _items.Add(new Item("AddSuffix"));
            _items.Add(new Item("AddPrefix"));
            _items.Add(new Item("ChangeExtension"));
            _items.Add(new Item("PascalCase"));
            _items.Add(new Item("Lowercase"));
            _items.Add(new Item("Replace"));

            listBoxOderRule.DisplayMemberPath = "Name";
            listBoxOderRule.ItemsSource = _items;

            listBoxOderRule.PreviewMouseMove += ListBox_PreviewMouseMove;

            var style = new Style(typeof(ListBoxItem));
            style.Setters.Add(new Setter(ListBoxItem.AllowDropProperty, true));
            style.Setters.Add(
                new EventSetter(
                    ListBoxItem.PreviewMouseLeftButtonDownEvent,
                    new MouseButtonEventHandler(ListBoxItem_PreviewMouseLeftButtonDown)));
            style.Setters.Add(
                    new EventSetter(
                        ListBoxItem.DropEvent,
                        new DragEventHandler(ListBoxItem_Drop)));
            listBoxOderRule.ItemContainerStyle = style;
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
            DirectoryInfo presetDir = new DirectoryInfo(@".\presets");
            string[] allfiles = Directory.GetFileSystemEntries(@".\presets");
            foreach (string sFileName in allfiles)
            {
                //files
                if (Path.GetExtension(sFileName) != "")
                {
                    cmbPreset.Items.Add(Path.GetFileName(sFileName));
                }
            }
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
                if (CheckTarget(targets[i]))
                {
                    string response = processBatchName(targets[i].name, actions);
                    int duplicate = CheckDuplicate(i, targets[i].dir + response);
                    if (targets[i].extension == "")
                    {
                        try
                        {
                            if (duplicate!=0)
                            {
                                Rule actionDuplicate = ruleFactory.createRule("Duplicate", new Argument_1 { arg1 = duplicate.ToString() });
                                response = actionDuplicate.Rename(response);
                            }
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
                            targets[i].newName = "";
                            targets[i].name = response;
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
                            if (duplicate != 0)
                            {
                                Rule actionDuplicate = ruleFactory.createRule("duplicate", new Argument_1 { arg1 = duplicate.ToString() });
                                response = actionDuplicate.Rename(response);
                            }
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
                            targets[i].newName = "";
                            targets[i].name = response;
                        }
                        catch
                        {
                            targets[i].newName = "|";
                            targets[i].status = "Error";
                        }
                    }
                }
            }
        }

        private int CheckDuplicate(int i, string response)
        {
            string directory = targets[i].dir;
            directory = directory.Remove(directory.Length - 1, 1);
            string[] all;
            int result = 0;
            if (targets[i].extension=="")
            {
                all = Directory.GetDirectories(directory);
            }
            else
            {
                all = Directory.GetFiles(directory);
            }
            foreach (string str in all)
            {
                Debug.WriteLine(str);
                if (response == str)
                {
                    result++;
                }
            }
            if (result == 1 && response == (targets[i].dir + targets[i].name))
            {
                return 0;
            }
            for (int index = 0; index < i; index++)
            {
                string[] str = targets[index].newName.Split("_duplicate_");
                if (response == targets[index].dir + str[0]+ Path.GetExtension(targets[index].newName)||response== targets[index].dir+ targets[index].newName)
                {
                    result++;
                }
            }
            return result;
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
            actions.Add(ruleFactory.createRule("AddCounter", new Argument_3 { arg1 = Int32.Parse(startValueString), arg2 = Int32.Parse(stepString), arg3 = Int32.Parse(numString) }));
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
            actions.Add(ruleFactory.createRule("AddSuffix", new Argument_1 { arg1 = _sufixText }));
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
            actions.Add(ruleFactory.createRule("AddPrefix", new Argument_1 { arg1 = _prefixText }));
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
            actions.Add(ruleFactory.createRule("Replace", new Argument_2 { arg1 = _oldReplaceText, arg2 = _newReplaceText }));
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
            actions.Add(ruleFactory.createRule("ChangeExtention", new Argument_1 { arg1 = _extentionText }));
            return true;
        }

        private void handleLowCaseRemoveSpacesRule()
        {
            actions.Add(ruleFactory.createRule("Lowercase", new Arguments { }));
        }

        private void handlePascalCaseRule()
        {
            actions.Add(ruleFactory.createRule("PascalCase", new Arguments { }));
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
                if (CheckTarget(targets[i]))
                {
                    string response = processBatchName(targets[i].name, actions);
                    int duplicate = CheckDuplicate(i, targets[i].dir + response);
                    if (duplicate != 0)
                    {
                        Rule actionDuplicate = ruleFactory.createRule("Duplicate", new Argument_1 { arg1 = duplicate.ToString() });
                        response = actionDuplicate.Rename(response);
                    }
                    targets[i].newName = response;
                }
            }

        }

        private bool CheckTarget(TargetInfor targetInfor)
        {
            return true;
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
            string timeNow = DateTime.Now.ToString("yyyy-MM-dd h_mm_ss tt");
            string presetContent = createPresetContent();
            System.IO.File.WriteAllText(@".\presets\" + timeNow + ".bin", presetContent);
            cmbPreset.Items.Add(timeNow);

        }
        private string createPresetContent()
        {
            string presetContent = "";
            string option = "";

            if (AddExtention.IsChecked == true && extentionText.Text.CompareTo("") != 0 )
            {
                presetContent += "ChangeExtension| TRUE|" + extentionText.Text;
            }
            else
            {
                presetContent += "ChangeExtension| FALSE|";
            }

            presetContent += "\r\n";
            if (AddReplace.IsChecked == true && oldReplaceText.Text.CompareTo("")!=0 && newReplaceText.Text.CompareTo("") != 0)
            {
                presetContent += "Replace|TRUE|" + oldReplaceText.Text + "|" + newReplaceText.Text + "|";
            }
            else
            {
                presetContent += "Replace|FALSE|";
            }

            presetContent += "\r\n";
            if (AddCounterBox.IsChecked == true && StartValueCounter.Text.CompareTo("") != 0 
                && StepCounter.Text.CompareTo("") != 0 && NumberDigitCounter.Text.CompareTo("") != 0)
            {
                presetContent += "AddCounter|TRUE|" + StartValueCounter.Text + "|" + StepCounter.Text + "|" + NumberDigitCounter.Text;
            }
            else
            {
                presetContent += "AddCounter|FALSE|";
            }

            presetContent += "\r\n";
            if (AddSuffix.IsChecked == true && sufixText.Text.CompareTo("") != 0)
            {
                presetContent += "AddSuffix|TRUE|" + sufixText.Text;
            }
            else
            {
                presetContent += "AddSuffix|FALSE|";
            }

            presetContent += "\r\n";
            if (AddPrefix.IsChecked == true && prefixText.Text.CompareTo("") != 0)
            {
                presetContent += "AddPrefix|TRUE|" + prefixText.Text;
            }
            else
            {
                presetContent += "AddPrefix|FALSE|";
            }

            presetContent += "\r\n";
            if (LowCaseRemoveSpaces.IsChecked == true)
            {
                presetContent += "Lowercase|TRUE|" ;
            }
            else
            {
                presetContent += "Lowercase|FALSE|";
            }

            presetContent += "\r\n";
            if (PascalCase.IsChecked == true)
            {
                presetContent += "PascalCase|TRUE|";
            }
            else
            {
                presetContent += "PascalCase|FALSE|";
            }

            return presetContent;
        }

        private void applyPresetToRename(string pathFile)
        {
            string[] lines = File.ReadAllLines(pathFile);
            foreach (string line in lines)
            {
                string[] pieces = line.Split('|');
                switch (pieces[0])
                {
                    case "Replace":
                        if (pieces[1] == "TRUE")
                        {
                            AddReplace.IsChecked = true;
                            oldReplaceText.Text = pieces[2];
                            newReplaceText.Text = pieces[3];
                        }
                        else
                            AddReplace.IsChecked = false;
                        break;
                    case "ChangeExtension":
                        if (pieces[1] == "TRUE")
                        {
                            AddExtention.IsChecked = true;
                            extentionText.Text = pieces[2];
                        }
                        else
                            AddExtention.IsChecked = false;
                        break;
                    case "AddCounter":
                        if (pieces[1] == "TRUE")
                        {
                            AddCounterBox.IsChecked = true;
                            StartValueCounter.Text = pieces[2];
                            StepCounter.Text = pieces[3];
                            NumberDigitCounter.Text = pieces[4];
                            
                        }
                        else
                            AddCounterBox.IsChecked = false;
                        break;
                    case "AddPrefix":
                        if (pieces[1] == "TRUE")
                        {
                            AddPrefix.IsChecked = true;
                            prefixText.Text = pieces[2];
                        }
                        else
                            AddPrefix.IsChecked = false;
                        break;
                    case "AddSuffix":
                        if (pieces[1] == "TRUE")
                        {
                            AddSuffix.IsChecked = true;
                            sufixText.Text = pieces[2];
                        }
                        else
                            AddSuffix.IsChecked = false; break;
                    case "Lowercase":
                        if (pieces[1] == "TRUE")
                        {
                            LowCaseRemoveSpaces.IsChecked = true;
                        }
                        else
                            LowCaseRemoveSpaces.IsChecked = false; break;
                    case "PascalCase":
                        if (pieces[1] == "TRUE")
                            PascalCase.IsChecked = true;
                        else
                            PascalCase.IsChecked = false; break;
                    default: break;

                }
            }
        }

        /// <summary>
        /// deleteOneSetOfRule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteOneSetOfRule(object sender, RoutedEventArgs e)
        {
            var index = cmbPreset.SelectedItem;
            var path = cmbPreset.SelectedValue;
            if (index != null)
            {
                cmbPreset.Items.Remove(index);
                File.Delete(path.ToString());
                System.IO.File.Delete(@".\presets\" + path.ToString());
            }
        }

        ///<sumary>
        ///dùng cho kéo thả listboxorderrule
        ///</summary>
        private void ListBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point point = e.GetPosition(null);
            Vector diff = _dragStartPoint - point;
            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                var lb = sender as ListBox;
                var lbi = FindVisualParent<ListBoxItem>(((DependencyObject)e.OriginalSource));
                if (lbi != null)
                {
                    DragDrop.DoDragDrop(lbi, lbi.DataContext, DragDropEffects.Move);
                }
            }
        }
        private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStartPoint = e.GetPosition(null);
        }
        private void ListBoxItem_Drop(object sender, DragEventArgs e)
        {
            if (sender is ListBoxItem)
            {
                var source = e.Data.GetData(typeof(Item)) as Item;
                var target = ((ListBoxItem)(sender)).DataContext as Item;

                int sourceIndex = listBoxOderRule.Items.IndexOf(source);
                int targetIndex = listBoxOderRule.Items.IndexOf(target);

                Move(source, sourceIndex, targetIndex);
            }
        }
        private void Move(Item source, int sourceIndex, int targetIndex)
        {
            if (sourceIndex < targetIndex)
            {
                _items.Insert(targetIndex + 1, source);
                _items.RemoveAt(sourceIndex);
            }
            else
            {
                int removeIndex = sourceIndex + 1;
                if (_items.Count + 1 > removeIndex)
                {
                    _items.Insert(targetIndex, source);
                    _items.RemoveAt(removeIndex);
                }
            }
        }

     
        private void choosePresets(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPreset.SelectedItem != null)
            {
                applyPresetToRename(@".\presets\" + cmbPreset.SelectedValue.ToString());
            }
        }
    }
}
