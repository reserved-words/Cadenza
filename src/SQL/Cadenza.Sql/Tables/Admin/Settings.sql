CREATE TABLE [Admin].[Settings]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Username] NVARCHAR(100) NOT NULL UNIQUE,
	[LastFmUsername] NVARCHAR(100),
	[LastFmSessionKey] NVARCHAR(500),
	[LastFmEnabled] BIT NOT NULL DEFAULT 1
)
