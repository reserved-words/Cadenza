﻿CREATE TABLE [LastFm].[ImportedLovedTracks]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Track] NVARCHAR(200) NOT NULL,
	[Artist] NVARCHAR(200) NOT NULL
)
