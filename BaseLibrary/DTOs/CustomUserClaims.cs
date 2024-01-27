namespace BaseLibrary.DTOs
{
    public record CustomUserClaims(
        string Id = null!,
        string Name = null!,
        string Email = null!,
        string Role = null!);
}
