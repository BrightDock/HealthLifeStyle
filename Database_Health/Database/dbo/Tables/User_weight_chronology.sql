CREATE TABLE [dbo].[User_weight_chronology] (
    [ID]      BIGINT     IDENTITY (1, 1) NOT NULL,
    [Date]    DATETIME   NOT NULL,
    [Weight]  FLOAT (53) NOT NULL,
    [User_ID] BIGINT     NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);




