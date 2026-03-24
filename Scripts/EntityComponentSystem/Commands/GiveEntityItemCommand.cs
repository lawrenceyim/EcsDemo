public struct GiveEntityItemCommand : ICommand {
    public int Id { get; }
    public int EntityId { get; }
    public int ItemId { get; }
    public int Quantity { get; }

    public GiveEntityItemCommand(int EntityId, int ItemId, int Quantity) {
        Id = CommandId.GiveEntityItemCommand;
        this.EntityId = EntityId;
        this.ItemId = ItemId;
        this.Quantity = Quantity;
    }
}