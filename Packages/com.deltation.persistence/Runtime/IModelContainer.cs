﻿namespace DELTation.Persistence
{
	public interface IModelContainer<T>
	{
		T Model { get; set; }
		void SaveChanges();
	}
}