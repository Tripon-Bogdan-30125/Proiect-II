CREATE TABLE [dbo].[players] (
    [id]                  INT          IDENTITY (1, 1) NOT NULL,
    [name]                VARCHAR (50) NOT NULL,
    [nationality]         VARCHAR (50) NOT NULL,
    [age]                 INT          NOT NULL,
    [position]            VARCHAR (50) NOT NULL,
    [height]              VARCHAR(50)   NOT NULL,
    [foot]                VARCHAR (50) NOT NULL,
    [playerValue]         VARCHAR (50) NOT NULL,
    [internationalStatus] VARCHAR (50) NOT NULL,
    [outfitter]           VARCHAR (50) NOT NULL,
    [idTeam]              INT          NOT NULL,
    [idNationalTeam]      INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([idTeam]) REFERENCES [dbo].[teams] ([id]),
    FOREIGN KEY ([idNationalTeam]) REFERENCES [dbo].[nationalTeam] ([id])
);

