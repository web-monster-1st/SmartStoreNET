﻿using System;
using SmartStore.Core.Domain.Messages;

namespace SmartStore.Services.Messages
{
	public interface IMessageFactory
	{
		(QueuedEmail Email, dynamic Model) CreateMessage(MessageContext messageContext, bool queue, params object[] modelParts);

		void QueueMessage(QueuedEmail queuedEmail, MessageContext messageContext, dynamic model);
	}
}
