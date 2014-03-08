﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SmartStore.Core.Data;
using SmartStore.Utilities;

namespace SmartStore.Data.Initializers
{
	
	/// <summary>
	///     An implementation of <see cref="IDatabaseInitializer{TContext}" /> that will use Code First Migrations
	///     to update the database to the latest version.
	/// </summary>
	public class MigrateDatabaseToLatestVersionEx<TContext, TConfig> : IDatabaseInitializer<TContext> 
		where TContext : DbContext
		where TConfig : DbMigrationsConfiguration<TContext>, new()
	{
		private readonly string _connectionString;
		private readonly string[] _sqlFiles;
		private IEnumerable<string> _tablesToCheck;
		private DbMigrationsConfiguration _config;

		#region Ctor

		public MigrateDatabaseToLatestVersionEx()
			: this(null, null, null)
		{
		}

		public MigrateDatabaseToLatestVersionEx(string connectionString)
			: this(connectionString, null, null)
		{
		}

		public MigrateDatabaseToLatestVersionEx(string[] sqlFiles)
			: this(null, null, sqlFiles)
		{
		}

		public MigrateDatabaseToLatestVersionEx(string[] tablesToCheck, string[] sqlFiles)
			: this(null, tablesToCheck, sqlFiles)
		{
		}

		public MigrateDatabaseToLatestVersionEx(string connectionString, string[] tablesToCheck, string[] sqlFiles)
		{
			this._connectionString = connectionString;
			this._tablesToCheck = tablesToCheck;
			this._sqlFiles = sqlFiles;
		}

		#endregion

		#region Interface members

		/// <summary>
		/// Initializes the database.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <inheritdoc />
		public void InitializeDatabase(TContext context)
		{
			if (_config == null)
			{
				_config = new TConfig();
				if (_connectionString.HasValue())
				{
					var dbContextInfo = new DbContextInfo(typeof(TContext));
					_config.TargetDatabase = new DbConnectionInfo(_connectionString, dbContextInfo.ConnectionProviderName);
				}
			}

			var newDb = !context.Database.Exists();

			var migrator = new DbMigrator(_config);
			if (!newDb)
			{
				var suppressInitialCreate = false;
				var tablesExist = CheckTables(context);
				if (tablesExist)
				{
					// Tables specific to the model exist in the database...
					var noHistoryEntry = !migrator.GetDatabaseMigrations().Any();
					if (noHistoryEntry)
					{
						// ...but there is no entry in the __MigrationHistory table (or __MigrationHistory doesn't exist at all)
						suppressInitialCreate = true;
						// ...we MUST assume that the database was created with a previous SmartStore version
						// prior integrating EF Migrations.
						// Running the Migrator with initial DDL would crash in this case as
						// the db objects exist already. Therefore we set a suppression flag
						// which we read in the corresposnding InitialMigration to exit early.
						DbMigrationContext.Current.SetSuppressInitialCreate<TContext>(true);
					}
				}

				if (!suppressInitialCreate)
				{
					// Obviously a blank DB...
					var local = migrator.GetLocalMigrations();
					var pending = migrator.GetPendingMigrations();
					if (local.Count() == pending.Count())
					{
						// ...and free of Migrations. We have to seed later. 
						newDb = true;
					}
				}
			}

			// create or migrate the database now
			try
			{
				migrator.Update();
			}
			catch (AutomaticMigrationsDisabledException)
			{
				if (context is SmartObjectContext)
				{
					throw;
				}

				// DbContexts in plugin assemblies tend to produce
				// this error, but obviously without any negative side-effect.
				// Therefore catch and forget!
				// TODO: (MC) investigate this and implement a cleaner solution
			}

			if (newDb)
			{
				// advanced db init
				RunSqlFiles(context);

				// seed data
				Seed(context);
			}
		}

		#endregion

		#region Utils

		/// <summary>
		/// Checks tables existence
		/// </summary>
		/// <returns>
		/// Returns <c>true</c> if the tables to check exist in the database or the check list is empty.
		/// </returns>
		protected bool CheckTables(TContext context)
		{
			if (_tablesToCheck == null || !_tablesToCheck.Any())
				return true;

			var existingTableNames = new List<string>(context.Database.SqlQuery<string>("SELECT table_name FROM INFORMATION_SCHEMA.TABLES WHERE table_type = 'BASE TABLE'"));
			var result = existingTableNames.Intersect(_tablesToCheck, StringComparer.InvariantCultureIgnoreCase).Count() == 0;
			return !result;
		}

		/// <summary>
		/// Seeds the specified context.
		/// </summary>
		/// <param name="context">The context.</param>
		protected virtual void Seed(TContext context)
		{
		}

		#endregion

		#region Sql File Handling

		private void RunSqlFiles(TContext context)
		{
			if (_sqlFiles == null || _sqlFiles.Length == 0)
				return;

			foreach (var file in _sqlFiles)
			{
				using (var reader = ReadSqlFile(file))
				{
					foreach (var cmd in ParseCommands(reader))
					{
						if (cmd.HasValue())
						{
							context.Database.ExecuteSqlCommand(cmd);
						}
					}
				}
			}
		}

		protected virtual StreamReader ReadSqlFile(string fileName)
		{
			Guard.ArgumentNotEmpty(() => fileName);

			if (fileName.StartsWith("~") || fileName.StartsWith("/"))
			{
				string path = CommonHelper.MapPath(fileName);
				if (!File.Exists(path)) 
				{
					return StreamReader.Null;
				}

				return new StreamReader(File.OpenRead(path));
			}

			// SQL file is obviously an embedded resource
			// TODO: (MC) add support for assemblies other than SmartStore.Data
			var asm = Assembly.GetExecutingAssembly();
			var asmName = asm.FullName.Substring(0, asm.FullName.IndexOf(','));
			var name = String.Format("{0}.Sql.{1}",
				asmName,
				fileName);
			var stream = asm.GetManifestResourceStream(name);
			Debug.Assert(stream != null);
			return new StreamReader(stream);
		}

		private IEnumerable<string> ParseCommands(TextReader reader)
		{
			var statement = string.Empty;
			while ((statement = ReadNextStatement(reader)) != null)
			{
				yield return statement;
			}
		}

		private string ReadNextStatement(TextReader reader)
		{
			var sb = new StringBuilder();

			string lineOfText;

			while (true)
			{
				lineOfText = reader.ReadLine();
				if (lineOfText == null)
				{
					if (sb.Length > 0)
						return sb.ToString();
					else
						return null;
				}

				if (lineOfText.TrimEnd().ToUpper() == "GO")
					break;

				sb.Append(lineOfText + Environment.NewLine);
			}

			return sb.ToString();
		}

		#endregion

	}

}
