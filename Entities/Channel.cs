class Channel
{
    public int Id { get; set; }
    public int ServerId { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public DateTime? CreationDate { get; set; }
    public bool IsActive { get; set; }
}