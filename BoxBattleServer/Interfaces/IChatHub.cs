﻿using System.Threading.Tasks;
using MagicOnion;

/// <summary>
/// CLient -> ServerのAPI
/// </summary>
public interface IChatHub : IStreamingHub<IChatHub, IChatHubReceiver>
{
	/// <summary>
	/// 参加することをサーバに伝える
	/// </summary>
	/// <param name="userName">参加者の名前</param>
	/// <returns></returns>
	Task JoinAsync(string userName);

	/// <summary>
	/// 退室することをサーバに伝える
	/// </summary>
	/// <returns></returns>
	Task LeaveAsync();

	/// <summary>
	/// メッセージをサーバに伝える
	/// </summary>
	/// <param name="message"></param>
	/// <returns></returns>
	Task SendMessageAsync(string message);
}