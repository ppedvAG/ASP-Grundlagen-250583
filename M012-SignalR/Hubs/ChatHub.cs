using Microsoft.AspNetCore.SignalR;

namespace M012_SignalR.Hubs;

public class ChatHub : Hub
{
	//Wird von JS (Client) aufgerufen, um eine Nachricht an den Server zu senden
	public async Task NachrichtSenden(string user, string msg)
	{
		//SendAsync: Sendet einen Befehl an alle Clients, eine JS Methode auszuführen
		//Die Methode hier heißt "NachrichtEmpfangen", und komsumiert zwei Parameter: den Absender der Chatnachricht, die Chatnachricht selbst
		await Clients.All.SendAsync("NachrichtEmpfangen", user, msg);
	}

	public override async Task OnConnectedAsync()
	{
		await Clients.All.SendAsync("UserVerbunden");
	}

	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		await Clients.All.SendAsync("UserGetrennt");
	}
}
