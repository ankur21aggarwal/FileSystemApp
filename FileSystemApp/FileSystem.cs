public class FileSystem {
    private Dictionary<String, File> _files;
    private BlockDevice _blockDevice;

    public FileSystem() {
        _files = new Dictionary<String, File>();
        _blockDevice = BlockDevice.GetInstance();
    }

    public File Fopen(string fileName) {
        if(_files.ContainsKey(fileName) == false)
            _files[fileName] = new File(fileName);

        var file = _files[fileName];
        if(file.IsOpen())
            throw new Exception("File already open and used by another process.");

        if(file.OpenFile() == false)
            throw new Exception("Could not open the file.");
        return file;
    }

    public void Fclose(File file) {
        if(file == null){
            throw new Exception("Invalid file argument");
        }

        file.CloseFile();
    }

    public bool Rename(string fileName, String newFileName){
        if(_files.ContainsKey(fileName) == false)
            throw new Exception("File does not Exist");

        if(_files.ContainsKey(newFileName)) {
            return false;
        }

        var file = _files[fileName];
        if(file.IsOpen())
            throw new Exception("File already open and used by another process.");
        
        _files.Remove(fileName);
        _files.Add(newFileName, file);
        return true;
    }

    public bool Remove(string fileName) {
        if(_files.ContainsKey(fileName) == false)
            throw new Exception("File does not Exist");

        var file = _files[fileName];
        if(file.IsOpen())
            throw new Exception("File already open and used by another process.");
        
        _files.Remove(fileName);

        file.ReleaseMemory();
        return true;
    }
}