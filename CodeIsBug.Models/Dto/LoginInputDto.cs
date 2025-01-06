using System.ComponentModel.DataAnnotations;

namespace CodeIsBug.Models.Dto;
/// <summary>
/// 用户登录dto
/// </summary>
[Display(Description = "用户登录dto")]
public class LoginInputDto
{

    /// <summary>
    /// 用户名
    /// </summary>
    [Display(Name = "Username",Description = "用户名")]
    public string UserName { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    [Display(Name = "Password",Description = "密码")]
    public string Password { get; set; }
}