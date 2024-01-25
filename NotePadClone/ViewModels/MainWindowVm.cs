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

using NotePadClone.Models;
using NotePadClone.Services;

using WpfEssentials.Base;

namespace NotePadClone.ViewModels;
public class MainWindowVm : WindowVm
{
    
    private readonly IDocumentService _documentService;
    private IDocument _document;

    public IDocument Document
    {
        get => _document;
        set => SetField(ref _document, value);
    }

    public Func<string?>? OpenFileSelectionDialogHandler { get; set; }
    public Func<string?>? OpenSaveFileDialogHandler { get; set; }

    public DelegateCommand CreateNewDocumentCommand { get; }
    public DelegateCommand OpenFileCommand { get; }
    public DelegateCommand SaveFileCommand { get; }
    public DelegateCommand SaveAsFileCommand { get; }

    public MainWindowVm()
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
