CREATE TABLE [dbo].[teamToTrophies] (
    [idTeam]        INT  NOT NULL,
    [idTrophy]      INT  NOT NULL,
    [dateOfWinning] DATE NOT NULL,
    FOREIGN KEY ([idTeam]) REFERENCES [dbo].[teams] ([id]),
    FOREIGN KEY ([idTrophy]) REFERENCES [dbo].[trophies] ([id])
);

