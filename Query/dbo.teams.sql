CREATE TABLE [dbo].[teams] (
    [id]        INT NOT NULL,
    [name]      VARCHAR (50) NOT NULL,
    [teamValue] VARCHAR (50) NOT NULL,
    [squadSize] INT          NOT NULL,
    [stadium]   VARCHAR (50) NOT NULL,
    [city]      VARCHAR (50) NOT NULL,
    [idLeague]  INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([idLeague]) REFERENCES [dbo].[league] ([id])
);

