using System.Text;


public class File {
    private bool _isOpen;
    private List<Block> _content;
    private BlockDevice _blockDevice;

    private bool _isActive = true;

    private int filelock = 0;

    public String FileName {get;set;}

    public File(string fileName) {
        FileName = fileName;
        _content = new List<Block>();
        _blockDevice = BlockDevice.GetInstance();
    }

    public String FRead() {
        if(_isActive == false)
            throw new Exception("File not valid.");
        if(_isOpen == false) {
            throw new Exception("File not open");
        }
        StringBuilder sb = new StringBuilder(_content.Sum(x=>x.Length));
        foreach(var block in _content) {
            sb.Append(block.GetContent());
        }
        return sb.ToString();
    }

    public bool FWrite(String newContent) {
        // if(Interlocked.CompareExchange(ref filelock, 1, 0) == 0){
        //     return false;
        // }
        if(_isActive == false)
            throw new Exception("File not valid.");
        if(_isOpen == false) {
            throw new Exception("File not open");
        }
        var lastBlock = GetLastOrDefault(_content);

        if(lastBlock != null && lastBlock.Length + newContent.Length <= lastBlock.Capacity)
        {
            lastBlock.Length = lastBlock.Length + newContent.Length;
            lastBlock.SetContent(lastBlock.GetContent() + newContent);
            return true;
        }
        var newBlock = _blockDevice.GetAvailableBlock(newContent.Length);
        newBlock.SetContent(newContent);
        _content.Add(newBlock);
        //Interlocked.Decrement(ref filelock);
        return true;
    }

    public bool IsOpen() {
        return _isOpen;
    }

private object fileOpenLock = new object();
    public bool OpenFile() {
        if(_isOpen == true)
            return false;
        
        lock(fileOpenLock){
            if(_isOpen == true)
                return false;
            _isOpen = true;
        }
        return true;
    }

    public void CloseFile(){
        _isOpen = false;
    }

    private Block GetLastOrDefault(List<Block> blocks) {
        if(blocks.Count == 0)
            return null;
        
        return blocks[blocks.Count - 1];
    }

    public void ReleaseMemory(){
        _content.ForEach(block => _blockDevice.ReleaseBlock(block));
        _content = null;
        _isOpen = false;
        _isActive = false;
    }
}