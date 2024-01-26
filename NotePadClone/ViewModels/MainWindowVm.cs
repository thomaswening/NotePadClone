using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;

using NotePadClone.DocumentModel;
using NotePadClone.Services;

using WpfEssentials.Base;

namespace NotePadClone.ViewModels;

/// <summary>
/// View model for the main window, representing the text editor.
/// </summary>
public class MainWindowVm : WindowVm
{
    
    private readonly IDocumentService _documentService;
    private IDocument _selectedDocument;

    public IDocument SelectedDocument
    {
        get => _selectedDocument;
        set => SetField(ref _selectedDocument, value);
    }

    public ObservableCollection<IDocument> Documents { get; } = [ ];

    /// <summary>
    /// Delegate to open the file selection dialog in the view that uses this view model and return the selected path.
    /// </summary>
    public Func<string?>? OpenFileSelectionDialogHandler { get; set; }

    /// <summary>
    /// Delegate to open the save file dialog in the view that uses this view model and return the file's selected path.
    /// </summary>
    public Func<string?>? OpenSaveFileDialogHandler { get; set; }

    public DelegateCommand OpenNewTabCommand { get; }
    public DelegateCommand OpenFileCommand { get; }
    public DelegateCommand SaveFileCommand { get; }
    public DelegateCommand SaveAsFileCommand { get; }

    public MainWindowVm(IWindowService windowService, IDocumentService documentService) : base(windowService)
    {
        _documentService = documentService;
        Documents.Add(_documentService.CreateNewDocument());
        _selectedDocument = Documents.First();

        OpenNewTabCommand = new DelegateCommand(_ => OpenNewTab());
        OpenFileCommand = new DelegateCommand(_ => OpenFile());
        SaveFileCommand = new DelegateCommand(_ => SaveFile());
        SaveAsFileCommand = new DelegateCommand(_ => SaveAsFile());
    }

    private void OpenNewTab()
    {
        Documents.Add(_documentService.CreateNewDocument());
        SelectedDocument = Documents.Last();
    }   

    private void OpenFile()
    {
        var document = _documentService.OpenDocument(OpenFileSelectionDialogHandler);
        if (document is not null)
        {
            Documents.Add(document);
            SelectedDocument = Documents.Last();
        }        
    }

    private void SaveFile()
    {
        if (!string.IsNullOrEmpty(SelectedDocument.Metadata.FilePath))
        {
            _documentService.Save(SelectedDocument);
        }
        else
        {
            SaveAsFile();
        }
    }

    private void SaveAsFile()
    {
        _documentService.SaveAs(OpenSaveFileDialogHandler, SelectedDocument);
    }
}
