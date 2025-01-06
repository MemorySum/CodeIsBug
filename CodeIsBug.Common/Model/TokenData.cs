namespace CodeIsBug.Common.Model;

public class TokenData
{
    public Guid UserId { get; set; }

    public string UserName { get; set; }

    public string UserAccount { get; set; }

    public bool IsSuperAdmin { get; set; }

    public List<Guid> UserRolesId { get; set; }

    public List<string> UserPermiss { get; set; }

    public DateTime? LoginTime { get; set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTime? ExpireTime { get; set; }
}