﻿using Avalonia.Controls;

using DynamicData;
using DynamicData.Binding;
using ReactiveUI;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace Aldwych.Logging.ViewModels
{
    public class LogViewViewModel : ReactiveObject
    {
        public ReadOnlyObservableCollection<LogItemViewModel> LogItems { get; }

        object selectedItem;
        public object SelectedItem
        {
            get => selectedItem;
            set => this.RaiseAndSetIfChanged(ref selectedItem, value);
        }

        private bool showLogLevel;
        public bool ShowLogLevel
        {
            get => showLogLevel;
            set => this.RaiseAndSetIfChanged(ref showLogLevel, value);
        }

        int filterSelectedIndex;
        public int FilterSelectedIndex
        {
            get => filterSelectedIndex;
            set => this.RaiseAndSetIfChanged(ref filterSelectedIndex, value);
        }


        private Func<LogItemViewModel, bool> CreatePredicate(int selectedIndex)
        {
            var inverted = ReverseNumber(selectedIndex, 0, 5);
            return logItemVM => (int)logItemVM.LogLevel <= inverted;
        }

        public int ReverseNumber(int num, int min, int max)
        {
            return (max + min) - num;
        }


        public LogViewViewModel()
        {
            /*
            var sl = LogCatcher.LogEntryObservable.ToObservableChangeSet(limitSizeTo: 2000).AsObservableList();
            var dynamicFilter = this.WhenAnyValue(x => x.FilterSelectedIndex).Select(x => CreatePredicate(x));
            var loader = sl.Connect().DisposeMany().Filter(dynamicFilter).Sort(SortExpressionComparer<LogItemViewModel>.Ascending(i => i.Created)).Bind(out var logItems).Subscribe();
            */
            LogItems = new ReadOnlyObservableCollection<LogItemViewModel>(LogCatcher.LogItems);
            //LogCatcher.LogEntryObservable.Subscribe(x => SelectedItem = LogItems.LastOrDefault());
            //var updateSelectedIndex = this.WhenAnyValue(x => x.FilterSelectedIndex).Do(_ => UpdateSelectedItem()).Subscribe();

            //var lvm = new LogItemViewModel("test", Microsoft.Extensions.Logging.LogLevel.None, new Microsoft.Extensions.Logging.EventId(5052, "omg"), this, null, "this is a test");
            //LogCatcher.Append(lvm);

            ShowLogLevel = true;
        }

        void UpdateSelectedItem()
        {
            //selectedItem = LogItems.LastOrDefault();
        }
    }
}
