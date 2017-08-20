CREATE TABLE [dbo].[Products] (
    [ID]              BIGINT     IDENTITY (1, 1) NOT NULL,
    [Name]            NCHAR (50) NOT NULL,
    [Colarific_value] FLOAT (53) NOT NULL,
    [Product_type_id] BIGINT     NOT NULL,
    [Fats]            FLOAT (53) NULL,
    [Carbohydrates]   FLOAT (53) NULL,
    [Proteins]        FLOAT (53) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Products_Prod_type] FOREIGN KEY ([Product_type_id]) REFERENCES [dbo].[Products_types] ([ID]),
    UNIQUE NONCLUSTERED ([Name] ASC)
);



