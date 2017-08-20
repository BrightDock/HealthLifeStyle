CREATE TABLE [dbo].[Users] (
    [ID]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (25)  NOT NULL,
    [Surname]         NVARCHAR (25)  NOT NULL,
    [Login]           NVARCHAR (20)  NOT NULL,
    [Password]        NVARCHAR (20)  NOT NULL,
    [Sex]             BIT            NULL,
    [Date_of_birth]   DATETIME2 (7)  NULL,
    [Weight]          BIGINT         NULL,
    [Height]          FLOAT (53)     NULL,
    [Account_type_id] INT            NOT NULL,
    [Avatar]          NVARCHAR (100) NULL,
    [About]           NVARCHAR (500) NULL,
    [Latest_online]   DATETIME2 (7)  NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Users_Account_type] FOREIGN KEY ([Account_type_id]) REFERENCES [dbo].[Account_types] ([ID]),
    CONSTRAINT [FK_Users_User_weight_chronology] FOREIGN KEY ([Weight]) REFERENCES [dbo].[User_weight_chronology] ([ID])
);





