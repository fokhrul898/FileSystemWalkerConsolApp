using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class FileSystemWalker
{
    static async Task Main(string[] args)
    {

        Console.WriteLine("Enter the File Path: ");
        string rootpath = Console.ReadLine();

        //Checked To Validate File Path
        if (!Directory.Exists(rootpath))
        {
            Console.WriteLine("Path is not valid ");
        }
        else
        {
            await PrintFileAndFoldersAsync(rootpath);
            Console.WriteLine("Done...");
        }
        Console.ReadKey();
    }

    private async static Task PrintFileAndFoldersAsync(string path)
    {
        try
        {
            DirectoryInfo info = new DirectoryInfo(path);

            // For file permission reason I Taken Only TopDirectoryOnly Search Option
            var files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);
            var directories = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);

            Console.WriteLine("The number of files encountered on " + path + ": " + files.Count());
            Console.WriteLine("The number of directories encountered on " + path + ": " + directories.Count());
            await PrintDirectorySizeAsync(files, path);
            foreach (var dirPath in directories)
            {
                await PrintFileAndFoldersAsync(dirPath);
            }
        }
        catch
        {
            //Exception Occurs for Not Permitted Files
        }
    }

    private async static Task PrintDirectorySizeAsync(string[] filepath, string path)
    {
        long fileSize = 0;
        foreach (string name in filepath)
        {
            FileInfo info = new FileInfo(name);
            fileSize += info.Length;
        }
        await Task.Run(() => Console.WriteLine("The file size total, in bytes, of all files encountered on " + path + ": " + fileSize));
        Console.WriteLine();
    }
}

