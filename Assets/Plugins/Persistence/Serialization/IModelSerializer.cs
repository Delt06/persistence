using System;
using JetBrains.Annotations;

namespace Persistence.Serialization
{
	public interface IModelSerializer
	{
		void SetUp(Type modelType);
		bool IsSetUp { get; }

		void Serialize([NotNull] object model);

		bool TryDeserialize(out object model);

		void Flush();
	}
}