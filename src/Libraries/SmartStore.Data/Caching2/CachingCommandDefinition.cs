﻿using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;

namespace SmartStore.Data.Caching2
{
	internal class CachingCommandDefinition : DbCommandDefinition
	{
		private readonly DbCommandDefinition _commandDefintion;
		private readonly CommandTreeFacts _commandTreeFacts;
		private readonly CacheTransactionInterceptor _cacheTransactionInterceptor;

		public CachingCommandDefinition(
			DbCommandDefinition commandDefinition, 
			CommandTreeFacts commandTreeFacts, 
			CacheTransactionInterceptor cacheTransactionInterceptor)
		{
			_commandDefintion = commandDefinition;
			_commandTreeFacts = commandTreeFacts;
			_cacheTransactionInterceptor = cacheTransactionInterceptor;
		}

		public bool IsQuery
		{
			get { return _commandTreeFacts.IsQuery; }
		}

		public bool IsCacheable
		{
			get { return _commandTreeFacts.IsQuery && !_commandTreeFacts.UsesNonDeterministicFunctions; }
		}

		public ReadOnlyCollection<EntitySetBase> AffectedEntitySets
		{
			get { return _commandTreeFacts.AffectedEntitySets; }
		}

		public override DbCommand CreateCommand()
		{
			return new CachingCommand(_commandDefintion.CreateCommand(), _commandTreeFacts, _cacheTransactionInterceptor);
		}
	}
}
