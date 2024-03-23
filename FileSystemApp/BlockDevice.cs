public class BlockDevice {
    private Dictionary<long, Block> _availableBlocks;
    private Dictionary<long, Block> _usedBlocks;

    private BlockDevice() {
        _availableBlocks = new Dictionary<long, Block>();
        _usedBlocks = new Dictionary<long, Block>();
    }

    private static BlockDevice _blockDevice;
    private static object _lockObject = new object();

    public static BlockDevice GetInstance() {
        if(_blockDevice != null)
            return _blockDevice;
        lock(_lockObject) {
            if (_blockDevice == null)
                _blockDevice = new BlockDevice();
        }
        return _blockDevice;
    }

    public Block GetAvailableBlock(int length) {
        Block block = null;
        foreach(var availableBlock in _availableBlocks.Values) {
            if(availableBlock.Capacity >= length)
            {
                if(block == null || availableBlock.Capacity < block.Capacity)
                    block = availableBlock;
            }
        }
        if(block == null)
        {
            block = new Block(length);
            _usedBlocks.Add(block.Id, block);
        }
        return block;
    }

    // public Block GetUsedBlock() {

    // }

    public void ReleaseBlock(Block block) {
        block.SetContent(null);
        block.Length = 0;
        _usedBlocks.Remove(block.Id);
        _availableBlocks.Add(block.Id, block);
    }


}