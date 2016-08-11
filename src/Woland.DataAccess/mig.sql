Project Woland.Domain (.NETCoreApp,Version=v1.0) was previously compiled. Skipping compilation.
Project Woland.DataAccess (.NETCoreApp,Version=v1.0) was previously compiled. Skipping compilation.
IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO
CREATE TABLE [WebRequestLogs] (
    [Id] int NOT NULL IDENTITY,
    [Method] nvarchar(max) NOT NULL,
    [Request] nvarchar(max) NOT NULL,
    [RequestBody] nvarchar(max),
    [Response] nvarchar(max) NOT NULL,
    [ResponseBody] nvarchar(max),
    [ResponseCode] int NOT NULL,
    [Timestamp] datetime2 NOT NULL,
    [Url] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_WebRequestLogs] PRIMARY KEY ([Id])
);
GO
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20160703140648_AddedWebRequestLog', N'1.0.0-rtm-21431');
GO
CREATE TABLE [JobLeads] (
    [Id] int NOT NULL IDENTITY,
    [AgencyName] nvarchar(128),
    [Body] nvarchar(max) NOT NULL,
    [Email] nvarchar(128),
    [FullName] nvarchar(128),
    [MaxRate] decimal(18, 2),
    [MinRate] decimal(18, 2),
    [PostedTimestamp] datetime2,
    [SearchKeywords] nvarchar(max) NOT NULL,
    [SearchLocation] nvarchar(max) NOT NULL,
    [SourceName] nvarchar(max) NOT NULL,
    [SourceUrl] nvarchar(max) NOT NULL,
    [Telephone] nvarchar(128),
    [Title] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_JobLeads] PRIMARY KEY ([Id])
);
GO
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20160703203142_AddedJobLeads', N'1.0.0-rtm-21431');
GO
DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'JobLeads') AND [c].[name] = N'PostedTimestamp');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [JobLeads] DROP CONSTRAINT [' + @var0 + ']');
ALTER TABLE [JobLeads] ALTER COLUMN [PostedTimestamp] datetime2 NOT NULL;
GO
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20160704194223_MadePublishTimestampRequired', N'1.0.0-rtm-21431');
GO
CREATE TABLE [ImportTasks] (
    [Id] int NOT NULL IDENTITY,
    [LastExecuted] datetime2,
    [SearchKeywords] nvarchar(max) NOT NULL,
    [SearchLocation] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ImportTasks] PRIMARY KEY ([Id])
);
GO
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20160705202830_AddedImportTasks', N'1.0.0-rtm-21431');
GO
CREATE TABLE [LogEntries] (
    [Id] int NOT NULL IDENTITY,
    [Level] nvarchar(max),
    [Message] nvarchar(max),
    [Timestamp] datetime2 NOT NULL,
    CONSTRAINT [PK_LogEntries] PRIMARY KEY ([Id])
);
GO
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20160705221316_AddedLogEntry', N'1.0.0-rtm-21431');
GO
EXEC sp_rename N'dbo.ImportTasks', N'ImportSchedules';
GO
DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'ImportSchedules') AND [c].[name] = N'LastExecuted');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ImportSchedules] DROP CONSTRAINT [' + @var1 + ']');
ALTER TABLE [ImportSchedules] DROP COLUMN [LastExecuted];
GO
ALTER TABLE [ImportSchedules] ADD [Hour] int;
GO
ALTER TABLE [ImportSchedules] ADD [Minute] int;
GO
ALTER TABLE [ImportSchedules] ADD [NextRunDate] datetime2;
GO
UPDATE ImportSchedules SET Hour = 19, Minute = 00;
GO
DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'ImportSchedules') AND [c].[name] = N'Hour');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [ImportSchedules] DROP CONSTRAINT [' + @var2 + ']');
ALTER TABLE [ImportSchedules] ALTER COLUMN [Hour] int NOT NULL;
GO
DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'ImportSchedules') AND [c].[name] = N'Minute');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [ImportSchedules] DROP CONSTRAINT [' + @var3 + ']');
ALTER TABLE [ImportSchedules] ALTER COLUMN [Minute] int NOT NULL;
GO
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20160719214226_ChangedTaskToSchedule', N'1.0.0-rtm-21431');
GO
ALTER TABLE [WebRequestLogs] ADD [DbTimestamp] rowversion;
GO
ALTER TABLE [JobLeads] ADD [DbTimestamp] rowversion;
GO
ALTER TABLE [ImportSchedules] ADD [DbTimestamp] rowversion;
GO
ALTER TABLE [LogEntries] ADD [DbTimestamp] rowversion;
GO
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20160723211001_AddedConcurrencyTimestamp', N'1.0.0-rtm-21431');
GO
CREATE TABLE [ImportResults] (
    [Id] int NOT NULL IDENTITY,
    [DbTimestamp] rowversion,
    [ImportScheduleId] int NOT NULL,
    [Timestamp] datetime2 NOT NULL,
    CONSTRAINT [PK_ImportResults] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ImportResults_ImportSchedules_ImportScheduleId] FOREIGN KEY ([ImportScheduleId]) REFERENCES [ImportSchedules] ([Id]) ON DELETE CASCADE
);
GO
CREATE TABLE [ImportScheduleProperty] (
    [Id] int NOT NULL IDENTITY,
    [DbTimestamp] rowversion,
    [ImportScheduleId] int NOT NULL,
    [Name] nvarchar(max),
    [Value] nvarchar(max),
    CONSTRAINT [PK_ImportScheduleProperty] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ImportScheduleProperty_ImportSchedules_ImportScheduleId] FOREIGN KEY ([ImportScheduleId]) REFERENCES [ImportSchedules] ([Id]) ON DELETE CASCADE
);
GO
CREATE TABLE [ImportResultProperty] (
    [Id] int NOT NULL IDENTITY,
    [DbTimestamp] rowversion,
    [ImportResultId] int NOT NULL,
    [Name] nvarchar(max),
    [Value] nvarchar(max),
    CONSTRAINT [PK_ImportResultProperty] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ImportResultProperty_ImportResults_ImportResultId] FOREIGN KEY ([ImportResultId]) REFERENCES [ImportResults] ([Id]) ON DELETE CASCADE
);
GO
ALTER TABLE [ImportSchedules] ADD [ImporterName] nvarchar(max) NOT NULL DEFAULT N'';
GO
CREATE INDEX [IX_ImportResults_ImportScheduleId] ON [ImportResults] ([ImportScheduleId]);
GO
CREATE INDEX [IX_ImportResultProperty_ImportResultId] ON [ImportResultProperty] ([ImportResultId]);
GO
CREATE INDEX [IX_ImportScheduleProperty_ImportScheduleId] ON [ImportScheduleProperty] ([ImportScheduleId]);
GO
SET IDENTITY_INSERT ImportResults ON;
GO
INSERT INTO ImportResults(Id, ImportScheduleId, Timestamp) SELECT (SELECT Id FROM ImportSchedules), Id, PostedTimestamp FROM JobLeads;
GO
SET IDENTITY_INSERT ImportResults OFF;
GO
INSERT INTO ImportResultProperty(ImportResultId, Name, Value) SELECT Id, 'SourceUrl', SourceUrl FROM JobLeads;
GO
INSERT INTO ImportResultProperty(ImportResultId, Name, Value) SELECT Id, 'Title', Title FROM JobLeads;
GO
INSERT INTO ImportResultProperty(ImportResultId, Name, Value) SELECT Id, 'Body', Body FROM JobLeads;
GO
INSERT INTO ImportResultProperty(ImportResultId, Name, Value) SELECT Id, 'MinRate', MinRate FROM JobLeads;
GO
INSERT INTO ImportResultProperty(ImportResultId, Name, Value) SELECT Id, 'MaxRate', MaxRate FROM JobLeads;
GO
INSERT INTO ImportResultProperty(ImportResultId, Name, Value) SELECT Id, 'PostedTimestamp', PostedTimestamp FROM JobLeads;
GO
INSERT INTO ImportResultProperty(ImportResultId, Name, Value) SELECT Id, 'FullName', FullName FROM JobLeads;
GO
INSERT INTO ImportResultProperty(ImportResultId, Name, Value) SELECT Id, 'Telephone', Telephone FROM JobLeads;
GO
INSERT INTO ImportResultProperty(ImportResultId, Name, Value) SELECT Id, 'Email', Email FROM JobLeads;
GO
INSERT INTO ImportResultProperty(ImportResultId, Name, Value) SELECT Id, 'AgencyName', AgencyName FROM JobLeads;
GO
INSERT INTO ImportScheduleProperty(ImportScheduleId, Name, Value) SELECT Id, 'SearchKeywords', SearchKeywords FROM ImportSchedules;
GO
INSERT INTO ImportScheduleProperty(ImportScheduleId, Name, Value) SELECT Id, 'SearchLocation', SearchLocation FROM ImportSchedules;
GO
DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'ImportSchedules') AND [c].[name] = N'SearchKeywords');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [ImportSchedules] DROP CONSTRAINT [' + @var4 + ']');
ALTER TABLE [ImportSchedules] DROP COLUMN [SearchKeywords];
GO
DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'ImportSchedules') AND [c].[name] = N'SearchLocation');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [ImportSchedules] DROP CONSTRAINT [' + @var5 + ']');
ALTER TABLE [ImportSchedules] DROP COLUMN [SearchLocation];
GO
DROP TABLE [JobLeads];
GO
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20160811203641_AddedGenericDataStructure', N'1.0.0-rtm-21431');
GO
