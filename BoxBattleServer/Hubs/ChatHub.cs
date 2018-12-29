﻿using System.Threading.Tasks;
using BoxBattle.Interfaces;
using MagicOnion.Server.Hubs;

public class ChatHub : StreamingHubBase<IChatHub, IChatHubReceiver>, IChatHub
{
	IGroup room;
	string me;

	public async Task JoinAsync(string userName)
	{
		//ルームは全員固定
		const string roomName = "SampleRoom";
		//ルームに参加&ルームを保持
		this.room = await this.Group.AddAsync(roomName);
		//自分の名前も保持
		me = userName;
		//参加したことをルームに参加している全メンバーに通知
		this.Broadcast(room).OnJoin(userName);
	}

	public async Task LeaveAsync()
	{
		//ルーム内のメンバーから自分を削除
		await room.RemoveAsync(this.Context);
		//退室したことを全メンバーに通知
		this.BroadcastExceptSelf(room).OnLeave(me);
	}

	public async Task SendMessageAsync(string message)
	{
		//発言した内容を全メンバーに通知
		this.Broadcast(room).OnSendMessage(me, message);
	}

	protected override ValueTask OnConnecting()
	{
		Logger.Debug("OnConnecting");
		return CompletedTask;
	}

	protected override ValueTask OnDisconnected()
	{
		Logger.Debug("OnDisconnected");
		return CompletedTask;
	}
}