namespace Persistence
{
	/// <inheritdoc />
	public sealed class StringVariable : Variable<string>
	{
		public StringVariable(string name, string defaultValue = default) : base(name, defaultValue) { }

		/// <inheritdoc />
		protected override string ReadValueFrom(IReadOnlyStorage storage) => storage.GetString(Name, DefaultValue);

		/// <inheritdoc />
		protected override void WriteValueTo(IStorage storage, string value)
		{
			storage.SetString(value, value);
		}
	}
}