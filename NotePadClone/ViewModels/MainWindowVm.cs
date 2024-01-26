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
    private IDocument _document;

    public IDocument Document
    {
        get => _document;
        set => SetField(ref _document, value);
    }

    /// <summary>
    /// Delegate to open the file selection dialog in the view that uses this view model and return the selected path.
    /// </summary>
    public Func<string?>? OpenFileSelectionDialogHandler { get; set; }

    /// <summary>
    /// Delegate to open the save file dialog in the view that uses this view model and return the file's selected path.
    /// </summary>
    public Func<string?>? OpenSaveFileDialogHandler { get; set; }

    public DelegateCommand CreateNewDocumentCommand { get; }
    public DelegateCommand OpenFileCommand { get; }
    public DelegateCommand SaveFileCommand { get; }
    public DelegateCommand SaveAsFileCommand { get; }

    public MainWindowVm(IWindowService windowService) : base(windowService)
    {
        _documentService = new DocumentService();
        _document = new Document();

        CreateNewDocumentCommand = new DelegateCommand(_ => CreateNewDocument());        
        OpenFileCommand = new DelegateCommand(_ => OpenFile());
        SaveFileCommand = new DelegateCommand(_ => SaveFile());
        SaveAsFileCommand = new DelegateCommand(_ => SaveAsFile());
    }

    private void CreateNewDocument() => Document = _documentService.CreateNewDocument();   

    private void OpenFile()
    {
        var document = _documentService.OpenDocument(OpenFileSelectionDialogHandler);
        if (document is not null)
        {
            Document = document;
        }        
    }

    private void SaveFile()
    {
        if (!string.IsNullOrEmpty(Document.Metadata.FilePath))
        {
            _documentService.Save(Document);
        }
        else
        {
            SaveAsFile();
        }
    }

    private void SaveAsFile()
    {
        _documentService.SaveAs(OpenSaveFileDialogHandler, Document);
    }
}
