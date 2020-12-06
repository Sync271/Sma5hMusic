﻿using Avalonia.Controls;
using Microsoft.Extensions.Logging;
using Sm5shMusic.GUI.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Sm5shMusic.GUI.Dialogs
{
    public class FileDialog : IFileDialog
    {
        private readonly ILogger _logger;
        private readonly IDialogWindow _rootDialogWindow;
        private readonly OpenFileDialog _openFileDialog;
        private string _savedDirectory;

        public FileDialog(IDialogWindow rootDialogWindow, ILogger<FileDialog> logger)
        {
            _logger = logger;
            _rootDialogWindow = rootDialogWindow;
            _savedDirectory = Environment.CurrentDirectory;
            _openFileDialog = new OpenFileDialog();
        }

        public async Task<string[]> OpenFileDialogAudio(Window parent = null)
        {
            _logger.LogDebug("Opening FileDialog...");

            _openFileDialog.AllowMultiple = true;
            _openFileDialog.Directory = _savedDirectory;
            _openFileDialog.Filters = new List<FileDialogFilter>()
            {
                new FileDialogFilter()
                {
                    Extensions = new List<string>()
                    {
                        "brstm", "lopus", "idsp", "nus3audio"
                    },
                    Name = "Songs"
                }
            };
            _openFileDialog.Title = "Load Audio Files";

            string[] results;
            if (parent == null)
                results = await _openFileDialog.ShowAsync(_rootDialogWindow.Window);
            else
                results = await _openFileDialog.ShowAsync(parent);

            if (results.Length > 0)
                _savedDirectory = Path.GetDirectoryName(results[0]);

            _logger.LogDebug("Selected {NbrItems} items", results?.Length);

            return results;
        }
    }
}