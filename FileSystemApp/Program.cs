// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

FileSystem fileSystem = new FileSystem();
var file1 = fileSystem.Fopen("file1");
Console.WriteLine(file1.FRead());
Console.WriteLine(file1.FWrite("Line1\n"));
Console.WriteLine(file1.FRead());
Console.WriteLine(file1.FWrite("Line2\n"));
Console.WriteLine(file1.FRead());
var file2 = fileSystem.Fopen("file2");
Console.WriteLine(file2.FRead());
Console.WriteLine(file2.FWrite("Line3\n"));
Console.WriteLine(file2.FRead());
Console.WriteLine(file2.FWrite("Line4\n"));
Console.WriteLine(file2.FRead());
fileSystem.Fclose(file2);
fileSystem.Remove(file2.FileName);
file1.CloseFile();
Console.WriteLine(fileSystem.Rename("file1", "file5"));
var file5 = fileSystem.Fopen("file5");
Console.WriteLine(file5.FRead());

//Console.WriteLine(file2.FRead());





