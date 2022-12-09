using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Business.Entities
{
    //interface ICustomPrincipal : System.Security.Principal.IPrincipal
    //{
    //    string UserName { get; set; }
    //    Guid UserId { get; set; }
    //    string Right { get; set; }
    //    string UserFullName { get; set; }
    //    string Role { get; set; }
    //    string DefaultController { get; set; }
    //    string DefaultAction { get; set; }
    //    int KhoId { get; set; }
    //}
    //public class CustomPrincipal : ICustomPrincipal
    //{
    //    public IIdentity Identity { get; private set; }

    //    public CustomPrincipal(string username)
    //    {
    //        this.Identity = new GenericIdentity(username);
    //    }

    //    public bool IsInRole(string role)
    //    {
    //        return Identity != null && Identity.IsAuthenticated &&
    //               !string.IsNullOrWhiteSpace(role) && this.Role.Contains(role);
    //    }

    //    public string UserName { get; set; }
    //    public Guid UserId { get; set; }
    //    public string Right { get; set; }
    //    public string Role { get; set; }
    //    public string DefaultController { get; set; }
    //    public string DefaultAction { get; set; }
    //    public string UserFullName { get; set; }
    //    public int KhoId { get; set; }
    //}

    //public class CustomPrincipalSerializedModel
    //{
    //    public Guid UserId { get; set; }
    //    public string UserName { get; set; }
    //    public string UserFullName { get; set; }
    //    public string Right { get; set; }
    //    public string Role { get; set; }
    //    public string DefaultController { get; set; }
    //    public string DefaultAction { get; set; }
    //    public int KhoId { get; set; }
    //}
}
