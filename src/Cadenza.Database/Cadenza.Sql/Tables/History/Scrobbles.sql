CREATE TABLE [History].[Scrobbles]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[UserId] INT NOT NULL FOREIGN KEY REFERENCES [Admin].[Users],
	[ScrobbledAt] DATETIME NOT NULL,
	[Track] NVARCHAR(200) NOT NULL,
	[Artist] NVARCHAR(200) NOT NULL,
	[Album] NVARCHAR(200),
	[AlbumArtist] NVARCHAR(200)
)

GO

CREATE INDEX [IX_Scrobbles_Track] ON [History].[Scrobbles] ([Track], [Artist])

GO

CREATE INDEX [IX_Scrobbles_Album] ON [History].[Scrobbles] ([Album], [AlbumArtist])

GO

CREATE INDEX [IX_Scrobbles_User] ON [History].[Scrobbles] ([UserId])
