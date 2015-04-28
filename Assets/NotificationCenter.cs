using System.Collections;
using System;

public class NotificationCenter 
{
	private static NotificationCenter mgr = new NotificationCenter();
	public static NotificationCenter getI  { get { return NotificationCenter.mgr; } }
	
	public delegate void callback (Object arg);
	
	private Hashtable callback_storage = new Hashtable();
	
	public NotificationCenter ()
	{
		NotificationCenter.mgr = this;
	}
	
	public void addCallback(string notificationName, callback method_in)
	{
		if (this.callback_storage.ContainsKey(notificationName))
		{
			if (!((ArrayList)this.callback_storage[notificationName]).Contains(method_in))
			{
				((ArrayList)this.callback_storage[notificationName]).Add(method_in);
			}
		} else {
			this.callback_storage.Add(notificationName, new ArrayList());
			((ArrayList)this.callback_storage[notificationName]).Add(method_in);
		}
	}
	
	public void removeCallback(string notificationName, callback method_in)
	{
		if (this.callback_storage.ContainsKey(notificationName))
		{
			if (((ArrayList)this.callback_storage[notificationName]).Contains(method_in))
			{
				((ArrayList)this.callback_storage[notificationName]).Remove(method_in);
			}
		}
	}
	
	public void postNotification(string notificationName, Object arg)
	{
		if (this.callback_storage.ContainsKey(notificationName))
		{
			int i;
			callback function;
			for (i = 0; i<((ArrayList)this.callback_storage[notificationName]).Count; i++)
			{
				function = ((ArrayList)this.callback_storage[notificationName])[i] as callback;
				function(arg);
			}
		}
	}
	
	public void clearCallbacks()
	{
		this.callback_storage.Clear();
	}
}