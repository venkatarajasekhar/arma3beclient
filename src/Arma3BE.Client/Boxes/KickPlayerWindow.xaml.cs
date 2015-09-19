﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using Arma3BEClient.Helpers;
using Arma3BEClient.Helpers.Views;
using GalaSoft.MvvmLight;

namespace Arma3BEClient.Boxes
{
    /// <summary>
    ///     Interaction logic for KickPlayerWindow.xaml
    /// </summary>
    public partial class KickPlayerWindow : Window
    {
        private readonly PlayerHelper _playerHelper;
        private readonly PlayerView _playerView;
        private readonly KickPlayerViewModel Model;

        public KickPlayerWindow(PlayerHelper playerHelper, PlayerView playerView)
        {
            _playerHelper = playerHelper;
            _playerView = playerView;
            InitializeComponent();

            Model = new KickPlayerViewModel(playerView);

            DataContext = Model;
        }

        private async void KickClick(object sender, RoutedEventArgs e)
        {
            await _playerHelper.KickAsync(_playerView, tbReason.Text);
            Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }


    public class KickPlayerViewModel : ViewModelBase
    {
        private string _reason;

        public KickPlayerViewModel(PlayerView playerView)
        {
            Player = playerView;
        }

        public PlayerView Player { get; }

        public string Reason
        {
            get { return _reason; }
            set
            {
                _reason = value;
                RaisePropertyChanged("Reason");
            }
        }

        public IEnumerable<string> Reasons
        {
            get
            {
                try
                {
                    var str =
                        ConfigurationManager.AppSettings["Kick_reasons"].Split('|')
                            .Where(x => !string.IsNullOrEmpty(x))
                            .Select(x => x.Trim())
                            .ToArray();

                    return str;
                }
                catch (Exception)
                {
                    return new[] {string.Empty};
                }
            }
        }
    }
}