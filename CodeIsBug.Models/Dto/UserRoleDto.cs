namespace CodeIsBug.Models.Dto;

public class UserRoleDto
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public string? RoleCode { get; set; }

    public string? RoleName { get; set; }
    
}