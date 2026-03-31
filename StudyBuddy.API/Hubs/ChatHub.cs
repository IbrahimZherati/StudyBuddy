using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;
using System.Text.RegularExpressions;

[SignalRHub]
[Authorize]
public class ChatHub : Hub
{
  
}
