using PoeSeedcraftPricer.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace PoeSeedcraftPricer.Views
{
    /// <summary>
    /// Interaction logic for PriceItControl.xaml
    /// </summary>
    public partial class PriceItControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string StationCode { get; set; }
        public string Note { get; set; }

        public SavedCraft FirstCraft { get; set; }
        public SavedCraft SecondCraft { get; set; }
        public SavedCraft ThirdCraft { get; set; }

        public List<SeedCraft> MasterList { get; set; }

        public PriceItControl()
        {
            FirstCraft = new SavedCraft();
            SecondCraft = new SavedCraft();
            ThirdCraft = new SavedCraft();

            InitializeComponent();

            MasterList = SeedCraft.Load();
        }

        private void PriceItClick(object sender, RoutedEventArgs e)
        {
            ButtonCopyNote.Content = "Copy";

            var pastedStationLines = StationCode.Split(Environment.NewLine);
            // Entry 5 (index 4) will hold the information how many crafts there are in this station. 

            if(pastedStationLines.Length > 10 && Regex.IsMatch(pastedStationLines[4], "Crafts: (1|2|3)/3")) //a valid export of a stationcode
            {
                var craftCount = int.Parse(pastedStationLines[4].Replace("Crafts: ", "").Replace("/3", ""));

                // Entry 8 (Index 7) will hold the first craft.
                FirstCraft = new SavedCraft(SeedCraft.Find(MasterList, pastedStationLines[7]));
                //FirstCraft = new SavedCraft(pastedStationLines[7]);

                if (craftCount >= 2)
                    SecondCraft = new SavedCraft(SeedCraft.Find(MasterList, pastedStationLines[8]));
                else
                    SecondCraft = new SavedCraft(); 
                if (craftCount == 3)
                    ThirdCraft = new SavedCraft(SeedCraft.Find(MasterList, pastedStationLines[9]));
                else
                    ThirdCraft = new SavedCraft();

                Note = ConverterUtil.GetNoteForCrafts(new SavedCraft[] { FirstCraft, SecondCraft, ThirdCraft });

                OnPropertyChanged("FirstCraft");
                OnPropertyChanged("SecondCraft");
                OnPropertyChanged("ThirdCraft");
                OnPropertyChanged("Note");
            }
            else
            {
                MessageBox.Show("Invalid Station-Code. Please copy / paste again from ingame!");
            }
        }

        private void FirstCraftSkipClicked(object sender, RoutedEventArgs e)
        {
            FirstCraft.SkipCraft = !FirstCraft.SkipCraft;
            OnPropertyChanged("FirstCraft");

            var newResourceName = FirstCraft.SkipCraft ? "Add" : "Skip";
            ButtonSkipCraft1.Content = FindResource(newResourceName);

            UpdateNote();
        }

        private void SecondCraftSkipClicked(object sender, RoutedEventArgs e)
        {
            SecondCraft.SkipCraft = !SecondCraft.SkipCraft;
            OnPropertyChanged("SecondCraft");

            var newResourceName = SecondCraft.SkipCraft ? "Add" : "Skip";
            ButtonSkipCraft2.Content = FindResource(newResourceName);

            UpdateNote();
        }

        private void ThirdCraftSkipClicked(object sender, RoutedEventArgs e)
        {
            ThirdCraft.SkipCraft = !ThirdCraft.SkipCraft;
            OnPropertyChanged("ThirdCraft");

            var newResourceName = ThirdCraft.SkipCraft ? "Add" : "Skip";
            ButtonSkipCraft3.Content = FindResource(newResourceName);

            UpdateNote();
        }

        private void CopyNoteClicked(object sender, RoutedEventArgs e)
        {
            ButtonCopyNote.Content = "Copied!";
            Clipboard.SetText(Note);
        }

        private void UpdateNote()
        {
            Note = ConverterUtil.GetNoteForCrafts(new SavedCraft[] { FirstCraft, SecondCraft, ThirdCraft });
            OnPropertyChanged("Note");
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void TextboxStationCode_GotFocus(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void TextBoxCraft1Amount_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            FirstCraft.Price.Amount = int.Parse(TextBoxCraft1Amount.Text);
            MasterList.First(craft => craft.ShortDescription == FirstCraft.CraftDescription).Price = FirstCraft.Price;
            SeedCraft.Save(MasterList);
            OnPropertyChanged("FirstCraft");
            UpdateNote();
        }

        private void TextBoxCraft2Amount_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            SecondCraft.Price.Amount = int.Parse(TextBoxCraft2Amount.Text);
            MasterList.First(craft => craft.ShortDescription == SecondCraft.CraftDescription).Price = SecondCraft.Price;
            SeedCraft.Save(MasterList);
            OnPropertyChanged("SecondCraft");
            UpdateNote();
        }

        private void TextBoxCraft3Amount_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ThirdCraft.Price.Amount = int.Parse(TextBoxCraft3Amount.Text);
            MasterList.First(craft => craft.ShortDescription == ThirdCraft.CraftDescription).Price = ThirdCraft.Price;
            SeedCraft.Save(MasterList);
            OnPropertyChanged("ThirdCraft");
            UpdateNote();
        }
    }
}
