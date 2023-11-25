CREATE TABLE [History].[PlayedItems]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[PlayedAt] DATETIME NOT NULL DEFAULT GETDATE(),
	[PlaylistTypeId] INT NOT NULL,
    CONSTRAINT [FK_PlayedItems_ToPlaylistTypes] FOREIGN KEY ([PlaylistTypeId]) REFERENCES [Admin].[PlaylistTypes]([Id])
)
