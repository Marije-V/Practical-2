using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace Cody.Services;

public sealed class FileDialogService
{
    private static readonly FilePickerFileType TextFiles = new("Text files")
    {
        Patterns = ["*.txt"]
    };

    private readonly IStorageProvider storageProvider;
    private readonly string initialDirectory;

    public FileDialogService(IStorageProvider storageProvider, string initialDirectory)
    {
        this.storageProvider = storageProvider;
        this.initialDirectory = initialDirectory;
    }

    // A null result means the user cancelled the dialog.
    public async Task<TextFile?> OpenTextAsync(string title)
    {
        var folder = await storageProvider.TryGetFolderFromPathAsync(new Uri(initialDirectory));
        var files = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = title,
            AllowMultiple = false,
            SuggestedStartLocation = folder,
            FileTypeFilter = [TextFiles]
        });

        if (files.Count == 0)
            return null;

        using var file = files[0];
        using var stream = await file.OpenReadAsync();
        using var reader = new StreamReader(stream);
        return new TextFile(file.Name, await reader.ReadToEndAsync());
    }

    // Returns the saved filename, or null when the user cancels.
    public async Task<string?> SaveTextAsync(string title, string suggestedName, string text)
    {
        var folder = await storageProvider.TryGetFolderFromPathAsync(new Uri(initialDirectory));
        using var file = await storageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = title,
            SuggestedStartLocation = folder,
            SuggestedFileName = string.IsNullOrWhiteSpace(suggestedName)
                ? "Untitled Program" : suggestedName,
            DefaultExtension = "txt",
            ShowOverwritePrompt = true,
            FileTypeChoices = [TextFiles]
        });

        if (file is null)
            return null;

        using (var stream = await file.OpenWriteAsync())
        {
            stream.SetLength(0);
            using var writer = new StreamWriter(stream);
            await writer.WriteAsync(text);
        }

        return file.Name;
    }
}

public sealed record TextFile(string FileName, string Text)
{
    public string Name => Path.GetFileNameWithoutExtension(FileName);
}
