using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public DelegateCommand CloseTabCommand { get; }
    public DelegateCommand OpenFileCommand { get; }
    public DelegateCommand SaveFileCommand { get; }
    public DelegateCommand SaveAsFileCommand { get; }
    public DelegateCommand SaveAllCommand { get; }

    public MainWindowVm(IWindowService windowService, IDocumentService documentService) : base(windowService)
    {
        _documentService = documentService;
        Documents.Add(_documentService.CreateNewDocument());
        _selectedDocument = Documents.First();

        OpenNewTabCommand = new DelegateCommand(_ => OpenNewTab());
        CloseTabCommand = new DelegateCommand(_ => CloseActiveDocument());
        OpenFileCommand = new DelegateCommand(_ => OpenFile());
        SaveFileCommand = new DelegateCommand(_ => SaveFile(SelectedDocument));
        SaveAsFileCommand = new DelegateCommand(_ => SaveAsFile(SelectedDocument));
        SaveAllCommand = new DelegateCommand(_ => SaveAll());
    }
       

    private void CloseActiveDocument()
    {
        switch (Documents.Count)
        {
            case 1:
                CloseWindowCommand.Execute(null);
                break;

            default:
                var index = Documents.IndexOf(SelectedDocument);
                Documents.Remove(SelectedDocument);
                SelectedDocument = Documents[Math.Min(index, Documents.Count - 1)];
                break;
        }
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

    private void SaveFile(IDocument document)
    {
        if (!string.IsNullOrEmpty(document.Metadata.FilePath))
        {
            _documentService.Save(document);
        }
        else
        {
            SaveAsFile(document);
        }
    }

    private void SaveAsFile(IDocument document)
    {
        _documentService.SaveAs(OpenSaveFileDialogHandler, document);
    }

    private void SaveAll()
    {
        foreach (var document in Documents)
        {
            SaveFile(document);
        }
    }
}
