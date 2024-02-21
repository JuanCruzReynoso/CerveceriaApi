-- Marca Table
USE [Cerveceria]
GO

/****** Object:  Table [dbo].[Marca]    Script Date: 20/2/2024 23:42:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Marca](
	[id_Marca] [int] IDENTITY(1,1) NOT NULL,
	[Marca] [varchar](20) NULL,
 CONSTRAINT [PK__Marca__7A5E10D3F47659A2] PRIMARY KEY CLUSTERED 
(
	[id_Marca] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Cerveza Table
USE [Cerveceria]
GO

/****** Object:  Table [dbo].[Cerveza]    Script Date: 20/2/2024 23:41:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cerveza](
	[id_Cerveza] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NULL,
	[id_Marca] [int] NULL,
 CONSTRAINT [PK__Cerveza__29CD74138450C70D] PRIMARY KEY CLUSTERED 
(
	[id_Cerveza] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Cerveza]  WITH CHECK ADD  CONSTRAINT [FK__Cerveza__id_Marc__3E52440B] FOREIGN KEY([id_Marca])
REFERENCES [dbo].[Marca] ([id_Marca])
GO

ALTER TABLE [dbo].[Cerveza] CHECK CONSTRAINT [FK__Cerveza__id_Marc__3E52440B]
GO

-- Marca sp
USE [Cerveceria]
GO

/****** Object:  StoredProcedure [dbo].[GetMarca]    Script Date: 20/2/2024 23:44:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetMarca]
AS
BEGIN
    SELECT * FROM Marca;
END;
GO

USE [Cerveceria]
GO

/****** Object:  StoredProcedure [dbo].[InsertMarca]    Script Date: 20/2/2024 23:45:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertMarca]
    @Marca varchar(20)
AS
BEGIN
    INSERT INTO Marca (Marca) VALUES (@Marca)
END;
GO

USE [Cerveceria]
GO

/****** Object:  StoredProcedure [dbo].[UpdateMarca]    Script Date: 20/2/2024 23:45:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateMarca]
    @id_Marca int,
    @Marca varchar(20)
AS
BEGIN
    UPDATE Marca SET Marca = @Marca WHERE id_Marca = @id_Marca
END;
GO

USE [Cerveceria]
GO

/****** Object:  StoredProcedure [dbo].[DeleteMarca]    Script Date: 20/2/2024 23:45:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteMarca]
    @id_Marca int
AS
BEGIN
    DELETE FROM Marca WHERE id_Marca = @id_Marca
END;
GO

-- Cerveza sp
USE [Cerveceria]
GO

/****** Object:  StoredProcedure [dbo].[GetCerveza]    Script Date: 20/2/2024 23:46:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCerveza]
    @Nombre varchar(20) = NULL,
    @id_Marca int = NULL
AS
BEGIN
    IF (@Nombre IS NULL OR @Nombre = '') AND (@id_Marca IS NULL OR @id_Marca = 0)
    BEGIN
        -- Return all records if both parameters are empty
        SELECT Cerveza.*, Marca.*
        FROM Cerveza
        LEFT JOIN Marca ON Cerveza.id_Marca = Marca.id_Marca;
    END
    ELSE
    BEGIN
        -- Filter by Nombre (using LIKE) and/or id_Marca
        SELECT Cerveza.*, Marca.*
        FROM Cerveza
        LEFT JOIN Marca ON Cerveza.id_Marca = Marca.id_Marca
        WHERE (@Nombre IS NULL OR Nombre = '' OR Nombre LIKE '%' + @Nombre + '%')
        AND (@id_Marca IS NULL OR @id_Marca = 0 OR Cerveza.id_Marca = @id_Marca);
    END
END;
GO

USE [Cerveceria]
GO

/****** Object:  StoredProcedure [dbo].[InsertCerveza]    Script Date: 20/2/2024 23:46:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertCerveza]
    @Nombre varchar(20),
    @id_Marca int
AS
BEGIN
    INSERT INTO Cerveza (Nombre, id_Marca) VALUES (@Nombre, @id_Marca)
END;
GO

USE [Cerveceria]
GO

/****** Object:  StoredProcedure [dbo].[UpdateCerveza]    Script Date: 20/2/2024 23:46:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateCerveza]
    @id_Cerveza int,
    @Nombre varchar(20),
    @id_Marca int
AS
BEGIN
    UPDATE Cerveza SET Nombre = @Nombre, id_Marca = @id_Marca WHERE id_Cerveza = @id_Cerveza
END;
GO

USE [Cerveceria]
GO

/****** Object:  StoredProcedure [dbo].[DeleteCerveza]    Script Date: 20/2/2024 23:46:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteCerveza]
    @id_Cerveza int
AS
BEGIN
    DELETE FROM Cerveza WHERE id_Cerveza = @id_Cerveza
END;
GO
