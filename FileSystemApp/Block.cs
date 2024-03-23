public class Block {
    private static long nextBlockId = 1;
    private String _content;

    public long Id {get; set;}
    public int Capacity{get;set;}
    public int Length{get;set;}

    public Block(int capacity) {
        Id = getNewBlockId();
        Capacity = capacity;
    }

    public void SetContent(String content) {
        if(content != null && content.Length > Capacity)
            throw new ArgumentException("");
        
        _content = content;
        if(content != null)
            Length = content.Length;
    }

    private static long getNewBlockId(){
        return nextBlockId++;
    }

    public String GetContent(){
        return _content;
    }
}